using System;
using System.Linq;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

class NetworkHelper
{
  int _port = 12345;

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

      if (ipAddress != null && ipAddress.ToString().Contains("192.168.1"))
      {
        result = ipAddress.ToString();
        break;
      }
    }

    return result;
  }

  public void AnnounceService(string serviceName, int servicePort)
  {
    string broadcastAddress = "255.255.255.255";

    using (UdpClient client = new UdpClient())
    {
      IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(broadcastAddress), _port);
      string message = $"{serviceName}:{servicePort}";
      byte[] bytes = Encoding.ASCII.GetBytes(message);
      client.Send(bytes, bytes.Length, endPoint);
    }
  }

  public bool DiscoverServices(int timeout)
  {
    using (UdpClient listener = new UdpClient(_port))
    {
      try
      {
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, _port);
        listener.Client.ReceiveTimeout = timeout;

        byte[] bytes = listener.Receive(ref groupEP);
        string message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        Console.WriteLine($"Discovered service: {message}");
        return true;
      }
      catch (SocketException ex)
      {
        return false;
      }
    }
  }
}