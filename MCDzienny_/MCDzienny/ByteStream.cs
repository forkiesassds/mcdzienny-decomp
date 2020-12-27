using System;
using System.IO;
using System.Net;
using System.Text;

namespace MCDzienny
{
    // Token: 0x0200000D RID: 13
    public class ByteStream : MemoryStream
    {
        // Token: 0x0600003D RID: 61 RVA: 0x000034A0 File Offset: 0x000016A0
        public ByteStream()
        {
        }

        // Token: 0x0600003E RID: 62 RVA: 0x000034A8 File Offset: 0x000016A8
        public ByteStream(int capacity) : base(capacity)
        {
        }

        // Token: 0x0600003F RID: 63 RVA: 0x000034B4 File Offset: 0x000016B4
        public void WriteInt16(short value)
        {
            var bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
            Write(bytes, 0, 2);
        }

        // Token: 0x06000040 RID: 64 RVA: 0x000034D8 File Offset: 0x000016D8
        public void WriteInt32(int value)
        {
            var bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
            Write(bytes, 0, 4);
        }

        // Token: 0x06000041 RID: 65 RVA: 0x000034FC File Offset: 0x000016FC
        public void WriteString(string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            Write(bytes, 0, bytes.Length);
        }
    }
}