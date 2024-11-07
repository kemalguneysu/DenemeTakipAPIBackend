using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Ders.UpdateDers
{
    public class UpdateDersCommandRequest:IRequest<UpdateDersCommandResponse>
    {
        public string DersId { get; set; }
        public string DersAdi { get; set; }
        public bool IsTyt { get; set; }
    }
}
