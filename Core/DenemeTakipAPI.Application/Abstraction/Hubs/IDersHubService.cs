using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Hubs
{
    public interface IDersHubService
    {
        Task DersAddedMessage(string message);
        Task DersDeletedMessage(string message);
        Task DersUpdatedMessage(string message);

    }
}
