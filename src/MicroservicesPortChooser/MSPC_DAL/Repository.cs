using Dapper;
using Microsoft.Data.Sqlite;
using MSPC_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSPC_DAL
{
    public class Repository : IRepository
    {
        public static string DbName1 = "Data Source=MSPC.db";
        public static string DbName = @"Data Source=C:\Users\Surface1\Desktop\MSPC.db";

        public async Task<IRegister> AddRegister(IRegister r)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", r.Name);
            parameters.Add("@HostName", r.HostName);
            parameters.Add("@Port", r.Port);
            parameters.Add("@Tag", r.Tag);
            parameters.Add("@Authority", r.Authority);
            parameters.Add("@PCName", r.PCName);
            parameters.Add("@stringRegisteredDate", r.dateRegistered.ToString("o"));
            parameters.Add("@EnvData", r.EnvData);
            using var connection = new SqliteConnection(DbName);

            await connection.ExecuteAsync(
                "insert into MSPC_Register(Name, Hostname,Port, Tag, Authority, PCName,stringRegisteredDate, EnvData) values" +
                "(@Name, @HostName,@Port, @Tag, @Authority,@PCName,@stringRegisteredDate, @EnvData)", parameters);
            return r;
        }
        public Task<int> UnRegister(string host, UInt16 port)
        {
            using var connection = new SqliteConnection(DbName);
            return connection.ExecuteAsync(
                "delete from MSPC_Register where Hostname= @host and Port= @port "
                , new { host, port });

        }
        public async Task<long> DeleteLastYear()
        {
            var query = "delete from MSPC_Register";
            var year = DateTime.UtcNow.Year ;
            string val = year.ToString();
            var nr = 4;
            query += $" where substr(stringRegisteredDate,1,  {nr})<@val";
            using var connection = new SqliteConnection(DbName);
            return await connection.ExecuteAsync(query, new { val });    
            
        }
        public async Task<long> DeleteHistory()
        {
            long total = 0;
            var data = await LoadFromDatabase();
            using var connection = new SqliteConnection(DbName);
            foreach (var item in data)
            {
                var nr= await connection.ExecuteAsync(
                    "delete from MSPC_Register where Hostname= @host and Port= @port and stringRegisteredDate<@regDate "
                    , new { host=  item.HostName, port=item.Port,regDate=item.dateRegistered.ToString("o") });
                total+= nr;
            }
            return total;
        }
        public async Task<IRegister[]> LoadFromDatabase()
        {
            using var connection = new SqliteConnection(DbName);
            var data = (await connection.QueryAsync<RegFake>("select * from MSPC_Register")).ToArray();
            foreach (var item in data)
            {
                if(string.IsNullOrEmpty(item.UniqueID))
                {
                    item.UniqueID = (item as IRegister).GenerateUniqueID();
                }
            }
            var groupUniqueId = data.GroupBy(it => it.UniqueID).Select(it => new { it.Key, it }).ToArray();

            if (groupUniqueId.Length == data.Length)
            {
                return data;
            }
            var ret = new List<IRegister>();

            foreach(var item in groupUniqueId)
            {
                var maxDate = item.it.Max(it => it.dateRegistered);
                var itemMax = item.it.First(it => it.dateRegistered == maxDate);
                itemMax.AddHistory(item.it.Where(i=>i != itemMax).ToArray());
                ret.Add(itemMax);
            }
            return ret.ToArray();
        }

        public async Task<IRegister[]> LoadFromYear(int year, int? month)
        {
            if ((month ?? 0) < 1)
                month = null;
                
            using var connection = new SqliteConnection(DbName);
            var query = "select * from MSPC_Register";

            string val = year.ToString();
            var nr = 4;
            
            if (month != null)
            {
                val += "-" + month.Value.ToString("0#");
                nr = 7;
            }
            query += $" where substr(stringRegisteredDate,1,  {nr})=@val";



            var data = (await connection.QueryAsync<RegFake>(query, new { val })).ToArray();
            foreach (var item in data)
            {
                if (string.IsNullOrEmpty(item.UniqueID))
                {
                    item.UniqueID = (item as IRegister).GenerateUniqueID();
                }
            }
            var groupUniqueId = data.GroupBy(it => it.UniqueID).Select(it => new { it.Key, it }).ToArray();

            if (groupUniqueId.Length == data.Length)
            {
                return data;
            }
            var ret = new List<IRegister>();

            foreach (var item in groupUniqueId)
            {
                var maxDate = item.it.Max(it => it.dateRegistered);
                var itemMax = item.it.First(it => it.dateRegistered == maxDate);
                itemMax.AddHistory(item.it.Where(i => i != itemMax).ToArray());
                ret.Add(itemMax);
            }
            ret.ForEach(it => { it.EnvData = null; it.History = null; });
            return ret.ToArray();
        }
    }
}