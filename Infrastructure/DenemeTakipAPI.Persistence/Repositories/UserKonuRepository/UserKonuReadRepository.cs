using DenemeTakipAPI.Application.Repositories.TytRepository;
using DenemeTakipAPI.Application.Repositories.UserKonuRepository;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using DenemeTakipAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Repositories.UserKonuRepository
{
    public class UserKonuReadRepository : ReadRepository<UserKonu>, IUserKonuReadRepository
    {
        public UserKonuReadRepository(denemeTakipAPIDbContext context) : base(context)
        {
        }
    }
}
