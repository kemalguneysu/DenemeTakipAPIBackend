using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.ToDoElement.CreateToDoElement
{
    public class CreateToDoElementCommandRequest:IRequest<CreateToDoElementCommandResponse>
    {
        public string ToDoElementTitle { get; set; }
        public DateOnly ToDoDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
