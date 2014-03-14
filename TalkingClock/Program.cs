﻿using System;
using System.Net;
using System.Net.Sockets;

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

                Console.WriteLine("Request received, sending repsonse...");

                // send the current time. 
                // communicating at this level could be with a program written in any language so 
                // the most generic form of data, bytes, are used
                client.Send(GetTime());
            }
        }

        private static byte[] GetTime()
        {
            var timeString = DateTime.Now.ToLongTimeString();
            var time = new byte[timeString.Length * sizeof(char)];
            Buffer.BlockCopy(timeString.ToCharArray(), 0, time, 0, time.Length);

            return time;
        }
    }
}