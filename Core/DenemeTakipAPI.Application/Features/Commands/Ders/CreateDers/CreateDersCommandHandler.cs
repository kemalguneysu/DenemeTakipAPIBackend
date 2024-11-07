using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Ders.CreateKonu
{
    public class CreateDersCommandHandler : IRequestHandler<CreateDersCommandRequest, CreateDersCommandResponse>
    {
        readonly IDersService _dersService;
        readonly IDersHubService _dersHubService;

        public CreateDersCommandHandler(IDersService dersService, IDersHubService dersHubService)
        {
            _dersService = dersService;
            _dersHubService = dersHubService;
        }

        public async Task<CreateDersCommandResponse> Handle(CreateDersCommandRequest request, CancellationToken cancellationToken)
        {
            await _dersService.CreateDersAsync(new()
            {
                DersAdi = request.DersAdi,
                IsTyt=request.IsTyt
                
            });
            return new();

        }
    }
}
