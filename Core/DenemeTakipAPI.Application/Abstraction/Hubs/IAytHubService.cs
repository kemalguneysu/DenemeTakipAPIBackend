using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Hubs
{
    public interface IAytHubService
    {
        Task AytAddedMessage(string userId,string message);
        Task AytUpdatedMessage(string userId,string message);
        Task AytDeletedMessage(string userId,string message);
    }
}
