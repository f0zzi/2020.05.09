using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace socket
{
    class Program
    {
        static string[] index = File.ReadAllLines("index.txt");

        static void Main(string[] args)
        {

            IPAddress ip = IPAddress.Parse("192.168.31.124");
            const int PORT = 2020;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPEndPoint ep = new IPEndPoint(ip, PORT);
                socket.Bind(ep);
                socket.Listen(10);//max queue 10 clients
                while (true)
                {
                    const int size = 1024;
                    Socket client = socket.Accept();
                    byte[] buffer = new byte[size];
                    int countBytes = 0;
                    do
                    {
                        countBytes = client.Receive(buffer);
                    }
                    while (client.Available > 0);
                    string data = Encoding.UTF8.GetString(buffer, 0, countBytes);
                    //Console.WriteLine("Got {0}, {1} bytes", data, countBytes);
                    //Console.WriteLine("Client: {0}", client.RemoteEndPoint);//ip port client
                    //byte[] response = Encoding.UTF8.GetBytes(String.Format("Got: {0} bytes {1}", countBytes, DateTime.Now.ToLongTimeString()));
                    byte[] response = Encoding.UTF8.GetBytes(GetInfo(data));
                    client.Send(response);
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            };

        }
        static string GetInfo(string text)
        {
            foreach (string item in index)
            {
                if(item.Contains(text))
                {
                    string[] info = item.Split('-');
                    return info[(info[0] == text ? 1 : 0)];
                }
            }
            return "No such info";
        }
    }
}
