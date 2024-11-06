using DenemeTakipAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Domain.Entities.DenemeFolder
{
    public class Ders:BaseEntity
    {
        public string DersAdi { get; set; }
        public List<Konu> Konular { get; set; } = new();
        public bool IsTyt { get; set; }

    }
}
