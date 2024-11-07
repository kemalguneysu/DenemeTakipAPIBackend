using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.User.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommandRequest, DeleteAccountCommandResponse>
    {
        readonly IUserService _userService;

        public DeleteAccountCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<DeleteAccountCommandResponse> Handle(DeleteAccountCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.DeleteAccount(request.UserId);
            return new()
            {
                Succeeded = response.Succeeded,
                Message = response.Message,
            };
        }
    }
}
