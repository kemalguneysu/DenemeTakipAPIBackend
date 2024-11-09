using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.ToDoElement.DeleteToDoElement
{
    public class DeleteToDoElementCommandRequest:IRequest<DeleteToDoElementCommandResponse>
    {
        public string ToDoId { get; set; }
    }
}
