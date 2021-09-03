using System.Threading.Tasks;

namespace MSPC_Interfaces
{
    public interface IRepository
    {
        Task<IRegister> AddRegister(IRegister r);
        Task<IRegister[]> LoadFromDatabase();
        Task<int> UnRegister(string host, ushort port);
    }
}
