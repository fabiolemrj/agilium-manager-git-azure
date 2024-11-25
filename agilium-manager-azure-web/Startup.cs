using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using agilium.webapp.manager.mvc.Configuration;
using agilium.webapp.manager.mvc.Extensions;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace agilium.webapp.manager.mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IKLogger>((provider) => Logger.Factory.Get());

            //services.AddLogging(logging =>
            //{
            //    logging.AddKissLog(options =>
            //    {
            //        options.Formatter = (FormatterArgs args) =>
            //        {
            //            if (args.Exception == null)
            //                return args.DefaultValue;

            //            string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);

            //            return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
            //        };
            //    });
            //});

            //services.AddLogging(logging =>
            //{
            //    logging.AddKissLog();
            //});

            services.AddCors();
            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews();
            services.AddIdentityConfiguration();
            services.AddHealthChecks();
            services.AddRazorPages();
            services.AddMvcConfiguration(Configuration);
            services.RegisterServices(Configuration);
            services.AddApiVersioning();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(3);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            //app.UseMvcConfiguration(env);
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //   // The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //// app.UseKissLogMiddleware(options => ConfigureKissLog());

            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMiddleware<ExceptionMiddleware>();

            var cultura = new CultureInfo("pt-BR");
          
            var dateformat = new DateTimeFormatInfo
            {
                ShortDatePattern = "dd/MM/yyyy",
                LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
            };
            cultura.DateTimeFormat= dateformat;

            var supportedCultures = new[] { cultura };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
              //  endpoints.MapControllers();
               // endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
             //   endpoints.MapAreaControllerRoute("Back", "Back", "back/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCookiePolicy();
        }


        //private void ConfigureKissLog(IOptionsBuilder options)
        //{
        //    KissLogConfiguration.Listeners
        //        .Add(new RequestLogsApiListener(new Application(Configuration["KissLog.OrganizationId"], Configuration["KissLog.ApplicationId"]))
        //        {
        //            ApiUrl = Configuration["KissLog.ApiUrl"]
        //        });
        //}

        //private void ConfigureKissLog()
        //{
        //    KissLogConfiguration.Listeners
        //        .Add(new CustomMongoDbListener(Configuration["KissLog.BdLog"], "LogFront"));
        //}
    }
}
