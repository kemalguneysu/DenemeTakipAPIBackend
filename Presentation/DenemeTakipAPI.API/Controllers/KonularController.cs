using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.Features.Commands.Konu.CreateKonu;
using DenemeTakipAPI.Application.Features.Commands.Konu.DeleteKonu;
using DenemeTakipAPI.Application.Features.Commands.Konu.UpdateKonu;
using DenemeTakipAPI.Application.Features.Queries.Ayt.GetAytById;
using DenemeTakipAPI.Application.Features.Queries.Ders.GetAllDers;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetAllAytKonular;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonu;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonularPaginated;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetAllTytKonular;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetKonuById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DenemeTakipAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KonularController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IKonuService _konuService;

        public KonularController(IMediator mediator, IKonuService konuService = null)
        {
            _mediator = mediator;
            _konuService = konuService;
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateKonu(CreateKonuCommandRequest createKonuCommandRequest)
        {
            CreateKonuCommandResponse response= await _mediator.Send(createKonuCommandRequest);
            return Ok(response);
        }
        [HttpDelete("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteKonu( DeleteKonuCommandRequest deleteKonuCommandRequest)
        {
            DeleteKonuCommandResponse response = await _mediator.Send(deleteKonuCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllKonular([FromQuery] GetAllKonuQueryRequest getAllKonuQueryRequest)
        {
            GetAllKonuQueryResponse response = await _mediator.Send(getAllKonuQueryRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateKonu(UpdateKonuCommandRequest updateKonuCommandRequest)
        {
            UpdateKonuCommandResponse response = await _mediator.Send(updateKonuCommandRequest);
            return Ok(response);
        }
        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetAllKonular()
        //{
        //    return Ok(await _konuService.GetAllKonular());
        //}
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllKonular([FromQuery] GetAllKonularPaginatedQueryRequest getAllKonularPaginatedQueryRequest)
        {
            GetAllKonularPaginatedQueryResponse getAllTytKonularQueryResponse = await _mediator.Send(getAllKonularPaginatedQueryRequest);
            return Ok(getAllTytKonularQueryResponse);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllTytKonular([FromQuery] GetAllTytKonularQueryRequest getAllTytKonularQueryRequest)
        {
            GetAllTytKonularQueryResponse getAllTytKonularQueryResponse = await _mediator.Send(getAllTytKonularQueryRequest);
            return Ok(getAllTytKonularQueryResponse);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAytKonular([FromQuery] GetAllAytKonularQueryRequest getAllAytKonularQueryRequest)
        {
            GetAllAytKonularQueryResponse getAllAytKonularQueryResponse = await _mediator.Send(getAllAytKonularQueryRequest);
            return Ok(getAllAytKonularQueryResponse);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetKonuById([FromQuery] GetKonuByIdQueryRequest getKonuByIdQueryRequest)
        {
            GetKonuByIdQueryResponse getKonuByIdQueryResponse = await _mediator.Send(getKonuByIdQueryRequest);
            return Ok(getKonuByIdQueryResponse);
        }
    }
}
