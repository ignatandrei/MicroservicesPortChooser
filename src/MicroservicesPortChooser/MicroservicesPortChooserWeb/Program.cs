
var builder = WebApplication.CreateBuilder(args);

var assControllers = typeof(UtilsControllers).Assembly;

builder.Services.AddControllers()
    .AddJsonOptions(c =>
    {
        c.JsonSerializerOptions.PropertyNamingPolicy = new LowerCaseNamingPolicy();
    })
    .PartManager.ApplicationParts.Add(new AssemblyPart(assControllers))
;
List<Type> typesContext = new();

foreach (IRegisterContext item in UtilsControllers.registerContexts)
{
    typesContext.Add(item.AddServices(builder.Services, builder.Configuration));
}

//this line register DB contexts
builder.Services.AddTransient((ctx) =>
{
    Func<string, DbContext?> a = (string dbName) =>
    {
        var t = typesContext.First(it => it.Name == dbName);

        var req = ctx.GetRequiredService(t);
        if (req == null) return null;
        return req as DbContext;
    };
    return a;
});
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

//builder.Services.AddHostedService<DiscoveryAndRegister>();
builder.Services.AddApiVersioning(act =>
{
    act.AssumeDefaultVersionWhenUnspecified = true;
    act.DefaultApiVersion = new ApiVersion(1,0);
});
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
/*builder.Services.AddProblemDetails(c =>
{
    c.IncludeExceptionDetails = (context, ex) => true;
});
*/
builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IRegister, Register>();
builder.Services.AddTransient<Register, Register>();
//var cn = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContextFactory<ApplicationDBContext>(
//        options =>
//        {
//            //options.UseSqlite(Repository.DbName);
//            options.UseSqlServer(cn);
//        }
//     )
//   ;
/*
builder.Services
    .AddHealthChecks()
    //.AddSqlite(Repository.DbName)
    ;
builder.Services
     .AddHealthChecksUI(setupSettings: setup =>
     {
         setup.AddHealthCheckEndpoint("myself", "/healthz");
         //setup.AddHealthCheckEndpoint("endpoint2", "health-messagebrokers");
         //setup.AddWebhookNotification("webhook1", uri: "/notify", payload: "{...}");
     })

    .AddInMemoryStorage();
*/
//builder.Services
//    .AddRazorComponents()
//    .AddInteractiveWebAssemblyComponents();

builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents();

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//app.UseWebAssemblyDebugging();
//app.UseProblemDetails();
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
    c.DefaultModelsExpandDepth(-1);
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);   
    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroservicesPortChooserWeb v1");
    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
}
    );

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true
});

app.UseRouting();

//app.UseAuthorization();

app.MapControllers();
app.UseAMS();
/*
app.MapHealthChecksUI();
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
*/
//app.MapSettingsView<MicroservicesPortChooserWeb.SettingsJson.appsettings>(app.Configuration);
app.MapFallbackToFile("/static/{**slug}", "index.html");
//app.MapFallbackToFile("/blazor/{**slug}", "/blazor/index.html");

app.UseBlocklyAutomation();

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/blazor"), admin =>
{
    admin.UseBlazorFrameworkFiles();
    //admin.UsePathBase("/blazor");
    //admin.UseStaticFiles(new StaticFileOptions
    //{
    //    ServeUnknownFileTypes = true
    //});
    admin.UseRouting();
    admin.UseEndpoints(endpoints =>
    {
        // Blazor
        //endpoints.MapBlazorHub();
        endpoints.MapFallbackToFile("/blazor/index.html");
    });
});

app.Run();
//needed for tests
public partial class Program { }
