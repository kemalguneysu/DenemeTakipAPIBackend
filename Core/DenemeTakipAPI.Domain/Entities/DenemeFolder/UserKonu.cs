using DenemeTakipAPI.Domain.Entities.Common;
using DenemeTakipAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Domain.Entities.DenemeFolder
{
    public class UserKonu:BaseEntity
    {
        public string UserId { get; set; }

        public AppUser User { get; set; }
        public Guid KonuId { get; set; }
        public Konu Konu { get; set; }

        public bool IsCompleted{ get; set; }
    }
}
