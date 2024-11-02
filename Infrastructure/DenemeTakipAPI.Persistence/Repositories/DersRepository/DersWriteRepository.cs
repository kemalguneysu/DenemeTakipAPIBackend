using DenemeTakipAPI.Application.Repositories.DersRepository;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using DenemeTakipAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Repositories.DersRepository
{
    public class DersWriteRepository : WriteRepository<Ders>, IDersWriteRepository
    {
        public DersWriteRepository(denemeTakipAPIDbContext context) : base(context)
        {
        }
    }
}
