using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs.Ayt;
using DenemeTakipAPI.Application.Repositories.AytRepository;
using DenemeTakipAPI.Application.Repositories.KonuRepository;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using DenemeTakipAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.AytFolder;
using Microsoft.EntityFrameworkCore;
using DenemeTakipAPI.Application.Repositories.DersRepository;
using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using DenemeTakipAPI.Application.Abstraction.Hubs;
using Microsoft.AspNet.Identity;
using ZstdSharp;

namespace DenemeTakipAPI.Persistence.Services
{
    public class AytService:IAytService
    {
        readonly IAytReadRepository _aytReadRepository;
        readonly IAytWriteRepository _aytWriteRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IKonuReadRepository _konuReadRepository;
        readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;
        readonly IDersReadRepository _dersReadRepository;
        readonly IAytHubService _aytHubService;

        public AytService(IAytReadRepository aytReadRepository, IAytWriteRepository aytWriteRepository, IHttpContextAccessor httpContextAccessor, IKonuReadRepository konuReadRepository, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager, IDersReadRepository dersReadRepository, IAytHubService aytHubService)
        {
            _aytReadRepository = aytReadRepository;
            _aytWriteRepository = aytWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _konuReadRepository = konuReadRepository;
            _userManager = userManager;
            _dersReadRepository = dersReadRepository;
            _aytHubService = aytHubService;
        }

