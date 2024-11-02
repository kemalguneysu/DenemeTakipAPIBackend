using DenemeTakipAPI.Application.Repositories.TytRepository;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using DenemeTakipAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Repositories.TytRepository
{
    public class TytWriteRepository : WriteRepository<TytDeneme>, ITytWriteRepository
    {
        public TytWriteRepository(denemeTakipAPIDbContext context) : base(context)
        {
        }
    }
}
