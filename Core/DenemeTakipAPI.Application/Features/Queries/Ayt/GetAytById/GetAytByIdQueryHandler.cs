using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DenemeTakipAPI.Application.Features.Queries.Ayt.GetAytById
{
    public class GetAytByIdQueryHandler : IRequestHandler<GetAytByIdQueryRequest, GetAytByIdQueryResponse>
    {
        readonly IAytService _aytService;

        public GetAytByIdQueryHandler(IAytService aytService)
        {
            _aytService = aytService;
        }

        public async Task<GetAytByIdQueryResponse> Handle(GetAytByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _aytService.GetAytById(request.AytId);

            return new()
            {
                Id = request.AytId.ToString(),
                MatematikDogru = data.MatematikDogru,
                MatematikYanlis = data.MatematikYanlis,
                FizikDogru = data.FizikDogru,
                FizikYanlis = data.FizikYanlis,
                KimyaDogru = data.KimyaDogru,
                KimyaYanlis = data.KimyaYanlis,
                BiyolojiDogru = data.BiyolojiDogru,
                BiyolojiYanlis = data.BiyolojiYanlis,
                EdebiyatDogru = data.EdebiyatDogru,
                EdebiyatYanlis = data.EdebiyatYanlis,
                Tarih1Dogru = data.Tarih1Dogru,
                Tarih1Yanlis = data.Tarih1Yanlis,
                Cografya1Dogru = data.Cografya1Dogru,
                Cografya1Yanlis = data.Cografya1Yanlis,
                Tarih2Dogru = data.Tarih2Dogru,
                Tarih2Yanlis = data.Tarih2Yanlis,
                Cografya2Dogru = data.Cografya2Dogru,
                Cografya2Yanlis = data.Cografya2Yanlis,
                FelsefeDogru = data.FelsefeDogru,
                FelsefeYanlis = data.FelsefeYanlis,
                DinDogru = data.DinDogru,
                DinYanlis = data.DinYanlis,
                DilDogru = data.DilDogru,
                DilYanlis = data.DilYanlis,
                YanlisKonularAdDers = data.YanlisKonularAdDers,
                BosKonularAdDers = data.BosKonularAdDers
            };
        }
    }
}
