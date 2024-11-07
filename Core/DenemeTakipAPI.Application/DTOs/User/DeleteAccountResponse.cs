using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.User
{
    public class DeleteAccountResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
