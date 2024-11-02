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
    public class UserHubService : IUserHubService
    {
        readonly IHubContext<UserHub> _hubContext;
        public UserHubService(IHubContext<UserHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task UserUpdatedMessage(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync(ReceiveFunctions.UserUpdatedMessage, message, userId);

        }
    }
}
