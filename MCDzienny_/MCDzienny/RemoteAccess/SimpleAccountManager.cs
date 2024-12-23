using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny.RemoteAccess
{
    public class SimpleAccountManager
    {
        readonly string directory;

        public SimpleAccountManager(string fullDirectory)
        {
            directory = fullDirectory;
            Directory.CreateDirectory(Path.GetDirectoryName(fullDirectory));
            if (!File.Exists(fullDirectory))
            {
                File.Create(fullDirectory).Close();
            }
            Load();
        }

        public AccountsCollection Accounts { get; private set; }

        public event EventHandler ElementChanged { add { Accounts.ElementChanged += value; } remove { Accounts.ElementChanged -= value; } }

        public void Load()
        {
            Accounts = new AccountsCollection();
            using (StreamReader streamReader = new StreamReader(File.Open(directory, FileMode.Open)))
            {
                string text;
                while ((text = streamReader.ReadLine()) != null)
                {
                    if (text.IndexOf(':') != -1)
                    {
                        string login = text.Split(':')[0].Trim();
                        string text2 = text.Split(':')[1].Trim();
                        if (Accounts.ContainsKey(text2))
                        {
                            Server.s.Log("File " + directory + " contains duplicates.");
                        }
                        else
                        {
                            Accounts.AddEncrypted(login, text2);
                        }
                    }
                }
            }
        }

        public void Save()
        {
            using (StreamWriter streamWriter = new StreamWriter(File.Open(directory, FileMode.Create)))
            {
                foreach (KeyValuePair<string, string> account in Accounts)
                {
                    streamWriter.WriteLine(account.Key + " : " + account.Value);
                }
            }
        }
    }
}