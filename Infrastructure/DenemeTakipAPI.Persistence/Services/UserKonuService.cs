using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.UserKonu;
using DenemeTakipAPI.Application.Repositories.UserKonuRepository;
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
    public class UserKonuService : IUserKonuService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IUserKonuReadRepository _userKonuReadRepository;
        readonly IUserKonuWriteRepository _userKonuWriteRepository;
        readonly UserManager<AppUser> _userManager;

        public UserKonuService(IHttpContextAccessor httpContextAccessor, IUserKonuReadRepository userKonuReadRepository, IUserKonuWriteRepository userKonuWriteRepository, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userKonuReadRepository = userKonuReadRepository;
            _userKonuWriteRepository = userKonuWriteRepository;
            _userManager = userManager;
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
        public async Task<SucceededMessageResponse> CreateUserKonuAsync(List<CreateOrUpdateUserKonu> createOrUpdateUserKonus)
        {
            var user = await ContextUser();

        }
    }
}
