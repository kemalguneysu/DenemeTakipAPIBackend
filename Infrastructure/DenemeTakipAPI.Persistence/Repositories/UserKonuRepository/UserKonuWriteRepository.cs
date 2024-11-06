using DenemeTakipAPI.Application.Repositories.UserKonuRepository;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using DenemeTakipAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Repositories.UserKonuRepository
{
    public class UserKonuWriteRepository : WriteRepository<UserKonu>, IUserKonuWriteRepository
    {
        public UserKonuWriteRepository(denemeTakipAPIDbContext context) : base(context)
        {
        }
    }
}
