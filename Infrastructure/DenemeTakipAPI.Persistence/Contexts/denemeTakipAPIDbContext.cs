using DenemeTakipAPI.Domain.Entities.Common;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.AytFolder;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using DenemeTakipAPI.Domain.Entities.Identity;
using DenemeTakipAPI.Domain.Entities.ToDo;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Contexts
{
    public class denemeTakipAPIDbContext:IdentityDbContext<AppUser, AppRole, string>
    {
        public denemeTakipAPIDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<AytDeneme> AytDenemes { get; set; }
        public DbSet<TytDeneme> TytDenemes { get; set; }
        public DbSet<Konu> Konular { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<UserKonu> UserKonular { get; set; }
        public DbSet<ToDoElement> ToDoElements{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<TytDeneme>()
                .HasMany(a => a.BosKonular)
                .WithMany(b => b.TytDenemesBos);
            builder.Entity<TytDeneme>()
                .HasMany(a => a.YanlisKonular)
                .WithMany(b => b.TytDenemesYanlis);

            builder.Entity<AytDeneme>()
                .HasMany(a => a.YanlisKonular)
                .WithMany(b => b.AytDenemesYanlis);
            builder.Entity<AytDeneme>()
                .HasMany(a => a.BosKonular)
                .WithMany(b => b.AytDenemesBos);
            

            builder.Entity<IdentityUserLogin>().HasNoKey();
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                //_= data.State switch
                //{
                //    EntityState.Added => data.Entity.CreatedDate=DateTime.UtcNow,
                //    //EntityState.Modified=> data.Entity.UpdatedDate=DateTime.UtcNow,
                //};
                if (data.State == EntityState.Added)
                {
                    data.Entity.CreatedDate = DateTime.UtcNow;
                }

            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
