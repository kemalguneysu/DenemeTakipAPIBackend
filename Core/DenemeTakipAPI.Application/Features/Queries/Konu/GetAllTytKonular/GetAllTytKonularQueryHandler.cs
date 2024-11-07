using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.Repositories.KonuRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllTytKonular
{
    public class GetAllTytKonularQueryHandler : IRequestHandler<GetAllTytKonularQueryRequest, GetAllTytKonularQueryResponse>
    {
        private readonly IKonuService _konuService;
        public GetAllTytKonularQueryHandler(IKonuService konuService)
        {
            _konuService = konuService;
        }
        public async Task<GetAllTytKonularQueryResponse> Handle(GetAllTytKonularQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _konuService.GetAllTytKonular(request.Page, request.Size,request.DersIds, request.KonuAdi);
            return new()
            {
                TotalCount = data.TotalCount,
                TytKonular = data.TytKonular
            };
        }
    }
}
