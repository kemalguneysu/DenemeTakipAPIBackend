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
    public class UserKonuHubService : IUserKonuHubService
    {
        readonly IHubContext<UserKonuHub> _hubContext;

        public UserKonuHubService(IHubContext<UserKonuHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task UserKonuAddedMessage(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.UserKonuAddedMessage, message, userId);
        }

        public async  Task UserKonuDeletedMessage(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.UserKonuDeletedMessage, message, userId);
        }

        public async Task UserKonuUpdatedMessage(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.UserKonuUpdatedMessage, message, userId);
        }
    }
     
}
