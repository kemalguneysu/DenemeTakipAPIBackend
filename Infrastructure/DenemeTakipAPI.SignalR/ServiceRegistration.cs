using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAytHubService, AytHubService>();
            serviceCollection.AddTransient<IKonuHubService, KonuHubService>();
            serviceCollection.AddTransient<ITytHubService, TytHubService>();
            serviceCollection.AddTransient<IDersHubService, DersHubService>();

            serviceCollection.AddTransient<IUserHubService, UserHubService>();
            serviceCollection.AddSignalR();

        }
    }
}
