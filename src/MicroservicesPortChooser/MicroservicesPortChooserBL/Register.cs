﻿using RSCG_UtilityTypesCommon;

namespace MicroservicesPortChooserBL;
/// <summary>
/// generated from Omit in Register
/// </summary>
public partial class RegisterAPI
{


}
/// <summary>
/// generated from Omit in Register
/// </summary>
public partial class RegisterNoEnv
{


}

[IGenerateDataFromClass("ClassDebuggerDisplay")]
[Omit("RegisterAPI",nameof(History),nameof(repository))]
[Omit("RegisterNoEnv", nameof(History), nameof(repository),nameof(EnvData))]
public partial class Register : IRegister
{
    
    private static ConcurrentDictionary<string, IRegister> register = new ConcurrentDictionary<string, IRegister>();
    public IRepository repository { get; internal set; }

    public async Task<Register> AddRegister(Register r)
    {
        await this.repository.AddRegister(r);
        register.AddOrUpdate(r.UniqueID, r, (key, r) => r);
        return r;
    }
    public static RegisterNoEnv[] RegisteredMSPCNoEnv()
    {
        return register.ToArray()
            .Select(it => it.Value)
            .Select(it=>
            new RegisterNoEnv()
            {
                HostName = it.HostName,
                Name = it.Name,
                Port = it.Port,
                Tag = it.Tag,
                Authority = it.Authority,
                PCName = it.PCName,
                dateRegistered = it.dateRegistered,
                UniqueID = it.UniqueID

            })
            .ToArray();
    }
    public static IRegister[] RegisteredMSPC()
    {
        return register.ToArray().Select(it => it.Value).ToArray();
    }
    public Task<Register> AddNew(string name, string host, int port, string tag = "", string authority = "http")
    {
        //var rb = new RegisterBuilder(this);
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
    //public async Task<IRegister[]> LoadFromYear(int year, int? month)
    //{
    //    var data = await repository.LoadFromYear(year, month);
    //    return data;
    //}
    public async Task<long> DeleteLastYear()
    {
        var data = await repository.DeleteLastYear();
        await LoadFromDatabase();
        return data;
    }
    public async Task<long> DeleteHistory()
    {
        var data = await repository.DeleteHistory();
        await LoadFromDatabase();
        return data;
    }
    public async Task<int> LoadFromDatabase()
    {
        var data = await repository.LoadFromDatabase();
        register.Clear();
        foreach (var r in data)
        {
            register.AddOrUpdate(r.UniqueID, r, (key, r) => r);
        }
        return data.Count();
    }
    public Register(IRepository repository):this()
    {
        this.repository = repository;
    }

    public Register()
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

    public string EnvData { get; set; }
    public DateTimeOffset dateRegistered { get; internal set; }
    public string UniqueID
    {
        get
        {
            
            return (this as IRegister).GenerateUniqueID();
        }
        set
        {

        }
    }
    public string PCName { get; set; }
    public string Name { get; set; }
    public string HostName { get; set; }
    public int Port { get; set; }
    public string Tag { get; set; }
    public string Authority { get; set; }
        
    public IRegister[] History { get; set; }
}
