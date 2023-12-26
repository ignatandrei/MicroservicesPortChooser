
namespace MSPCWebExtension;

class ConfigDataMSPC
{
    public string tag { get; set; }

    public string appName { get; set; }
    public string registerUrl { get; set; }

    public void ConfigureDefaults()
    {
        if (string.IsNullOrWhiteSpace(appName))
            appName = Assembly.GetEntryAssembly().GetName().Name;
        if (string.IsNullOrWhiteSpace(registerUrl))
            registerUrl = "https://microservicesportchooser.azurewebsites.net/";
    }
}
