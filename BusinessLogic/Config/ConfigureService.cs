using BusinessLogic.Service;
using BusinessLogic.Service.Common;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Config
{
    public static class ConfigureService
    {
        public static void ConfgureBllServices(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DictionaryDbConstr");

            services.AddDbContext<YourDictionaryDataEntities>(options => options.UseSqlServer(connectionString),ServiceLifetime.Singleton);
            services.AddScoped<YourDictionaryDbContext, YourDictionaryDbContext>();

            //per scope
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IWordService, WordService>();


        }
    }
}
