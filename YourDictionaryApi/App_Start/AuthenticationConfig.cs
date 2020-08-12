using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace YourDictionaryApi.App_Start
{
    public static class AuthenticationConfig
    {
        public static string allowedScope = "companyApi";
        public static string authority = "https://localhost:44326";
        public static void ConfigureAuthentication(this IApplicationBuilder app)
        {
            app.UseIdentityServer();
        }
        public static void ConfigureAuthenticationService(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryApiScopes(AuthenticationConfig.GetApiScopes())
                .AddInMemoryApiResources(AuthenticationConfig.GetApiResources())
                .AddInMemoryIdentityResources(AuthenticationConfig.GetIdentityResources())
                .AddInMemoryClients(AuthenticationConfig.GetClients())
                .AddDeveloperSigningCredential();//AddSigningCredentials 

            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", opt =>
                    {
                        opt.RequireHttpsMetadata = false;
                        opt.Authority = authority;
                        opt.Audience = allowedScope;
                    });

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentutyServer.Cookie";
                config.LoginPath = "/Account/Login";
            });
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "company-employee",
                    ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, allowedScope }
                }
            };
        }

        

        public static IEnumerable<ApiScope> GetApiScopes() 
        {
            return new List<ApiScope> { new ApiScope(allowedScope, "CompanyEmployee API") };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var resource = new ApiResource("companyApi", "CompanyEmployee API")
            {
                Scopes = { "companyApi" }
            };
            return new List<ApiResource> { resource };
        }
    
    }
}
