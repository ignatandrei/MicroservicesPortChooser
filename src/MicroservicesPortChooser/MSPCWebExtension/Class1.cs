using Microsoft.AspNetCore.Hosting;
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
        public static IWebHostBuilder UseAutomaticUrls(this IWebHostBuilder hostBuilder, string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
                return hostBuilder;

            var port = GetPort("https://microservicesportchooser.azurewebsites.net/api/v1/PortChooser/GetDeterministicPortFrom/" + appName)
                
                .GetAwaiter()
                .GetResult()
                ;

            if (port < 0)
                return hostBuilder;

            var host = hostBuilder.GetSetting("hostName");
            if (string.IsNullOrWhiteSpace(host))
                host = Environment.MachineName;

            hostBuilder.yser
            return hostBuilder;
        }
    }
}
