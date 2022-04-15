namespace MicroservicesPortChooserWeb;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    readonly IApiVersionDescriptionProvider provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
      this.provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        var groups = provider.ApiVersionDescriptions
            .Select(it => it.GroupName)
            .Distinct()
            .Where(it=>it !="v1")
            .ToArray();

        foreach (var description in groups)
        {
            options.SwaggerDoc(
              description,
                new OpenApiInfo()
                {
                    Title = $"{ThisAssembly.Project.AssemblyName} {description}",
                    Version = description,
                });
        }
    }
}
