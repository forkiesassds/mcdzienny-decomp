using System.Collections.Generic;
using System.IO;

namespace MCDzienny.RemoteAccess
{
    public class BanListManager
    {
        readonly string directory;

        public BanListManager(string fullDirectory)
        {
            directory = fullDirectory;
            Directory.CreateDirectory(Path.GetDirectoryName(fullDirectory));
            if (!File.Exists(fullDirectory))
            {
                File.Create(fullDirectory).Close();
            }
            Load();
        }

        public List<string> BannnedIPs { get; private set; }

        public void Load()
        {
            BannnedIPs = new List<string>();
            using (StreamReader streamReader = new StreamReader(File.Open(directory, FileMode.Open)))
            {
                string item;
                while ((item = streamReader.ReadLine()) != null)
                {
                    BannnedIPs.Add(item);
                }
            }
        }

        public void Save()
        {
            using (StreamWriter streamWriter = new StreamWriter(File.Open(directory, FileMode.Create)))
            {
                foreach (string bannedIP in BannnedIPs)
                {
                    streamWriter.WriteLine(bannedIP);
                }
            }
        }
    }
}