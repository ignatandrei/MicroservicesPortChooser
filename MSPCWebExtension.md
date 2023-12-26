# ASP.NET Core fast configuring

```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
            .UseStartup<Startup>()
            .UseAutomaticUrls(
                ThisAssembly.Project.AssemblyName,
                $"http://{Environment.MachineName}"
                )
            ;
            //if you want to use different ports for the same microservice
            //assuming there are in 2 different folders
                    .UseAutomaticUrls(
                        ThisAssembly.Project.AssemblyName,
                        $"http://{Environment.MachineName}",
                        //download app from below and use your own url
                        "https://microservicesportchooser.azurewebsites.net/",
                        new DirectoryInfo(Environment.CurrentDirectory).Name
                        )
        });

```

If you want just to register your service,download the app.
Then put this in Startup.cs of your microservice:
```csharp
public void ConfigureServices(IServiceCollection services)
{
   services.AddHostedService<DiscoveryAndRegister>();
```

and in appsettings.json
```json
"MSPC": {
  "tag": "Here_You_Can_Put_Tags",
  "appName": "Port Chooser ",
  "registerUrl": "https://microservicesportchooser.azurewebsites.net/",
}
```
And see the result at https://microservicesportchooser.azurewebsites.net/static/services
