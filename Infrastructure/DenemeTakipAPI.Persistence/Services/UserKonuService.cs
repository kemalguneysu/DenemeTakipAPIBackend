using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.UserKonu;
using DenemeTakipAPI.Application.Repositories.KonuRepository;
using DenemeTakipAPI.Application.Repositories.UserKonuRepository;
using DenemeTakipAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Services
{
    public class UserKonuService : IUserKonuService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IUserKonuReadRepository _userKonuReadRepository;
        readonly IUserKonuWriteRepository _userKonuWriteRepository;
        readonly UserManager<AppUser> _userManager;
        readonly IKonuReadRepository _konuReadRepository;
        readonly IUserKonuHubService _userKonuHubService;

        public UserKonuService(IHttpContextAccessor httpContextAccessor, IUserKonuReadRepository userKonuReadRepository, IUserKonuWriteRepository userKonuWriteRepository, UserManager<AppUser> userManager, IKonuReadRepository konuReadRepository, IUserKonuHubService userKonuHubService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userKonuReadRepository = userKonuReadRepository;
            _userKonuWriteRepository = userKonuWriteRepository;
            _userManager = userManager;
            _konuReadRepository = konuReadRepository;
            _userKonuHubService = userKonuHubService;
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
        public async Task CreateUserKonuAsync(List<string> konuIds)
        {
            var user = await ContextUser();
            if(konuIds!=null && konuIds.Any())
            {
                foreach (var konuId in konuIds)
                {
                    var konu = await _konuReadRepository.GetByIdAsync(konuId);
                    if (konu == null)
                        throw new Exception("Konu bulunamadı.");
                    await _userKonuWriteRepository.AddAsync(new()
                    {
                        User = user,
                        UserId = user.Id,
                        Konu = konu,
                        KonuId = konu.Id
                    });
                    await _userKonuHubService.UserKonuAddedMessage(user.Id, "Tamamlanan konu başarıyla eklendi.");
                    await _userKonuWriteRepository.SaveAsync();
                }
            }
            else
                throw new Exception("Konu boş olmamalıdır.");
        }
    }
}
