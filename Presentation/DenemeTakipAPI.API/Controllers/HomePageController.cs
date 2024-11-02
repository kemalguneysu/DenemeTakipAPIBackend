using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.Features.Commands.Konu.CreateKonu;
using DenemeTakipAPI.Application.Repositories.AytRepository;
using DenemeTakipAPI.Application.Repositories.TytRepository;
using DenemeTakipAPI.Domain.Entities.Identity;
using DenemeTakipAPI.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenemeTakipAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class HomePageController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ITytReadRepository _tytReadRepository;
        readonly IAytReadRepository _aytReadRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;

        public HomePageController(IMediator mediator, ITytReadRepository tytReadRepository, IAytReadRepository aytReadRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _tytReadRepository = tytReadRepository;
            _aytReadRepository = aytReadRepository;
            _httpContextAccessor = httpContextAccessor;
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastAytDeneme()
        {
            var user = await ContextUser();
            var lastAyt= await _aytReadRepository.GetWhere(u=>u.User==user)
                .OrderByDescending(u => u.CreatedDate).Select(u => new
                {
                    Id=u.Id.ToString(),
                    CreatedDate=u.CreatedDate,
                    MatematikDogru=u.MatematikDogru,
                    MatematikYanlis=u.MatematikYanlis,
                    FizikDogru = u.FizikDogru,
                    FizikYanlis = u.FizikYanlis,
                    BiyolojiDogru = u.BiyolojiDogru,
                    BiyolojiYanlis = u.BiyolojiYanlis,
                    KimyaDogru = u.KimyaDogru,
                    KimyaYanlis = u.KimyaYanlis,
                    EdebiyatDogru = u.EdebiyatDogru,
                    EdebiyatYanlis = u.EdebiyatYanlis,
                    Tarih1Dogru = u.Tarih1Dogru,
                    Tarih1Yanlis = u.Tarih1Yanlis,
                    Cografya1Dogru = u.Cografya1Dogru,
                    Cografya1Yanlis = u.Cografya1Yanlis,
                    Tarih2Dogru = u.Tarih2Dogru,
                    Tarih2Yanlis = u.Tarih2Yanlis,
                    Cografya2Dogru = u.Cografya2Dogru,
                    Cografya2Yanlis = u.Cografya2Yanlis,
                    FelsefeDogru = u.FelsefeDogru,
                    FelsefeYanlis = u.FelsefeYanlis,
                    DinDogru = u.DinDogru,
                    DinYanlis = u.DinYanlis,
                    DilDogru = u.DilDogru,
                    DilYanlis = u.DilYanlis,
                    SayisalNet = u.FizikDogru - 0.25m * u.FizikYanlis +
                                    u.MatematikDogru - 0.25m * u.MatematikYanlis +
                                    u.KimyaDogru - 0.25m * u.KimyaYanlis +
                                    u.BiyolojiDogru - 0.25m * u.BiyolojiYanlis,
                    EsitAgirlikNet = u.EdebiyatDogru - 0.25m * u.EdebiyatYanlis +
                                        u.MatematikDogru - 0.25m * u.MatematikYanlis +
                                        u.Tarih1Dogru - 0.25m * u.Tarih1Yanlis +
                                        u.Cografya1Dogru - 0.25m * u.Cografya1Yanlis,
                    SozelNet = u.EdebiyatDogru - 0.25m * u.EdebiyatYanlis +
                                u.Tarih1Dogru - 0.25m * u.Tarih1Yanlis +
                                u.Cografya1Dogru - 0.25m * u.Cografya1Yanlis +
                                u.Tarih2Dogru - 0.25m * u.Tarih2Yanlis +
                                u.Cografya2Dogru - 0.25m * u.Cografya2Yanlis +
                                u.FelsefeDogru - 0.25m * u.FelsefeYanlis +
                                u.DinDogru - 0.25m * u.DinYanlis,
                    DilNet = u.DilDogru - 0.25m * u.DilYanlis,
                }).FirstOrDefaultAsync();
            if (lastAyt == null)
                return Ok(null);
            return Ok(lastAyt);
            
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastTytDeneme()
        {
            var user = await ContextUser();
            var lastTyt = await _tytReadRepository.GetWhere(u => u.User == user)
                .OrderByDescending(u => u.CreatedDate).Select(u => new
                {
                    Id = u.Id.ToString(),
                    CreatedDate = u.CreatedDate,
                    TurkceDogru=u.TurkceDogru,
                    TurkceYanlis = u.TurkceYanlis,
                    MatematikDogru = u.MatematikDogru,
                    MatematikYanlis = u.MatematikYanlis,
                    FenDogru = u.FenDogru,
                    FenYanlis = u.FenYanlis,
                    SosyalDogru = u.SosyalDogru,
                    SosyalYanlis = u.SosyalYanlis,
                    ToplamNet = u.TurkceDogru - 0.25m * u.TurkceYanlis +
                                u.MatematikDogru - 0.25m * u.MatematikYanlis +
                                u.SosyalDogru - 0.25m * u.SosyalYanlis +
                                u.FenDogru - 0.25m * u.FenYanlis,
                }).FirstOrDefaultAsync();
            if (lastTyt == null)
                return Ok(null);
            return Ok(lastTyt);
        }
    }
}
