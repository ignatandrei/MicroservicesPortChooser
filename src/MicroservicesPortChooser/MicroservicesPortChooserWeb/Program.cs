using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MSPCWebExtension;
using System.IO;

namespace MicroservicesPortChooserWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    //cannout use his own url ;-) 
    //                .UseAutomaticUrls(
    //ThisAssembly.Project.AssemblyName,
    //$"http://{Environment.MachineName}"
    //)
                    //.UseAutomaticUrls(
                    //    ThisAssembly.Project.AssemblyName,
                    //    $"http://{Environment.MachineName}",
                    //    //download app and use your own url
                    //    "https://microservicesportchooser.azurewebsites.net/",
                    //    new DirectoryInfo(Environment.CurrentDirectory).Name
                    //    )
                    ;
                   
                });
    }
}
