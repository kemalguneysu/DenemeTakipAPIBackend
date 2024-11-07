using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.GetAllTyt
{
    public class GetAllTytQueryHandler:IRequestHandler<GetAllTytQueryRequest,GetAllTytQueryResponse>
    {
        readonly ITytService _tytService;

        public GetAllTytQueryHandler(ITytService tytService)
        {
            _tytService = tytService;
        }

        public async Task<GetAllTytQueryResponse> Handle(GetAllTytQueryRequest request, CancellationToken cancellationToken)
        {
            var data=await _tytService.Get(request.Page, request.Size,request.OrderByAndDirections);
            return new()
            {
                TytDenemes=data.Tyts,
                TotalCount=data.TotalTyt
            };
        }
    }
}
