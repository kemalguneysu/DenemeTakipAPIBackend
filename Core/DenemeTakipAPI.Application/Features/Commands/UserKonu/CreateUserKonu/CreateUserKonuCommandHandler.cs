using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.UserKonu.CreateOrUpdate
{
    public class CreateUserKonuCommandHandler : IRequestHandler<CreateUserKonuCommandRequest, CreateUserKonuCommandResponse>
    {
        readonly IUserKonuService _userKonuService;

        public CreateUserKonuCommandHandler(IUserKonuService userKonuService)
        {
            _userKonuService = userKonuService;
        }

        public async Task<CreateUserKonuCommandResponse> Handle(CreateUserKonuCommandRequest request, CancellationToken cancellationToken)
        {
            await _userKonuService.CreateUserKonuAsync(request.KonuIds);
            return new();
            
        }
    }
}
