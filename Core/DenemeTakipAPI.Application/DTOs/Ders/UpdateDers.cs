using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Ders
{
    public class UpdateDers
    {
        public string DersId { get; set; }
        public string DersAdi { get; set; }
        public bool IsTyt { get; set; }

    }
}
