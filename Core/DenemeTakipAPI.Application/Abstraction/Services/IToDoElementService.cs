using DenemeTakipAPI.Application.DTOs.ToDoElement;
using DenemeTakipAPI.Application.DTOs.UserKonu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Services
{
    public interface IToDoElementService
    {
        Task CreateToDoElementAsync(string ToDoElementTitle, DateOnly ToDoDate, bool IsCompleted);
        Task<GetToDoElements> GetToDoElements(DateOnly toDoDate,bool? isCompleted);

    }
}
