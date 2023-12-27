
var builder = WebAssemblyHostBuilder.CreateDefault(args);

//Microsoft.Extensions.Hosting.Extensions.ConfigureOpenTelemetry(new ProxyHost(builder));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddFluentUIComponents();

var hostApi = builder.Configuration["HOSTAPI"];
//TODO: andrei hostApi=="/"
if (string.IsNullOrEmpty(hostApi)|| hostApi=="/")
{
    hostApi = builder.HostEnvironment.BaseAddress;
    var uri= new Uri(hostApi);
    hostApi = $"{uri.Scheme}://{uri.Host}:{uri.Port}/";
    var dict = new Dictionary<string, string?> { { "HOSTAPI", hostApi } };
    builder.Configuration.AddInMemoryCollection(dict.ToArray());
}

builder.Services.AddKeyedScoped("db", (sp, _) => new HttpClient { BaseAddress = new Uri(hostApi) });
builder.Services.AddScoped<WebAPIInteraction>();

await builder.Build().RunAsync();
