using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesPortChooserBL
{
    public class Register
    {
        private static ConcurrentDictionary<string, Register> register = new ConcurrentDictionary<string, Register>();
        static string DbName = "Data Source=MSPC.db";
        public async static Task<Register> AddRegister(Register r)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", r.Name);
            parameters.Add("@HostName", r.HostName);
            parameters.Add("@Port", r.Port);
            parameters.Add("@Tag", r.Tag);
            parameters.Add("@Authority", r.Authority);
            using var connection = new SqliteConnection(DbName);
            
            await connection.ExecuteAsync(
                "insert into MSPC_Register(Name, Hostname,Port, Tag, Authority) values" +
                "(@Name, @HostName,@Port, @Tag, @Authority)", parameters);
            register.AddOrUpdate(r.UniqueID, r, (key, r) => r);
            return r; 
        }
        public static Register[] RegisteredMSPC()
        {
            return register.ToArray().Select(it => it.Value).ToArray();
        }
        public static Task<Register> AddNew(string name,string host, UInt16 port, string tag = "",string authority="http")
        {
            return AddRegister(new Register(name, host, port, tag, authority));
        } 
        public static bool UnRegister(string host, UInt16 port)
        {
            using var connection = new SqliteConnection(DbName);
            connection.Execute(
                "delete from MSPC_Register where Hostname= @host and Port= @port "
                , new { host, port });
            var r = new Register(host, host, port);
            return register.Remove(r.UniqueID, out _);
        }
        public static async Task<int> LoadFromDatabase()
        {
            using var connection = new SqliteConnection(DbName);
            var data = await connection.QueryAsync<Register>("select * from MSPC_Register");
            foreach(var r in data)
            {
                register.AddOrUpdate(r.UniqueID, r, (key, r) => r);
            }
            return data.Count();
        }
        public Register()
        {
            this.dateRegistered = DateTime.UtcNow;
        }
        public Register(string name,string host,UInt16 port, string tag="", string authority="http")
        {
            this.dateRegistered = DateTime.UtcNow;
            this.Name = name;
            this.HostName = host;
            this.Port = port;
            this.Tag = tag;
            Authority = authority;
        }
        public DateTimeOffset dateRegistered { get; private set; }
        public string UniqueID
        {
            get
            {
                return $"{HostName}_{Port}";
            }
        }
        public string Name { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Tag { get; set; }
        public string Authority { get; }
    }
}
