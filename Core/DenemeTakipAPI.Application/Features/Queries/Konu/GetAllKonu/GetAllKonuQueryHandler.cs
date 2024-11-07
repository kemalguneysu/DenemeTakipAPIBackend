using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonu
{
    public class GetAllKonuQueryHandler : IRequestHandler<GetAllKonuQueryRequest, GetAllKonuQueryResponse>
    {
        readonly IKonuService _service;

        public GetAllKonuQueryHandler(IKonuService service)
        {
            _service = service;
        }

        public async Task<GetAllKonuQueryResponse> Handle(GetAllKonuQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _service.Get(request.Page, request.Size);
            return new()
            {
                TotalKonu = data.TotalKonu,
                Konular = data.Konular
            };

        }
    }
}
