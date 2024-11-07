using DenemeTakipAPI.Application.DTOs.Konu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetKonuById
{
    public class GetKonuByIdQueryResponse
    {
        public string Id { get; set; }
        public string KonuAdi { get; set; }
        public bool IsTyt { get; set; }
        public string DersAdi { get; set; }
        public string DersId { get; set; }
    }
}
