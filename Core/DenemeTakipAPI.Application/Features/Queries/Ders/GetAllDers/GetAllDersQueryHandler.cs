using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ders.GetAllDers
{
    public class GetAllDersQueryHandler : IRequestHandler<GetAllDersQueryRequest, GetAllDersQueryResponse>
    {
        readonly IDersService _service;

        public GetAllDersQueryHandler(IDersService service)
        {
            _service = service;
        }

        public async Task<GetAllDersQueryResponse> Handle(GetAllDersQueryRequest request, CancellationToken cancellationToken)
        {
            var data =await _service.Get(request.Page,request.Size);
            return new()
            {
                TotalDers = data.TotalDers,
                Dersler = data.Dersler
            };
        }
    }
}
