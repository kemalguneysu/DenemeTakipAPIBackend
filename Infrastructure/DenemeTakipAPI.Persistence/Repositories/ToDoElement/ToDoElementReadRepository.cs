using DenemeTakipAPI.Application.Repositories;
using DenemeTakipAPI.Domain.Entities.ToDo;
using DenemeTakipAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Repositories.ToDoElement
{
    public class ToDoElementReadRepository : ReadRepository<Domain.Entities.ToDo.ToDoElement>, IToDoElementReadRepository
    {
        public ToDoElementReadRepository(denemeTakipAPIDbContext context) : base(context)
        {
        }
    }
}
