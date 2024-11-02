using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR.Hubs
{
    public class TytHub : Hub
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
        public async Task TytAddedMessage(string userId,string message)
        {
            await Clients.User(userId).SendAsync(ReceiveFunctions.TytAddedMessage, message);
            // Mesajı belirli bir kullanıcıya gönderin
        }

        public async Task TytDeletedMessage(string message)
        {
            var userId = Context.UserIdentifier;
            await Clients.User(userId).SendAsync(ReceiveFunctions.TytDeletedMessage, message);
        }

        public async Task TytUpdatedMessage(string message)
        {
            var userId = Context.UserIdentifier;
            await Clients.User(userId).SendAsync(ReceiveFunctions.TytUpdatedMessage, message);
        }
    }
}
