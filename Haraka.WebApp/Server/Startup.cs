using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Haraka.WebApp.Shared.Services;
using System;
using Haraka.Runtime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Haraka.Core;
using Microsoft.AspNetCore.Mvc.Controllers;
using Haraka.Core.IoC;
using Haraka.WebApp.Shared;
using System.Collections;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Haraka.Model;

namespace Haraka.WebApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var typeContainer = TypeContainer.Get<ITypeContainer>();
            SQLite.SqliteStartup.Register(typeContainer);
            RegisterController(typeContainer);
            typeContainer.Register<IControllerActivator, CustomControllerActivator>(InstanceBehaviour.Singleton);

            services.AddControllersWithViews();
            services.AddSingleton(typeContainer.Get<IControllerActivator>());

            typeContainer.Register<JobService>(InstanceBehaviour.Singleton);
            typeContainer.Register<GameService>(InstanceBehaviour.Singleton);
            typeContainer.Register(typeof(GameProvider), typeof(GameProvider), GameProvider.Create());

            var sessionService = new UserSessionService($"{nameof(Haraka)}.{nameof(Server)}.JWT");
            typeContainer.Register<IUserSessionService>(sessionService);
            
            sessionService.LoadOrCreateKey();
            typeContainer.Get<IDbProvider>().RegisterDatabase("main.db", typeContainer);

            services
                .AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        IssuerSigningKey = sessionService.Key,
                        ValidIssuer = sessionService.Issuer

                    };
                    jwt.SecurityTokenValidators.Clear();
                    jwt.SecurityTokenValidators.Add(sessionService);

#if DEBUG
                    jwt.RequireHttpsMetadata = false;
#endif
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }

        public void RegisterController(ITypeContainer typeContainer)
        {
            Assembly
                .GetAssembly(typeof(Startup))
                .GetTypes()
                .Where(t => t.GetCustomAttribute<RouteAttribute>() != null)
                .ForEach(t => typeContainer.Register(t, t, InstanceBehaviour.Instance));
        }
    }
}