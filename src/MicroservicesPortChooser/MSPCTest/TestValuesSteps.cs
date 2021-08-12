using LightBDD.XUnit2;
using MicroservicesPortChooserBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MSPCTest
{
    partial class TestValues: FeatureFixture
    {
        private string name;
        void When_Having_The_MicroService_Name(string name)
        {
            this.name = name;
        }
        void Then_The_Port_Choosed_Is(UInt16 portChoosed)
        {
            var mspc = new MSPC();
            var port = mspc.GetDeterministicPort(name);
            Assert.Equal(portChoosed,port);
        }
    }
}
