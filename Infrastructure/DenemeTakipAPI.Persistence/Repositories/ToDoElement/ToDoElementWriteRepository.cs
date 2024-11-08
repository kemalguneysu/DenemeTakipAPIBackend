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
    public class ToDoElementWriteRepository : WriteRepository<Domain.Entities.ToDo.ToDoElement>, IToDoElementWriteRepository
    {
        public ToDoElementWriteRepository(denemeTakipAPIDbContext context) : base(context)
        {
        }
    }
}
