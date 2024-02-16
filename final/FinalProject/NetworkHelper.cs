using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

class NetworkHelper
{
  public static void GetLocalIp()
  {
    NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
        .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
        .ToArray();

    foreach (NetworkInterface networkInterface in networkInterfaces)
    {
        IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
        IPAddress ipAddress = ipProperties.UnicastAddresses
            .FirstOrDefault(addr => addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.Address;

        if (ipAddress != null)
        {
            Console.WriteLine($"Interface: {networkInterface.Name}, IP Address: {ipAddress}");
        }
    }
  }
}