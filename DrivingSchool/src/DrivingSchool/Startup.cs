using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using DrivingSchool.Services;
using DrivingSchool.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DrivingSchool.Entities;

namespace DrivingSchool
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).
                AddJsonFile("appsettings.json").AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary> This method gets called by the runtime. Use this method to add services to
        /// the container. For more information on how to configure your application, visit
        /// go.microsoft.com/fwlink/?LinkID=398940.<summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddDataServices();
            services.AddDbContext<DrivingSchoolDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DrivingSchool"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<DrivingSchoolDbContext>();
            
        }

        /// <summary>This method gets called by the runtime. Use this method to configure the HTTP
        /// request pipeline.</summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseNodeModules(env.ContentRootPath);

            app.UseIdentity();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticChallenge = true,
                LoginPath = new PathString("/Users/Login")
            });

            app.UseMvc(ConfigureRoutes);

            //app.Run(context => context.Response.WriteAsync("Opps! Nothing to see here."));
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder) =>
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
    }
}
