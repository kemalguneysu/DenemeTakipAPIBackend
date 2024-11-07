using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Konu.CreateKonu
{
    public class CreateKonuCommandRequest:IRequest<CreateKonuCommandResponse>
    {
        public string KonuAdi { get; set; }
        public string DersId { get; set; }
        public bool IsTyt { get; set; }

    }
}
