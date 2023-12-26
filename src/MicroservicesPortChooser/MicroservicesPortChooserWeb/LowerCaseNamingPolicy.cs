using System.Text.Json;

namespace MicroservicesPortChooserWeb;

public class LowerCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return name;

        return name.ToLower();
    }
}