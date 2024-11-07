using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.GetAllTyt
{
    public class GetAllTytQueryResponse
    {
        public int TotalCount { get; set; }
        public object TytDenemes { get; set; }
    }
}
