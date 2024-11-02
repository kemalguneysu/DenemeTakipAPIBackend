using DenemeTakipAPI.Application.Features.Queries.Roles.GetAllRoles;
using DenemeTakipAPI.Application.Features.Queries.Users.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DenemeTakipAPI.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DenemeTakipAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    [Authorize(Roles = "admin")]
    public class RolesController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly RoleManager<AppRole> _roleManager;

        public RolesController(IMediator meaditor, RoleManager<AppRole> roleManager)
        {
            _mediator = meaditor;
            _roleManager = roleManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoles()
        {
            var x = await _roleManager.FindByNameAsync("admin");
            return Ok(await _roleManager.Roles.Select(u => new
            {
                Id=u.Id.ToString(),
                Name=u.Name,
            }).ToListAsync());
        }
    }
}
