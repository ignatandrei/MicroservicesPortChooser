namespace MicroservicesPortChooserWeb.Controllers;

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
    public async Task<int> GetDeterministicPortFrom([FromServices] MSPC mspc,[FromServices]Register register, string name, string tag)
    {
        var host = this.Request.GetRemoteIP();

        var port =mspc.GetDeterministicPort(name,tag);
        var r = await register.AddNew(name, host, port, tag);
        return r.Port;
        
    }
    

   
}
