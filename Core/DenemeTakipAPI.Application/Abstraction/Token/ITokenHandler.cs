using DenemeTakipAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        //DTOs.Token CreateAccessToken(int day, AppUser user);
        Task<DTOs.Token> CreateAccessToken(int day, AppUser user);

        string CreateRefreshToken();
    }
}
