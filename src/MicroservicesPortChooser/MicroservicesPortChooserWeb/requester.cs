using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicesPortChooserWeb
{
    public static class requester
    {
        public static string GetRemoteIP(this HttpRequest req)
        {
            var ip = GetHeaderValue(req, "X-Forwarded-For").FirstCsv();
            if (!string.IsNullOrWhiteSpace(ip))
                return ip;

            ip = req.HttpContext.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrWhiteSpace(ip))
                return ip;

            ip = GetHeaderValue(req,"REMOTE_ADDR").FirstCsv();
            if (!string.IsNullOrWhiteSpace(ip))
                return ip;

            return "not_found_ip";

        }
        private static string GetHeaderValue(HttpRequest req,string headerName)
        {
            StringValues values="";

            if (req.HttpContext?.Request?.Headers?.TryGetValue(headerName, out values)?? false)
            {
                return values.ToString();   
            }
            return "";
        }
        public static string FirstCsv(this string csvList)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return "";

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable<string>()
                .Select(s => s.Trim())
                .FirstOrDefault();
        }
    }
}
