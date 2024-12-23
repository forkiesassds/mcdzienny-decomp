using System;
using System.IO;
using System.Net;
using System.Text;

namespace MCDzienny
{
    public class ByteStream : MemoryStream
    {
        public ByteStream() {}

        public ByteStream(int capacity)
            : base(capacity) {}

        public void WriteInt16(short value)
        {
            byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
            Write(bytes, 0, 2);
        }

        public void WriteInt32(int value)
        {
            byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
            Write(bytes, 0, 4);
        }

        public void WriteString(string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            Write(bytes, 0, bytes.Length);
        }
    }
}