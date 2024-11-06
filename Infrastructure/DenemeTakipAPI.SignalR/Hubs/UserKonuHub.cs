using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR.Hubs
{
    public class UserKonuHub:Hub
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
        public async Task UserKonuAddedMessage(string userId, string message)
        {
            await Clients.User(userId).SendAsync(ReceiveFunctions.UserKonuAddedMessage, message);
            // Mesajı belirli bir kullanıcıya gönderin
        }

        public async Task UserKonuDeletedMessage(string message)
        {
            var userId = Context.UserIdentifier;
            await Clients.User(userId).SendAsync(ReceiveFunctions.UserKonuDeletedMessage, message);
        }

        public async Task UserKonuUpdatedMessage(string message)
        {
            var userId = Context.UserIdentifier;
            await Clients.User(userId).SendAsync(ReceiveFunctions.UserKonuUpdatedMessage, message);
        }
    }
}
