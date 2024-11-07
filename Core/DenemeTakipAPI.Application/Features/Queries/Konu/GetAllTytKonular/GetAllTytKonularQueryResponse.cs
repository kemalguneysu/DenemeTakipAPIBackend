using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllTytKonular
{
    public class GetAllTytKonularQueryResponse
    {
        public int TotalCount { get; set; }
        public object TytKonular { get; set; }
    }
}
