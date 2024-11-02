using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Application.Features.Commands.Ayt.DeleteAyt;
using DenemeTakipAPI.Application.Features.Commands.Tyt.CreateTyt;
using DenemeTakipAPI.Application.Features.Commands.Tyt.DeleteTyt;
using DenemeTakipAPI.Application.Features.Commands.Tyt.UpdateTyt;
using DenemeTakipAPI.Application.Features.Queries.Tyt.GetAllTyt;
using DenemeTakipAPI.Application.Features.Queries.Tyt.GetTytById;
using DenemeTakipAPI.Application.Features.Queries.Tyt.TytAnaliz;
using DenemeTakipAPI.Application.Features.Queries.Tyt.TytNetAnaliz;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DenemeTakipAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class TytsController : ControllerBase
    {
        readonly IMediator _mediator;

        public TytsController(IMediator meaditor)
        {
            _mediator = meaditor;
        }

        [HttpPost("[action]")]

        public async Task<IActionResult> CreateTyt(CreateTytCommandRequest createTytCommandRequest)
        {
            CreateTytCommandResponse response = await _mediator.Send(createTytCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]

        public async Task<IActionResult> GetAllTyt([FromQuery] GetAllTytQueryRequest getAllTytQueryRequest)
        {
            GetAllTytQueryResponse response = await _mediator.Send(getAllTytQueryRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> UpdateTyt(UpdateTytCommandRequest updateTytCommandRequest)
        {
            UpdateTytCommandResponse updateTytCommandResponse = await _mediator.Send(updateTytCommandRequest);
            return Ok(updateTytCommandResponse);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTytById([FromQuery] GetTytByIdQueryRequest getTytByIdQueryRequest)
        {
            GetTytByIdQueryResponse getTytByIdQueryResponse = await _mediator.Send(getTytByIdQueryRequest);
            return Ok(getTytByIdQueryResponse);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteTyt(DeleteTytCommandRequest deleteTytCommandRequest)
        {
            DeleteTytCommandResponse deleteTytCommandResponse = await _mediator.Send(deleteTytCommandRequest);
            return Ok(deleteTytCommandResponse);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> TytAnaliz([FromQuery] TytAnalizQueryRequest tytAnalizQueryRequest)
        {
            TytAnalizQueryResponse response = await _mediator.Send(tytAnalizQueryRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> TytNetAnaliz([FromQuery] TytNetAnalizQueryRequest tytNetAnalizQueryRequest)
        {
            TytNetAnalizQueryResponse response = await _mediator.Send(tytNetAnalizQueryRequest);
            return Ok(response);
        }

    }
}
