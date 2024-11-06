using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenemeTakipAPI.Persistence.Contexts;

namespace DenemeTakipAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<denemeTakipAPIDbContext>
    {
        public denemeTakipAPIDbContext CreateDbContext(string[] args)
        {

            DbContextOptionsBuilder<denemeTakipAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=denemeTakipDatabase;");
            //dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            //dbContextOptionsBuilder.UseMySQL(Configuration.MySQLConnectionString);

            return new(dbContextOptionsBuilder.Options);
        }
    }
}
