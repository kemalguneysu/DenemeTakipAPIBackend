using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.Features.Queries.Tyt.GetAllTyt;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ayt.GetAllAyt
{
    public class GetAllAytQueryHandler:IRequestHandler<GetAllAytQueryRequest,GetAllAytQueryResponse>
    {
        readonly IAytService _aytService;

        public GetAllAytQueryHandler(IAytService aytService)
        {
            _aytService = aytService;
        }
        public async Task<GetAllAytQueryResponse> Handle(GetAllAytQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _aytService.Get(request.Page, request.Size, request.OrderByAndDirections);
            return new()
            {
                AytDenemes = data.Ayts,
                TotalCount=data.TotalAyt
            };
        }
    }
}
