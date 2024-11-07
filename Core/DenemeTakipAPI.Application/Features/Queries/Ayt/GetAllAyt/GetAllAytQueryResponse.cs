using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ayt.GetAllAyt
{
    public class GetAllAytQueryResponse
    {
        public int TotalCount { get; set; }
        public object AytDenemes { get; set; }
    }
}
