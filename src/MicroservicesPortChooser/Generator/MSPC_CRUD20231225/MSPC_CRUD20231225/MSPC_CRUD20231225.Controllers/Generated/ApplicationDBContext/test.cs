using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Generated;

[ApiController]
[Route("api/[controller]/[action]")]
public class testController: Controller
{
    private readonly ApplicationDBContext applicationDBContext;

    public testController(ApplicationDBContext applicationDBContext)
    {
        this.applicationDBContext = applicationDBContext;
    }
    [HttpGet]
    public string test1()
    {
        return "test1";
    }
    [HttpGet]
    public string test2()
    {
        return applicationDBContext.ContextId.InstanceId.ToString();
    }
    [HttpGet]
    public string test3()
    {
        return applicationDBContext.Database?.GetConnectionString()??"NO connection string";
    } 
    [HttpGet]
    public string test4()
    {
        var s await applicationDBContext.MSPC_Register.ToArray();
        return "number:"+s.Length.ToString();
    }
}
