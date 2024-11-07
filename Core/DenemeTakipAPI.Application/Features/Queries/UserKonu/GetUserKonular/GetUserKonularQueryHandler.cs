using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.UserKonu.GetUserKonular
{
    public class GetUserKonularQueryHandler : IRequestHandler<GetUserKonularQueryRequest, GetUserKonularQueryResponse>
    {
        readonly IUserKonuService _userKonuService;
        public GetUserKonularQueryHandler(IUserKonuService userKonuService)
        {
            _userKonuService = userKonuService;
        }

        public async Task<GetUserKonularQueryResponse> Handle(GetUserKonularQueryRequest request, CancellationToken cancellationToken)
        {
            var response= await _userKonuService.GetUserKonular(request.Page, request.Size, request.DersId);
            return new()
            {
                TotalCount = response.TotalCount,
                UserKonular = response.UserKonular
            };
        }
    }
}
