using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.TytAnaliz
{
    public class TytAnalizQueryHandler : IRequestHandler<TytAnalizQueryRequest, TytAnalizQueryResponse>
    {
        readonly ITytService _tytService;
        public TytAnalizQueryHandler(ITytService tytService)
        {
            _tytService = tytService;
        }
        public async Task<TytAnalizQueryResponse> Handle(TytAnalizQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _tytService.GetTytAnaliz(request.DenemeSayisi, request.KonuSayisi, request.DersId, request.Type);
            return new()
            {
                TytAnaliz = response
            };
        }
    }
}
