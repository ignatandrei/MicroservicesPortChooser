using Dapper;
using Microsoft.Data.Sqlite;
using MSPC_Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MSPC_DAL
{
    public class Repository : IRepository
    {
        public static string DbName = "Data Source=MSPC.db";

        public async Task<IRegister> AddRegister(IRegister r)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", r.Name);
            parameters.Add("@HostName", r.HostName);
            parameters.Add("@Port", r.Port);
            parameters.Add("@Tag", r.Tag);
            parameters.Add("@Authority", r.Authority);
            parameters.Add("@PCName", r.PCName);
            using var connection = new SqliteConnection(DbName);

            await connection.ExecuteAsync(
                "insert into MSPC_Register(Name, Hostname,Port, Tag, Authority, PCName) values" +
                "(@Name, @HostName,@Port, @Tag, @Authority,@PCName)", parameters);
            return r;
        }
        public Task<int> UnRegister(string host, UInt16 port)
        {
            using var connection = new SqliteConnection(DbName);
            return connection.ExecuteAsync(
                "delete from MSPC_Register where Hostname= @host and Port= @port "
                , new { host, port });

        }
        public async Task<IRegister[]> LoadFromDatabase()
        {
            using var connection = new SqliteConnection(DbName);
            var data = await connection.QueryAsync<RegFake>("select * from MSPC_Register");
            return data.ToArray();
        }
    }
    class RegFake : IRegister
    {
        public string Authority { get; set; }
        public DateTimeOffset dateRegistered { get; set; } 

        public string HostName { get; set; }
        public string Name { get; set; } 
        public string PCName { get; set; }
        public int Port { get; set; }
        public string Tag { get; set; }

        public string UniqueID { get; set; }
    }
}