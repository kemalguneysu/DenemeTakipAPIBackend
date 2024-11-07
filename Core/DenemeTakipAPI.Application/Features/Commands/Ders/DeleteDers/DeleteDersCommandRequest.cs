using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Ders.DeleteDers
{
    public class DeleteDersCommandRequest:IRequest<DeleteDersCommandResponse>
    {
        public List<string> Ids { get; set; }
    }
}
