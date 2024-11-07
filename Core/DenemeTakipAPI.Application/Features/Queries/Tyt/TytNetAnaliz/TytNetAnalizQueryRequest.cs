using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.TytNetAnaliz
{
    public class TytNetAnalizQueryRequest:IRequest<TytNetAnalizQueryResponse>
    {
        public int DenemeSayisi { get; set; }
        public string? DersAdi { get; set; }
    }
}
