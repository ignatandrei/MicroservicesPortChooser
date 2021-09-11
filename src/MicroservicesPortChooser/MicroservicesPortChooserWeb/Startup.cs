using AMSWebAPI;
using appSettingsEditor;
using HealthChecks.UI.Client;
using Hellang.Middleware.ProblemDetails;
using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MSPC_DAL;
using MSPC_Interfaces;
using MSPCWebExtension;
using NetCore2Blockly;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MicroservicesPortChooserWeb
{
    /// <summary>
    /// https://github.com/dotnet/aspnet-api-versioning
    /// https://github.com/dotnet/aspnet-api-versioning/wiki/Versioning-via-the-URL-Path
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<MSPC>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                                  builder => builder
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowAnyOrigin()
                                            );
            });

            services.AddHostedService<DiscoveryAndRegister>();
            services.AddApiVersioning();
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = ThisAssembly.Project.AssemblyName, Version = "v1" });
            });
            services.AddProblemDetails(c=>
            {
                c.IncludeExceptionDetails = (context, ex) => true;                
            });
            services.AddBlockly();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IRegister, Register>();
            services.AddTransient<Register, Register>();
            services
                .AddHealthChecks()
                .AddSqlite(Repository.DbName);
            services
                 .AddHealthChecksUI(setupSettings: setup =>
                 {
                     setup.AddHealthCheckEndpoint("myself", "/healthz");
                     //setup.AddHealthCheckEndpoint("endpoint2", "health-messagebrokers");
                     //setup.AddWebhookNotification("webhook1", uri: "/notify", payload: "{...}");
                 })
                 
                .AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseProblemDetails();
            app.UseCors("AllowAll");
            app.UseBlocklyUI();
            app.UseBlockly();
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
                endpoints.MapSettingsView<SettingsJson.appsettings>(Configuration);
                endpoints.MapFallbackToFile("/static/{**slug}", "index.html");
            });
        }
    }
}
