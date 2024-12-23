using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MCDzienny.SettingsFrame
{
    class Test
    {
        static object someValue;

        public static object SerializedValue
        {
            get { return someValue.ToString(); }
            set
            {
                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, value);
                memoryStream.Flush();
                memoryStream.Position = 0L;
                StreamReader streamReader = new StreamReader(memoryStream);
                someValue = streamReader.ReadToEnd();
            }
        }

        public static void Tester()
        {
            Server.s.Log(someValue.ToString());
        }
    }
}