using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenemeTakipAPI.Application.Abstraction.Token;
using DenemeTakipAPI.Infrastructure.Services;
using FluentValidation;
using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Application.Validators.TytValidators;
using DenemeTakipAPI.Application.Abstraction.Services;

namespace DenemeTakipAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IMailService, MailService>();
        }


    }
}
