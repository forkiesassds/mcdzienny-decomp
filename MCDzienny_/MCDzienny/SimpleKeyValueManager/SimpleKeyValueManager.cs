using System.Collections.Generic;
using System.IO;

namespace MCDzienny.SimpleKeyValueManager
{
    // Token: 0x02000231 RID: 561
    public class SimpleKeyValueManager
    {
        // Token: 0x04000890 RID: 2192
        private readonly string directory;

        // Token: 0x04000891 RID: 2193

        // Token: 0x0600105A RID: 4186 RVA: 0x00055B44 File Offset: 0x00053D44
        public SimpleKeyValueManager(string filePath)
        {
            directory = filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            if (!File.Exists(filePath)) File.Create(filePath).Close();
            Load();
        }

        // Token: 0x0600105B RID: 4187 RVA: 0x00055B78 File Offset: 0x00053D78
        public SimpleKeyValueManager(string filePath, string initialContent)
        {
            directory = filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            if (!File.Exists(filePath))
                using (var streamWriter = new StreamWriter(File.Create(filePath)))
                {
                    streamWriter.Write(initialContent);
                    streamWriter.Flush();
                }

            Load();
        }

        // Token: 0x170005E1 RID: 1505
        // (get) Token: 0x06001059 RID: 4185 RVA: 0x00055B3C File Offset: 0x00053D3C
        public Dictionary<string, string> KeyValues { get; private set; }

        // Token: 0x0600105C RID: 4188 RVA: 0x00055BE4 File Offset: 0x00053DE4
        public void Load()
        {
            KeyValues = new Dictionary<string, string>();
            using (var streamReader = new StreamReader(File.Open(directory, FileMode.Open)))
            {
                string text;
                while ((text = streamReader.ReadLine()) != null)
                    if (text.IndexOf(':') != -1)
                    {
                        var key = text.Split(':')[0].Trim();
                        var text2 = text.Split(':')[1].Trim();
                        if (KeyValues.ContainsKey(text2))
                            Server.s.Log("File " + directory + " contains duplicates.");
                        else
                            KeyValues.Add(key, text2);
                    }
            }
        }

        // Token: 0x0600105D RID: 4189 RVA: 0x00055CC0 File Offset: 0x00053EC0
        public void Save()
        {
            using (var streamWriter = new StreamWriter(File.Open(directory, FileMode.Create)))
            {
                foreach (var keyValuePair in KeyValues)
                    streamWriter.WriteLine(keyValuePair.Key + " : " + keyValuePair.Value);
            }
        }
    }
}