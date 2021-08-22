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
        public ActionResult<UInt16> GetDeterministicPortFrom([FromServices] MSPC mspc, string name)
        {
            var host = this.Request.Host.Host;

            var port = mspc.GetDeterministicPort(name);
            return Register.AddNew(name,host, port, "").Port;
            
        }
        [HttpGet("{name}")]
        public ActionResult<UInt16> GetNonDeterministicPortFrom([FromServices] MSPC mspc, string name)
        {

            var host = this.Request.Host.Host;

            var port = mspc.GetNonDeterministicPort(name); 
            return Register.AddNew(name,host, port, "").Port;
            
        }

    }
}
