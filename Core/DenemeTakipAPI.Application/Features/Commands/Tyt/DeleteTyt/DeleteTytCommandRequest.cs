using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Tyt.DeleteTyt
{
    public class DeleteTytCommandRequest:IRequest<DeleteTytCommandResponse>
    {
        public List<string> Ids { get; set; }=new List<string>();

    }
}
