using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ders.GetDersById
{
    public class GetDersByIdQueryRequest:IRequest<GetDersByIdQueryResponse>
    {
        public string DersId { get; set; }

    }
}
