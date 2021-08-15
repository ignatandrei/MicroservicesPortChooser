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

namespace MicroservicesPortChooserWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static async Task<int> GetPort(string url)
        {
            HttpClient httpClient=null;
            try{
            //https://microservicesportchooser.azurewebsites.net/api/v1/PortChooser/GetDeterministicPortFrom/{name}
            httpClient =new HttpClient();
            
            var port = await httpClient.GetStringAsync(url);            
            return int.Parse(port);
            // Console.WriteLine(port);
            }
            catch  {
                return -1;
            }
            finally{
                httpClient?.Dispose();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
