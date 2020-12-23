using System;
using Data;
using DataAccessor.GlobalAccelerex;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CodingTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {     
            var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main function");
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<GlobalAccelerexDataContext>();
                        context.Database.EnsureCreated();
                        Seed.SeedData(context);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "An Error Occured during Migration");
                    }
                }
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), "Error in init");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                }).UseNLog();
    }

}
