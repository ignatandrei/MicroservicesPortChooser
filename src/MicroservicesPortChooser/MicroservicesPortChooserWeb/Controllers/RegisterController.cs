using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicesPortChooserWeb.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpGet]
        public Register[] GetAll()
        {
            return Register.RegisteredMSPC();
        }
    }
}
