using System;
using System.Net;
using System.Linq;
using System.Net.NetworkInformation;

class NetworkHelper
{
  public static void GetLocalIp()
  {
    // Get all network interfaces
        NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

        // Filter out loopback and non-operational interfaces
        NetworkInterface localInterface = networkInterfaces
            .FirstOrDefault(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback && n.OperationalStatus == OperationalStatus.Up);

        if (localInterface != null)
        {
            IPInterfaceProperties ipProperties = localInterface.GetIPProperties();
            UnicastIPAddressInformationCollection unicastAddresses = ipProperties.UnicastAddresses;

            foreach (UnicastIPAddressInformation unicastAddress in unicastAddresses)
            {
                if (unicastAddress.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    Console.WriteLine($"Interface: {localInterface.Name}, IP Address: {unicastAddress.Address}");
                    break; // Exit the loop after printing the first IP address
                }
            }
        }
        else
        {
            Console.WriteLine("No active local interfaces found.");
        }
  }
}