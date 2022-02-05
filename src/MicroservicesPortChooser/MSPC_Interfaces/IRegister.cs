using System;

namespace MSPC_Interfaces
{
    public partial class Helpers
    {
        public partial ISystem_Environment FromStaticEnvironment();
    }
    public interface IRegisterParse : IRegister
    {
        string UserName { get; set; }
        string CurrentDirectory { get; set; }
        long ProcessId { get; set; }
    }
    public interface IRegister
    {
        string Authority { get; set; }
        DateTimeOffset dateRegistered { get; }
        string HostName { get; set; }
        string Name { get; set; }
        string PCName { get; set; }
        int Port { get; set; }
        string Tag { get; set; }
        string UniqueID { get; }
        string EnvData { get; set; }
        IRegister[] History { get; set; }
    }
}