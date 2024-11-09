using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.ToDoElement.UpdateToDoElement
{
    public class UpdateToDoElementCommandRequest:IRequest<UpdateToDoElementCommandResponse>
    {
        public string ToDoId { get; set; }
        public bool? IsCompleted { get; set; }
        public string? ToDoTitle { get; set; }

    }
}
