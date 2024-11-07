using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonu
{
    public class GetAllKonuQueryRequest:IRequest<GetAllKonuQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
