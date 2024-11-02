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
    public class AytHubService : IAytHubService
    {
        readonly IHubContext<AytHub> _hubContext;

        public AytHubService(IHubContext<AytHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AytAddedMessage(string userId,string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.AytAddedMessage, message, userId);
        }

        public async Task AytDeletedMessage(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.AytDeletedMessage, message, userId);
        }

        public async Task AytUpdatedMessage(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.AytUpdatedMessage, message, userId);
        }
    }
}
