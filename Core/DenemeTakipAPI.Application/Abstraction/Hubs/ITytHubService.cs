using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Hubs
{
    public interface ITytHubService
    {
        Task TytAddedMessage(string userId, string message);
        Task TytUpdatedMessage(string userId, string message);
        Task TytDeletedMessage(string userId, string message);

    }
}
