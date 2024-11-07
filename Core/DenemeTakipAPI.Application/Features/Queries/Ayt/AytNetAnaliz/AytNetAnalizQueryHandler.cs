using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.TytNetAnaliz
{
    public class AytNetAnalizQueryHandler : IRequestHandler<AytNetAnalizQueryRequest, AytNetAnalizQueryResponse>
    {
        readonly IAytService _aytservice;

        public AytNetAnalizQueryHandler(IAytService aytservice)
        {
            _aytservice = aytservice;
        }
        public async Task<AytNetAnalizQueryResponse> Handle(AytNetAnalizQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _aytservice.GetAytNetAnaliz(request.DenemeSayisi,request.AlanTuru,request.DersAdi);
            return new()
            {
                Ayts = response.Ayts,
            };
        }
    }
}
