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
                httpClient.Timeout=TimeSpan.FromSeconds(25);
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
        public static IWebHostBuilder UseAutomaticUrls(
    this IWebHostBuilder hostBuilder,
    string appName,
    string hostName = "http://localhost",
    string urlServices = "https://microservicesportchooser.azurewebsites.net/",
            string tag=""
    )
        {
            if (string.IsNullOrWhiteSpace(tag))
                return UseAutomaticUrlsv1(hostBuilder, appName, hostName, urlServices);

            var port = GetPort($"{urlServices}api/v2/PortChooser/GetDeterministicPortFrom/{appName}/{tag}")
                    .GetAwaiter()
                    .GetResult();

            hostBuilder.UseUrls($"{hostName}:{port}");

            return hostBuilder;
        }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="hostBuilder">extension</param>
            /// <param name="appName">you can use ThisAssembly.Project.AssemblyName </param>
            /// <param name="hostName">you can use Environment.MachineName</param>
            /// <param name="urlServices">any url + <see cref="hostname" /> that return the port </param>
            /// <returns></returns>
            private static IWebHostBuilder UseAutomaticUrlsv1(
            this IWebHostBuilder hostBuilder, 
            string appName,
            string hostName= "http://localhost",
            string urlServices = "https://microservicesportchooser.azurewebsites.net/"

            )
        {
            if (string.IsNullOrWhiteSpace(appName))
                return hostBuilder;

            var port = GetPort($"{urlServices}/api/v1/PortChooser/GetDeterministicPortFrom/{appName}")                
                .GetAwaiter()
                .GetResult()
                ;

            if (port < 0)
                return hostBuilder;
            if(string.IsNullOrWhiteSpace(hostName))
                hostName = Environment.MachineName;

            hostBuilder.UseUrls($"{hostName}:{port}");
            return hostBuilder;
        }
    }
}
