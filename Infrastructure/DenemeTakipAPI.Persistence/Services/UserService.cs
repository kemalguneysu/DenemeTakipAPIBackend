using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs.Konu;
using DenemeTakipAPI.Application.DTOs.User;
using DenemeTakipAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections;
using DenemeTakipAPI.Application.Abstraction.Hubs;
using Microsoft.AspNetCore.Http;

namespace DenemeTakipAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IUserHubService _userHubService;
        readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<AppUser> userManager, IUserHubService userHubService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _userHubService = userHubService;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<CreateUserResponse> CreateUserAsync(CreateUser createUser)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUser.UserName,
                Email = createUser.Email,
                
            },createUser.Password);
            CreateUserResponse response = new() { Succeeded=result.Succeeded};
            if(result.Succeeded) {
                response.Message = "Kullanıcı başarıyla oluşturuldu.";
            }
            else
                foreach (var error in result.Errors)
                {
                    switch (error.Code)
                    {
                        case "DuplicateUserName":
                            response.Message += $"{createUser.UserName} kullanıcı adı zaten kullanılmaktadır.\n";
                            break;
                        case "DuplicateEmail":
                            response.Message += $"{createUser.Email} e-mail adresi zaten kullanılmaktadır.\n";
                            break;
                    }
                }

            return response;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception("Kullanıcı bulunamadı");
        }
        public async Task<UserList> GetAllUsers(int page, int size,string? nameOrEmail,string? isAdmin)
        {
            var query= _userManager.Users.AsQueryable();
            var adminUserIds = (await _userManager.GetUsersInRoleAsync("Admin")).Select(u => u.Id).ToList();
            if (!string.IsNullOrEmpty(nameOrEmail))
            {
                var loweredNameOrEmail = nameOrEmail.ToLower();
                query = query.Where(u => u.UserName.ToLower().Contains(loweredNameOrEmail) || u.Email.ToLower().Contains(loweredNameOrEmail));
            }
            if (!string.IsNullOrEmpty(isAdmin))
            {
                if (isAdmin.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(u => adminUserIds.Contains(u.Id));
                }
                else if (isAdmin.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(u => !adminUserIds.Contains(u.Id));
                }
            }
            var users = await query.Select(u => new UserListSingle
            {
                Id= u.Id,
                UserName=u.UserName,
                Email=u.Email,
                IsAdmin = adminUserIds.Contains(u.Id)
            }).Skip((page-1)*size).Take(size).ToListAsync();

            var totalCount = await query.CountAsync();
            return new UserList()
            {
                TotalCount = totalCount,
                Users = users
            };
        }

        public  async Task<string[]> GetUserRoles(string username)
        {
           AppUser user= await _userManager.FindByNameAsync(username);
            if(user == null)
                user=await _userManager.FindByEmailAsync(username);
            if(user == null)
                user = await _userManager.FindByIdAsync(username);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToArray();
            }
            return new string[] {};
        }

        public async Task AssignToRolesAsync(string userId, string[] roles)
        {
            var admin = await ContextUser();
            if (admin == null)
                throw new Exception("Kullanıcı bulunamadı.");
            AppUser user= await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);
                await _userHubService.UserUpdatedMessage(admin.Id, "Kullanıcı bilgileri başarıyla güncellenmiştir.");
            }
            else
                throw new Exception("Kullanıcı bulunamadı.");
        }

        public async Task<UserById> GetUserById(string userId)
        {
            AppUser user=await _userManager.FindByIdAsync(userId);
            if (user == null )
                throw new Exception("Kullanıcı bulunamadı.");
            return new UserById
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed= user.EmailConfirmed,
            };
        }

        public async Task UpdatePassword(string userId, string resetToken, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");
            if (user!= null)
            {
                byte[] tokenBytes=WebEncoders.Base64UrlDecode(resetToken);
                resetToken=Encoding.UTF8.GetString(tokenBytes);
                IdentityResult result=await _userManager.ResetPasswordAsync(user,resetToken,newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new Exception("Şifre yenilenirken bir hata oluştu.");
            }
        }
    }
}
