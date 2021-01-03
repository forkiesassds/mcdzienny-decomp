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
    // Token: 0x020001D5 RID: 469
    internal class DevMessages
    {
        // Token: 0x040006C8 RID: 1736
        private const int Second = 1000;

        // Token: 0x040006C9 RID: 1737
        private const int Minute = 60000;

        // Token: 0x040006CA RID: 1738
        private static readonly int CheckFrequency = 900000;

        // Token: 0x040006CE RID: 1742
        private readonly string cfgFilePath = "properties/news.cfg";

        // Token: 0x040006CB RID: 1739
        private Timer checker;

        // Token: 0x040006CD RID: 1741
        private readonly string feedURL = "http://mcdzienny.cba.pl/newsfeed.xml";

        // Token: 0x040006CC RID: 1740
        private volatile bool isStopped;

        // Token: 0x06000D16 RID: 3350 RVA: 0x0004B098 File Offset: 0x00049298
        public List<string> GetNewItemsIDs(XPathDocument xdoc)
        {
            var list = new List<string>();
            list.AddRange(GetItemsIDs(xdoc, Channel.General.ID));
            if (Server.IsLavaModeOn()) list.AddRange(GetItemsIDs(xdoc, Channel.Lava.ID));
            if (Server.IsZombieModeOn()) list.AddRange(GetItemsIDs(xdoc, Channel.Zombie.ID));
            if (Server.IsFreebuildModeOn()) list.AddRange(GetItemsIDs(xdoc, Channel.Freebuild.ID));
            return FindNewItems(list);
        }

        // Token: 0x06000D17 RID: 3351 RVA: 0x0004B128 File Offset: 0x00049328
        private List<string> GetItemsIDs(XPathDocument nsDocument, string channelID)
        {
            var list = new List<string>();
            var xpathNavigator = nsDocument.CreateNavigator();
            var xpathNodeIterator = xpathNavigator.Select(string.Format("/sn/channel[id={0}]/item/id", channelID));
            while (xpathNodeIterator.MoveNext())
            {
                var xpathNavigator2 = xpathNodeIterator.Current;
                list.Add(xpathNavigator2.Value);
            }

            return list;
        }

        // Token: 0x06000D18 RID: 3352 RVA: 0x0004B170 File Offset: 0x00049370
        private List<string> FindNewItems(List<string> sourceIDs)
        {
            var list = new List<string>();
            var list2 = new List<string>();
            using (var streamReader = new StreamReader(GetIDsFromCfgStream()))
            {
                string text;
                while ((text = streamReader.ReadLine()) != null)
                {
                    text = text.Trim();
                    if (!text.StartsWith("#"))
                    {
                        foreach (var text2 in sourceIDs)
                            if (text2 == text)
                                list2.Add(text2);
                        if (sourceIDs.Count == list2.Count) return new List<string>();
                    }
                }
            }

            foreach (var item in sourceIDs)
                if (!list2.Contains(item))
                    list.Add(item);
            return list;
        }

        // Token: 0x06000D19 RID: 3353 RVA: 0x0004B280 File Offset: 0x00049480
        private FileStream GetIDsFromCfgStream()
        {
            if (!File.Exists(cfgFilePath))
                using (var streamWriter = File.CreateText(cfgFilePath))
                {
                    streamWriter.WriteLine("# IDs of news that were already downloaded");
                }

            return new FileStream(cfgFilePath, FileMode.Open);
        }

        // Token: 0x06000D1A RID: 3354 RVA: 0x0004B2DC File Offset: 0x000494DC
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

        // Token: 0x06000D1B RID: 3355 RVA: 0x0004B330 File Offset: 0x00049530
        private void checker_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isStopped) return;
            try
            {
                UpdateMessages();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000D1C RID: 3356 RVA: 0x0004B36C File Offset: 0x0004956C
        private void UpdateMessages()
        {
            try
            {
                var webRequest = WebRequest.Create(feedURL);
                using (var responseStream = webRequest.GetResponse().GetResponseStream())
                {
                    var xdoc = new XPathDocument(responseStream);
                    var newItemsIDs = GetNewItemsIDs(xdoc);
                    if (newItemsIDs.Count != 0)
                    {
                        var itemsByIDs = GetItemsByIDs(xdoc, newItemsIDs);
                        if (itemsByIDs.Count != 0) SpreadMessages(itemsByIDs);
                    }
                }
            }
            catch (WebException)
            {
            }
        }

        // Token: 0x06000D1D RID: 3357 RVA: 0x0004B3F4 File Offset: 0x000495F4
        private void SpreadMessages(ItemCollection newItems)
        {
            newItems.SortByPubDateIncreasing();
            SaveIDsToCfg(newItems);
            foreach (var obj in newItems)
            {
                var item = (Item) obj;
                SendMessageToInbox(item.Content, item.Author, GeneralSettings.All.DevMessagePermission);
            }

            if (!Server.CLI) ShowPopUpMessage(newItems);
        }

        // Token: 0x06000D1E RID: 3358 RVA: 0x0004B478 File Offset: 0x00049678
        private void SaveIDsToCfg(ItemCollection newItems)
        {
            var tempFileName = Path.GetTempFileName();
            using (var stream = File.Open(cfgFilePath, FileMode.Open))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var streamWriter = new StreamWriter(tempFileName))
                    {
                        foreach (Item newItem in newItems) streamWriter.WriteLine(newItem.ID);
                        while (!streamReader.EndOfStream) streamWriter.WriteLine(streamReader.ReadLine());
                    }
                }
            }

            File.Copy(tempFileName, cfgFilePath, true);
        }

        // Token: 0x06000D1F RID: 3359 RVA: 0x0004B564 File Offset: 0x00049764
        private void ShowPopUpMessage(ItemCollection newItems)
        {
            var stringBuilder = new StringBuilder();
            for (var i = newItems.Count - 1; i >= 0; i--)
                stringBuilder.AppendLine("-------------------------------------------------").AppendLine()
                    .Append("Written by: ").AppendLine(newItems[i].Author).Append("Published: ")
                    .AppendLine(newItems[i].PubDate.ToString()).AppendLine().AppendLine("Message:").AppendLine()
                    .AppendLine(newItems[i].Content).AppendLine();
            var popUpMessage = new PopUpMessage(stringBuilder.ToString());
            popUpMessage.ShowDialog();
        }

        // Token: 0x06000D20 RID: 3360 RVA: 0x0004B624 File Offset: 0x00049824
        private void SendMessageToInbox(string message, string from, int toPermission)
        {
            var source = from g in Group.groupList
                where g.Permission >= (LevelPermission) toPermission
                select g;
            var selectedPlayers = source.SelectMany(g => g.playerList.All());
            try
            {
                foreach (var to in selectedPlayers) Player.SendToInbox(from, to, message);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            var list = (from p in Player.players
                where selectedPlayers.Contains(p.name)
                select p).ToList();
            list.ForEach(delegate(Player p) { p.SendMessage("You've received a new message. Check your /inbox"); });
        }

        // Token: 0x06000D21 RID: 3361 RVA: 0x0004B720 File Offset: 0x00049920
        private ItemCollection GetItemsByIDs(XPathDocument xdoc, List<string> newItemsIDs)
        {
            var itemCollection = new ItemCollection();
            var xpathNavigator = xdoc.CreateNavigator();
            var xpathNodeIterator = xpathNavigator.Select("/sn/channel/item");
            while (xpathNodeIterator.MoveNext())
            {
                var xpathNavigator2 = xpathNodeIterator.Current;
                var value = xpathNavigator2.SelectSingleNode("id").Value;
                if (newItemsIDs.Contains(value))
                {
                    var xpathNavigator3 = xpathNodeIterator.Current.SelectSingleNode("expiration");
                    if (xpathNavigator3 != null && !xpathNavigator3.Value.IsNullOrWhiteSpaced())
                        if (DateTime.Parse(xpathNavigator3.Value) >= DateTime.Now)
                            continue;
                    try
                    {
                        var xpathNavigator4 = xpathNodeIterator.Current.SelectSingleNode("version");
                        if (xpathNavigator4 != null && new Version(xpathNavigator4.Value) <
                            new Version(FileVersionInfo.GetVersionInfo("MCDzienny_.dll").FileVersion)) continue;
                    }
                    catch
                    {
                    }

                    var value2 = xpathNodeIterator.Current.SelectSingleNode("content").Value;
                    var priority = Priority.Normal;
                    var xpathNavigator5 = xpathNodeIterator.Current.SelectSingleNode("priority");
                    if (xpathNavigator5 != null && !xpathNavigator5.Value.IsNullOrWhiteSpaced())
                        priority = (Priority) Enum.Parse(typeof(Priority),
                            xpathNodeIterator.Current.SelectSingleNode("priority").Value);
                    var author = "Unknown";
                    var xpathNavigator6 = xpathNodeIterator.Current.SelectSingleNode("author");
                    if (xpathNavigator6 != null) author = xpathNavigator6.Value;
                    var pubDate = DateTime.Parse(xpathNodeIterator.Current.SelectSingleNode("pubDate").Value);
                    var item = new Item(value, value2, priority, author, pubDate);
                    itemCollection.Add(item);
                }
            }

            return itemCollection;
        }

        // Token: 0x06000D22 RID: 3362 RVA: 0x0004B8D4 File Offset: 0x00049AD4
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