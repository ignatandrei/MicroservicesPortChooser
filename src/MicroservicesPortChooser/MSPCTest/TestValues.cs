using LightBDD.Framework;
using LightBDD.XUnit2;
using LightBDD.Framework.Scenarios;
using System;
using Xunit;
[assembly: LightBddScope]
namespace MSPCTest
{
    [FeatureDescription(

  @"This will test just how will generate the port number
")]
    public partial class TestValues
    {
        [Scenario]

        [Label("generating ports")]

        [Trait("Category", "Easy")]
        //[InlineData("MicroservicesPort", 28026)]
        [InlineData("MicroservicesPort", 28026)]
        public void TestDeterministicPort(string name, UInt16 port)
        {
            Runner.RunScenario(

                   _ => When_Having_The_MicroService_Name(name),


                   _ => Then_The_Port_Choosed_Is(port)
                   );
        }
    }
}
