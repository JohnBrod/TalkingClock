using System;
using System.Net;
using System.Net.Sockets;

namespace AskingClient
{
    public class Program
    {
        static void Main()
        {
            // connect to where the talking clock is listening, this will not work if the it isn't there
            // it should be on 4510 on this machine
            var ipAddr = Dns.GetHostAddresses("")[0];
            var askTime = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            askTime.Connect(ipAddr, 4510);

            while (true)
            {
                askTime.Send("Time".AsBytes());

                // need something to store the incoming response
                var response = new byte[100];

                // Receive will block until we get a response
                askTime.Receive(response);

                Console.WriteLine("Time is " + response.AsString());

                Console.ReadLine();
            }
        }
    }
}
