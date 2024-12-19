using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


const string serverAddress = "::1";
const int serverPort = 4600;

using var udpClient = new UdpClient(AddressFamily.InterNetworkV6);
udpClient.Connect(IPAddress.Parse(serverAddress), serverPort);

Console.WriteLine("Enter messages to send to the server:");

var message = Console.ReadLine();

var data = Encoding.UTF8.GetBytes(message);
udpClient.Send(data, data.Length);

IPEndPoint remoteEndPoint = null;
var response = udpClient.Receive(ref remoteEndPoint);
Console.WriteLine($"Response from server: {Encoding.UTF8.GetString(response)}");
