using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Konu.UpdateKonu
{
    public class UpdateKonuCommandHandler : IRequestHandler<UpdateKonuCommandRequest, UpdateKonuCommandResponse>
    {
        readonly IKonuService _konuService;

        public UpdateKonuCommandHandler(IKonuService konuService)
        {
            _konuService = konuService;
        }

        public async Task<UpdateKonuCommandResponse> Handle(UpdateKonuCommandRequest request, CancellationToken cancellationToken)
        {
            await _konuService.UpdateKonu(new()
            {
                KonuId = request.KonuId,
                KonuAdi = request.KonuAdi,
                DersId = request.DersId,
            });
            return new();
        }
    }
}
