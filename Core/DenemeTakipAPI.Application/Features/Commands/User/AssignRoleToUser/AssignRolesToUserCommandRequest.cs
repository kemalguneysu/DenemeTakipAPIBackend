using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.User.AssignRoleToUser
{
    public class AssignRolesToUserCommandRequest:IRequest<AssignRolesToUserCommandResponse>
    {
        public string UserId { get; set; }
        public string[]? Roles { get; set; }

    }
}
