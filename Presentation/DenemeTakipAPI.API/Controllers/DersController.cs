using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.Features.Commands.Ders.CreateKonu;
using DenemeTakipAPI.Application.Features.Commands.Ders.DeleteDers;
using DenemeTakipAPI.Application.Features.Commands.Ders.UpdateDers;
using DenemeTakipAPI.Application.Features.Commands.Konu.CreateKonu;
using DenemeTakipAPI.Application.Features.Queries.Ders.GetAllAytDersler;
using DenemeTakipAPI.Application.Features.Queries.Ders.GetAllDers;
using DenemeTakipAPI.Application.Features.Queries.Ders.GetAllTytDersler;
using DenemeTakipAPI.Application.Features.Queries.Ders.GetDersById;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetAllTytKonular;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetKonuById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DenemeTakipAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IDersService _dersService;
        public DersController(IMediator mediator, IDersService dersService)
        {
            _mediator = mediator;
            _dersService = dersService;
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateDers(CreateDersCommandRequest createDersCommandRequest)
        {
            CreateDersCommandResponse response = await _mediator.Send(createDersCommandRequest);
            return Ok(response);
        }
        [HttpDelete("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteDers(DeleteDersCommandRequest deleteDersCommandRequest)
        {
            DeleteDersCommandResponse response = await _mediator.Send(deleteDersCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDers([FromQuery] GetAllDersQueryRequest getAllDersQueryRequest)
        {
            GetAllDersQueryResponse response = await _mediator.Send(getAllDersQueryRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllDersler([FromQuery] int? page, [FromQuery] int? size,[FromQuery] bool? isTyt = null, [FromQuery] string? dersAdi = null)
        {
            return Ok(await _dersService.GetAllDersler(isTyt,dersAdi,page,size));
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateDers(UpdateDersCommandRequest updateDersCommandRequest)
        {
            UpdateDersCommandResponse response=await _mediator.Send(updateDersCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllTytDersler([FromQuery] GetAllTytDerslerQueryRequest getAllTytDerslerQueryRequest)
        {
            GetAllTytDerslerQueryResponse getAllTytDerslerQueryResponse = await _mediator.Send(getAllTytDerslerQueryRequest);
            return Ok(getAllTytDerslerQueryResponse);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAytDersler([FromQuery] GetAllAytDerslerQueryRequest getAllAytDerslerQueryRequest)
        {
            GetAllAytDerslerQueryResponse getAllAytDerslerQueryResponse = await _mediator.Send(getAllAytDerslerQueryRequest);
            return Ok(getAllAytDerslerQueryResponse);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetDersById([FromQuery] GetDersByIdQueryRequest getDersByIdQueryRequest)
        {
            GetDersByIdQueryResponse getDersByIdQueryResponse = await _mediator.Send(getDersByIdQueryRequest);
            return Ok(getDersByIdQueryResponse);
        }
        

    }
}
