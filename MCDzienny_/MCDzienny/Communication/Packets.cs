using System;
using System.Net;
using System.Text;

namespace MCDzienny.Communication
{
    // Token: 0x02000090 RID: 144
    public class Packets
    {
        // Token: 0x040001E1 RID: 481
        public static readonly byte ChangeModel = 29;

        // Token: 0x060003D4 RID: 980 RVA: 0x00014330 File Offset: 0x00012530
        public byte[] MakeEnvSetColor(byte type, short red, short green, short blue)
        {
            var array = new byte[8];
            array[0] = 25;
            array[1] = type;
            var bytes = BitConverter.GetBytes(red);
            array[2] = bytes[0];
            array[3] = bytes[1];
            var bytes2 = BitConverter.GetBytes(green);
            array[4] = bytes2[0];
            array[5] = bytes2[1];
            var bytes3 = BitConverter.GetBytes(blue);
            array[6] = bytes3[0];
            array[7] = bytes3[1];
            return array;
        }

        // Token: 0x060003D5 RID: 981 RVA: 0x00014388 File Offset: 0x00012588
        public byte[] MakeChangeModel(byte entityId, string model)
        {
            if (model.Length > 64) throw new ArgumentException("model can't be longer than 64 chars");
            var array = new byte[66];
            array[0] = ChangeModel;
            array[1] = entityId;
            var bytes = Encoding.ASCII.GetBytes(model.PadRight(64));
            Array.Copy(bytes, 0, array, 2, 64);
            return array;
        }

        // Token: 0x060003D6 RID: 982 RVA: 0x000143E0 File Offset: 0x000125E0
        public byte[] MakeExtInfo(string serverName, int extensionCount)
        {
            if (serverName == null) throw new ArgumentNullException("serverName");
            if (serverName.Length > 64) throw new ArgumentException("serverName can't be longer than 64 chars.");
            if (extensionCount < 0 || extensionCount > 32767)
                throw new ArgumentOutOfRangeException("extensionCount has to be within the inclusive range 0..32767");
            var array = new byte[67];
            array[0] = 16;
            var bytes = Encoding.ASCII.GetBytes(serverName.PadRight(64));
            var num = bytes.Length;
            if (num > 64)
            {
                Server.s.Log("ExtInfo: Incorrect serverName length. The name can't take more than 64 bytes.");
                num = 64;
            }

            Array.Copy(bytes, 0, array, 1, num);
            var host = (short) extensionCount;
            var bytes2 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(host));
            Array.Copy(bytes2, 0, array, 65, 2);
            return array;
        }

        // Token: 0x060003D7 RID: 983 RVA: 0x0001448C File Offset: 0x0001268C
        public byte[] MakeExtEntry(string extensionName, int version)
        {
            if (extensionName == null) throw new ArgumentNullException("extensionName");
            if (extensionName.Length > 64) throw new ArgumentException("extensionName can't be longer than 64 chars.");
            var array = new byte[69];
            array[0] = 17;
            var bytes = Encoding.ASCII.GetBytes(extensionName.PadRight(64));
            if (bytes.Length > 64)
                Server.s.Log("ExtEntry: Incorrect extensionName length. The name can't take more than 64 bytes.");
            Array.Copy(bytes, 0, array, 1, bytes.Length);
            BitConverter.GetBytes(version);
            Array.Copy(new byte[]
            {
                0,
                0,
                0,
                1
            }, 0, array, 65, 4);
            return array;
        }
    }
}