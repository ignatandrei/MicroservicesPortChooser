using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSPC_Interfaces;
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
        public async Task<int> GetDeterministicPortFrom([FromServices] MSPC mspc,[FromServices]IRepository repo, string name, string tag)
        {
            var host = this.Request.GetRemoteIP();

            var port =mspc.GetDeterministicPort(name,tag);
            var r = await new Register(repo).AddNew(name, host, port, tag);
            return r.Port;
            
        }
        

       
    }
}
