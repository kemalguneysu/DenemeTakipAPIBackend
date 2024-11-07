using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllAytKonular
{
    public class GetAllAytKonularQueryHandler : IRequestHandler<GetAllAytKonularQueryRequest, GetAllAytKonularQueryResponse>
    {
        readonly IKonuService _konuService;

        public GetAllAytKonularQueryHandler(IKonuService konuService)
        {
            _konuService = konuService;
        }

        public async Task<GetAllAytKonularQueryResponse> Handle(GetAllAytKonularQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _konuService.GetAllAytKonular(request.Page, request.Size, request.DersIds,request.KonuAdi);
            return new()
            {
                TotalCount = data.TotalCount,
                AytKonular = data.AytKonular
            };
        }
    }
}
