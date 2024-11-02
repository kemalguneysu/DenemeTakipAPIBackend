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
    public class DersHubService : IDersHubService
    {
        readonly IHubContext<DersHub> _hubContext;

        public DersHubService(IHubContext<DersHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task DersAddedMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctions.DersAddedMessage, message);

        }

        public async Task DersDeletedMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctions.DersDeletedMessage, message);
        }

        public async Task DersUpdatedMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctions.DersUpdatedMessage, message);
        }
    }
}
