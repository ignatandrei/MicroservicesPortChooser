namespace MSPCWebExtension;

public interface IPortService
{
    [Post("/api/v1/Register/AddNew")]
    public Task<Register> AddNew ([Body]Register r);
   
}
