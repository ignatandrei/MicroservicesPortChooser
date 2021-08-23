using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesPortChooserBL
{
    public class Register
    {
        private static ConcurrentDictionary<string, Register> register = new ConcurrentDictionary<string, Register>();
        
        public static Register AddRegister(Register r)
        {
            register.AddOrUpdate(r.UniqueID, r, (key, r) => r);
            return r;
        }
        public static Register[] RegisteredMSPC()
        {
            return register.ToArray().Select(it => it.Value).ToArray();
        }
        public static Register AddNew(string name,string host, UInt16 port, string tag = "")
        {
            return AddRegister(new Register(name, host, port, tag));
        }
        public static bool UnRegister(string host, UInt16 port)
        {
            var r = new Register(host, host, port);
            return register.Remove(r.UniqueID, out _);
        }
        public Register(string name,string host,UInt16 port, string tag="")
        {
            dateRegistered = DateTime.UtcNow;
            this.Name = name;
            this.HostName = host;
            this.Port = port;
            this.Tag = tag;
            
        }
        public DateTimeOffset dateRegistered { get; init; }
        public string UniqueID
        {
            get
            {
                return $"{HostName}_{Port}";
            }
        }
        public string Name { get; set; }
        public string HostName { get; set; }
        public UInt16 Port { get; set; }
        public string Tag { get; set; }
    }
}
