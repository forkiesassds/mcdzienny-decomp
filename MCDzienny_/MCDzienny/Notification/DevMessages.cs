using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Xml.XPath;
using MCDzienny.Gui;
using MCDzienny.Misc;
using MCDzienny.Settings;

namespace MCDzienny.Notification
{
    class DevMessages
    {
        const int Second = 1000;

        const int Minute = 60000;

        static readonly int CheckFrequency = 900000;

        readonly string cfgFilePath = "properties/news.cfg";

        readonly string feedURL = "http://mcdzienny.cba.pl/newsfeed.xml";

        Timer checker;

        volatile bool isStopped;

        public List<string> GetNewItemsIDs(XPathDocument xdoc)
        {
            var list = new List<string>();
            list.AddRange(GetItemsIDs(xdoc, Channel.General.ID));
            if (Server.IsLavaModeOn())
            {
                list.AddRange(GetItemsIDs(xdoc, Channel.Lava.ID));
            }
            if (Server.IsZombieModeOn())
            {
                list.AddRange(GetItemsIDs(xdoc, Channel.Zombie.ID));
            }
            if (Server.IsFreebuildModeOn())
            {
                list.AddRange(GetItemsIDs(xdoc, Channel.Freebuild.ID));
            }
            return FindNewItems(list);
        }

        List<string> GetItemsIDs(XPathDocument nsDocument, string channelID)
        {
            var list = new List<string>();
            XPathNavigator val = nsDocument.CreateNavigator();
            XPathNodeIterator val2 = val.Select(string.Format("/sn/channel[id={0}]/item/id", channelID));
            while (val2.MoveNext())
            {
                list.Add(val2.Current.Value);
            }
            return list;
        }

        List<string> FindNewItems(List<string> sourceIDs)
        {
            var list = new List<string>();
            var list2 = new List<string>();
            using (StreamReader streamReader = new StreamReader(GetIDsFromCfgStream()))
            {
                string text;
                while ((text = streamReader.ReadLine()) != null)
                {
                    text = text.Trim();
                    if (text.StartsWith("#"))
                    {
                        continue;
                    }
                    foreach (string sourceID in sourceIDs)
                    {
                        if (sourceID == text)
                        {
                            list2.Add(sourceID);
                        }
                    }
                    if (sourceIDs.Count == list2.Count)
                    {
                        return new List<string>();
                    }
                }
            }
            foreach (string sourceID2 in sourceIDs)
            {
                if (!list2.Contains(sourceID2))
                {
                    list.Add(sourceID2);
                }
            }
            return list;
        }

        FileStream GetIDsFromCfgStream()
        {
            if (!File.Exists(cfgFilePath))
            {
                using (StreamWriter streamWriter = File.CreateText(cfgFilePath))
                {
                    streamWriter.WriteLine("# IDs of news that were already downloaded");
                }
            }
            return new FileStream(cfgFilePath, FileMode.Open);
        }

        public void Start()
        {
            if (checker == null)
            {
                checker = new Timer(CheckFrequency);
                checker.Elapsed += checker_Elapsed;
            }
            isStopped = false;
            checker.Start();
        }

