using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.Features.Commands.User.AssignRoleToUser;
using DenemeTakipAPI.Application.Features.Commands.User.CreateUserer;
using DenemeTakipAPI.Application.Features.Commands.User.UpdatePassword;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonu;
using DenemeTakipAPI.Application.Features.Queries.Users.GetAllUsers;
using DenemeTakipAPI.Application.Features.Queries.Users.GetUserById;
using DenemeTakipAPI.Application.Features.Queries.Users.GetUserRoles;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DenemeTakipAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService _mailService;

        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetUserRoles([FromQuery] GetUserRolesQueryRequest getUserRolesQueryRequest)
        {
            GetUserRolesQueryResponse response = await _mediator.Send(getUserRolesQueryRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignRolesToUser(AssignRolesToUserCommandRequest assignRolesToUserCommandRequest)
        {
            AssignRolesToUserCommandResponse response = await _mediator.Send(assignRolesToUserCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdQueryRequest GetUserByIdQueryRequest)
        {
            GetUserByIdQueryResponse response = await _mediator.Send(GetUserByIdQueryRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }

    }
}
