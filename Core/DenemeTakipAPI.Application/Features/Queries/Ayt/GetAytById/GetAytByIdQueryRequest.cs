using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ayt.GetAytById
{
    public class GetAytByIdQueryRequest:IRequest<GetAytByIdQueryResponse>
    {
        public string AytId { get; set; }
    }
}