        void checker_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isStopped)
            {
                return;
            }
            try
            {
                UpdateMessages();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        void UpdateMessages()
        {
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            //IL_001f: Expected O, but got Unknown
            try
            {
                WebRequest webRequest = WebRequest.Create(feedURL);
                using (Stream stream = webRequest.GetResponse().GetResponseStream())
                {
                    XPathDocument xdoc = new XPathDocument(stream);
                    List<string> newItemsIDs = GetNewItemsIDs(xdoc);
                    if (newItemsIDs.Count != 0)
                    {
                        ItemCollection itemsByIDs = GetItemsByIDs(xdoc, newItemsIDs);
                        if (itemsByIDs.Count != 0)
                        {
                            SpreadMessages(itemsByIDs);
                        }
                    }
                }
            }
            catch (WebException) {}
        }

        void SpreadMessages(ItemCollection newItems)
        {
            newItems.SortByPubDateIncreasing();
            SaveIDsToCfg(newItems);
            foreach (Item newItem in newItems)
            {
                SendMessageToInbox(newItem.Content, newItem.Author, GeneralSettings.All.DevMessagePermission);
            }
            if (!Server.CLI)
            {
                ShowPopUpMessage(newItems);
            }
        }

        void SaveIDsToCfg(ItemCollection newItems)
        {
            string tempFileName = Path.GetTempFileName();
            using (FileStream stream = File.Open(cfgFilePath, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    using (StreamWriter streamWriter = new StreamWriter(tempFileName))
                    {
                        foreach (Item newItem in newItems)
                        {
                            streamWriter.WriteLine(newItem.ID);
                        }
                        while (!streamReader.EndOfStream)
                        {
                            streamWriter.WriteLine(streamReader.ReadLine());
                        }
                    }
                }
            }
            File.Copy(tempFileName, cfgFilePath, overwrite: true);
        }

        void ShowPopUpMessage(ItemCollection newItems)
        {
            //IL_00ab: Unknown result type (might be due to invalid IL or missing references)
            StringBuilder stringBuilder = new StringBuilder();
            for (int num = newItems.Count - 1; num >= 0; num--)
            {
                stringBuilder.AppendLine("-------------------------------------------------").AppendLine().Append("Written by: ")
                    .AppendLine(newItems[num].Author)
                    .Append("Published: ")
                    .AppendLine(newItems[num].PubDate.ToString())
                    .AppendLine()
                    .AppendLine("Message:")
                    .AppendLine()
                    .AppendLine(newItems[num].Content)
                    .AppendLine();
            }
            PopUpMessage popUpMessage = new PopUpMessage(stringBuilder.ToString());
            popUpMessage.ShowDialog();
        }

        void SendMessageToInbox(string message, string from, int toPermission)
        {
            IEnumerable<Group> source = Group.groupList.Where(g => (int)g.Permission >= toPermission);
            IEnumerable<string> selectedPlayers = source.SelectMany(g => g.playerList.All());
            try
            {
                foreach (string item in selectedPlayers)
                {
                    Player.SendToInbox(from, item, message);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            var list = Player.players.Where(p => selectedPlayers.Contains(p.name)).ToList();
            list.ForEach(delegate(Player p) { p.SendMessage("You've received a new message. Check your /inbox"); });
        }

        ItemCollection GetItemsByIDs(XPathDocument xdoc, List<string> newItemsIDs)
        {
            ItemCollection itemCollection = new ItemCollection();
            XPathNavigator val = xdoc.CreateNavigator();
            XPathNodeIterator val2 = val.Select("/sn/channel/item");
            while (val2.MoveNext())
            {
                string value = val2.Current.SelectSingleNode("id").Value;
                if (!newItemsIDs.Contains(value))
                {
                    continue;
                }
                XPathNavigator val3 = val2.Current.SelectSingleNode("expiration");
                if (val3 != null && !val3.Value.IsNullOrWhiteSpaced() && DateTime.Parse(val3.Value) >= DateTime.Now)
                {
                    continue;
                }
                try
                {
                    XPathNavigator val4 = val2.Current.SelectSingleNode("version");
                    if (val4 != null && new Version(val4.Value) < new Version(FileVersionInfo.GetVersionInfo("MCDzienny_.dll").FileVersion))
                    {
                        continue;
                    }
                }
                catch {}
                string value2 = val2.Current.SelectSingleNode("content").Value;
                Priority priority = Priority.Normal;
                XPathNavigator val5 = val2.Current.SelectSingleNode("priority");
                if (val5 != null && !val5.Value.IsNullOrWhiteSpaced())
                {
                    priority = (Priority)Enum.Parse(typeof(Priority), val2.Current.SelectSingleNode("priority").Value);
                }
                string author = "Unknown";
                XPathNavigator val6 = val2.Current.SelectSingleNode("author");
                if (val6 != null)
                {
                    author = val6.Value;
                }
                DateTime pubDate = DateTime.Parse(val2.Current.SelectSingleNode("pubDate").Value);
                Item item = new Item(value, value2, priority, author, pubDate);
                itemCollection.Add(item);
            }
            return itemCollection;
        }

        public void Stop()
        {
            if (checker != null)
            {
                isStopped = true;
                checker.Stop();
            }
        }
    }
}