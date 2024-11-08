using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.ToDoElement.GetToDoElement
{
    public class GetToDoElementQueryResponse
    {
        public int TotalCount { get; set; }
        public object ToDoElements { get; set; }
    }
}
