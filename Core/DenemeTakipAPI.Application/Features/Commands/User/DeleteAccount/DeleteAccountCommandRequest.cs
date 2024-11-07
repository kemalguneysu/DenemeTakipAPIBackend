using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.User.DeleteAccount
{
    public class DeleteAccountCommandRequest:IRequest<DeleteAccountCommandResponse>
    {
        public string? UserId { get; set; }
    }
}
