using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters(); //Note: Here we are adding MVC model to our project.
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddIdentity<ApplicationUser,  IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10; //Method2: For configuring the default password.
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<AppDbContext>();

            //Method1: For configuring the default password.
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequiredUniqueChars = 3;
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) 
        {
            if (env.IsDevelopment()) //This is for development environment.
            {
                app.UseDeveloperExceptionPage();
            }
            else //This is for other environments.
            {
                //app.UseStatusCodePages();
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute(); //Method1
            app.UseMvc(routes => { routes.MapRoute("default", "{Controller=Home}/{Action=Index}/{id?}"); });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");  
            //});
        }
    }
}

















