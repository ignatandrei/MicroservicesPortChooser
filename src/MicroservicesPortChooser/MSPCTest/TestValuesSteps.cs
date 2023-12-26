namespace MSPCTest;

partial class TestValues: FeatureFixture
{
    private string name;
    private string tag;

    void When_Having_The_MicroService_Name(string name)
    {
        this.name = name;
    }
    void When_Having_The_MicroService_Name_And_Tag(string name,string tag)
    {
        this.name = name;
        this.tag = tag;
    }
    void Then_The_Port_Choosed_Is(UInt16 portChoosed)
    {
        var mspc = new MSPC();
        var port = mspc.GetDeterministicPort(name);
        StepExecution.Current.Comment($"generated port for {name} is {port}");
        Assert.Equal(portChoosed,port);
    }
    void Then_The_Port_Choosed_Can_Be_Anything_But(UInt16 portChoosed)
    {
        var mspc = new MSPC();
        var port = mspc.GetNonDeterministicPort(name);
        StepExecution.Current.Comment($"generated port for {name} is {port}");
        Assert.NotEqual(portChoosed, port);
    }
    void Then_The_Port_With_Tag_Is_Same_With_Port_Without_Tag()
    {
        var mspc = new MSPC();
        var portNotTag = mspc.GetDeterministicPort(name);
        StepExecution.Current.Comment($"generated port for {name} is {portNotTag}");
        var portTag= mspc.GetDeterministicPort(name,"");
        StepExecution.Current.Comment($"generated port with tag for {name} is {portTag}");
        Assert.Equal(portTag, portNotTag);
    }
    void Then_The_Port_Choosed_With_Tag_Is(UInt16 portChoosed)
    {
        var mspc = new MSPC();
        var port = mspc.GetDeterministicPort(name,tag);
        StepExecution.Current.Comment($"generated port for {name} is {port}");
        Assert.Equal(portChoosed, port);
    }
    //void Then_The_Port_Choosed_CiprianMethod_With_Tag_Is_Not(UInt16 portChoosed)
    //{
    //    var mspc = new MSPC();
    //    var port = mspc.GetNonDeterministicCiprianPort(name, tag);
    //    StepExecution.Current.Comment($"generated port for {name} is {port}");
    //    Assert.NotEqual(portChoosed, port);
    //}
}
