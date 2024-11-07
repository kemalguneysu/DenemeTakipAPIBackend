using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Hubs
{
    public interface IKonuHubService
    {
        Task KonuAddedMessage(string message);
        Task KonuUpdatedMessage(string message);
        Task KonuDeletedMessage(string message);


    }
}
