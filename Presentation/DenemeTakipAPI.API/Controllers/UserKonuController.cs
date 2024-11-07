using DenemeTakipAPI.Application.Features.Commands.Tyt.CreateTyt;
using DenemeTakipAPI.Application.Features.Commands.UserKonu.CreateOrUpdate;
using DenemeTakipAPI.Application.Features.Queries.UserKonu.GetUserKonular;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DenemeTakipAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class UserKonuController : ControllerBase
    {
        readonly IMediator _mediator;

        public UserKonuController(IMediator meaditor)
        {
            _mediator = meaditor;
        }

        [HttpPost("[action]")]

        public async Task<IActionResult> CreateUserKonu(CreateUserKonuCommandRequest createOrUpdateUserKonuCommandRequest)
        {
            CreateUserKonuCommandResponse response = await _mediator.Send(createOrUpdateUserKonuCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]

        public async Task<IActionResult> GetUserKonular([FromQuery]GetUserKonularQueryRequest getUserKonularQueryRequest)
        {
            GetUserKonularQueryResponse response = await _mediator.Send(getUserKonularQueryRequest);
            return Ok(response);
        }
    }
}
