using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.GetTytById
{
    public class GetTytByIdQueryRequest:IRequest<GetTytByIdQueryResponse>
    {
        public string TytId { get; set; }
    }
}
