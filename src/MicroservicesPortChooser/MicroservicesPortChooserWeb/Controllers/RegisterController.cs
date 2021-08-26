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
        [HttpGet]
        public Task<int> LoadFromDatabase()
        {
            //do copy constructor
            return Register.LoadFromDatabase();

        }
        [HttpPost]
        public Task<Register> AddNew(Register r)
        {
            //do copy constructor
            return Register.AddRegister(r);

        }
        [HttpDelete("{host}/{port}")]
        public bool UnRegister(string host, UInt16 port)
        {
            return Register.UnRegister(host, port);
        }
    }
}
