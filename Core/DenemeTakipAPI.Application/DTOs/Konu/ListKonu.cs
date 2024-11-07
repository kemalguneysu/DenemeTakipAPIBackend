using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Konu
{
    public class ListKonu
    {
        public int TotalKonu { get; set; }
        public object Konular { get; set; }
    }
    public class ListAllKonu
    {
        public string Id { get; set; }
        public string KonuAdi { get; set; }
        public bool IsTyt { get; set; }
        public string DersAdi { get; set; }
        public string DersId { get; set; }
    }
    
}
