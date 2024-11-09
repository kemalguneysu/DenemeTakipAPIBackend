using DenemeTakipAPI.Application.DTOs;
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
        Task CreateToDoElementAsync(string ToDoElementTitle, DateTime ToDoDate, bool IsCompleted);
        Task<GetToDoElements> GetToDoElements(DateTime toDoDateStart, DateTime toDoDateEnd, bool? isCompleted);
        Task<SucceededMessageResponse> UpdateToDoElementAsync(string todoId,bool? isCompleted,string? toDoTitle);
        Task<SucceededMessageResponse> DeleteToDoElementAsync(string todoId);

    }
}
