﻿using System;
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
                askTime.Send(Message("Time"));

                // need something to store the incoming response
                var response = new byte[100];

                // Receive will block until we get a response
                askTime.Receive(response);

                // communication was in bytes so it should be converted into a string so it can be displayed
                var time = GetString(response).TrimEnd('\0');

                Console.WriteLine("Time is " + time);

                Console.ReadLine();
            }
        }

        private static byte[] Message(string msg)
        {
            var msgBytes = new byte[msg.Length * sizeof(char)];
            Buffer.BlockCopy(msg.ToCharArray(), 0, msgBytes, 0, msgBytes.Length);

            return msgBytes;
        }

        static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
