using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.ToDoElement;
using DenemeTakipAPI.Application.Repositories;
using DenemeTakipAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Services
{
    public class ToDoElementService : IToDoElementService
    {
        readonly IToDoElementReadRepository _toDoElementReadRepository;
        readonly IToDoElementWriteRepository _toDoElementWriteRepository;
        readonly UserManager<AppUser> _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IToDoElementHubService _toDoElementHubService;
        public ToDoElementService(IToDoElementReadRepository toDoElementReadRepository, IToDoElementWriteRepository toDoElementWriteRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IToDoElementHubService toDoElementHubService)
        {
            _toDoElementReadRepository = toDoElementReadRepository;
            _toDoElementWriteRepository = toDoElementWriteRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _toDoElementHubService = toDoElementHubService;
        }
        private async Task<AppUser> ContextUser()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
                return user;
            }
            throw new Exception("Kullanıcı bulunamadı");


        }
        public async Task CreateToDoElementAsync(string ToDoElementTitle, DateTime ToDoDate, bool IsCompleted)
        {
            var user = await ContextUser();
            await _toDoElementWriteRepository.AddAsync(new()
            {
                User=user,
                UserId=user.Id,
                ToDoElementTitle=ToDoElementTitle,
                ToDoDate=ToDoDate,
                IsCompleted=IsCompleted
            });
            await _toDoElementHubService.ToDoElementAddedMessage(user.Id, "To do görevi başarıyla eklendi.");
            await _toDoElementWriteRepository.SaveAsync();
        }

        public async Task<GetToDoElements> GetToDoElements(DateTime toDoDateStart, DateTime toDoDateEnd, bool? isCompleted)
        {
            var user = await ContextUser();
            var query = _toDoElementReadRepository.GetWhere(u => u.User == user)
                .Where(u => u.ToDoDate >= toDoDateStart && u.ToDoDate <= toDoDateEnd);

            if (isCompleted != null)
                query = query.Where(u => u.IsCompleted == isCompleted);

            // Veritabanında gruplama yapıyoruz
            var groupedToDos = await query
                .GroupBy(u => u.ToDoDate.Date) 
                .Select(g => new
                {
                    Date = g.Key,
                    ToDoElements = g.Select(u => new
                    {
                        u.Id,
                        u.ToDoElementTitle,
                        u.ToDoDate,
                        u.IsCompleted
                    }).ToList() 
                })
                .ToListAsync();
            var totalCount = groupedToDos.Count;

            return new()
            {
                ToDoElements = groupedToDos,
                TotalCount = totalCount,
            };
        }

        public async Task<SucceededMessageResponse> UpdateToDoElementAsync(string todoId, bool? isCompleted, string? toDoTitle)
        {
            var user= await ContextUser();
            var toDoElement = await _toDoElementReadRepository.GetWhere(u => u.User == user).FirstOrDefaultAsync(u=>u.Id.ToString()==todoId);
            if (toDoElement == null)
                throw new Exception("Hedef bulunamadı.");
            if(!string.IsNullOrEmpty(toDoTitle))
                toDoElement.ToDoElementTitle = toDoTitle;
            if (isCompleted!=null)
                toDoElement.IsCompleted = (Boolean)isCompleted;
            await _toDoElementWriteRepository.SaveAsync();
            return new()
            {
                Succeeded = true,
                Message = "Hedef başarıyla güncellenmiştir."
            };
        }

        public async Task<SucceededMessageResponse> DeleteToDoElementAsync(string todoId)
        {
            var user=await ContextUser();
            var toDoElement = await _toDoElementReadRepository.GetWhere(u => u.User == user).FirstOrDefaultAsync(u => u.Id.ToString() == todoId);
            if (toDoElement == null)
                throw new Exception("Hedef bulunamadı.");
            var response=await _toDoElementWriteRepository.RemoveAsync(toDoElement.Id.ToString());
            await _toDoElementWriteRepository.SaveAsync();
            return new()
            {
                Message = response ? "Hedef başarıyla silindi." : "Hedef silinirken bir hata ile karşılaşıldı.",
                Succeeded = response
            };
        }
    }
}
