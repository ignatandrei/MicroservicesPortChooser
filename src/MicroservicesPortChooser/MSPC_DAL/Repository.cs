using Microsoft.EntityFrameworkCore;

namespace MSPC_DAL;

public class Repository : IRepository
{
    //public static string DbName = "Data Source=MSPC.db";
    private readonly ApplicationDBContext? context;

    public Repository(ApplicationDBContext? context)
    {
        this.context = context;
    }
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
        var regDate = r.dateRegistered.ToString("o");
        //using var connection = new SqliteConnection(DbName);
        var connection = context.Database;
        //var data=context.MSPC_Register.ToArray();
        FormattableString fs = @$"insert into MSPC_Register(Name, Hostname,Port, Tag, Authority, PCName,stringRegisteredDate, EnvData) 
values 
({r.Name}, {r.HostName},{r.Port}, {r.Tag}, {r.Authority},{r.PCName},{regDate}, {r.EnvData}
)";

        await connection.ExecuteSqlInterpolatedAsync(fs);
        return r;
    }
    public Task<int> UnRegister(string host, UInt16 port)
    {
        //using var connection = new SqliteConnection(DbName);
        var connection = context.Database;
        return connection.ExecuteSqlInterpolatedAsync(
            $"delete from MSPC_Register where Hostname= {host} and Port= {port} ");

    }
    public async Task<long> DeleteLastYear()
    {
        var year = DateTime.UtcNow.Year-1 ;
        string val = year.ToString();
        var connection = context.Database;

        return await connection.ExecuteSqlInterpolatedAsync(
            $"delete from MSPC_Register where stringRegisteredDate like '{val}%' "
            );    
        
    }
    public async Task<long> DeleteHistory()
    {
        long total = 0;
        var data = await LoadFromDatabase();
        //using var connection = new SqliteConnection(DbName);
        var connection = context.Database;
        foreach (var item in data)
        {
            var regDate = item.dateRegistered.ToString("o");
            var nr= await connection.ExecuteSqlInterpolatedAsync(
                $"delete from MSPC_Register where Hostname= {item.HostName} and Port= {item.Port} and stringRegisteredDate<{regDate} "
                );
            total+= nr;
        }
        return total;
    }
    public async Task<IRegister[]> LoadFromDatabase()
    {
        //using var connection = new SqliteConnection(DbName);
        var connection = context.Database;
        var dataDB = (connection.SqlQuery<RegFakeDb>($"select * from MSPC_Register")).ToArray();
        var data = dataDB.Select(it => (RegFake)it  ).ToArray();
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

    //public async Task<IRegister[]> LoadFromYear(int year, int? month)
    //{
    //    if ((month ?? 0) < 1)
    //        month = null;
    //    var connection = context.Database;

    //    //using var connection = new SqliteConnection(DbName);
    //    var query = "select * from MSPC_Register";

    //    string val = year.ToString();
    //    var nr = 4;
        
    //    if (month != null)
    //    {
    //        val += "-" + month.Value.ToString("0#");
    //        nr = 7;
    //    }
    //    query += $" where substr(stringRegisteredDate,1,  {nr})=@val";



    //    var data = (connection.SqlQuery<RegFake>(query, new { val })).ToArray();
    //    foreach (var item in data)
    //    {
    //        if (string.IsNullOrEmpty(item.UniqueID))
    //        {
    //            item.UniqueID = (item as IRegister).GenerateUniqueID();
    //        }
    //    }
    //    var groupUniqueId = data.GroupBy(it => it.UniqueID).Select(it => new { it.Key, it }).ToArray();

    //    if (groupUniqueId.Length == data.Length)
    //    {
    //        return data;
    //    }
    //    var ret = new List<IRegister>();

    //    foreach (var item in groupUniqueId)
    //    {
    //        var maxDate = item.it.Max(it => it.dateRegistered);
    //        var itemMax = item.it.First(it => it.dateRegistered == maxDate);
    //        itemMax.AddHistory(item.it.Where(i => i != itemMax).ToArray());
    //        ret.Add(itemMax);
    //    }
    //    ret.ForEach(it => { it.EnvData = null; it.History = null; });
    //    return ret.ToArray();
    //}
}