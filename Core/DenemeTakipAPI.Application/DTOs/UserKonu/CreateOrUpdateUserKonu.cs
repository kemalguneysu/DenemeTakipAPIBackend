using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.UserKonu
{
    public class CreateOrUpdateUserKonu
    {
        public string KonuId { get; set; }
        public bool IsCompleted{ get; set; }
    }
}
