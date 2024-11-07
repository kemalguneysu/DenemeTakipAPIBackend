using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.UserKonu.GetUserKonular
{
    public class GetUserKonularQueryRequest:IRequest<GetUserKonularQueryResponse>
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
        public string? DersId{ get; set; }
    }
}
