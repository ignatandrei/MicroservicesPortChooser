﻿using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MSPC_DAL;
using MSPC_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MSPCWebExtension
{
    public class DiscoveryAndRegister : BackgroundService
    {
        private readonly IServiceProvider sp;
        private readonly IConfiguration config;
        

        public DiscoveryAndRegister(IServiceProvider sp, IConfiguration config)
        {
            this.sp = sp;
            this.config = config;            
        }
        private async Task<bool> RegisterMe(IServer s, CancellationToken stoppingToken)
        {
            if (s == null)
                return false;
            var add = s?.Features?.Get<IServerAddressesFeature>()?.Addresses?.ToArray();
            var nrAddresses = add?.Length ?? 0;
            Console.WriteLine($"nr adresses {nrAddresses}");
            if (nrAddresses == 0)
                return false;

            var configData = new ConfigDataMSPC();
            config.GetSection("MSPC").Bind(configData);
            if(configData == null)
            {
                configData = new ConfigDataMSPC();
            }
            configData.ConfigureDefaults();

            await Task.Delay(5000, stoppingToken);
            using var h = new HttpClient();
            h.BaseAddress = new Uri(configData.registerUrl);
            string machineName = Environment.MachineName;
            var repo = sp.GetService(typeof(IRepository)) as IRepository;
            if(repo == null)
            {
                repo = new Repository();
            }
            var p = new PortService(h);
            foreach (var addres in add)
            {
                var url = addres;
                if (url.Contains("*"))
                    url = url.Replace("*", machineName);

                Console.WriteLine($"try to register {url}");
                var u = new Uri(url);
                string host = u.Host;
                if (host == "[::]" || host == "0.0.0.0" || host=="::"|| host=="*")
                    host = machineName;

                var r = new Register(repo);
                r.Name = configData.appName;
                r.HostName = host;
                r.Port = u.Port;
                r.Tag = configData.tag;
                r.Authority = u.Authority;
                r.PCName = Environment.MachineName;
                var env = new Helpers().FromStaticEnvironment();                
                r.EnvData = JsonSerializer.Serialize(env);

                try
                {
                    await p.AddNew(r);
                    Console.WriteLine($"done register {url}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error in register {url} : {ex.Message}");
                    return false;
                }
            }
            return true;
        }
        ILogger log;
        private void LogInfo(string message)
        {
            if (log == null) {
                log = sp.GetService(typeof(ILogger<DiscoveryAndRegister>)) as ILogger;
            }
            if(log == null)
            {
                Console.WriteLine(message);
                return;
            }
            log.LogInformation(message);

        }
        private void LogError(string message, Exception ex)
        {
            if (log == null)
            {
                log = sp.GetService(typeof(ILogger<DiscoveryAndRegister>)) as ILogger;
            }
            if (log == null)
            {
                Console.WriteLine($"{message} : {ex.Message}");
                return;
            }
            log.LogError(ex,message);

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                var s = sp.GetService(typeof(IServer)) as IServer;

                LogInfo($"waiting for IServer : {s == null}");               
                await Task.Delay(5000, stoppingToken);
                try
                {
                    var success= await RegisterMe(s, stoppingToken);
                    if (success)
                        return;
                }
                catch (Exception ex)
                {
                    LogError("error in register",ex);
                }

            }
        }
    }
}
