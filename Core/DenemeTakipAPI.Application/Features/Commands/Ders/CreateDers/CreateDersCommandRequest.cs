using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Ders.CreateKonu
{
    public class CreateDersCommandRequest:IRequest<CreateDersCommandResponse>
    {
        public string DersAdi { get; set; }
        public bool IsTyt { get; set; }


    }
}
