using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Ders
{
    public class ListDers
    {
        public int TotalDers { get; set; }
        public object Dersler { get; set; }
    }
    public class ListAllDers
    {
        public string Id { get; set; }
        public string DersAdi { get; set; }
        public bool IsTyt { get; set; }


    }
}
