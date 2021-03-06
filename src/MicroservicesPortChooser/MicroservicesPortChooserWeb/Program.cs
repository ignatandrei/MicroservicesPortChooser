using AMS_Base;
[assembly: AMS_Base.VersionReleased(Name = "FutureRelease", ISODateTime = "9999-04-16", recordData = AMS_Base.RecordData.Merges)]
[assembly: VersionReleased(Name = "CRUD", ISODateTime = "2022-04-18", recordData = RecordData.Merges)]
[assembly: VersionReleased(Name = "Net6", ISODateTime = "2022-04-15", recordData = RecordData.Merges)]
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MSPC>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                      builder => builder
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin()
                                );
});

builder.Services.AddHostedService<DiscoveryAndRegister>();
builder.Services.AddApiVersioning();
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = ThisAssembly.Project.AssemblyName, Version = "v1" });
});
builder.Services.AddProblemDetails(c =>
{
    c.IncludeExceptionDetails = (context, ex) => true;
});
builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IRegister, Register>();
builder.Services.AddTransient<Register, Register>();
builder.Services.AddDbContextFactory<ApplicationDBContext>(

        options =>
        {
            options.UseSqlite(Repository.DbName);
        }
     )
   ;
builder.Services
    .AddHealthChecks()
    .AddSqlite(Repository.DbName);
builder.Services
     .AddHealthChecksUI(setupSettings: setup =>
     {
         setup.AddHealthCheckEndpoint("myself", "/healthz");
                 //setup.AddHealthCheckEndpoint("endpoint2", "health-messagebrokers");
                 //setup.AddWebhookNotification("webhook1", uri: "/notify", payload: "{...}");
             })

    .AddInMemoryStorage();

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseProblemDetails();
app.UseCors("AllowAll");
app.UseBlocklyUI(app.Environment);
var q = NetCore2BlocklyNew.Extensions.FileProvider;
//app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroservicesPortChooserWeb v1");
    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
}
    );

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.UseAMS();
    endpoints.MapHealthChecksUI();
    endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    endpoints.MapSettingsView<MicroservicesPortChooserWeb.SettingsJson.appsettings>(app.Configuration);
    endpoints.MapFallbackToFile("/static/{**slug}", "index.html");
    endpoints.UseBlocklyAutomation();
});

app.Run();
//needed for tests
public partial class Program { }