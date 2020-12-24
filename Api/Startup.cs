
using Api.Middleware;
using Api.Utilities;
using Application;
using DataAccessor;
using DataAccessor.GlobalAccelerex;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CodingTest.Api
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
            services.AddControllers();
            var host = Configuration["DBHOST"]?? "localhost";
            var port = Configuration["DBPORT"]?? "3306";
            var password = Configuration["MYSQL_PASSWORD"]?? Configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = Configuration["MYSQL_USER"]?? Configuration.GetConnectionString("MYSQL_USER");

            services.AddDbContext<GlobalAccelerexDataContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                opt.UseMySQL($"server={host}; userid=root;pwd={password};port={port};database=GlobalAccelerexdb");
            });
            services.AddScoped<ITask, Task>();
            services.AddScoped<IDbInterfacing, DbInterfacing>();
            services.AddScoped<IUtilities, HelperMethods>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IAppUtilites, AppUtilities>();
            services.AddSwaggerGen(c =>
            {
            c.IncludeXmlComments(string.Format(@"{0}/CodingTest.WebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "GlobalAccelerex Coding Test - WebApi",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodingTest.WebApi.xml");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
