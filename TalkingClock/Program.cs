using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TalkingClock
{
    /// <summary>
    /// Start listening for requests and send out the current time whenever one arrives.
    /// </summary>
    public class Program
    {
        static void Main()
        {
            // I'm going to listen for any incoming requests on this machine so I need to find out 
            // what my address is. An empty string tells it to get the address of this machine
            var ipAddr = Dns.GetHostAddresses("")[0];

            // create a server socket, its only purpose is to listen for connections 
            // and to create client sockets to handle requests
            // going to use TCP to communicate
            var server = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // the server will be located on port 4510 on this machine
            server.Bind(new IPEndPoint(ipAddr, 4510));

            // start listening and allow for a backlog of 10 request between our accepts
            server.Listen(10);

            Console.WriteLine("Waiting to get asked the time");

            while (true)
            {
                // Wait for a client to connect, Accept is blocking
                // request is a client socket and will be used for this piece of communication
                var client = server.Accept();

                Console.WriteLine("Connection accepted");

                Task.Factory.StartNew(() => Chat(client));
            }
        }

        private static void Chat(Socket client)
        {
            while (true)
            {
                var request = new byte[100];
                client.Receive(request);
                Console.WriteLine("Request received, sending repsonse...");
                client.Send(DateTime.Now.ToLongTimeString().AsBytes());
            }
        }
    }
}
