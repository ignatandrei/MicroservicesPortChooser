using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicesPortChooserWeb.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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
            return mspc.GetDeterministicPort(name);
        }
        [HttpGet("{name}")]
        public ActionResult<UInt16> GetNonDeterministicPortFrom([FromServices] MSPC mspc, string name)
        {
            return mspc.GetNonDeterministicPort(name);
        }

    }
}
