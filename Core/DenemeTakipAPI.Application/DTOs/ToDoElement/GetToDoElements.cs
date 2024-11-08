using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.ToDoElement
{
    public class GetToDoElements
    {
        public int TotalCount { get; set; }
        public object ToDoElements{ get; set; }

    }
}
