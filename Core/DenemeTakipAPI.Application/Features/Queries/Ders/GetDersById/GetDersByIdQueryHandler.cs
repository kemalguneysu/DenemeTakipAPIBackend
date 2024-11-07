using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ders.GetDersById
{
    public class GetDersByIdQueryHandler : IRequestHandler<GetDersByIdQueryRequest, GetDersByIdQueryResponse>
    {
        readonly IDersService _dersService;

        public GetDersByIdQueryHandler(IDersService dersService)
        {
            _dersService = dersService;
        }

        public async Task<GetDersByIdQueryResponse> Handle(GetDersByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _dersService.GetDersById(request.DersId);
            if (data == null)
                throw new Exception("Ders bulunamadı.");
            return new()
            {
                Id = data.Id,
                DersAdi = data.DersAdi,
                IsTyt = data.IsTyt,
            };
        }
    }
}
