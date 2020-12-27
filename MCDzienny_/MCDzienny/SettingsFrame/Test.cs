using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x02000227 RID: 551
    internal class Test
    {
        // Token: 0x04000880 RID: 2176
        private static object someValue;

        // Token: 0x17000558 RID: 1368
        // (get) Token: 0x06000F28 RID: 3880 RVA: 0x00053AA0 File Offset: 0x00051CA0
        // (set) Token: 0x06000F29 RID: 3881 RVA: 0x00053AAC File Offset: 0x00051CAC
        public static object SerializedValue
        {
            get { return someValue.ToString(); }
            set
            {
                var memoryStream = new MemoryStream();
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, value);
                memoryStream.Flush();
                memoryStream.Position = 0L;
                var streamReader = new StreamReader(memoryStream);
                someValue = streamReader.ReadToEnd();
            }
        }

        // Token: 0x06000F27 RID: 3879 RVA: 0x00053A88 File Offset: 0x00051C88
        public static void Tester()
        {
            Server.s.Log(someValue.ToString());
        }
    }
}