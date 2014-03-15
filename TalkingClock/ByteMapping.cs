using System;

namespace TalkingClock
{
    public static class ByteMapping
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
