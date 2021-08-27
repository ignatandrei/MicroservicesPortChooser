using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSPCWebExtension
{
    public class DiscoveryAndRegister : BackgroundService
    {
        private readonly IConfiguration config;
        private readonly IServiceProvider serviceProvider;

        public DiscoveryAndRegister(IConfiguration config, IServiceProvider serviceProvider)
        {
            this.config = config;
            this.serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var configData = new ConfigDataMSPC();
            config.GetSection("MSPC").Bind(configData);
            if (string.IsNullOrWhiteSpace(configData.appName))
                configData.appName = Assembly.GetEntryAssembly().GetName().Name;
            if (string.IsNullOrWhiteSpace(configData.registerUrl))
                configData.appName = "https://microservicesportchooser.azurewebsites.net/api/v1/";

            while (!stoppingToken.IsCancellationRequested)
            {
                var s = serviceProvider.GetService(typeof(IServer)) as IServer;
                var add = s?.Features?.Get<IServerAddressesFeature>()?.Addresses?.ToArray();
                if ((add?.Length ??0) > 0)
                {
                    await Task.Delay(5000, stoppingToken);
                    using var h = new HttpClient();
                    h.BaseAddress = new Uri(configData.registerUrl);
                    var p = new PortService(h);
                    foreach (var item in add) {
                        var u = new Uri(item);
                        string host = u.Host;
                        if (host == "[::]" || host=="0.0.0.0")
                            host = Environment.MachineName;

                        var r = new Register(configData.appName, host, u.Port, configData.tag, u.Authority);
                        try
                        {
                            await p.AddNew(r);
                            Console.WriteLine($"done register {item}");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("error register: " + ex.Message);
                        }
                    }
                    
                    //Console.WriteLine("not null!!!" + string.Join(",", add));
                    return;
                }
                else
                {
                    Console.WriteLine("waiting");
                }
                await Task.Delay(5000, stoppingToken);

            }
        }
    }
}
