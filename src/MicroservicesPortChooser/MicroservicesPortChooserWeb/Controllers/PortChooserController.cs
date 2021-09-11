using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSPC_Interfaces;
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
        private readonly Register register;
        private readonly ILogger<PortChooserController> _logger;

        public PortChooserController([FromServices] Register register, ILogger<PortChooserController> logger)
        {
            this.register = register;
            _logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<int> GetDeterministicPortFrom([FromServices] MSPC mspc,[FromServices] IRepository repository, string name)
        {
            var host = this.Request.GetRemoteIP();

            var port = mspc.GetDeterministicPort(name);            
            var r = await register.AddNew(name, host, port, "");
            return (r.Port);
            
        }
        [HttpGet("{name}")]
        public async Task<int> GetNonDeterministicPortFrom([FromServices] IRepository repo,[FromServices] MSPC mspc, string name)
        {

            var host = this.Request.GetRemoteIP();

            var port = mspc.GetNonDeterministicPort(name);
            var r = await register.AddNew(name, host, port, "");
            return r.Port;
            
        }

    }
}
