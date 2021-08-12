using System;

namespace MicroservicesPortChooserBL
{
    public class MSPC
    {
        public UInt16 Port(string name)
        {
            int hash =Math.Abs( name.GetHashCode());
            if (hash < UInt16.MaxValue)
                return (UInt16)hash;

            var remainder = hash % UInt16.MaxValue;
            return (UInt16)remainder;
        }
    }
}
