using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Ayt.UpdateAyt
{
    public class UpdateAytCommandHandler : IRequestHandler<UpdateAytCommandRequest, UpdateAytCommandResponse>
    {
        readonly IAytService _aytService;
        readonly IAytHubService _aytHubService;

        public UpdateAytCommandHandler(IAytService aytService, IAytHubService aytHubService)
        {
            _aytService = aytService;
            _aytHubService = aytHubService;
        }

        public async Task<UpdateAytCommandResponse> Handle(UpdateAytCommandRequest request, CancellationToken cancellationToken)
        {
            await _aytService.UpdateAytAsync(new()
            {
                AytId=request.AytId,
                MatematikDogru=request.MatematikDogru,
                MatematikYanlis = request.MatematikYanlis,
                FizikDogru = request.FizikDogru,
                FizikYanlis = request.FizikYanlis,
                KimyaDogru = request.KimyaDogru,
                KimyaYanlis = request.KimyaYanlis,
                BiyolojiDogru = request.BiyolojiDogru,
                BiyolojiYanlis = request.BiyolojiYanlis,
                EdebiyatDogru = request.EdebiyatDogru,
                EdebiyatYanlis = request.EdebiyatYanlis,
                Tarih1Dogru = request.Tarih1Dogru,
                Tarih1Yanlis = request.Tarih1Yanlis,
                Cografya1Dogru = request.Cografya1Dogru,
                Cografya1Yanlis = request.Cografya1Yanlis,
                Tarih2Dogru = request.Tarih2Dogru,
                Tarih2Yanlis = request.Tarih2Yanlis,
                Cografya2Dogru = request.Cografya2Dogru,
                Cografya2Yanlis = request.Cografya2Yanlis,
                FelsefeDogru = request.FelsefeDogru,
                FelsefeYanlis = request.FelsefeYanlis,
                DinDogru = request.DinDogru,
                DinYanlis = request.DinYanlis,
                DilDogru = request.DilDogru,
                DilYanlis = request.DilYanlis,
                YanlisKonularId=request.YanlisKonular,
                BosKonularId=request.BosKonular
            });
            
            return new();

        }
    }
}
