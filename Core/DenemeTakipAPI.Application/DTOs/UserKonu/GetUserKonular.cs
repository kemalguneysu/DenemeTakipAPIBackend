using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.UserKonu
{
    public class GetUserKonular
    {
        public int TotalCount { get; set; }
        public object UserKonular { get; set; }
    }
}
