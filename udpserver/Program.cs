using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


const int port = 4600;
var udpEndpoint = new IPEndPoint(IPAddress.IPv6Any, port);

using var udpServer = new UdpClient(AddressFamily.InterNetworkV6);
udpServer.Client.Bind(udpEndpoint);
Console.WriteLine($"UDP Server listening on [{udpEndpoint.Address}]:{udpEndpoint.Port}");

while (true)
{
    var result = udpServer.Receive(ref udpEndpoint);
    var receivedData = Encoding.UTF8.GetString(result);
    var timestamp = DateTime.UtcNow.ToString("o");

    Console.WriteLine($"Received message from [{udpEndpoint.Address}]:{udpEndpoint.Port} at {timestamp}: {receivedData}");

    // Send echo response
    var response = $"Echo: {receivedData} | From: [{udpEndpoint.Address}] | Timestamp: {timestamp}";
    var responseBytes = Encoding.UTF8.GetBytes(response);
    udpServer.Send(responseBytes, responseBytes.Length, udpEndpoint);
}
