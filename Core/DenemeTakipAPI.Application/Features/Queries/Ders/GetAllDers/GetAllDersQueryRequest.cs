using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ders.GetAllDers
{
    public class GetAllDersQueryRequest:IRequest<GetAllDersQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
