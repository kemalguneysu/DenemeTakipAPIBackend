using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ders.GetDersById
{
    public class GetDersByIdQueryResponse
    {
        public string Id { get; set; }
        public string DersAdi { get; set; }
        public bool IsTyt { get; set; }
    }
}
