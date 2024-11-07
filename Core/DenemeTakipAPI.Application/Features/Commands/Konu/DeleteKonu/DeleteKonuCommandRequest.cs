using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Konu.DeleteKonu
{
    public class DeleteKonuCommandRequest:IRequest<DeleteKonuCommandResponse>
    {
        public List<string> Ids { get; set; }
    }
}
