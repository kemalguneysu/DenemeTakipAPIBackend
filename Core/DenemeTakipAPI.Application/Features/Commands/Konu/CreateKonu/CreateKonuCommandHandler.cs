using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Konu.CreateKonu
{
    public class CreateKonuCommandHandler : IRequestHandler<CreateKonuCommandRequest, CreateKonuCommandResponse>
    {
        readonly IKonuService _konuService;
        readonly IKonuHubService _konuHubService;

        public CreateKonuCommandHandler(IKonuService konuService, IKonuHubService konuHubService)
        {
            _konuService = konuService;
            _konuHubService = konuHubService;
        }

        public async Task<CreateKonuCommandResponse> Handle(CreateKonuCommandRequest request, CancellationToken cancellationToken)
        {
            await _konuService.CreateKonuAsync(new()
            {
                KonuAdi = request.KonuAdi,
                DersId=request.DersId,
                IsTyt=request.IsTyt

            });
            return new();
        }
    }
}
