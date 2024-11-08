using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR.Hubs
{
    public class ToDoElementHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            await base.OnDisconnectedAsync(exception);
        }
        public async Task ToDoElementAddedMessage(string userId, string message)
        {
            await Clients.User(userId).SendAsync(ReceiveFunctions.ToDoElementAddedMessage, message);
            // Mesajı belirli bir kullanıcıya gönderin
        }

        public async Task ToDoElementDeletedMessage(string message)
        {
            var userId = Context.UserIdentifier;
            await Clients.User(userId).SendAsync(ReceiveFunctions.ToDoElementDeletedMessage, message);
        }

        public async Task ToDoElementUpdatedMessage(string message)
        {
            var userId = Context.UserIdentifier;
            await Clients.User(userId).SendAsync(ReceiveFunctions.ToDoElementUpdatedMessage, message);
        }
    }
}
