using MicroservicesPortChooserBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSPC_Interfaces;
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
        public IRegister[] GetAll()
        {
            return Register.RegisteredMSPC();
        }
        [HttpGet]
        public Task<int> LoadFromDatabase([FromServices]IRepository repo)
        {
            //do copy constructor
            return new Register(repo).LoadFromDatabase();

        }
        [HttpGet("{year:int}/{month:int?}")]
        public Task<IRegister[]> LoadFromYear([FromServices] IRepository repo, int year, int? month=null)
        {
            //do copy constructor
            
            return new Register(repo).LoadFromYear(year,month);

        }

        [HttpPost]
        public Task<Register> AddNew([FromServices] IRepository repo,Register r)
        {
            //do copy constructor
            return new Register(repo).AddRegister(r);

        }
        [HttpDelete("{host}/{port}")]
        public Task<bool> UnRegister([FromServices] IRepository repo,string host, UInt16 port)
        {
            return new Register(repo).UnRegister(host, port);
        }
    }
}
