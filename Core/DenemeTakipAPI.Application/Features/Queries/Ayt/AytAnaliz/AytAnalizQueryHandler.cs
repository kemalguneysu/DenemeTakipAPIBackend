using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.Features.Queries.Tyt.TytAnaliz;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ayt.AytAnaliz
{
    public class AytAnalizQueryHandler : IRequestHandler<AytAnalizQueryRequest, AytAnalizQueryResponse>
    {
        readonly IAytService _aytService;
        public AytAnalizQueryHandler(IAytService aytService)
        {
            _aytService = aytService;
        }

        public async Task<AytAnalizQueryResponse> Handle(AytAnalizQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _aytService.GetAytAnaliz(request.DenemeSayisi, request.KonuSayisi, request.DersId, request.Type);
            return new()
            {
                AytAnaliz = response
            };
        }
    }
}
