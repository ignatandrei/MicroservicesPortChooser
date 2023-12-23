
namespace MSPCWebExtension
{
    public partial class PortService
    {
        [HttpPost("Register/AddNew")]
        public partial Task<Register> AddNew(Register r);
       
    }
}
