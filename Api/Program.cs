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
