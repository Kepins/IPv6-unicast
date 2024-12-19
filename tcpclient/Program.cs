using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

const string serverAddress = "::1";
const int serverPort = 4600;

using var client = new TcpClient(AddressFamily.InterNetworkV6);
client.Connect(IPAddress.Parse(serverAddress), serverPort);

Console.WriteLine("Connected to the server. Enter messages to send (type 'exit' to quit):");

using var networkStream = client.GetStream();

var message = Console.ReadLine();

var data = Encoding.UTF8.GetBytes(message);
networkStream.Write(data, 0, data.Length);

var buffer = new byte[1024];
var bytesRead = networkStream.Read(buffer, 0, buffer.Length);
Console.WriteLine($"Response from server: {Encoding.UTF8.GetString(buffer, 0, bytesRead)}");
