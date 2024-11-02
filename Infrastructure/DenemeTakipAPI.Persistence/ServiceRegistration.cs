using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.Repositories.AytRepository;
using DenemeTakipAPI.Application.Repositories.DersRepository;
using DenemeTakipAPI.Application.Repositories.KonuRepository;
using DenemeTakipAPI.Application.Repositories.TytRepository;
using DenemeTakipAPI.Domain.Entities.Identity;
using DenemeTakipAPI.Persistence.Contexts;
using DenemeTakipAPI.Persistence.Repositories.AytRepository;
using DenemeTakipAPI.Persistence.Repositories.DersRepository;
using DenemeTakipAPI.Persistence.Repositories.KonuRepository;
using DenemeTakipAPI.Persistence.Repositories.TytRepository;
using DenemeTakipAPI.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            //services.AddDbContext<denemeTakipAPIDbContext>(options => options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=denemeTakipDatabase;"));
            services.AddDbContext<denemeTakipAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            //services.AddDbContext<denemeTakipAPIDbContext>(options => options.UseMySQL(Configuration.MySQLConnectionString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<denemeTakipAPIDbContext>()
            .AddDefaultTokenProviders();
            

            services.AddScoped<ITytReadRepository, TytReadRepository>();
            services.AddScoped<ITytWriteRepository,TytWriteRepository>();

            services.AddScoped<IAytReadRepository, AytReadRepository>();
            services.AddScoped<IAytWriteRepository,AytWriteRepository>();

            services.AddScoped<IKonuReadRepository, KonuReadRepository>();
            services.AddScoped<IKonuWriteRepository,KonuWriteRepository>();

            services.AddScoped<IDersReadRepository, DersReadRepository>();
            services.AddScoped<IDersWriteRepository, DersWriteRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IKonuService, KonuService>();
            services.AddScoped<IAytService, AytService>();
            services.AddScoped<ITytService, TytService>();
            services.AddScoped<IDersService, DersService>();

        }
    }
}
