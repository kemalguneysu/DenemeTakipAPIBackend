using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR.Hubs
{
    public class KonuHub : Hub
    {
        public async Task KonuAddedMessage(string message)
        {
            await Clients.All.SendAsync(ReceiveFunctions.KonuAddedMessage, message);
        }
        public async Task KonuDeletedMessage(string message)
        {
            await Clients.All.SendAsync(ReceiveFunctions.KonuDeletedMessage, message);
        }
        public async Task KonuUpdatedMessage(string message)
        {
            await Clients.All.SendAsync(ReceiveFunctions.KonuUpdatedMessage, message);

        }
    }
}
