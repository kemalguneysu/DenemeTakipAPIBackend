using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR.HubServices
{
    public class ToDoElementHubService : IToDoElementHubService
    {
        readonly IHubContext<ToDoElementHub> _hubContext;

        public ToDoElementHubService(IHubContext<ToDoElementHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ToDoElementAddedMessage(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.ToDoElementAddedMessage, message, userId);
        }

        public async Task ToDoElementDeletedMessage(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.ToDoElementDeletedMessage, message, userId);
        }

        public async Task ToDoElementUpdatedMessage(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.ToDoElementUpdatedMessage, message, userId);
        }
    }
}
