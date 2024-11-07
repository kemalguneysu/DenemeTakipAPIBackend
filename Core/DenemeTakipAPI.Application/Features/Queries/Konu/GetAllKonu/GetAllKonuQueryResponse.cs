using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonu
{
    public class GetAllKonuQueryResponse
    {
        public int TotalKonu { get; set; }
        public object Konular { get; set; }
    }
}
