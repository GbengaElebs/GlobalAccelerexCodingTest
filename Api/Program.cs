using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Data;
using DataAccessor.GlobalAccelerex;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;

namespace CodingTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
            // var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            // var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            // NLog.LogManager.LogFactory.SetCandidateConfigFilePaths(new List<string> { $"{Path.Combine(pathToContentRoot, "nlog.config")}" });
            // var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();


            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("InfoLogs") { FileName = "log.txt" };
            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Info, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
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
                }).UseNLog();
    }

}
