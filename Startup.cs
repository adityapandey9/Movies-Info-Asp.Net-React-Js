using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using moviesnet.Services;

namespace moviesnet
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Send the DBContext to the Controllers
            var connectionString =
"Server=localhost;Database=MoviesDB;User Id=sa;Password=Passw0rd!";
            services.AddDbContext<MoviesDbContext>(o =>
                   o.UseSqlServer(connectionString));

            //Add MVC with Json.
            services.AddMvc()
                .AddJsonOptions(s => s.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Add CorsPolicy for API
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin() 
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                        
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
               MoviesDbContext moviesDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            app.UseStaticFiles();
            //Create Seeds
            moviesDbContext.CreateSeedData();
            //Implement API routes in MVC
            app.UseMvc((routes)=> {
                routes.MapRoute(name: "api", template: "api/{controller}");
                routes.MapRoute(name: "apiaction", template: "api/{controller}/{action}");
                routes.MapRoute(name: "apiactionid", template: "api/{controller}/{action}/{id}");
            });
            //Send index.html file
            app.Run(async (context) => {
                await context.Response.SendFileAsync("wwwroot/index.html");
            });
        }
    }
}
