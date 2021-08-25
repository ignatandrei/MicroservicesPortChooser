using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicesPortChooserWeb.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class PortChooserController : ControllerBase
    {

        private readonly ILogger<PortChooserController> _logger;

        public PortChooserController(ILogger<PortChooserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<UInt16> GetDeterministicPortFrom([FromServices] MSPC mspc, string name)
        {
            var host = this.Request.GetRemoteIP();

            var port = mspc.GetDeterministicPort(name);
            return (await Register.AddNew(name,host, port, "")).Port;
            
        }
        [HttpGet("{name}")]
        public async Task<UInt16> GetNonDeterministicPortFrom([FromServices] MSPC mspc, string name)
        {

            var host = this.Request.GetRemoteIP();

            var port = mspc.GetNonDeterministicPort(name); 
            return (await Register.AddNew(name,host, port, "")).Port;
            
        }

    }
}
