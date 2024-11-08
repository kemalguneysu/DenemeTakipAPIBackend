using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs.ToDoElement;
using DenemeTakipAPI.Application.Repositories;
using DenemeTakipAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        public async Task CreateToDoElementAsync(string ToDoElementTitle, DateOnly ToDoDate, bool IsCompleted)
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

        public async Task<GetToDoElements> GetToDoElements(DateOnly toDoDate, bool? isCompleted)
        {
            var user = await ContextUser();
            throw new Exception();
            //return new()
            //{
            //    ToDoElements = toDoElements,
            //    TotalCount = totalCount
            //};
        }
    }
}
