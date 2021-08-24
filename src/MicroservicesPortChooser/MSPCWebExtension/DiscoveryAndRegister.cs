using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSPCWebExtension
{
    public class DiscoveryAndRegister : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public DiscoveryAndRegister(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var s = serviceProvider.GetService(typeof(IServer)) as IServer;
                var add = s?.Features?.Get<IServerAddressesFeature>()?.Addresses?.ToArray();
                if ((add?.Length ??0) > 0)
                {
                    await Task.Delay(5000, stoppingToken);
                    using var h = new HttpClient();
                    h.BaseAddress = new Uri("https://microservicesportchooser.azurewebsites.net/api/v1/");
                    var p = new PortService(h);
                    foreach (var item in add) {
                        var u = new Uri(item);
                        var r = new Register("andrei",u.Host, (UInt16)u.Port,"tag");
                        await p.AddNew(r);
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
