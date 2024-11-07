using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.UserKonu;
using DenemeTakipAPI.Application.Repositories.KonuRepository;
using DenemeTakipAPI.Application.Repositories.UserKonuRepository;
using DenemeTakipAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Org.BouncyCastle.Asn1;
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
                var userKonular = await _userKonuReadRepository.GetWhere(u => u.User == user).ToListAsync();
                foreach (var konuId in konuIds)
                {
                    var konu = await _konuReadRepository.GetByIdAsync(konuId);
                    if (konu == null)
                        throw new Exception("Konu bulunamadı.");
                    //Eğer konu daha önce eklendiyse silinir.
                    else if (userKonular.Any(u => u.Konu == konu))
                    {
                        var existingUserKonu = userKonular.FirstOrDefault(u => u.Konu == konu);
                        await _userKonuWriteRepository.RemoveAsync(existingUserKonu.Id.ToString());
                    }
                    else
                    {
                        await _userKonuWriteRepository.AddAsync(new()
                        {
                            User = user,
                            UserId = user.Id,
                            Konu = konu,
                            KonuId = konu.Id
                        });
                    }
                    await _userKonuHubService.UserKonuUpdatedMessage(user.Id, "Tamamlanan konu başarıyla güncellendi.");
                    await _userKonuWriteRepository.SaveAsync();
                }
            }
            else
                throw new Exception("Konu boş olmamalıdır.");
        }

        public async Task<GetUserKonular> GetUserKonular(int? page, int? size, string? dersId)
        {
            var user = await ContextUser();
            var query = _userKonuReadRepository.GetWhere(u => u.User == user).Include(u=>u.Konu).ThenInclude(u=>u.Ders).AsQueryable();
            if(page!=null && size != null)
            {
                query = query.Skip((page.Value-1)*size.Value).Take(size.Value);
            }
            if (!string.IsNullOrEmpty(dersId))
                query = query.Where(u => u.Konu.Ders.Id.ToString() == dersId);
            var totalCount = await query.CountAsync();
            var userKonular = await query.Select(u => new
            {
                Id=u.Id.ToString(),
                KonuId=u.KonuId.ToString(),
                KonuAdi=u.Konu.KonuAdi,
                DersAdi=u.Konu.Ders.DersAdi,
                DersId= u.Konu.Ders.Id.ToString()
            }).ToListAsync();
            return new()
            {
                TotalCount = totalCount,
                UserKonular = userKonular
            };

        }
    }
}
