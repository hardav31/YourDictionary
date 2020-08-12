using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YourDictionaryApi.App_Start
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c=> 
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });
        }
        public static void ConfigureSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Your Dictionary API",
                    Description = "some description",
                    TermsOfService = new Uri("https://facebook.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "David Harutyunyan",
                        Email = string.Empty,
                        Url = new Uri("https://facebook.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "My Licensee",
                        Url = new Uri("https://facebook.com"),
                    }
                });
            });
        }
    }
    //public class SwaggerAddParameters : IOperationFilter
    //{
    //    public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
    //    {
    //        if (operation.parameters == null)
    //            operation.parameters = new List<Parameter>();

    //        operation.parameters.Add(new Parameter
    //        {
    //            name = "accept-language",
    //            @in = "header",
    //            type = "string",
    //            description = "Language",
    //            required = true,
    //            @default = Constants.DefaultLanguageId
    //        });
    //    }
    //}
}
