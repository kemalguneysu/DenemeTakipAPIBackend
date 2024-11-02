using DenemeTakipAPI.Application.Repositories.KonuRepository;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using DenemeTakipAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Repositories.KonuRepository
{
    public class KonuWriteRepository : WriteRepository<Konu>, IKonuWriteRepository
    {
        public KonuWriteRepository(denemeTakipAPIDbContext context) : base(context)
        {
        }
    }
}
