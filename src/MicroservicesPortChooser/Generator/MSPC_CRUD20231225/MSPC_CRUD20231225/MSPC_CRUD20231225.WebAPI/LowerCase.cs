using System.Text.Json;

namespace MSPC_CRUD20231225.WebAPIWebAPI;
public class LowerCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return name;

        return name.ToLower();
    }
}
