using DenemeTakipAPI.Application.Repositories.AytRepository;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.AytFolder;
using DenemeTakipAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Repositories.AytRepository
{
    public class AytReadRepository : ReadRepository<AytDeneme>, IAytReadRepository
    {
        public AytReadRepository(denemeTakipAPIDbContext context) : base(context)
        {
        }
    }
}
