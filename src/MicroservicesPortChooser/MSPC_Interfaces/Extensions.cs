namespace MSPC_Interfaces
{
    public static class Extensions
    {
        public static string GenerateUniqueID(this IRegister r)
        {
            var name = $"{r.HostName}_{r.Port}";
            if (r.PCName?.Trim().Length > 0)
                return name += $"_{r.PCName}";

            return name;
        }
    }
}