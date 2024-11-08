using DenemeTakipAPI.Application.Features.Queries.ToDoElement.GetToDoElement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.ToDoElement.GetToDoElements
{
    public class GetToDoElementQueryRequest:IRequest<GetToDoElementQueryResponse>
    {
        public DateOnly ToDoDate { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
