using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Application.Repositories.KonuRepository;
using DenemeTakipAPI.Application.Repositories.TytRepository;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using DenemeTakipAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DenemeTakipAPI.Application.Repositories.DersRepository;
using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.Ayt;
using Microsoft.AspNetCore.SignalR;
using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Persistence.Repositories.DersRepository;
using System.Linq.Expressions;
using System.Drawing;

namespace DenemeTakipAPI.Persistence.Services
{
    public class TytService:ITytService
    {
        readonly ITytReadRepository _tytReadRepository;
        readonly ITytWriteRepository _tytWriteRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IKonuReadRepository _konuReadRepository;
        readonly IDersReadRepository _dersReadRepository;
        readonly ITytHubService _tytHubService;

        public TytService(ITytReadRepository tytReadRepository, ITytWriteRepository tytWriteRepository, IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager, IKonuReadRepository konuReadRepository, IDersReadRepository dersReadRepository, ITytHubService tytHubService)
        {
            _tytReadRepository = tytReadRepository;
            _tytWriteRepository = tytWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _konuReadRepository = konuReadRepository;
            _dersReadRepository = dersReadRepository;
            _tytHubService = tytHubService;
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
        public async Task CreateTytAsync(CreateTyt createTyt)
        {
            var user= await ContextUser();
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");
            var yanlisKonular = new List<Konu>();
            foreach (var yanlisKonuId in createTyt.YanlisKonularId)
            {
                if (string.IsNullOrEmpty(yanlisKonuId))
                    continue;
                var konu = await _konuReadRepository.GetByIdAsync(yanlisKonuId);
                if (konu == null)
                    continue;
                yanlisKonular.Add(konu);
            }

            var bosKonular = new List<Konu>();
            foreach (var bosKonuId in createTyt.BosKonularId)
            {
                if (string.IsNullOrEmpty(bosKonuId))
                    continue;
                var konu = await _konuReadRepository.GetByIdAsync(bosKonuId);
                if (konu == null)
                    continue;
                bosKonular.Add(konu);
            }
            
            await _tytWriteRepository.AddAsync(new()
            {
                TurkceDogru=createTyt.TurkceDogru,
                TurkceYanlis=createTyt.TurkceYanlis,

                MatematikDogru = createTyt.MatematikDogru,
                MatematikYanlis = createTyt.MatematikYanlis,

                FenDogru = createTyt.FenDogru,
                FenYanlis = createTyt.FenYanlis,

                SosyalDogru = createTyt.SosyalDogru,
                SosyalYanlis = createTyt.SosyalYanlis,

                User = user,
                BosKonular = bosKonular,
                YanlisKonular=yanlisKonular
            });
            await _tytHubService.TytAddedMessage(user.Id, "TYT denemesi başarıyla eklendi.");
            await _tytWriteRepository.SaveAsync();
        }

        public async Task<ListTyt> Get(int page, int size,List<string>? orderByAndDirections)
        {
            var user= await ContextUser();
            if(user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            var query = _tytReadRepository.GetWhere(u => u.User == user).AsQueryable();

            if (orderByAndDirections != null && orderByAndDirections.Any())
            {
                IOrderedQueryable<TytDeneme> orderedQuery = null; // Sıralanmış sorgu için

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
                        case "toplamnet":
                            // İlk sıralamayı yap
                            orderedQuery = orderDirection == "asc" ?
                                query.OrderBy(s => s.TurkceDogru - 0.25m * s.TurkceYanlis +
                                                    s.MatematikDogru - 0.25m * s.MatematikYanlis +
                                                    s.SosyalDogru - 0.25m * s.SosyalYanlis +
                                                    s.FenDogru - 0.25m * s.FenYanlis) :
                                query.OrderByDescending(s => s.TurkceDogru - 0.25m * s.TurkceYanlis +
                                                              s.MatematikDogru - 0.25m * s.MatematikYanlis +
                                                              s.SosyalDogru - 0.25m * s.SosyalYanlis +
                                                              s.FenDogru - 0.25m * s.FenYanlis);
                            break;

                        // Diğer durumlar için benzer şekilde
                        case "turkcenet":
                            orderedQuery = orderDirection == "asc" ?
                                query.OrderBy(u => u.TurkceDogru - 0.25m * u.TurkceYanlis) :
                                query.OrderByDescending(u => u.TurkceDogru - 0.25m * u.TurkceYanlis);
                            break;

                        case "matematiknet":
                            orderedQuery = orderDirection == "asc" ?
                                query.OrderBy(u => u.MatematikDogru - 0.25m * u.MatematikYanlis) :
                                query.OrderByDescending(u => u.MatematikDogru - 0.25m * u.MatematikYanlis);
                            break;

                        case "fennet":
                            orderedQuery = orderDirection == "asc" ?
                                query.OrderBy(u => u.FenDogru - 0.25m * u.FenYanlis) :
                                query.OrderByDescending(u => u.FenDogru - 0.25m * u.FenYanlis);
                            break;

                        case "sosyalnet":
                            orderedQuery = orderDirection == "asc" ?
                                query.OrderBy(u => u.SosyalDogru - 0.25m * u.SosyalYanlis) :
                                query.OrderByDescending(u => u.SosyalDogru - 0.25m * u.SosyalYanlis);
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
            if (orderByAndDirections.Count == 0)
                query = query.OrderByDescending(u => u.CreatedDate);
            var totalCount = await query.CountAsync();
            var tytDenemes= await query.Skip((page - 1) * size).Take(size)
                .Select(s => new
                {
                    Id = s.Id.ToString(),
                    TurkceNet = s.TurkceDogru - 0.25m * s.TurkceYanlis,
                    MatematikNet = s.MatematikDogru - 0.25m * s.MatematikYanlis,
                    SosyalNet = s.SosyalDogru - 0.25m * s.SosyalYanlis,
                    FenNet = s.FenDogru - 0.25m * s.FenYanlis,

                    ToplamNet = s.TurkceDogru - 0.25m * s.TurkceYanlis +
                                s.MatematikDogru - 0.25m * s.MatematikYanlis +
                                s.SosyalDogru - 0.25m * s.SosyalYanlis +
                                s.FenDogru - 0.25m * s.FenYanlis,

                    Tarih = s.CreatedDate.ToString(),
                }).ToListAsync();
            return new()
            {
                TotalTyt = totalCount,
                Tyts = tytDenemes
            };
        }

        public async Task<ListSingleTyt> GetTytById(string id)
        {
            var user = await ContextUser();
            if(user==null)
                throw new Exception("Kullanıcı bulunamadı.");
            var userRoles = await _userManager.GetRolesAsync(user);
            TytDeneme tyt;
            List<KonularAdDers> yanlisKonular = new List<KonularAdDers>();
            List<KonularAdDers> bosKonular = new List<KonularAdDers>();
            if (userRoles.Contains("admin"))
            {
                tyt=await _tytReadRepository.GetWhere(u=>u.Id.ToString()==id)
                        .Include(u=>u.BosKonular).ThenInclude(u=>u.Ders).Include(u=>u.YanlisKonular).ThenInclude(u => u.Ders).SingleOrDefaultAsync();
                if (tyt == null)
                    throw new Exception("TYT denemesi bulunamadı.");
                ListSingleTyt singleListTyt = new ListSingleTyt
                {
                    Id=tyt.Id.ToString(),
                    TurkceDogru = tyt.TurkceDogru,
                    TurkceYanlis = tyt.TurkceYanlis,
                    MatematikDogru = tyt.MatematikDogru,
                    MatematikYanlis = tyt.MatematikYanlis,
                    FenDogru = tyt.FenDogru,
                    FenYanlis = tyt.FenYanlis,
                    SosyalDogru = tyt.SosyalDogru,
                    SosyalYanlis = tyt.SosyalYanlis,
                    YanlisKonularAd= tyt.YanlisKonular.Select(konu=>new KonularAdDers
                    {
                        KonuAdi = konu.KonuAdi,
                        DersAdi = konu.Ders.DersAdi,
                        DersId = konu.Ders.Id.ToString(),
                        KonuId = konu.Id.ToString()

                    }).ToList(),
                    BosKonularAd = tyt.BosKonular.Select(konu => new KonularAdDers
                    {
                        KonuAdi = konu.KonuAdi,
                        DersAdi = konu.Ders.DersAdi,
                        DersId = konu.Ders.Id.ToString(),
                        KonuId = konu.Id.ToString()
                    }).ToList()
                };
                return singleListTyt;
            }
            else
            {
                tyt=await _tytReadRepository.GetWhere(u=>u.User==user)
                    .Include(u => u.YanlisKonular).ThenInclude(u => u.Ders).Include(u => u.BosKonular).ThenInclude(u => u.Ders).FirstOrDefaultAsync(u => u.Id.ToString() == id);
                if (tyt == null)
                    throw new Exception("TYT denemesi bulunamadı.");
                ListSingleTyt singleListTyt = new ListSingleTyt
                {
                    Id = tyt.Id.ToString(),
                    TurkceDogru = tyt.TurkceDogru,
                    TurkceYanlis = tyt.TurkceYanlis,
                    MatematikDogru = tyt.MatematikDogru,
                    MatematikYanlis = tyt.MatematikYanlis,
                    FenDogru = tyt.FenDogru,
                    FenYanlis = tyt.FenYanlis,
                    SosyalDogru = tyt.SosyalDogru,
                    SosyalYanlis = tyt.SosyalYanlis,
                    YanlisKonularAd = tyt.YanlisKonular.Select(konu => new KonularAdDers
                    {
                        KonuAdi = konu.KonuAdi,
                        DersAdi = konu.Ders.DersAdi,
                        DersId = konu.Ders.Id.ToString(),
                        KonuId = konu.Id.ToString()

                    }).ToList(),
                    BosKonularAd = tyt.BosKonular.Select(konu => new KonularAdDers
                    {
                        KonuAdi = konu.KonuAdi,
                        DersAdi = konu.Ders.DersAdi,
                        DersId = konu.Ders.Id.ToString(),
                        KonuId = konu.Id.ToString()
                    }).ToList()
                };
                return singleListTyt;
            }
        }

        public async Task UpdateTytAsync(UpdateTyt updateTyt)
        {
            var yanlisKonular = new List<Konu>();
            foreach (var yanlisKonularId in updateTyt.YanlisKonular)
            {
                var konu = await _konuReadRepository.GetByIdAsync(yanlisKonularId);
                if (konu == null)
                    continue;
                yanlisKonular.Add(konu);
            }
            var bosKonular=new List<Konu>();
            foreach (var bosKonularId in updateTyt.BosKonular)
            {
                var konu = await _konuReadRepository.GetByIdAsync(bosKonularId);
                if (konu == null)
                    continue;
                bosKonular.Add(konu);
            }
            var user = await ContextUser();
            var tyt = await _tytReadRepository.GetAll().Where(u => u.User == user)
                .Include(u=>u.BosKonular).Include(u=>u.YanlisKonular).FirstOrDefaultAsync(u => u.Id.ToString() == updateTyt.TytId);
            if (tyt == null)
                throw new Exception("Tyt denemesi bulunamadı");
            

            tyt.TurkceDogru=updateTyt.TurkceDogru;
            tyt.TurkceYanlis = updateTyt.TurkceYanlis;

            tyt.MatematikDogru = updateTyt.MatematikDogru;
            tyt.MatematikYanlis = updateTyt.MatematikYanlis;

            tyt.SosyalDogru = updateTyt.SosyalDogru;
            tyt.SosyalYanlis = updateTyt.SosyalYanlis;

            tyt.FenDogru = updateTyt.FenDogru;
            tyt.FenYanlis = updateTyt.FenYanlis;

            tyt.BosKonular = bosKonular;
            tyt.YanlisKonular = yanlisKonular;
            await _tytHubService.TytUpdatedMessage(user.Id, "TYT denemesi başarıyla güncellendi.");
            await _tytWriteRepository.SaveAsync();
        }

        public async Task<DeleteTytResponse> DeleteTyt(List<string> ids)
        {
            var user = await ContextUser();
            var userRoles= await _userManager.GetRolesAsync(user);
            List<TytDeneme> tyts = new List<TytDeneme>();
            if (userRoles.Contains("admin"))
            {
                tyts = await _tytReadRepository.GetWhere(u => ids.Contains(u.Id.ToString())).ToListAsync();
            }
            else
            {
                tyts=await _tytReadRepository.GetWhere(u => u.User == user && ids.Contains(u.Id.ToString())).ToListAsync();
            }
            if(tyts!=null && tyts.Any()){
                 _tytWriteRepository.RemoveRange(tyts);
                await _tytWriteRepository.SaveAsync();
                await _tytHubService.TytDeletedMessage(user.Id,"Seçilen TYT denemeleri başarıyla silinmiştir.");
                return new()
                {
                    Succeeded = true,
                    Message = "Seçilen TYT denemeleri başarıyla silinmiştir."
                };
            }
            else
            {
                return new()
                {
                    Succeeded = false,
                    Message = "Size ait olmayan TYT denemelerini silemezsiniz."
                };
            }
        }


        public async Task<List<DenemeAnaliz>> GetTytAnaliz(int DenemeSayısı, int KonuSayısı, string DersId,string type)
        {
            var user = await ContextUser();
            List<TytDeneme> tyts;
            var tytAnalizSayilar = new List<DenemeAnaliz>();

            IEnumerable<Konu> konular = Enumerable.Empty<Konu>();

            var tytQuery = _tytReadRepository.GetWhere(u => u.User == user)
                .OrderByDescending(u => u.CreatedDate)
                .Take(DenemeSayısı)
                .AsQueryable();

            if (type.Equals("yanlis", StringComparison.OrdinalIgnoreCase))
            {
                tyts = await tytQuery.Include(u => u.YanlisKonular).ThenInclude(u => u.Ders).ToListAsync();
                if (!tyts.Any())
                    return new List<DenemeAnaliz>();
                konular = tyts.SelectMany(u => u.YanlisKonular);
            }
            else if (type.Equals("bos", StringComparison.OrdinalIgnoreCase))
            {
                tyts = await tytQuery.Include(u => u.BosKonular).ThenInclude(u => u.Ders).ToListAsync();
                if (!tyts.Any())
                    return new List<DenemeAnaliz>();
                konular = tyts.SelectMany(u => u.BosKonular);
            }
            else
            {
                throw new Exception($"Geçersiz tür değeri.");
            }

            if (!konular.Any())
            {
                return new List<DenemeAnaliz>();
            }

            var tytAnaliz = konular
                .Where(u => u.Ders.Id.ToString() == DersId)
                .GroupBy(u => u.Id)
                .OrderByDescending(g => g.Count())
                .Take(KonuSayısı)
                .ToList();

            if (!tytAnaliz.Any())
            {
                return new List<DenemeAnaliz>();
            }

            var konuIds = tytAnaliz.Select(g => g.Key).ToList();
            var konuBilgileri = await _konuReadRepository.GetWhere(k => konuIds.Contains(k.Id))
                .Include(k => k.Ders)
                .ToDictionaryAsync(k => k.Id);

            tytAnalizSayilar = tytAnaliz.Select(item => new DenemeAnaliz
            {
                KonuId = item.Key.ToString(),
                DersId = konuBilgileri[item.Key].Ders.Id.ToString(),
                KonuAdi = konuBilgileri[item.Key].KonuAdi,
                Sayi = item.Count()
            }).ToList();

            return tytAnalizSayilar;
        }

        public async Task<ListTytAnaliz> GetTytNetAnaliz(int denemeSayisi, string? dersAdi)
        {
            var user = await ContextUser();
            var query = _tytReadRepository.GetWhere(u => u.User == user).OrderByDescending(u=>u.CreatedDate).Take(denemeSayisi).AsQueryable();
            object tyts;
            if(dersAdi!=null)
                dersAdi = dersAdi.ToLower();
            switch (dersAdi)
            {
                case "turkce":
                    tyts = query.Select(u => new
                    {
                        Net = u.TurkceDogru - 0.25m * u.TurkceYanlis,
                        Dogru = u.TurkceDogru,
                        Yanlis = u.TurkceYanlis
                    });
                    return new()
                    {
                        Tyts = tyts
                    };
                    break;
                case "matematik":
                    tyts = query.Select(u => new
                    {
                        Net = u.MatematikDogru - 0.25m * u.MatematikYanlis,
                        Dogru = u.MatematikDogru,
                        Yanlis = u.MatematikYanlis
                    });
                    return new()
                    {
                        Tyts = tyts
                    };
                    break;
                case "fen":
                    tyts = query.Select(u => new
                    {
                        Net = u.FenDogru - 0.25m * u.FenYanlis,
                        Dogru = u.FenDogru,
                        Yanlis = u.FenYanlis
                    });
                    return new()
                    {
                        Tyts = tyts
                    };
                    break;
                case "sosyal":
                    tyts = query.Select(u => new
                    {
                        Net = u.SosyalDogru - 0.25m * u.SosyalYanlis,
                        Dogru = u.SosyalDogru,
                        Yanlis = u.SosyalYanlis
                    });
                    return new()
                    {
                        Tyts = tyts
                    };
                    break;
                default:
                    tyts = query.Select(u => new
                    {
                        Net = u.TurkceDogru - 0.25m * u.TurkceYanlis +
                                u.MatematikDogru - 0.25m * u.MatematikYanlis +
                                u.SosyalDogru - 0.25m * u.SosyalYanlis +
                                u.FenDogru - 0.25m * u.FenYanlis,
                        TurkceNet = u.TurkceDogru - 0.25m * u.TurkceYanlis,
                        MatematikNet = u.MatematikDogru - 0.25m * u.MatematikYanlis,
                        SosyalNet = u.SosyalDogru - 0.25m * u.SosyalYanlis,
                        FenNet = u.FenDogru - 0.25m * u.FenYanlis,
                    });
                    return new()
                    {
                        Tyts = tyts
                    };

            }
            throw new Exception("Hatalı veri gönderimi yapıldı.");
        }
    }
}
