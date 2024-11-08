using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Hubs
{
    public interface IToDoElementHubService
    {
        Task ToDoElementAddedMessage(string userId, string message);
        Task ToDoElementUpdatedMessage(string userId, string message);
        Task ToDoElementDeletedMessage(string userId, string message);
    }
}
