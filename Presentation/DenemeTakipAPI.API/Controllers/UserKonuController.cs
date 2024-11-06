using DenemeTakipAPI.Application.Features.Commands.Tyt.CreateTyt;
using DenemeTakipAPI.Application.Features.Commands.UserKonu.CreateOrUpdate;
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

        public async Task<IActionResult> CreateOrUpdateUserKonu(CreateOrUpdateUserKonuCommandRequest createOrUpdateUserKonuCommandRequest)
        {
            CreateOrUpdateUserKonuCommandResponse response = await _mediator.Send(createOrUpdateUserKonuCommandRequest);
            return Ok(response);
        }
    }
}
