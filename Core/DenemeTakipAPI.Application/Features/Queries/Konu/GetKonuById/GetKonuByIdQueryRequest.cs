using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetKonuById
{
    public class GetKonuByIdQueryRequest:IRequest<GetKonuByIdQueryResponse>
    {
        public string KonuId { get; set; }

    }
}
