using System;

namespace MSPC_Interfaces
{
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

        IRegister[] History { get; }
    }
}