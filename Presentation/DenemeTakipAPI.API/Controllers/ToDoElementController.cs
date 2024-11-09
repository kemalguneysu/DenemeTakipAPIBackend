using DenemeTakipAPI.Application.Features.Commands.ToDoElement.CreateToDoElement;
using DenemeTakipAPI.Application.Features.Commands.ToDoElement.DeleteToDoElement;
using DenemeTakipAPI.Application.Features.Commands.ToDoElement.UpdateToDoElement;
using DenemeTakipAPI.Application.Features.Commands.UserKonu.CreateOrUpdate;
using DenemeTakipAPI.Application.Features.Queries.ToDoElement.GetToDoElement;
using DenemeTakipAPI.Application.Features.Queries.ToDoElement.GetToDoElements;
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

    public class ToDoElementController : ControllerBase
    {
        readonly IMediator _mediator;

        public ToDoElementController(IMediator meaditor)
        {
            _mediator = meaditor;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDoElement([FromQuery] GetToDoElementQueryRequest getToDoElementQueryRequest)
        {
            GetToDoElementQueryResponse response = await _mediator.Send(getToDoElementQueryRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]

        public async Task<IActionResult> CreateToDoElement(CreateToDoElementCommandRequest createOrUpdateToDoElementCommandRequest)
        {
            CreateToDoElementCommandResponse response = await _mediator.Send(createOrUpdateToDoElementCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> UpdateToDoElement(UpdateToDoElementCommandRequest UpdateToDoElementCommandRequest)
        {
            UpdateToDoElementCommandResponse response = await _mediator.Send(UpdateToDoElementCommandRequest);
            return Ok(response);
        }
        [HttpDelete("[action]")]

        public async Task<IActionResult> DeleteToDoElement(DeleteToDoElementCommandRequest DeleteToDoElementCommandRequest)
        {
            DeleteToDoElementCommandResponse response = await _mediator.Send(DeleteToDoElementCommandRequest);
            return Ok(response);
        }

    }
}
