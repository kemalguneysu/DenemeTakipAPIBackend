using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Hubs
{
    public interface IUserKonuHubService
    {
        Task UserKonuAddedMessage(string userId, string message);
        Task UserKonuUpdatedMessage(string userId, string message);
        Task UserKonuDeletedMessage(string userId, string message);
    }
}
