using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using agilium.api.infra.Context;
using agilium.api.manager.Configuration;
using agilium.api.manager.Logger;
using AutoMapper;
using KissLog;
using KissLog.AspNetCore;
using KissLog.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace agilium.api.manager
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
             //   builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AgiliumContext>(options =>
            {
                var versaobd_major = Convert.ToInt32(Configuration.GetConnectionString("versaobd-major"));
                var versaobd_minor = Convert.ToInt32(Configuration.GetConnectionString("versaobd-minor"));
                var versaobd_build = Convert.ToInt32(Configuration.GetConnectionString("versaobd-build"));
         
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                      b => b.MigrationsAssembly("agilium.api.manager"));
            });         

            //services.ResolveLogger(Configuration);

            services.AddIdentityConfig(Configuration);

            services.AddAutoMapper(typeof(Startup));

            services.AddApiConfig();

            services.AddSwaggerConfig();

            services.ResolveDependencies(Configuration);

     //       services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider
            //, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory
            )
        {
            app.UseApiConfig(env);

            //app.UseLoggerConfig(env, Configuration);


            app.UseSwaggerConfig(provider);
            app.UseCors(option => option.AllowAnyOrigin()); ;
            //app.UseLoggingConfiguration();


            //loggerFactory.AddProvider(new CustomLoggerProvider(new CustomerLoggerProviderConfiguration
            //{
            //    LogLevel = LogLevel.Information
            //}, env, Configuration));

        }
    }
}
