namespace MSPCTest;

public class Test: WebApplicationFactory<Program> 
{
    public Test()
    {
        
    }
    protected override TestServer CreateServer(IWebHostBuilder builder)
    {
        return base.CreateServer(builder);
    }
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            
        });
        return base.CreateHost(builder);
    }
}
[FeatureDescription("This will test just the register the name when calling the controllers")]
[Label(nameof(TestRegister))]
public partial class TestRegister: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    
    public TestRegister()
    {
        _factory = new Test();
    }
    [Scenario]

    [Label("generating ports")]

    [Trait("Category", "Integration")]
    [InlineData("MicroservicesPort", 28026)]

    public async Task RegisterServiceV1(string name, UInt16 port)
    {
        // Arrange

        await Runner
            .AddSteps(
            _ => When_Creating_The_App(),
            _ => When_Having_The_MicroService_Name(name)
                )

            .AddAsyncSteps(

            _ => Then_The_Port_ChoosedForV1_Is(port)
            )
            .AddSteps(
            _ => Then_Must_Have_RegisterPort(port)
            )
            .RunAsync();
    }
}