        private async Task<AppUser> ContextUser()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user= _userManager.Users.FirstOrDefault(u=>u.UserName == username);
                return user;
            }
            throw new Exception("Kullanıcı bulunamadı");
            

        }
        public async Task CreateAytAsync(CreateAyt createAyt)
        {
            var user = await ContextUser();
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");
            var yanlisKonular = new List<Konu>();
            foreach (var yanlisKonuId in createAyt.YanlisKonularId)
            {
                if (string.IsNullOrEmpty(yanlisKonuId))
                    continue;
                var konu = await _konuReadRepository.GetByIdAsync(yanlisKonuId);
                if (konu == null)
                    continue;
                yanlisKonular.Add(konu);
            }

            var bosKonular = new List<Konu>();
            foreach (var bosKonuId in createAyt.BosKonularId)
            {
                if (string.IsNullOrEmpty(bosKonuId))
                    continue;
                var konu = await _konuReadRepository.GetByIdAsync(bosKonuId);
                if (konu == null)
                    continue;
                bosKonular.Add(konu);
            }
            await _aytWriteRepository.AddAsync(new()
            {
                MatematikDogru = createAyt.MatematikDogru,
                MatematikYanlis = createAyt.MatematikYanlis,

                FizikDogru = createAyt.FizikDogru,
                FizikYanlis = createAyt.FizikYanlis,
                BiyolojiDogru = createAyt.BiyolojiDogru,
                BiyolojiYanlis = createAyt.BiyolojiYanlis,
                KimyaDogru = createAyt.KimyaDogru,
                KimyaYanlis = createAyt.KimyaYanlis,


                EdebiyatDogru = createAyt.EdebiyatDogru,
                EdebiyatYanlis = createAyt.EdebiyatYanlis,
                Tarih1Dogru = createAyt.Tarih1Dogru,
                Tarih1Yanlis = createAyt.Tarih1Yanlis,
                Cografya1Dogru = createAyt.Cografya1Dogru,
                Cografya1Yanlis = createAyt.Cografya1Yanlis,


                Cografya2Dogru = createAyt.Cografya2Dogru,
                Cografya2Yanlis = createAyt.Cografya2Yanlis,
                Tarih2Dogru = createAyt.Tarih2Dogru,
                Tarih2Yanlis = createAyt.Tarih2Yanlis,
                DinDogru = createAyt.DinDogru,
                DinYanlis = createAyt.DinYanlis,
                FelsefeDogru = createAyt.FelsefeDogru,
                FelsefeYanlis = createAyt.FelsefeYanlis,

                DilDogru = createAyt.DilDogru,
                DilYanlis = createAyt.DilYanlis,

                User = user,
                YanlisKonular = yanlisKonular,
                BosKonular = bosKonular


            });
            await _aytHubService.AytAddedMessage(user.Id, "AYT denemesi başarıyla eklendi.");
            await _aytWriteRepository.SaveAsync();
        }


        public async Task UpdateAytAsync(UpdateAyt updateAyt)
        {
            var user = await ContextUser();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<Konu> yanlisKonular = new List<Konu>();
            List<Konu> bosKonular = new List<Konu>();
            AytDeneme aytDeneme;
            if (userRoles.Contains("admin"))
            {
                aytDeneme = await _aytReadRepository.GetWhere(u => u.Id.ToString() == updateAyt.AytId)
                            .Include(u=>u.YanlisKonular).Include(u=>u.BosKonular).FirstOrDefaultAsync();
                if (aytDeneme == null)
                    throw new Exception("AYT denemesi bulunamadı.");
                aytDeneme.MatematikDogru = updateAyt.MatematikDogru;
                aytDeneme.MatematikYanlis = updateAyt.MatematikYanlis;
                aytDeneme.FizikDogru = updateAyt.FizikDogru;
                aytDeneme.FizikYanlis = updateAyt.FizikYanlis;
                aytDeneme.BiyolojiDogru = updateAyt.BiyolojiDogru;
                aytDeneme.BiyolojiYanlis = updateAyt.BiyolojiYanlis;
                aytDeneme.KimyaDogru = updateAyt.KimyaDogru;
                aytDeneme.KimyaYanlis = updateAyt.KimyaYanlis;
                aytDeneme.EdebiyatDogru = updateAyt.EdebiyatDogru;
                aytDeneme.EdebiyatYanlis = updateAyt.EdebiyatYanlis;
                aytDeneme.Tarih1Dogru = updateAyt.Tarih1Dogru;
                aytDeneme.Tarih1Yanlis = updateAyt.Tarih1Yanlis;
                aytDeneme.Cografya1Dogru = updateAyt.Cografya1Dogru;
                aytDeneme.Cografya1Yanlis = updateAyt.Cografya1Yanlis;
                aytDeneme.Cografya2Dogru = updateAyt.Cografya2Dogru;
                aytDeneme.Cografya2Yanlis = updateAyt.Cografya2Yanlis;
                aytDeneme.Tarih2Dogru = updateAyt.Tarih2Dogru;
                aytDeneme.Tarih2Yanlis = updateAyt.Tarih2Yanlis;
                aytDeneme.DinDogru = updateAyt.DinDogru;
                aytDeneme.DinYanlis = updateAyt.DinYanlis;
                aytDeneme.FelsefeDogru = updateAyt.FelsefeDogru;
                aytDeneme.FelsefeYanlis = updateAyt.FelsefeYanlis;
                aytDeneme.DilDogru = updateAyt.DilDogru;
                aytDeneme.DilYanlis = updateAyt.DilYanlis;
                if (updateAyt.YanlisKonularId != null && updateAyt.YanlisKonularId.Any())
                {
                    yanlisKonular = await _konuReadRepository.GetWhere(u => updateAyt.YanlisKonularId.Contains(u.Id.ToString())).ToListAsync();
                    aytDeneme.YanlisKonular=yanlisKonular;
                }
                if (updateAyt.BosKonularId != null && updateAyt.BosKonularId.Any())
                {
                    bosKonular = await _konuReadRepository.GetWhere(u => updateAyt.BosKonularId.Contains(u.Id.ToString())).ToListAsync();
                    aytDeneme.BosKonular = bosKonular;
                }
                await _aytHubService.AytUpdatedMessage(user.Id, "AYT denemesi başarıyla güncellendi.");
                await _aytWriteRepository.SaveAsync();
            }
            else
            {
                aytDeneme = await _aytReadRepository.GetWhere(u=>u.User==user)
                            .Include(u => u.YanlisKonular).Include(u => u.BosKonular).FirstOrDefaultAsync(u => u.Id.ToString() == updateAyt.AytId);
                if (aytDeneme == null)
                    throw new Exception("AYT denemesi bulunamadı.");
                aytDeneme.MatematikDogru = updateAyt.MatematikDogru;
                aytDeneme.MatematikYanlis = updateAyt.MatematikYanlis;
                aytDeneme.FizikDogru = updateAyt.FizikDogru;
                aytDeneme.FizikYanlis = updateAyt.FizikYanlis;
                aytDeneme.BiyolojiDogru = updateAyt.BiyolojiDogru;
                aytDeneme.BiyolojiYanlis = updateAyt.BiyolojiYanlis;
                aytDeneme.KimyaDogru = updateAyt.KimyaDogru;
                aytDeneme.KimyaYanlis = updateAyt.KimyaYanlis;
                aytDeneme.EdebiyatDogru = updateAyt.EdebiyatDogru;
                aytDeneme.EdebiyatYanlis = updateAyt.EdebiyatYanlis;
                aytDeneme.Tarih1Dogru = updateAyt.Tarih1Dogru;
                aytDeneme.Tarih1Yanlis = updateAyt.Tarih1Yanlis;
                aytDeneme.Cografya1Dogru = updateAyt.Cografya1Dogru;
                aytDeneme.Cografya1Yanlis = updateAyt.Cografya1Yanlis;
                aytDeneme.Cografya2Dogru = updateAyt.Cografya2Dogru;
                aytDeneme.Cografya2Yanlis = updateAyt.Cografya2Yanlis;
                aytDeneme.Tarih2Dogru = updateAyt.Tarih2Dogru;
                aytDeneme.Tarih2Yanlis = updateAyt.Tarih2Yanlis;
                aytDeneme.DinDogru = updateAyt.DinDogru;
                aytDeneme.DinYanlis = updateAyt.DinYanlis;
                aytDeneme.FelsefeDogru = updateAyt.FelsefeDogru;
                aytDeneme.FelsefeYanlis = updateAyt.FelsefeYanlis;
                aytDeneme.DilDogru = updateAyt.DilDogru;
                aytDeneme.DilYanlis = updateAyt.DilYanlis;
                if (updateAyt.YanlisKonularId != null && updateAyt.YanlisKonularId.Any())
                {
                    yanlisKonular = await _konuReadRepository.GetWhere(u => updateAyt.YanlisKonularId.Contains(u.Id.ToString())).ToListAsync();
                    aytDeneme.YanlisKonular = yanlisKonular;
                }
                if (updateAyt.BosKonularId != null && updateAyt.BosKonularId.Any())
                {
                    bosKonular = await _konuReadRepository.GetWhere(u => updateAyt.BosKonularId.Contains(u.Id.ToString())).ToListAsync();
                    aytDeneme.BosKonular = bosKonular;
                }
                await _aytHubService.AytUpdatedMessage(user.Id, "AYT denemesi başarıyla güncellendi.");
                await _aytWriteRepository.SaveAsync();

            }
        }

        public async Task<ListSingleAyt> GetAytById(string id)
        {
            var user = await ContextUser();
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");
            var userRoles = await _userManager.GetRolesAsync(user);
            AytDeneme ayt;
            List<KonularAdDers> yanlisKonular = new List<KonularAdDers>();
            List<KonularAdDers> bosKonular = new List<KonularAdDers>();
            if (userRoles.Contains("admin"))
            {
                ayt = await _aytReadRepository.GetWhere(u => u.Id.ToString() == id)
                        .Include(u => u.BosKonular).ThenInclude(u => u.Ders).Include(u => u.YanlisKonular).ThenInclude(u => u.Ders).SingleOrDefaultAsync();
                if (ayt == null)
                    throw new Exception("AYT denemesi bulunamadı.");
                ListSingleAyt singleListAyt = new ListSingleAyt
                {
                    Id = ayt.Id.ToString(),
                    MatematikDogru = ayt.MatematikDogru,
                    MatematikYanlis = ayt.MatematikYanlis,
                    YanlisKonularAdDers = ayt.YanlisKonular.Select(konu => new KonularAdDers
                    {
                        KonuAdi = konu.KonuAdi,
                        DersAdi = konu.Ders.DersAdi,
                        DersId = konu.Ders.Id.ToString(),
                        KonuId = konu.Id.ToString()

                    }).ToList(),
                    BosKonularAdDers = ayt.BosKonular.Select(konu => new KonularAdDers
                    {
                        KonuAdi = konu.KonuAdi,
                        DersAdi = konu.Ders.DersAdi,
                        DersId = konu.Ders.Id.ToString(),
                        KonuId = konu.Id.ToString()
                    }).ToList()
                };
                return singleListAyt;
            }
            else
            {
                ayt = await _aytReadRepository.GetWhere(u => u.User == user)
                    .Include(u => u.YanlisKonular).ThenInclude(u => u.Ders).Include(u => u.BosKonular).ThenInclude(u => u.Ders).FirstOrDefaultAsync(u => u.Id.ToString() == id);
                if (ayt == null)
                    throw new Exception("AYT denemesi bulunamadı.");
                ListSingleAyt singleListAyt = new ListSingleAyt
                {
                    Id = ayt.Id.ToString(),
                    MatematikDogru = ayt.MatematikDogru,
                    MatematikYanlis = ayt.MatematikYanlis,
                    FizikDogru = ayt.FizikDogru,
                    FizikYanlis = ayt.FizikYanlis,
                    KimyaDogru = ayt.KimyaDogru,
                    KimyaYanlis = ayt.KimyaYanlis,
                    BiyolojiDogru = ayt.BiyolojiDogru,
                    BiyolojiYanlis = ayt.BiyolojiYanlis,
                    EdebiyatDogru = ayt.EdebiyatDogru,
                    EdebiyatYanlis = ayt.EdebiyatYanlis,
                    Tarih1Dogru = ayt.Tarih1Dogru,
                    Tarih1Yanlis = ayt.Tarih1Yanlis,
                    Cografya1Dogru = ayt.Cografya1Dogru,
                    Cografya1Yanlis = ayt.Cografya1Yanlis,
                    Tarih2Dogru = ayt.Tarih2Dogru,
                    Tarih2Yanlis = ayt.Tarih2Yanlis,
                    Cografya2Dogru = ayt.Cografya2Dogru,
                    Cografya2Yanlis = ayt.Cografya2Yanlis,
                    FelsefeDogru = ayt.FelsefeDogru,
                    FelsefeYanlis = ayt.FelsefeYanlis,
                    DinDogru = ayt.DinDogru,
                    DinYanlis = ayt.DinYanlis,
                    DilDogru = ayt.DilDogru,
                    DilYanlis = ayt.DilYanlis,
                    YanlisKonularAdDers = ayt.YanlisKonular.Select(konu => new KonularAdDers
                    {
                        KonuAdi = konu.KonuAdi,
                        DersAdi = konu.Ders.DersAdi,
                        DersId = konu.Ders.Id.ToString(),
                        KonuId = konu.Id.ToString()

                    }).ToList(),
                    BosKonularAdDers = ayt.BosKonular.Select(konu => new KonularAdDers
                    {
                        KonuAdi = konu.KonuAdi,
                        DersAdi = konu.Ders.DersAdi,
                        DersId = konu.Ders.Id.ToString(),
                        KonuId = konu.Id.ToString()
                    }).ToList()
                };
                return singleListAyt;
            }
        }


        public async Task<DeleteAytResponse> DeleteAyt(List<string> ids)
        {
            var user = await ContextUser();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<AytDeneme> ayts = new List<AytDeneme>();
            if (userRoles.Contains("admin"))
            {
                ayts = await _aytReadRepository.GetWhere(u => ids.Contains(u.Id.ToString())).ToListAsync();
            }
            else
            {
                ayts = await _aytReadRepository.GetWhere(u => u.User == user && ids.Contains(u.Id.ToString())).ToListAsync();
            }
            if (ayts != null && ayts.Any())
            {
                _aytWriteRepository.RemoveRange(ayts);
                await _aytWriteRepository.SaveAsync();
                await _aytHubService.AytDeletedMessage(user.Id,"Seçilen AYT denemeleri başarıyla silinmiştir.");
                return new()
                {
                    Succeeded = true,
                    Message = "Seçilen AYT denemeleri başarıyla silinmiştir."
                };
            }
            else
            {
                return new()
                {
                    Succeeded = false,
                    Message = "Size ait olmayan AYT denemelerini silemezsiniz."
                };
            }
        }

        public async Task<ListAyt> Get(int page, int size, List<string>? orderByAndDirections)
        {
            var user = await ContextUser();
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");
            var query = _aytReadRepository.GetWhere(u => u.User == user).AsQueryable();
            if (orderByAndDirections != null && orderByAndDirections.Any())
            {
                IOrderedQueryable<AytDeneme> orderedQuery = null; // Sıralanmış sorgu için

                foreach (var item in orderByAndDirections)
                {
                    var parts = item.Split(',');

                    // Geçerli formatı kontrol et
                    if (parts.Length != 2)
                    {
                        throw new Exception("Filtreleme türü doğru formatta değildir.");
                    }

                    string orderBy = parts[0].ToLower();
                    string orderDirection = parts[1].ToLower();

                    switch (orderBy)
                    {
                        case "sayisalnet":
                            // İlk sıralamayı yap
                            orderedQuery = orderDirection == "asc" ?
                                query.OrderBy(s => s.FizikDogru - 0.25m * s.FizikYanlis +
                                                    s.MatematikDogru - 0.25m * s.MatematikYanlis +
                                                    s.KimyaDogru - 0.25m * s.KimyaYanlis +
                                                    s.BiyolojiDogru - 0.25m * s.BiyolojiYanlis) :
                                query.OrderByDescending(s => s.FizikDogru - 0.25m * s.FizikYanlis +
                                                              s.MatematikDogru - 0.25m * s.MatematikYanlis +
                                                              s.KimyaDogru - 0.25m * s.KimyaYanlis +
                                                              s.BiyolojiDogru - 0.25m * s.BiyolojiYanlis);
                            break;

                        // Diğer durumlar için benzer şekilde
                        case "esitagirliknet":
                            orderedQuery = orderDirection == "asc" ?
                                query.OrderBy(s => s.EdebiyatDogru - 0.25m * s.EdebiyatYanlis +
                                                    s.MatematikDogru - 0.25m * s.MatematikYanlis +
                                                    s.Tarih1Dogru - 0.25m * s.Tarih1Yanlis +
                                                    s.Cografya1Dogru - 0.25m * s.Cografya1Yanlis) :
                                query.OrderByDescending(s => s.EdebiyatDogru - 0.25m * s.EdebiyatYanlis +
                                                    s.MatematikDogru - 0.25m * s.MatematikYanlis +
                                                    s.Tarih1Dogru - 0.25m * s.Tarih1Yanlis +
                                                    s.Cografya1Dogru - 0.25m * s.Cografya1Yanlis);
                            break;

                        case "sozelnet":
                            orderedQuery = orderDirection == "asc" ?
                                query.OrderBy(s => s.EdebiyatDogru - 0.25m * s.EdebiyatYanlis +
                                                    s.Tarih1Dogru - 0.25m * s.Tarih1Yanlis +
                                                    s.Cografya1Dogru - 0.25m * s.Cografya1Yanlis+
                                                    s.Tarih2Dogru - 0.25m * s.Tarih2Yanlis +
                                                    s.Cografya2Dogru - 0.25m * s.Cografya2Yanlis +
                                                    s.FelsefeDogru - 0.25m * s.FelsefeYanlis +
                                                    s.DinDogru - 0.25m * s.DinYanlis ) :
                                query.OrderByDescending(s => s.EdebiyatDogru - 0.25m * s.EdebiyatYanlis +
                                                    s.Tarih1Dogru - 0.25m * s.Tarih1Yanlis +
                                                    s.Cografya1Dogru - 0.25m * s.Cografya1Yanlis +
                                                    s.Tarih2Dogru - 0.25m * s.Tarih2Yanlis +
                                                    s.Cografya2Dogru - 0.25m * s.Cografya2Yanlis +
                                                    s.FelsefeDogru - 0.25m * s.FelsefeYanlis +
                                                    s.DinDogru - 0.25m * s.DinYanlis);
                            break;

                        case "dilnet":
                            orderedQuery = orderDirection == "asc" ?
                               query.OrderBy(s => s.DilDogru - 0.25m * s.DilYanlis) :
                               query.OrderByDescending(s => s.DilDogru - 0.25m * s.DilYanlis);
                            break;

                        case "tarih":
                            if (orderedQuery != null)
                            {
                                orderedQuery = orderDirection == "asc" ?
                                    orderedQuery.ThenBy(u => u.CreatedDate) :
                                    orderedQuery.ThenByDescending(u => u.CreatedDate);
                            }
                            else
                            {
                                orderedQuery = orderDirection == "asc" ?
                                    query.OrderBy(u => u.CreatedDate) :
                                    query.OrderByDescending(u => u.CreatedDate);
                            }
                            break;
                    }
                }

                if (orderedQuery == null)
                {
                    orderedQuery = query.OrderByDescending(u => u.CreatedDate);
                }

                query = orderedQuery;
            }
            if (orderByAndDirections.Count==0)
                query = query.OrderByDescending(u => u.CreatedDate);
            var totalCount = await query.CountAsync();
            var aytDenemes = await query.Skip((page - 1) * size).Take(size)
                .Select(s => new
                {
                    Id = s.Id.ToString(),
                    SayisalNet = s.FizikDogru - 0.25m * s.FizikYanlis +
                                    s.MatematikDogru - 0.25m * s.MatematikYanlis +
                                    s.KimyaDogru - 0.25m * s.KimyaYanlis +
                                    s.BiyolojiDogru - 0.25m * s.BiyolojiYanlis,
                    EsitAgirlikNet = s.EdebiyatDogru - 0.25m * s.EdebiyatYanlis +
                                        s.MatematikDogru - 0.25m * s.MatematikYanlis +
                                        s.Tarih1Dogru - 0.25m * s.Tarih1Yanlis +
                                        s.Cografya1Dogru - 0.25m * s.Cografya1Yanlis,
                    SozelNet = s.EdebiyatDogru - 0.25m * s.EdebiyatYanlis +
                                s.Tarih1Dogru - 0.25m * s.Tarih1Yanlis +
                                s.Cografya1Dogru - 0.25m * s.Cografya1Yanlis +
                                s.Tarih2Dogru - 0.25m * s.Tarih2Yanlis +
                                s.Cografya2Dogru - 0.25m * s.Cografya2Yanlis +
                                s.FelsefeDogru - 0.25m * s.FelsefeYanlis +
                                s.DinDogru - 0.25m * s.DinYanlis,
                    DilNet = s.DilDogru - 0.25m * s.DilYanlis,
                    Tarih = s.CreatedDate.ToString(),
                }).ToListAsync();
            return new()
            {
                TotalAyt = totalCount,
                Ayts = aytDenemes
            };

        }

        public async Task<List<DenemeAnaliz>> GetAytAnaliz(int DenemeSayısı, int KonuSayısı, string DersId, string type)
        {
            var user = await ContextUser();
            List<AytDeneme> ayts;
            var aytAnalizSayilar = new List<DenemeAnaliz>();

            IEnumerable<Konu> konular = Enumerable.Empty<Konu>();

            var aytQuery = _aytReadRepository.GetWhere(u => u.User == user)
                .OrderByDescending(u => u.CreatedDate)
                .Take(DenemeSayısı)
                .AsQueryable();

            if (type.Equals("yanlis", StringComparison.OrdinalIgnoreCase))
            {
                ayts = await aytQuery.Include(u => u.YanlisKonular).ThenInclude(u => u.Ders).ToListAsync();
                if (!ayts.Any())
                    return new List<DenemeAnaliz>();
                konular = ayts.SelectMany(u => u.YanlisKonular);
            }
            else if (type.Equals("bos", StringComparison.OrdinalIgnoreCase))
            {
                ayts = await aytQuery.Include(u => u.BosKonular).ThenInclude(u => u.Ders).ToListAsync();
                if (!ayts.Any())
                    return new List<DenemeAnaliz>();
                konular = ayts.SelectMany(u => u.BosKonular);
            }
            else
            {
                throw new Exception($"Geçersiz tür değeri.");
            }

            if (!konular.Any())
            {
                return new List<DenemeAnaliz>();
            }

            var aytAnaliz = konular
                .Where(u => u.Ders.Id.ToString() == DersId)
                .GroupBy(u => u.Id)
                .OrderByDescending(g => g.Count())
                .Take(KonuSayısı)
                .ToList();

            if (!aytAnaliz.Any())
            {
                return new List<DenemeAnaliz>();
            }

            var konuIds = aytAnaliz.Select(g => g.Key).ToList();
            var konuBilgileri = await _konuReadRepository.GetWhere(k => konuIds.Contains(k.Id))
                .Include(k => k.Ders)
                .ToDictionaryAsync(k => k.Id);

            aytAnalizSayilar = aytAnaliz.Select(item => new DenemeAnaliz
            {
                KonuId = item.Key.ToString(),
                DersId = konuBilgileri[item.Key].Ders.Id.ToString(),
                KonuAdi = konuBilgileri[item.Key].KonuAdi,
                Sayi = item.Count()
            }).ToList();

            return aytAnalizSayilar;
        }

        public async Task<ListAytAnaliz> GetAytNetAnaliz(int denemeSayisi,string alanTuru,string? dersAdi)
        {
            var user = await ContextUser();
            var query = _aytReadRepository.GetWhere(u => u.User == user).OrderByDescending(u => u.CreatedDate).Take(denemeSayisi).AsQueryable();
            object ayts;
            alanTuru = alanTuru.ToLower();
            if (dersAdi != null)
                dersAdi = dersAdi.ToLower();
            switch (alanTuru)
            {
                case "sayisal":
                    if (!string.IsNullOrEmpty(dersAdi))
                    {
                        if (dersAdi.Equals("matematik", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.MatematikDogru - 0.25m * u.MatematikYanlis,
                                Dogru = u.MatematikDogru,
                                Yanlis = u.MatematikYanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("fizik", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.FizikDogru - 0.25m * u.FizikYanlis,
                                Dogru = u.FizikDogru,
                                Yanlis = u.FizikYanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("kimya", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.KimyaDogru - 0.25m * u.KimyaYanlis,
                                Dogru = u.KimyaDogru,
                                Yanlis = u.KimyaYanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("biyoloji", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.BiyolojiDogru - 0.25m * u.BiyolojiYanlis,
                                Dogru = u.BiyolojiDogru,
                                Yanlis = u.BiyolojiYanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                    }
                    
                    else
                    {
                        ayts = await query.Select(u => new
                        {
                            Id = u.Id.ToString(),
                            Tarih = u.CreatedDate,
                            Net = u.MatematikDogru - 0.25m * u.MatematikYanlis +
                                     u.FizikDogru - 0.25m * u.FizikYanlis +
                                     u.KimyaDogru - 0.25m * u.KimyaYanlis +
                                     u.BiyolojiDogru - 0.25m * u.BiyolojiYanlis,
                            MatematikNet = u.MatematikDogru - 0.25m * u.MatematikYanlis,
                            FizikNet = u.FizikDogru - 0.25m * u.FizikYanlis,
                            KimyaNet = u.KimyaDogru - 0.25m * u.KimyaYanlis,
                            BiyolojiNet = u.BiyolojiDogru - 0.25m * u.BiyolojiYanlis,

                        }).ToListAsync();
                        return new()
                        {
                            Ayts = ayts
                        };
                    }
                    break;
                case "esitagirlik":
                    if (!string.IsNullOrEmpty(dersAdi))
                    {
                        if (dersAdi.Equals("matematik", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.MatematikDogru - 0.25m * u.MatematikYanlis,
                                Dogru = u.MatematikDogru,
                                Yanlis = u.MatematikYanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("edebiyat", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.EdebiyatDogru - 0.25m * u.EdebiyatYanlis,
                                Dogru = u.EdebiyatDogru,
                                Yanlis = u.EdebiyatYanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("tarih1", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.Tarih1Dogru - 0.25m * u.Tarih1Yanlis,
                                Dogru = u.Tarih1Dogru,
                                Yanlis = u.Tarih1Yanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("cografya1", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await  query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.Cografya1Dogru - 0.25m * u.Cografya1Yanlis,
                                Dogru = u.Cografya1Dogru,
                                Yanlis = u.Cografya1Yanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                    }
                    
                    else
                    {
                        ayts = await query.Select(u => new
                        {
                            Id = u.Id.ToString(),
                            Tarih = u.CreatedDate,
                            Net = u.MatematikDogru - 0.25m * u.MatematikYanlis +
                                   u.EdebiyatDogru - 0.25m * u.EdebiyatYanlis +
                                   u.Cografya1Dogru - 0.25m * u.Cografya1Yanlis +
                                   u.Tarih1Dogru - 0.25m * u.Tarih1Yanlis,
                            EdebiyatNet = u.EdebiyatDogru - 0.25m * u.EdebiyatYanlis,
                            MatematikNet = u.MatematikDogru - 0.25m * u.MatematikYanlis,
                            Tarih1Net = u.Tarih1Dogru - 0.25m * u.Tarih1Yanlis,
                            Cografya1Net = u.Cografya1Dogru - 0.25m * u.Cografya1Yanlis
                        }).ToListAsync();
                        return new()
                        {
                            Ayts = ayts
                        };
                        

                    }
                    break;
                case "sozel":
                    if (!string.IsNullOrEmpty(dersAdi))
                    {
                        if (dersAdi.Equals("edebiyat", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.EdebiyatDogru - 0.25m * u.EdebiyatYanlis,
                                Dogru = u.EdebiyatDogru,
                                Yanlis = u.EdebiyatYanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("tarih1", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.Tarih1Dogru - 0.25m * u.Tarih1Yanlis,
                                Dogru = u.Tarih1Dogru,
                                Yanlis = u.Tarih1Yanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("cografya1", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.Cografya1Dogru - 0.25m * u.Cografya1Yanlis,
                                Dogru = u.Cografya1Dogru,
                                Yanlis = u.Cografya1Yanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("tarih2", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.Tarih2Dogru - 0.25m * u.Tarih2Yanlis,
                                Dogru = u.Tarih2Dogru,
                                Yanlis = u.Tarih2Yanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("cografya2", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.Cografya2Dogru - 0.25m * u.Cografya2Yanlis,
                                Dogru = u.Cografya2Dogru,
                                Yanlis = u.Cografya2Yanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("felsefe", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.FelsefeDogru - 0.25m * u.FelsefeYanlis,
                                Dogru = u.FelsefeDogru,
                                Yanlis = u.FelsefeYanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                        else if (dersAdi.Equals("din", StringComparison.OrdinalIgnoreCase))
                        {
                            ayts = await query.Select(u => new
                            {
                                Id = u.Id.ToString(),
                                Tarih = u.CreatedDate,
                                Net = u.DinDogru - 0.25m * u.DinYanlis,
                                Dogru = u.DinDogru,
                                Yanlis = u.DinYanlis
                            }).ToListAsync();
                            return new()
                            {
                                Ayts = ayts
                            };
                        }
                    }
                    
                    else
                    {

                        ayts = await query.Select(u => new
                        {
                            Id = u.Id.ToString(),
                            Tarih = u.CreatedDate,
                            Net = u.EdebiyatDogru - 0.25m * u.EdebiyatYanlis +
                                    u.Cografya1Dogru - 0.25m * u.Cografya1Yanlis +
                                    u.Tarih1Dogru - 0.25m * u.Tarih1Yanlis +
                                    u.Tarih2Dogru - 0.25m * u.Tarih2Yanlis +
                                    u.Cografya2Dogru - 0.25m * u.Cografya2Yanlis +
                                    u.DinDogru - 0.25m * u.DinYanlis +
                                    u.FelsefeDogru - 0.25m * u.FelsefeYanlis,
                            EdebiyatNet = u.EdebiyatDogru - 0.25m * u.EdebiyatYanlis,
                            Tarih1Net = u.Tarih1Dogru - 0.25m * u.Tarih1Yanlis,
                            Cografya1Net = u.Cografya1Dogru - 0.25m * u.Cografya1Yanlis,
                            Tarih2Net = u.Tarih2Dogru - 0.25m * u.Tarih2Yanlis,
                            Cografya2Net = u.Cografya2Dogru - 0.25m * u.Cografya2Yanlis,
                            DinNet = u.DinDogru - 0.25m * u.DinYanlis,
                            FelsefeNet = u.FelsefeDogru - 0.25m * u.FelsefeYanlis,
                        }).ToListAsync();
                        return new()
                        {
                            Ayts = ayts
                        };
                    }
                    break;
                case "dil":
                    ayts = await query.Select(u => new
                    {
                        Id = u.Id.ToString(),
                        Tarih = u.CreatedDate,
                        Net = u.DilDogru - 0.25m * u.DilYanlis,
                        Dogru = u.DilDogru,
                        Yanlis = u.DilYanlis
                    }).ToListAsync();
                    return new()
                    {
                        Ayts = ayts
                    };
                    break;
            };
            throw new Exception("Hatalı veri gönderimi yapıldı.");
        }
    }
    
}
