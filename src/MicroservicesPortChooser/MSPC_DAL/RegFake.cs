using MSPC_Interfaces;
using System;
using System.Collections.Generic;

namespace MSPC_DAL
{
    class RegFake : IRegister
    {
        private List<RegFake> history;
        public RegFake()
        {
            history = new List<RegFake>();
        }
        public void AddHistory(RegFake[] hist)
        {
            history.AddRange(hist);
        }
        public string Authority { get; set; }
        public DateTimeOffset dateRegistered { get; set; } 

        public string HostName { get; set; }
        public string Name { get; set; } 
        public string PCName { get; set; }
        public int Port { get; set; }
        public string Tag { get; set; }

        public string UniqueID { get; set; }

        public string stringRegisteredDate
        {
            get
            {
                return dateRegistered.ToString("o");
            }
            set
            {
                dateRegistered = DateTimeOffset.Parse(value);
            }
        }

        public IRegister[] History { get =>  history.ToArray();  }

        public string EnvData { get; set; }
    }
}