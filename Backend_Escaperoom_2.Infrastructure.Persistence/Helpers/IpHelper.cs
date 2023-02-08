using System;
using System.Net;
using System.Net.Sockets;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Helpers
{
    public class IpHelper
    {
        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return String.Empty;
        }
    }
}
