using MicroservicesPortChooserBL;
using HttpClientGenerator.Shared;
using System.Threading.Tasks;

namespace MSPCWebExtension
{
    public partial class PortService
    {
        [HttpPost("Register/AddNew")]
        public partial Task<Register> AddNew(Register r);
       
    }
}
