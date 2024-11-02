using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR.Hubs
{
    public class DersHub:Hub
    {
        public async Task DersAddedMessage(string message)
        {
            await Clients.All.SendAsync(ReceiveFunctions.DersAddedMessage, message);
        }
        public async Task DersDeletedMessage(string message)
        {
            await Clients.All.SendAsync(ReceiveFunctions.DersDeletedMessage, message);
        }
        public async Task DersUpdatedMessage(string message)
        {
            await Clients.All.SendAsync(ReceiveFunctions.DersUpdatedMessage, message);

        }
    }
}
