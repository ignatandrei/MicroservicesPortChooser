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
                Environment.MachineName
                )
            ;
        });

```
