using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.TytAnaliz
{
    public class TytAnalizQueryRequest:IRequest<TytAnalizQueryResponse>
    {
        public int DenemeSayisi { get; set; }
        public int KonuSayisi { get; set; }
        public string DersId { get; set; }
        public string Type { get; set; }

    }
}
