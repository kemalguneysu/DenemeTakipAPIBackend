using DenemeTakipAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<AytHub>("/ayt-hub");
            webApplication.MapHub<TytHub>("/tyt-hub");
            webApplication.MapHub<KonuHub>("/konu-hub");
            webApplication.MapHub<DersHub>("/ders-hub");
            webApplication.MapHub<UserHub>("/user-hub");

        }
    }
}
