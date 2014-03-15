using System;

namespace Bytes
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "Hello world";
            var b = s.AsBytes();

            Console.WriteLine(string.Format("{0}, {1}, {2}", s, s.Length, s.GetType().Name));
            Console.WriteLine(string.Format("{0}, {1}, {2}", b, b.Length, b.GetType().Name));

            Console.ReadLine();
        }
    }

    public static class ByteExtensions
    {
        public static byte[] AsBytes(this string str)
        {
            var msgBytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, msgBytes, 0, msgBytes.Length);

            return msgBytes;
        }

        public static string AsString(this byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
