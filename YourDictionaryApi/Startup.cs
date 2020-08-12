using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourDictionaryApi.App_Start;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;

namespace YourDictionaryApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureCoreServices(services);
            this.ConfigureMVCMiddleware(services);
            this.ConfigureModelValidation(services);

            this.ConfigureCustomServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseExceptionHandler(builder =>
                builder.Run(async context =>
                {
                    var handler = context.RequestServices.GetRequiredService<ApiExceptionHandler>();
                    await handler.HandleAsync(context);
                })
            );

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            this.ConfigureCustom(app);

            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            //app.UseMiddleware<MiddlewareLog>();
            app.UseRouting();
            
            //login
            app.UseAuthentication();
            //role
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

            //SwaggerConfiguration.ConfigSwaggerEndpoints(app, Configuration);
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(name: "Default", template: "{controller}/{action}");
            //});
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //});
            //app.Map("/values", branch =>
            //{
            //    branch.Run(async context =>
            //    {
            //        string path = context.Request.Path;
            //        context.Response.StatusCode = 200;
            //    });
            //});
        }

        private void ConfigureCustom(IApplicationBuilder app)
        {
            app.ConfigureAuthentication();
            app.ConfigureSwagger();
        }
        private void ConfigureCustomServices(IServiceCollection services)
        {
            services.ConfigureAuthenticationService();
            services.ConfigureSwaggerService();
            services.ConfigureAutoMapper();
        }
        private void ConfigureCoreServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddSingleton<ApiExceptionHandler, ApiExceptionHandler>();
            

            //services.AddDbContextPool<PlayersContext>(options =>
            //    options.UseNpgsql(Configuration.GetConnectionString("PlayersDatabaseConnection")));
            //services.AddDbContextPool<BuilderDbContext>(options =>
            //   options.UseNpgsql(Configuration.GetConnectionString("BuilderDatabaseConnection")));

            //services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //services.AddScoped<IUrlHelper>(implementationFactory =>
            //{
            //    ActionContext actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
            //    return new UrlHelper(actionContext);
            //});

            //services.AddAuthorization(options =>
            //           options.AddPolicy("Player", policy =>
            //               policy.RequireAssertion(context =>
            //               {
            //                   bool result = context.User.HasClaim(c => c.Type == ClaimNames.PlayerId)
            //                       && context.User.HasClaim(c => c.Type == ClaimNames.BrandId);
            //                   return result;
            //               })));

            //services.Configure<PlayerSettings>(Configuration.GetSection("PlayerSettings"));
            //services.Configure<ClientConfiguration>(Configuration.GetSection("ClientConfiguration"));
            //services.AddAuthentication(config =>
            //{
            //    config.DefaultAuthenticateScheme = "ClientCookie";
            //    config.DefaultSignInScheme = "ClientCookie";
            //    config.DefaultChallengeScheme = "ClientCookie";
            //    config.
            //})
            //    .AddCookie("ClientCookie")
            //    .AddOAuth("OutServer", config => {
            //        config.ClientId = "client_id";
            //        config.ClientSecret = "client_secret";
            //        config.CallbackPath = "/token";
            //        config.
            //    });


            services.AddHttpContextAccessor();
            services.AddSingleton(Configuration);

        }
        private void ConfigureMVCMiddleware(IServiceCollection services)
        {
            services.AddControllers(setupAction =>
            {
                //setupAction.Filters.Add(
                //    new ProducesResponseTypeAttribute(typeof(SiteErrorResult), StatusCodes.Status400BadRequest));
                //setupAction.Filters.Add(
                //   new ProducesResponseTypeAttribute(typeof(SiteErrorResult), StatusCodes.Status404NotFound));
                //setupAction.Filters.Add(
                //    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                //setupAction.Filters.Add(
                //    new ProducesResponseTypeAttribute(typeof(SiteErrorResult), StatusCodes.Status500InternalServerError));

                //setupAction.InputFormatters.Add(new CsvInputFormatter());
                //setupAction.OutputFormatters.Add(new CsvOutputFormatter());

                setupAction.ReturnHttpNotAcceptable = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                /* options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;*/
            });
        }
        private void ConfigureModelValidation(IServiceCollection services)
        {
            //services.Configure<ApiBehaviorOptions>(opt => opt.InvalidModelStateResponseFactory = context => context.ModelState.GetValidationActionResult());
        }
    }
}
