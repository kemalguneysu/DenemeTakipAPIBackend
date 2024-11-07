using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Konu.UpdateKonu
{
    public class UpdateKonuCommandRequest:IRequest<UpdateKonuCommandResponse>
    {
        public string KonuId { get; set; }
        public string KonuAdi { get; set; }
        public string DersId { get; set; }

    }
}
