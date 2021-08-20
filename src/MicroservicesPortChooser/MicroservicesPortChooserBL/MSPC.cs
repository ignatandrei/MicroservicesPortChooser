using System;

namespace MicroservicesPortChooserBL
{
    /// <summary>
    /// read https://docs.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-5.0
    /// read https://andrewlock.net/why-is-string-gethashcode-different-each-time-i-run-my-program-in-net-core/
    /// read https://rehansaeed.com/gethashcode-made-easy/
    /// </summary>
    public class MSPC
    {
        static int GetDeterministicHashCode(string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }
        //private int HashCodeFromString(string name)
        //{
        //    var h = new HashCode();
        //    h.Add(name);
        //    return h.ToHashCode();
        //    //return name.GetHashCode();
        //}
        public UInt16 GetNonDeterministicPort(string name)
        {
            int hash =Math.Abs( name.GetHashCode());
            if (hash < UInt16.MaxValue)
                return (UInt16)hash;

            var remainder = hash % UInt16.MaxValue;
            return (UInt16)remainder;
        }
        public UInt16 GetDeterministicPort(string name)
        {
            int hash = Math.Abs(GetDeterministicHashCode(name));
            if (hash < UInt16.MaxValue)
                return (UInt16)hash;

            var remainder = hash % UInt16.MaxValue;
            return (UInt16)remainder;
        }


        public UInt16 GetDeterministicPort(string name, string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return GetDeterministicPort(name);


            return GetDeterministicPort(name + "_" + tag);

        }
        //public UInt16 GetNonDeterministicCiprianPort(string name, string tag)
        //{
        //    var hash = (name + "_" + tag).GetHashCode();
        //    //Console.WriteLine(hash);
        //    var int16Val = Convert.ToInt16((hash >> 16) ^ hash & 0xFFFF);
        //    return Convert.ToUInt16 (Math.Abs(int16Val)); 

        //}
    }
}
