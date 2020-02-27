﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Team_4_Project.Models;

//TODO: Once you have a database on Azure, you will need to uncomment this line 
//and make the name match your project name
using Team_4_Project.DAL;

//Ex.  namespace Gray_Katie_HWX
namespace Team_4_Project
{
    public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
            //TODO: Add a connection string here once you have created it on Azure
            String connectionString = "Server = tcp:finaldatabase.database.windows.net,1433; Initial Catalog = finaldatabase; Persist Security Info = False; User ID = MISAdmin; Password =Password123; MultipleActiveResultSets = true; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30";
            //TODO: Uncomment this line once you have your connection string
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            //TODO: Uncomment these lines once you have added Identity to your project
            ////NOTE: This is where you would change your password requirements
            services.AddIdentity<AppUser, IdentityRole>(opts => {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider service)
    {
        //These lines allow you to see more detailed error messages
        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();

        //This line allows you to use static pages like style sheets and images
        app.UseStaticFiles();
        app.UseAuthentication();

            //This line configures the routing for MVC
            app.UseMvc(routes => {
            routes.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" });
        });

            //TODO: Once you have added Identity into your project, you will need to uncomment this line
            //Seeding.SeedIdentity.AddAdmin(service).Wait();
            //Seeding.SeedCustomers.AddCustomer(service).Wait();
            //Seeding.SeedEmployees.AddEmployee(service).Wait();
            //Seeding.SeedAccounts.SeedAllAccounts(service);
            //Seeding.SeedPayees.SeedAllPayees(service);
            //Seeding.SeedStocks.SeedAllStocks(service);
        }
    }
}
