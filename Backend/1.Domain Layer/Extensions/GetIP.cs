using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Extensions
{
    public class GetIP
    {
        public IPAddress? GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress;
        }
        public IPAddress? GetRemoteHostIpAddressUsingXForwardedFor(HttpContext httpContext)
        {
            IPAddress? remoteIpAddress = null;
            var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                var ips = forwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(s => s.Trim());
                foreach (var ip in ips)
                {
                    if (IPAddress.TryParse(ip, out var address) &&
                        (address.AddressFamily is AddressFamily.InterNetwork
                         or AddressFamily.InterNetworkV6))
                    {
                        remoteIpAddress = address;
                        break;
                    }
                }
            }
            return remoteIpAddress;
        }
        public IPAddress? GetRemoteHostIpAddressUsingXRealIp(HttpContext httpContext)
        {
            IPAddress? remoteIpAddress = null;
            var xRealIpExists = httpContext.Request.Headers.TryGetValue("X-Real-IP", out var xRealIp);
            if (xRealIpExists)
            {
                if (!IPAddress.TryParse(xRealIp, out IPAddress? address))
                {
                    return remoteIpAddress;
                }
                var isValidIP = (address.AddressFamily is AddressFamily.InterNetwork
                                 or AddressFamily.InterNetworkV6);

                if (isValidIP)
                {
                    remoteIpAddress = address;
                }
                return remoteIpAddress;
            }
            return remoteIpAddress;
        }
    }
}
