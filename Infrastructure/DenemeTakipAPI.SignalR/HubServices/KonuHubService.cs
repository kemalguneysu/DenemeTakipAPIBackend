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
    public class KonuHubService : IKonuHubService
    {
        readonly IHubContext<KonuHub> _hubContext;

        public KonuHubService(IHubContext<KonuHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task KonuAddedMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctions.KonuAddedMessage, message);

        }

        public async Task KonuDeletedMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctions.KonuDeletedMessage, message);
        }

        public  async Task KonuUpdatedMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctions.KonuUpdatedMessage, message);
        }
    }
}
