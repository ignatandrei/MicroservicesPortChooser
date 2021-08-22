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
        
        public static void AddRegister(Register r)
        {
            register.AddOrUpdate(r.UniqueID, r, (key, r) => r);
        }
        public Register()
        {
            dateRegistered = DateTime.UtcNow;
        }
        public DateTimeOffset dateRegistered = DateTime.Now;
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
