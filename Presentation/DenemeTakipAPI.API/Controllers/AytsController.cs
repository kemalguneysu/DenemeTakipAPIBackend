using DenemeTakipAPI.Application.Features.Commands.Ayt.CreateAyt;
using DenemeTakipAPI.Application.Features.Commands.Ayt.DeleteAyt;
using DenemeTakipAPI.Application.Features.Commands.Ayt.UpdateAyt;
using DenemeTakipAPI.Application.Features.Queries.Ayt.AytAnaliz;
using DenemeTakipAPI.Application.Features.Queries.Ayt.GetAllAyt;
using DenemeTakipAPI.Application.Features.Queries.Ayt.GetAytById;
using DenemeTakipAPI.Application.Features.Queries.Tyt.GetAllTyt;
using DenemeTakipAPI.Application.Features.Queries.Tyt.TytNetAnaliz;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DenemeTakipAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class AytsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AytsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> CreateAyt(CreateAytCommandRequest createAytCommandRequest)
        {
            CreateAytCommandResponse response = await _mediator.Send(createAytCommandRequest);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAyt([FromQuery] GetAllAytQueryRequest getAllAytQueryRequest)
        {
            GetAllAytQueryResponse response = await _mediator.Send(getAllAytQueryRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAyt(UpdateAytCommandRequest updateAytCommandRequest)
        {
            UpdateAytCommandResponse updateAytCommandResponse = await _mediator.Send(updateAytCommandRequest);
            return Ok(updateAytCommandResponse);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAytById([FromQuery]GetAytByIdQueryRequest getAytByIdQueryRequest)
        {
            GetAytByIdQueryResponse getAytByIdQueryResponse = await _mediator.Send(getAytByIdQueryRequest);
            return Ok(getAytByIdQueryResponse);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAyt(DeleteAytCommandRequest deleteAytCommandRequest)
        {
            DeleteAytCommandResponse deleteAytCommandResponse=await _mediator.Send(deleteAytCommandRequest);
            return Ok(deleteAytCommandResponse);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> AytAnaliz([FromQuery] AytAnalizQueryRequest aytAnalizQueryRequest)
        {
            AytAnalizQueryResponse response = await _mediator.Send(aytAnalizQueryRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> AytNetAnaliz([FromQuery] AytNetAnalizQueryRequest aytNetAnalizQueryRequest)
        {
            AytNetAnalizQueryResponse response = await _mediator.Send(aytNetAnalizQueryRequest);
            return Ok(response);
        }

    }
}
