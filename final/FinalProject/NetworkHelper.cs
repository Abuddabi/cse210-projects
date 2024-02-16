using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

class NetworkHelper
{
  public string GetLocalIp()
  {
    string result = "";

    NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
        .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
        .ToArray();

    foreach (NetworkInterface networkInterface in networkInterfaces)
    {
        IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
        IPAddress ipAddress = ipProperties.UnicastAddresses
            .FirstOrDefault(addr => addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.Address;
        string ip = ipAddress.ToString();

        if (ip.Contains("192.168.1"))
        {
          result = ip;
          break; 
        }
    }

    return result;
  }
}