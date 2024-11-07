using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.TytNetAnaliz
{
    public class AytNetAnalizQueryRequest:IRequest<AytNetAnalizQueryResponse>
    {
        public int DenemeSayisi { get; set; }
        public string AlanTuru { get; set; }
        public string? DersAdi { get; set; }

    }
}
