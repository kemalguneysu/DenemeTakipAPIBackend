using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonularPaginated
{
    public class GetAllKonularPaginatedQueryHandler : IRequestHandler<GetAllKonularPaginatedQueryRequest, GetAllKonularPaginatedQueryResponse>
    {
        readonly IKonuService _konuService;

        public GetAllKonularPaginatedQueryHandler(IKonuService konuService)
        {
            _konuService = konuService;
        }
        public async Task<GetAllKonularPaginatedQueryResponse> Handle(GetAllKonularPaginatedQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _konuService.GetAllKonular(request.Page, request.Size, request.DersIds, request.KonuOrDersAdi,request.IsTyt);
            return new()
            {
                TotalCount = data.TotalCount,
                Konular = data.Konular
            };
        }
    }
}
