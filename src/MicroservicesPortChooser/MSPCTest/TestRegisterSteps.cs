namespace MSPCTest;

partial class TestRegister: FeatureFixture
{
    private string name;
    private string tag;
    private HttpClient client;
    void When_Creating_The_App()
    {
        client = _factory.CreateClient();

    }
    void When_Having_The_MicroService_Name(string name)
    {
        this.name = name;
    }
    void When_Having_The_MicroService_Name_And_Tag(string name, string tag)
    {
        this.name = name;
        this.tag = tag;
    }
    async Task Then_The_Port_ChoosedForV1_Is(UInt16 portChoosed)
    {
        // Act
        var response = await client.GetAsync("api/v1.0/PortChooser/GetDeterministicPortFrom/"+name);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var port = await response.Content.ReadAsStringAsync();
        StepExecution.Current.Comment($"generated port for {name} is {port}");
        Assert.Equal(portChoosed.ToString(), port);
    }
    void Then_Must_Have_RegisterPort(UInt16 port)
    {
        var p = Register.RegisteredMSPC().FirstOrDefault(it => it.Port == port);
        Assert.NotNull(p);
        Assert.Equal("not_found_ip", p.HostName);
    }
    void Then_The_Port_Choosed_Can_Be_Anything_But(UInt16 portChoosed)
    {
        //var mspc = new MSPC();
        //var port = mspc.GetNonDeterministicPort(name);
        //StepExecution.Current.Comment($"generated port for {name} is {port}");
        //Assert.NotEqual(portChoosed, port);
    }
    void Then_The_Port_With_Tag_Is_Same_With_Port_Without_Tag()
    {
        //var mspc = new MSPC();
        //var portNotTag = mspc.GetDeterministicPort(name);
        //StepExecution.Current.Comment($"generated port for {name} is {portNotTag}");
        //var portTag = mspc.GetDeterministicPort(name, "");
        //StepExecution.Current.Comment($"generated port with tag for {name} is {portTag}");
        //Assert.Equal(portTag, portNotTag);
    }
    void Then_The_Port_Choosed_With_Tag_Is(UInt16 portChoosed)
    {
        //var mspc = new MSPC();
        //var port = mspc.GetDeterministicPort(name, tag);
        //StepExecution.Current.Comment($"generated port for {name} is {port}");
        //Assert.Equal(portChoosed, port);
    }
}