using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllAytKonular
{
    public class GetAllAytKonularQueryResponse
    {
        public int TotalCount { get; set; }
        public object AytKonular { get; set; }

    }
}
