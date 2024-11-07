using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.TytNetAnaliz
{
    public class TytNetAnalizQueryHandler : IRequestHandler<TytNetAnalizQueryRequest, TytNetAnalizQueryResponse>
    {
        readonly ITytService _tytservice;

        public TytNetAnalizQueryHandler(ITytService tytservice)
        {
            _tytservice = tytservice;
        }

        public async Task<TytNetAnalizQueryResponse> Handle(TytNetAnalizQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _tytservice.GetTytNetAnaliz(request.DenemeSayisi,request.DersAdi);
            return new()
            {
                Tyts = response.Tyts,
            };
        }
    }
}
