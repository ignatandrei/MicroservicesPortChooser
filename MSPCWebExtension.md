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
