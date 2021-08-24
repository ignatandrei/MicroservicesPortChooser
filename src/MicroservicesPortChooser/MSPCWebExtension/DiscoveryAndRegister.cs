using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Console.WriteLine("not null!!!" + string.Join(",", add));
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
