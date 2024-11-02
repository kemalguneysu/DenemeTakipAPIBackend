using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DotNetEnv;

namespace DenemeTakipAPI.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                Env.Load();

                return Environment.GetEnvironmentVariable("ConnectionStrings__PostgreSQL");
            }
        }
        static public string MySQLConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                try
                {
                    configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/DenemeTakipAPI.API"));
                    configurationManager.AddJsonFile("appsettings.json");
                }
                catch
                {
                    configurationManager.AddJsonFile("appsettings.Production.json");
                }

                return configurationManager.GetConnectionString("MySQL");
            }
        }
    }
}
