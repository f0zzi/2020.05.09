using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostName = Dns.GetHostName();
            Console.WriteLine(hostName);

            PrintHostInfo(hostName);
        }

        private static void PrintHostInfo(string hostName)
        {
            IPHostEntry entry = Dns.GetHostEntry(hostName);
            Console.WriteLine(entry.HostName);
            foreach (IPAddress ip in entry.AddressList)
            {
                Console.WriteLine(ip);
            }

            foreach (var item in entry.Aliases)
            {
                Console.WriteLine(item);
            }
        }
    }
}
