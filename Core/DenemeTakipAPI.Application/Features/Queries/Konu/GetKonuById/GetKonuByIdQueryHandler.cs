using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetKonuById
{
    public class GetKonuByIdQueryHandler : IRequestHandler<GetKonuByIdQueryRequest, GetKonuByIdQueryResponse>
    {
        readonly IKonuService _konuService;

        public GetKonuByIdQueryHandler(IKonuService konuService)
        {
            _konuService = konuService;
        }

        public async Task<GetKonuByIdQueryResponse> Handle(GetKonuByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _konuService.GetKonuById(request.KonuId);
            return new()
            {
                Id = data.Id,
                KonuAdi=data.KonuAdi,
                IsTyt=data.IsTyt,
                DersAdi =data.DersAdi,
                DersId=data.DersId
            };
        }
    }
}
