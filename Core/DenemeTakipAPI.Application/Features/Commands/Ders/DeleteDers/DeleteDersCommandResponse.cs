using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Ders.DeleteDers
{
    public class DeleteDersCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
