using System.Collections.Generic;
using System.IO;

namespace MCDzienny.SimpleKeyValueManager
{
    public class SimpleKeyValueManager
    {
        readonly string directory;

        public SimpleKeyValueManager(string filePath)
        {
            directory = filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            Load();
        }

        public SimpleKeyValueManager(string filePath, string initialContent)
        {
            directory = filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            if (!File.Exists(filePath))
            {
                using (StreamWriter streamWriter = new StreamWriter(File.Create(filePath)))
                {
                    streamWriter.Write(initialContent);
                    streamWriter.Flush();
                }
            }
            Load();
        }

        public Dictionary<string, string> KeyValues { get; private set; }

        public void Load()
        {
            KeyValues = new Dictionary<string, string>();
            using (StreamReader streamReader = new StreamReader(File.Open(directory, FileMode.Open)))
            {
                string text;
                while ((text = streamReader.ReadLine()) != null)
                {
                    if (text.IndexOf(':') != -1)
                    {
                        string key = text.Split(':')[0].Trim();
                        string text2 = text.Split(':')[1].Trim();
                        if (KeyValues.ContainsKey(text2))
                        {
                            Server.s.Log("File " + directory + " contains duplicates.");
                        }
                        else
                        {
                            KeyValues.Add(key, text2);
                        }
                    }
                }
            }
        }

        public void Save()
        {
            using (StreamWriter streamWriter = new StreamWriter(File.Open(directory, FileMode.Create)))
            {
                foreach (KeyValuePair<string, string> keyValue in KeyValues)
                {
                    streamWriter.WriteLine(keyValue.Key + " : " + keyValue.Value);
                }
            }
        }
    }
}