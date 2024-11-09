using DenemeTakipAPI.Domain.Entities.Common;
using DenemeTakipAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Domain.Entities.ToDo
{
    public class ToDoElement:BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User{ get; set; }
        public string ToDoElementTitle { get; set; }
        public bool IsCompleted{ get; set; }
        public DateTime ToDoDate { get; set; }

    }
}
