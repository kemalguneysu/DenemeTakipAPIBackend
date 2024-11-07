using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs.Ayt;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Ayt.CreateAyt
{
    public class CreateAytCommandHandler : IRequestHandler<CreateAytCommandRequest, CreateAytCommandResponse>
    {
        readonly IAytService _aytService;
        readonly IAytHubService _aytHubService;

        public CreateAytCommandHandler(IAytHubService aytHubService, IAytService aytService)
        {
            _aytHubService = aytHubService;
            _aytService = aytService;
        }

        public async Task<CreateAytCommandResponse> Handle(CreateAytCommandRequest request, CancellationToken cancellationToken)
        {
            await _aytService.CreateAytAsync(new()
            {
                MatematikDogru=request.MatematikDogru,
                MatematikYanlis = request.MatematikYanlis,
                FizikDogru = request.FizikDogru,
                FizikYanlis = request.FizikYanlis,
                BiyolojiDogru = request.BiyolojiDogru,
                BiyolojiYanlis = request.BiyolojiYanlis,
                KimyaDogru = request.KimyaDogru,
                KimyaYanlis = request.KimyaYanlis,
                EdebiyatDogru = request.EdebiyatDogru,
                EdebiyatYanlis = request.EdebiyatYanlis,
                Tarih1Dogru = request.Tarih1Dogru,
                Tarih1Yanlis = request.Tarih1Yanlis,
                Tarih2Dogru = request.Tarih2Dogru,
                Tarih2Yanlis = request.Tarih2Yanlis,
                Cografya1Dogru = request.Cografya1Dogru,
                Cografya1Yanlis = request.Cografya1Yanlis,
                Cografya2Dogru = request.Cografya2Dogru,
                Cografya2Yanlis = request.Cografya2Yanlis,
                DinDogru =request.DinDogru,
                DinYanlis = request.DinYanlis,
                FelsefeDogru = request.FelsefeDogru,
                FelsefeYanlis = request.FelsefeYanlis,
                DilDogru = request.DilDogru,
                DilYanlis = request.DilYanlis,
                BosKonularId=request.BosKonularId,
                YanlisKonularId=request.YanlisKonularId

            });
            return new();
        }
    }
}
