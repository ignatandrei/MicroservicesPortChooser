using Dapper;
using Microsoft.Data.Sqlite;
using MSPC_DAL;
using MSPC_Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesPortChooserBL
{
    public class Register : IRegister
    {
        private static ConcurrentDictionary<string, IRegister> register = new ConcurrentDictionary<string, IRegister>();
        private readonly IRepository repository;

        public async Task<Register> AddRegister(Register r)
        {
            await this.repository.AddRegister(r);
            register.AddOrUpdate(r.UniqueID, r, (key, r) => r);
            return r;
        }
        public static IRegister[] RegisteredMSPC()
        {
            return register.ToArray().Select(it => it.Value).ToArray();
        }
        public Task<Register> AddNew(string name, string host, int port, string tag = "", string authority = "http")
        {
            var r = new Register(repository);
            r.Name = name;
            r.HostName = host;
            r.Port = port;
            r.Tag = tag;
            r.Authority = authority;
            return AddRegister(r);
        }
        public async Task<bool> UnRegister(string host, UInt16 port)
        {
            await repository.UnRegister(host, port);
            var r = new Register(repository);
            r.HostName = host;
            r.Name = host;
            r.Port = port;
            return register.Remove(r.UniqueID, out _);
        }
        public async Task<int> LoadFromDatabase()
        {
            var data = await repository.LoadFromDatabase();
            foreach (var r in data)
            {
                register.AddOrUpdate(r.UniqueID, r, (key, r) => r);
            }
            return data.Count();
        }
        public Register(IRepository repository):base()
        {
            this.repository = repository;
        }

        private  Register()
        {
            this.dateRegistered = DateTime.UtcNow;
        }
        //public Register(string name, string host, int port, string tag = "", string authority = "http"):base()
        //{
            
        //    this.Name = name;
        //    this.HostName = host;
        //    this.Port = port;
        //    this.Tag = tag;
        //    Authority = authority;
        //}
        public DateTimeOffset dateRegistered { get; private set; }
        public string UniqueID
        {
            get
            {
                var name = $"{HostName}_{Port}";
                if (PCName?.Trim().Length > 0)
                    return name += $"_{PCName}";

                return name;
            }
        }
        public string PCName { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Tag { get; set; }
        public string Authority { get; set; }
    }
}
