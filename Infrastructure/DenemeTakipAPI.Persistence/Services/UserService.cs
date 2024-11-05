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
using Microsoft.AspNetCore.WebUtilities;
using System.Collections;
using DenemeTakipAPI.Application.Abstraction.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using DenemeTakipAPI.Application.DTOs;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using DenemeTakipAPI.Application.Repositories.AytRepository;
using DenemeTakipAPI.Application.Repositories.TytRepository;
using System.IO.Compression;

namespace DenemeTakipAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IUserHubService _userHubService;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IAytReadRepository _aytReadRepository;
        readonly ITytReadRepository _tytReadRepository;


        public UserService(UserManager<AppUser> userManager, IUserHubService userHubService, IHttpContextAccessor httpContextAccessor, IAytReadRepository aytReadRepository, ITytReadRepository tytReadRepository)
        {
            _userManager = userManager;
            _userHubService = userHubService;
            _httpContextAccessor = httpContextAccessor;
            _aytReadRepository = aytReadRepository;
            _tytReadRepository = tytReadRepository;
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

        public async Task<SucceededMessageResponse> UpdatePassword(string userId, string resetToken, string newPassword)
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
                {
                    await _userManager.UpdateSecurityStampAsync(user);

                    return new()
                    {
                        Succeeded = true,
                        Message = "Şifreniz başarıyla yenilenmiştir."
                    };
                }
                else
                {
                    var errorMessages = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        switch (error.Code)
                        {
                            case "PasswordTooShort":
                                errorMessages.Add("Yeni şifre çok kısa. En az 6 karakter olmalıdır.");
                                break;
                            case "PasswordRequiresNonAlphanumeric":
                                errorMessages.Add("Yeni şifre en az bir özel karakter içermelidir.");
                                break;
                            case "PasswordRequiresDigit":
                                errorMessages.Add("Yeni şifre en az bir rakam içermelidir.");
                                break;
                            case "PasswordRequiresLower":
                                errorMessages.Add("Yeni şifre en az bir küçük harf içermelidir.");
                                break;
                            case "PasswordRequiresUpper":
                                errorMessages.Add("Yeni şifre en az bir büyük harf içermelidir.");
                                break;
                            case "InvalidToken":
                                errorMessages.Add("Geçersiz veya süresi dolmuş token. Lütfen tekrar deneyin.");
                                break;
                            default:
                                errorMessages.Add("Şifre değiştirilirken bilinmeyen bir hata oluştu.");
                                break;
                        }
                    }

                    var detailedErrorMessage = string.Join(" ", errorMessages);

                    return new()
                    {
                        Succeeded = false,
                        Message = $"{detailedErrorMessage}"
                    };
                }
            }
            return new()
            {
                Succeeded = false,
                Message = "Kullanıcı bulunamadı."
            };
        }

        public async Task<DeleteAccountResponse> DeleteAccount(string? userId)
        {
            var requestingUser = await ContextUser();
            var userRoles = await _userManager.GetRolesAsync(requestingUser);
            if (userRoles.Contains("admin"))
            {
                if (userId == null)
                    throw new Exception("UserId boş girildi.");
                var requestedUser = await _userManager.FindByIdAsync(userId);
                if(requestedUser==null)
                    throw new Exception("Kullanıcı bulunamadı.");
                var result = await _userManager.DeleteAsync(requestedUser);
                if (result.Succeeded)
                {
                    return new()
                    {
                        Succeeded = true,
                        Message = "Hesap başarıyla silinmiştir."
                    };
                }
                else
                {
                    return new()
                    {
                        Succeeded = false,
                        Message = "Hesap silinirken bir hata ile karşılaşıldı."
                    };
                }
            }
            else
            {
                var result = await _userManager.DeleteAsync(requestingUser);
                if (result.Succeeded)
                {
                    return new()
                    {
                        Succeeded = true,
                        Message = "Hesabınız başarıyla silinmiştir."
                    };
                }
                else
                {
                    return new()
                    {
                        Succeeded = false,
                        Message = "Hesabınız silinirken bir hata ile karşılaşıldı."
                    };
                }

            }
        }

        public async Task<SucceededMessageResponse> UpdateUserPassword(string currentPassword, string newPassword)
        {
            var user=await ContextUser();
            var result=await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                return new()
                {
                    Succeeded = true,
                    Message = "Şifreniz başarıyla değiştirilmiştir."
                };
            }
            else
            {
                var errorMessages = new List<string>();
                foreach (var error in result.Errors)
                {
                    switch (error.Code)
                    {
                        case "PasswordMismatch":
                            errorMessages.Add("Mevcut şifre yanlış.");
                            break;
                        case "PasswordTooShort":
                            errorMessages.Add("Yeni şifre çok kısa. En az 6 karakter olmalıdır.");
                            break;
                        case "PasswordRequiresNonAlphanumeric":
                            errorMessages.Add("Yeni şifre en az bir özel karakter içermelidir.");
                            break;
                        case "PasswordRequiresDigit":
                            errorMessages.Add("Yeni şifre en az bir rakam içermelidir.");
                            break;
                        case "PasswordRequiresLower":
                            errorMessages.Add("Yeni şifre en az bir küçük harf içermelidir.");
                            break;
                        case "PasswordRequiresUpper":
                            errorMessages.Add("Yeni şifre en az bir büyük harf içermelidir.");
                            break;
                        default:
                            errorMessages.Add("Şifre değiştirilirken bilinmeyen bir hata oluştu.");
                            break;
                    }
                }

                var detailedErrorMessage = string.Join(" ", errorMessages);

                return new()
                {
                    Succeeded = false,
                    Message = $"{detailedErrorMessage}"
                };
            }
        }
        private void WriteCsv<T>(string path, IEnumerable<T> records)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }
        public async Task<string> ExportUserDataAsZipAsync(string? userId)
        {
            var user = await ContextUser();
            string requestedUserId;
            var userRoles = await _userManager.GetRolesAsync(user);

            // Kullanıcı yetkilerini kontrol et
            if (userRoles.Contains("admin"))
            {
                requestedUserId = userId ?? user.Id; // Eğer userId yoksa mevcut kullanıcının ID'sini kullan
            }
            else
            {
                requestedUserId = user.Id; // Admin değilse mevcut kullanıcının ID'sini kullan
            }

            // wwwroot dizinini ve UserDatas alt dizinini oluştur
            string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string userDatasPath = Path.Combine(webRootPath, "UserDatas");
            Directory.CreateDirectory(userDatasPath);

            // ZIP dosyasının yolunu belirle
            string zipPath = Path.Combine(userDatasPath, $"{requestedUserId}_data.zip");

            // Eğer ZIP dosyası zaten varsa, onu döndür
            if (File.Exists(zipPath))
            {
                return zipPath;
            }

            // Kullanıcı verilerini içeren dizinleri oluştur
            string basePath = Path.Combine(userDatasPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(basePath);
            Directory.CreateDirectory(Path.Combine(basePath, "TytDenemeler"));
            Directory.CreateDirectory(Path.Combine(basePath, "AytDenemeler"));

            // Kullanıcı verilerini CSV dosyaları olarak oluştur
            await GenerateUserCSVAsync(requestedUserId, Path.Combine(basePath, "bilgiler.csv"));
            await GenerateTytCSVAsync(requestedUserId, Path.Combine(basePath, "TytDenemeler", "tytDenemeler.csv"));
            await GenerateAytCSVAsync(requestedUserId, Path.Combine(basePath, "AytDenemeler", "aytDenemeler.csv"));
            await GenerateAytIncroccetTopicsCSVAsync(requestedUserId, Path.Combine(basePath, "AytDenemeler", "AytDenemelerYanlisKonular.csv"));
            await GenerateAytEmptyTopicsCSVAsync(requestedUserId, Path.Combine(basePath, "AytDenemeler", "AytDenemelerBosKonular.csv"));
            await GenerateTytIncroccetTopicsCSVAsync(requestedUserId, Path.Combine(basePath, "TytDenemeler", "TytDenemelerYanlisKonular.csv"));
            await GenerateTytEmptyTopicsCSVAsync(requestedUserId, Path.Combine(basePath, "TytDenemeler", "TytDenemelerBosKonular.csv"));

            // Dizini ZIP dosyası olarak oluştur
            ZipFile.CreateFromDirectory(basePath, zipPath);

            // Geçici dizini sil
            Directory.Delete(basePath, true);

            return zipPath; // ZIP dosyasının yolunu döndür
        }


        private async Task GenerateUserCSVAsync(string userId,string path)
        {
            var userData = await _userManager.Users.Where(u => u.Id == userId).Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email,
                u.EmailConfirmed,
                u.PhoneNumber,
                u.PhoneNumberConfirmed,
                u.TwoFactorEnabled
            }).ToListAsync();
            WriteCsv(path, userData);
        }
        private async Task GenerateTytCSVAsync(string userId,string path)
        {
            var tytData=await _tytReadRepository.GetWhere(u=>u.UserId== userId).Select(u => new
            {
                u.Id,
                u.TurkceDogru,
                u.TurkceYanlis,
                u.MatematikDogru,
                u.MatematikYanlis,
                u.FenDogru,
                u.FenYanlis,
                u.SosyalDogru,
                u.SosyalYanlis,
                u.CreatedDate,
            }).ToListAsync();
            WriteCsv(path, tytData);
        }
        private async Task GenerateAytCSVAsync(string userId,string path)
        {
            var aytData = await _aytReadRepository.GetWhere(u => u.UserId == userId).Select(u => new
            {
                u.Id,
                u.MatematikDogru,
                u.MatematikYanlis,
                u.FizikDogru,
                u.FizikYanlis,
                u.KimyaDogru,
                u.KimyaYanlis,
                u.BiyolojiDogru,
                u.BiyolojiYanlis,
                u.EdebiyatDogru,
                u.EdebiyatYanlis,
                u.Tarih1Dogru,
                u.Tarih1Yanlis,
                u.Cografya1Dogru,
                u.Cografya1Yanlis,
                u.Tarih2Dogru,
                u.Tarih2Yanlis,
                u.Cografya2Dogru,
                u.Cografya2Yanlis,
                u.FelsefeDogru,
                u.FelsefeYanlis,
                u.DinDogru,
                u.DinYanlis,
                u.DilDogru,
                u.DilYanlis,
                u.CreatedDate
            }).ToListAsync();
            WriteCsv(path, aytData);
        }
        private async Task GenerateAytIncroccetTopicsCSVAsync(string userId,string path)
        {
            var data = await _aytReadRepository.GetWhere(u => u.UserId == userId)
                .Include(u => u.YanlisKonular).ThenInclude(u=>u.Ders).SelectMany(u => u.YanlisKonular, (u, yanlisKonu) => new
                {
                    u.Id,
                    yanlisKonu.KonuAdi,
                    yanlisKonu.IsTyt,
                    yanlisKonu.Ders.DersAdi
                })
            .ToListAsync();
            WriteCsv(path, data);
        }
        private async Task GenerateAytEmptyTopicsCSVAsync(string userId, string path)
        {
            var data = await _aytReadRepository.GetWhere(u => u.UserId == userId).Include(u => u.BosKonular).ThenInclude(u => u.Ders)
                .SelectMany(u => u.YanlisKonular, (u, bosKonu) => new
            {
                u.Id,
                bosKonu.KonuAdi,
                bosKonu.IsTyt,
                bosKonu.Ders.DersAdi
            })
            .ToListAsync();
            WriteCsv(path, data);
        }
        private async Task GenerateTytIncroccetTopicsCSVAsync(string userId, string path)
        {
            var data = await _tytReadRepository.GetWhere(u => u.UserId == userId).Include(u => u.YanlisKonular).ThenInclude(u => u.Ders).SelectMany(u => u.YanlisKonular, (u, yanlisKonu) => new
            {
                u.Id,
                yanlisKonu.KonuAdi,
                yanlisKonu.IsTyt,
                yanlisKonu.Ders.DersAdi
            })
            .ToListAsync();
            WriteCsv(path, data);
        }
        private async Task GenerateTytEmptyTopicsCSVAsync(string userId, string path)
        {
            var data = await _tytReadRepository.GetWhere(u => u.UserId == userId).Include(u => u.BosKonular).ThenInclude(u => u.Ders)
                .SelectMany(u => u.YanlisKonular, (u, bosKonu) => new
            {
                u.Id,
                bosKonu.KonuAdi,
                bosKonu.IsTyt,
                bosKonu.Ders.DersAdi
            })
            .ToListAsync();
            WriteCsv(path, data);
        }


    }
}
