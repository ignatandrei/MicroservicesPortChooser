using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using MicroservicesPortChooserWeb;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
//https://andrewlock.net/exploring-dotnet-6-part-6-supporting-integration-tests-with-webapplicationfactory-in-dotnet-6/
//https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
namespace MSPCTest
{
    [FeatureDescription("This will test just the register the name when calling the controllers")]
    [Label(nameof(TestRegister))]
    public partial class TestRegister: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        
        public TestRegister(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
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

}
