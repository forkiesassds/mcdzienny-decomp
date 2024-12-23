using System;
using System.Net;
using System.Text;

namespace MCDzienny.Communication
{
    public class Packets
    {
        public static readonly byte ChangeModel = 29;

        public byte[] MakeEnvSetColor(byte type, short red, short green, short blue)
        {
            byte[] array = new byte[8]
            {
                25, type, 0, 0, 0, 0, 0, 0
            };
            byte[] bytes = BitConverter.GetBytes(red);
            array[2] = bytes[0];
            array[3] = bytes[1];
            byte[] bytes2 = BitConverter.GetBytes(green);
            array[4] = bytes2[0];
            array[5] = bytes2[1];
            byte[] bytes3 = BitConverter.GetBytes(blue);
            array[6] = bytes3[0];
            array[7] = bytes3[1];
            return array;
        }

        public byte[] MakeChangeModel(byte entityId, string model)
        {
            if (model.Length > 64)
            {
                throw new ArgumentException("model can't be longer than 64 chars");
            }
            byte[] array = new byte[66];
            array[0] = ChangeModel;
            array[1] = entityId;
            byte[] bytes = Encoding.ASCII.GetBytes(model.PadRight(64));
            Array.Copy(bytes, 0, array, 2, 64);
            return array;
        }

        public byte[] MakeExtInfo(string serverName, int extensionCount)
        {
            if (serverName == null)
            {
                throw new ArgumentNullException("serverName");
            }
            if (serverName.Length > 64)
            {
                throw new ArgumentException("serverName can't be longer than 64 chars.");
            }
            if (extensionCount < 0 || extensionCount > 32767)
            {
                throw new ArgumentOutOfRangeException("extensionCount has to be within the inclusive range 0..32767");
            }
            byte[] array = new byte[67];
            array[0] = 16;
            byte[] bytes = Encoding.ASCII.GetBytes(serverName.PadRight(64));
            int num = bytes.Length;
            if (num > 64)
            {
                Server.s.Log("ExtInfo: Incorrect serverName length. The name can't take more than 64 bytes.");
                num = 64;
            }
            Array.Copy(bytes, 0, array, 1, num);
            short host = (short)extensionCount;
            byte[] bytes2 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(host));
            Array.Copy(bytes2, 0, array, 65, 2);
            return array;
        }

        public byte[] MakeExtEntry(string extensionName, int version)
        {
            if (extensionName == null)
            {
                throw new ArgumentNullException("extensionName");
            }
            if (extensionName.Length > 64)
            {
                throw new ArgumentException("extensionName can't be longer than 64 chars.");
            }
            byte[] array = new byte[69];
            array[0] = 17;
            byte[] bytes = Encoding.ASCII.GetBytes(extensionName.PadRight(64));
            if (bytes.Length > 64)
            {
                Server.s.Log("ExtEntry: Incorrect extensionName length. The name can't take more than 64 bytes.");
            }
            Array.Copy(bytes, 0, array, 1, bytes.Length);
            BitConverter.GetBytes(version);
            Array.Copy(new byte[4]
            {
                0, 0, 0, 1
            }, 0, array, 65, 4);
            return array;
        }
    }
}