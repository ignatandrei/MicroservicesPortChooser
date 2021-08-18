using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using System;
using Xunit;
namespace MSPCTest
{
    [FeatureDescription(

  @"This will test just how will generate the port number
")]
    [Label(nameof(TestValues))]
    public partial class TestValues
    {
        [Scenario]

        [Label("generating ports")]

        [Trait("Category", "Easy")]
        [InlineData("MicroservicesPort", 28026)]
        public void TestDeterministicPort(string name, UInt16 port)
        {
            Runner.RunScenario(

                   _ => When_Having_The_MicroService_Name(name),


                   _ => Then_The_Port_Choosed_Is(port)
                   );
        }
        [Scenario]

        [Label("generating ports")]
        [Trait("Category", "Easy")]
        [InlineData("MicroservicesPort", 28026)]
        public void TestNonDeterministicPort(string name, UInt16 port)
        {
            Runner.RunScenario(

                   _ => When_Having_The_MicroService_Name(name),


                   _ => Then_The_Port_Choosed_Can_Be_Anything_But(port)
                   );
        }
        [Scenario]

        [Label("port same")]

        [Trait("Category", "Easy")]
        [InlineData("MicroservicesPort")]
        public void TestDeterministicPortWithTagNull(string name)
        {
            Runner.RunScenario(

       _ => When_Having_The_MicroService_Name(name),


       _ => Then_The_Port_With_Tag_Is_Same_With_Port_Without_Tag()
       );
        }

        [Scenario]

        [Label("generating ports")]

        [Trait("Category", "Easy")]
        [InlineData("test","asd" ,3345)]
        public void TestDeterministicPortWithTag(string name, string tag, UInt16 port)
        {
            Runner.RunScenario(

                   _ => When_Having_The_MicroService_Name_And_Tag(name,tag),


                   _ => Then_The_Port_Choosed_With_Tag_Is(port)
                   );
        }
    }
}
