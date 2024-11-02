using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR.Hubs
{
    public class AytHub : Hub
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
        public async Task AytAddedMessage(string userId, string message)
        {
            await Clients.User(userId).SendAsync(ReceiveFunctions.AytAddedMessage, message);
            // Mesajı belirli bir kullanıcıya gönderin
        }

        public async Task AytDeletedMessage(string userId,string message)
        {
            await Clients.User(userId).SendAsync(ReceiveFunctions.AytDeletedMessage, message);
        }

        public async Task AytUpdatedMessage(string userId,string message)
        {
            await Clients.User(userId).SendAsync(ReceiveFunctions.AytUpdatedMessage, message);
        }
    }
}
