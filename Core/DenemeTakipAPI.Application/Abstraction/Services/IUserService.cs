using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.Konu;
using DenemeTakipAPI.Application.DTOs.User;
using DenemeTakipAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUser createUser);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task<UserList> GetAllUsers(int page, int size, string? nameOrEmail,string? isAdmin);
        Task<string[]> GetUserRoles(string username);
        Task AssignToRolesAsync(string userId, string[] roles);
        Task<UserById> GetUserById(string userId);
        Task<SucceededMessageResponse> UpdatePassword(string userId, string resetToken,string newPassword);
        Task<DeleteAccountResponse> DeleteAccount(string? userId);
        Task<SucceededMessageResponse> UpdateUserPassword(string currentPassword,string newPassword);
        Task<string> ExportUserDataAsZipAsync(string? userId);
    }
}
