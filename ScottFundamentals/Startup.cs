using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Ef.Core;
using Restaurant.DataAccess.Ef.Infrastructure;

namespace ScottFundamentals
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();

            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.Use(next =>
            //   {
            //       return async context =>
            //       {
            //           logger.LogInformation("Request incoming");
            //           if (context.Request.Path.StartsWithSegments("/mym"))
            //           {
            //               await context.Response.WriteAsync("Hit!!!");
            //               logger.LogInformation("Request handled");
            //           }
            //           else
            //           {
            //               await next(context);
            //               logger.LogInformation("Response outgoing");
            //           }
            //       };
            //   });
            //app.UseWelcomePage(new WelcomePageOptions()
            //{
            //    Path = "wp"
            //});

            //app.Run(async (context) =>
            //{
            //    // dotnet run Greeting="Hello!!!!"
            //    var greeting = configuration["Greeting"];
            //    await context.Response.WriteAsync(greeting);
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
