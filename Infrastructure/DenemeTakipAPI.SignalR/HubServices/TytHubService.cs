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
    public class TytHubService : ITytHubService
    {
        readonly IHubContext<TytHub> _hubContext;
        public TytHubService(IHubContext<TytHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task TytAddedMessage(string userId,string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.TytAddedMessage, message, userId);
        }

        public async Task TytDeletedMessage(string userId,string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.TytDeletedMessage, message, userId);
             
        }

        public async Task TytUpdatedMessage(string userId,string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.TytUpdatedMessage, message, userId);

        }
    }
}
