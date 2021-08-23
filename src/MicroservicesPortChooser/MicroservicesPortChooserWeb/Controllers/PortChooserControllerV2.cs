using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicesPortChooserWeb.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/PortChooser/[action]")]
    //[ControllerName("PortChooser")]
    public class PortChooserControllerV2 : ControllerBase
    {
        private readonly ILogger<PortChooserControllerV2> _logger;

        public PortChooserControllerV2(ILogger<PortChooserControllerV2> logger)
        {
            _logger = logger;
        }

        [HttpGet("{name}/{tag?}")]
        public ActionResult<UInt16> GetDeterministicPortFrom([FromServices] MSPC mspc, string name, string tag)
        {
            var host = this.Request.HttpContext.Connection.RemoteIpAddress?.ToString();

            var port =mspc.GetDeterministicPort(name,tag);
            return Register.AddNew(name,host, port, tag).Port;
            
        }
        

       
    }
}
