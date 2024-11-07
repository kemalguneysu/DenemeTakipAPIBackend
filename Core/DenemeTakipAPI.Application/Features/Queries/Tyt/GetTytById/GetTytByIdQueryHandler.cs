using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.GetTytById
{
    public class GetTytByIdQueryHandler : IRequestHandler<GetTytByIdQueryRequest, GetTytByIdQueryResponse>
    {
        readonly ITytService _tytService;

        public GetTytByIdQueryHandler(ITytService tytService)
        {
            _tytService = tytService;
        }

        public async Task<GetTytByIdQueryResponse> Handle(GetTytByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _tytService.GetTytById(request.TytId);
            return new()
            {
                Id = request.TytId.ToString(),
                TurkceDogru = data.TurkceDogru,
                TurkceYanlis = data.TurkceYanlis,
                MatematikDogru = data.MatematikDogru,
                MatematikYanlis = data.MatematikYanlis,
                FenDogru = data.FenDogru,
                FenYanlis = data.FenYanlis,
                SosyalDogru= data.SosyalDogru,
                SosyalYanlis = data.SosyalYanlis,
                YanlisKonularAdDers=data.YanlisKonularAd,
                BosKonularAdDers =data.BosKonularAd
            };
        }
    }
}
