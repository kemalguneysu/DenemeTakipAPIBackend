using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Konu
{
    public class UpdateKonu
    {
        public string KonuId { get; set; }
        public string KonuAdi { get; set; }
        public string DersId { get; set; }
    }
}
