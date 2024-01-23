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
    public string test1()
    {
        return "test1";
    }
    public string test2()
    {
        return applicationDBContext.ContextId.InstanceId.ToString();
    }
    public string test3()
    {
        return applicationDBContext.Database?.GetConnectionString()??"NO connection string";
    }
}
