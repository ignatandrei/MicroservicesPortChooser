
namespace MSPC_DAL;
[Omit("RegFakeDb", nameof(History))]
class RegFake : IRegisterParse
{
    private List<RegFake> history;
    public RegFake()
    {
        history = new List<RegFake>();
    }
    public void AddHistory(RegFake[] hist)
    {
        history.AddRange(hist);
    }
    public string Authority { get; set; }
    public DateTimeOffset dateRegistered { get; set; } 

    public string HostName { get; set; }
    public string Name { get; set; } 
    public string PCName { get; set; }
    public int Port { get; set; }
    public string Tag { get; set; }

    public string UniqueID { get; set; }

    public string stringRegisteredDate
    {
        get
        {
            return dateRegistered.ToString("o");
        }
        set
        {
            dateRegistered = DateTimeOffset.Parse(value);
        }
    }

    public IRegister[] History { get => history.ToArray(); 
        set { 
            if (value == null) 
                history = new List<RegFake>();
            else
                throw new NotImplementedException(); 
        }
    }

    public string EnvData { get; set; }
    public string UserName { get  {
            try
            {
                var d = JsonDocument.Parse(EnvData);
                var props = d.RootElement.EnumerateObject();
                foreach (var item in props)
                {
                    if (item.Name == nameof(UserName))
                        return item.Value.ToString();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        set => throw new NotImplementedException(); }
    public string CurrentDirectory { get {
            try
            {
                var d = JsonDocument.Parse(EnvData);
                var props = d.RootElement.EnumerateObject();
                foreach (var item in props)
                {
                    if (item.Name == nameof(CurrentDirectory))
                        return item.Value.ToString();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        set => throw new NotImplementedException(); }
    public long ProcessId { get
        {
            try
            {
                var d = JsonDocument.Parse(EnvData);
                var props = d.RootElement.EnumerateObject();
                foreach (var item in props)
                {
                    if (item.Name == nameof(ProcessId))
                        return long.Parse(item.Value.ToString());
                }

                return 0;
            }
            catch (Exception)
            {
                return 0;
            }

        } set => throw new NotImplementedException(); }
}