using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PostGISWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("dbsettings.json", optional: false, reloadOnChange: false)
                .Build();
            CreateHostBuilder(args, configuration).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration config) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(config).UseStartup<Startup>();
                });
    }
}
