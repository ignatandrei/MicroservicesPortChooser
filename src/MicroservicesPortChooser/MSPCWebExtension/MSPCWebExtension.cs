using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSPCWebExtension
{
    public static class Extensions
    {
        private static async Task<int> GetPort(string url)
        {
            HttpClient httpClient = null;
            try
            {
                //https://microservicesportchooser.azurewebsites.net/api/v1/PortChooser/GetDeterministicPortFrom/{name}
                httpClient = new HttpClient();

                var port = await httpClient.GetStringAsync(url);
                return int.Parse(port);
                // Console.WriteLine(port);
            }
            catch
            {
                return -1;
            }
            finally
            {
                httpClient?.Dispose();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostBuilder">extension</param>
        /// <param name="appName">you can use ThisAssembly.Project.AssemblyName </param>
        /// <param name="hostName">you can use Environment.MachineName</param>
        /// <param name="urlServices">any url + <see cref="hostname" /> that return the port </param>
        /// <returns></returns>
        public static IWebHostBuilder UseAutomaticUrls(
            this IWebHostBuilder hostBuilder, 
            string appName,
            string hostName= "localhost",
            string urlServices = "https://microservicesportchooser.azurewebsites.net/api/v1/PortChooser/GetDeterministicPortFrom/"

            )
        {
            if (string.IsNullOrWhiteSpace(appName))
                return hostBuilder;

            var port = GetPort( urlServices + appName)
                
                .GetAwaiter()
                .GetResult()
                ;

            if (port < 0)
                return hostBuilder;
            if(string.IsNullOrWhiteSpace(hostName))
                hostName = Environment.MachineName;

            hostBuilder.UseUrls($"http://{hostName}:{port}");
            return hostBuilder;
        }
    }
}
