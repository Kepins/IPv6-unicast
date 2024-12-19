using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

const int port = 4600; // Replace with port based on your index
var tcpEndpoint = new IPEndPoint(IPAddress.IPv6Any, port);

var tcpListener = new TcpListener(tcpEndpoint);
tcpListener.Start();
Console.WriteLine($"TCP Server listening on [{tcpEndpoint.Address}]:{tcpEndpoint.Port}");

while (true)
{
    var client = tcpListener.AcceptTcpClient();
    var clientEndpoint = client.Client.RemoteEndPoint.ToString();
    Console.WriteLine($"Connected to client: {clientEndpoint}");

    using var networkStream = client.GetStream();
    var buffer = new byte[1024];
    var bytesRead = networkStream.Read(buffer, 0, buffer.Length);

    var receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    var timestamp = DateTime.UtcNow.ToString("o");
    Console.WriteLine($"Message from {clientEndpoint}: {receivedData} at {timestamp}");

    // Send response
    var response = Encoding.UTF8.GetBytes($"Echo: {receivedData} | Timestamp: {timestamp}");
    networkStream.Write(response, 0, response.Length);

    client.Close();
}