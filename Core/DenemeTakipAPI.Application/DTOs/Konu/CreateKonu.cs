using DenemeTakipAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Konu
{
    public class CreateKonu:BaseEntity
    {
        public string KonuAdi { get; set; }
        public string DersId { get; set; }
        public bool IsTyt { get; set; }
    }
}
