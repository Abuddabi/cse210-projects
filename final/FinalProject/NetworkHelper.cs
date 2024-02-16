using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

class NetworkHelper
{
  public static void GetLocalIp()
  {
    string hostName = Dns.GetHostName();

        // Get the IP addresses associated with the host
        IPAddress[] addresses = Dns.GetHostAddresses(hostName);

        // Find the IPv4 address
        foreach (IPAddress address in addresses)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                Console.WriteLine($"Local IP Address: {address}");
                break;
            }
        }
  }
}