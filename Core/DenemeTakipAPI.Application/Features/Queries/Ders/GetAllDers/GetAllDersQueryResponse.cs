using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ders.GetAllDers
{
    public class GetAllDersQueryResponse
    {
        public int TotalDers { get; set; }
        public object Dersler { get; set; }
    }
}
