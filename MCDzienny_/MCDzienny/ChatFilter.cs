using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MCDzienny.Misc;
using MCDzienny.Settings;

namespace MCDzienny
{
    public class ChatFilter
    {

        [Flags]
        public enum BadLanguageAction
        {
            Null = 0,
            DisplaySubstitution = 1,
            SendWarning = 2
        }

        public enum BadLanguageDetectionLevel
        {
            Normal,
            High
        }

        [Flags]
        public enum CharacterSpamAction
        {
            Null = 0,
            DisplaySubstitution = 1,
            SendWarning = 2
        }

        const string KeyBadLanguageWarnings = "BadLanguageWarnings";

        const string KeySpamWarnings = "SpamWarnings";

        const string KeyRecentMessagesTimes = "RecentMessagesDates";

        readonly string badWordsPath = "text/badwords.txt";

        readonly Dictionary<string, string> masks;

        readonly string whiteWordsPath = "text/whitewords.txt";

        List<string> badWords;

        int maxCaps = 3;

        List<string> whiteWords;

        public ChatFilter()
        {
            masks = new Dictionary<string, string>();
            masks.Add("@", "a");
            masks.Add("(", "c");
            masks.Add("3", "e");
            masks.Add("ph", "f");
            masks.Add("6", "g");
            masks.Add("!", "i");
            masks.Add("1", "i");
            masks.Add("0", "o");
            masks.Add("9", "q");
            masks.Add("$", "s");
            masks.Add("5", "s");
            masks.Add("+", "t");
            masks.Add("vv", "w");
            masks.Add("2", "z");
        }

        public int MaxCaps { get { return maxCaps; } set { maxCaps = value; } }

        void IncreaseWarningsAndKick(Player p)
        {
            if (p.ExtraData.ContainsKey("BadLanguageWarnings"))
            {
                int num = (int)p.ExtraData["BadLanguageWarnings"] + 1;
                if (num > ChatFilterSettings.All.BadLanguageWarningLimit)
                {
                    p.Kick(ChatFilterSettings.All.BadLanguageKickMessage);
                }
                else
                {
                    p.ExtraData["BadLanguageWarnings"] = num;
                }
            }
            else
            {
                p.ExtraData.Add("BadLanguageWarnings", 1);
            }
        }

        public void Initialize()
        {
            LoadBadWords();
            LoadWhiteWords();
            Player.PlayerChatEvent += FilterChat;
        }

        public void LoadBadWords()
        {
            try
            {
                badWords = new List<string>();
                FileUtil.CreateIfNotExists(badWordsPath);
                string[] array = File.ReadAllLines(badWordsPath);
                foreach (string text in array)
                {
                    string text2 = text.Trim();
                    if (!text2.StartsWith("#") && !string.IsNullOrEmpty(text2))
                    {
                        badWords.Add(Regex.Escape(text2.ToLower()));
                    }
                }
            }
            catch (Exception ex)
            {
                Server.s.Log("Error during bad words loading.");
                Server.ErrorLog(ex);
            }
        }

        public void LoadWhiteWords()
        {
            try
            {
                whiteWords = new List<string>();
                FileUtil.CreateIfNotExists(whiteWordsPath);
                string[] array = File.ReadAllLines(whiteWordsPath);
                foreach (string text in array)
                {
                    string text2 = text.Trim();
                    if (!text2.StartsWith("#"))
                    {
                        whiteWords.Add(text2.ToLower());
                    }
                }
            }
            catch (Exception ex)
            {
                Server.s.Log("Error during white words loading.");
                Server.ErrorLog(ex);
            }
        }

        void FilterChat(Player p, ref string message, ref bool stopIt)
        {
            if (p != null)
            {
                if (ChatFilterSettings.All.MessagesCooldown)
                {
                    CheckForMessageSpam(p, ref stopIt);
                }
                if (ChatFilterSettings.All.RemoveCaps)
                {
                    CheckCaps(ref message);
                }
                if (ChatFilterSettings.All.ShortenRepetitions)
                {
                    ShortenRepetitions(p, ref message, ref stopIt);
                }
                if (ChatFilterSettings.All.RemoveBadWords)
                {
                    FilterBadWords(p, ref message, ref stopIt);
                }
            }
        }

        void CheckCaps(ref string message)
        {
            int num = 0;
            string text = message;
            foreach (char c2 in text)
            {
                if (char.IsUpper(c2))
                {
                    num++;
                    if (num > ChatFilterSettings.All.MaxCaps)
                    {
                        message = message.ToLower();
                        break;
                    }
                }
            }
        }

        void ShortenRepetitions(Player p, ref string message, ref bool stopIt)
        {
            Regex regex = new Regex("((.)\\2{" + ChatFilterSettings.All.CharSpamMaxChars + ",})");
            MatchCollection matchCollection = regex.Matches(message);
            if (matchCollection.Count > ChatFilterSettings.All.CharSpamMaxIllegalGroups)
            {
                if (ChatFilterSettings.All.CharSpamAction == CharacterSpamAction.DisplaySubstitution)
                {
                    message = ChatFilterSettings.All.CharSpamSubstitution;
                }
                else if (ChatFilterSettings.All.CharSpamAction == CharacterSpamAction.SendWarning)
                {
                    Player.SendMessage(p, ChatFilterSettings.All.CharSpamWarning);
                    stopIt = true;
                }
                else
                {
                    message = ChatFilterSettings.All.CharSpamSubstitution;
                    Player.SendMessage(p, ChatFilterSettings.All.CharSpamWarning);
                }
            }
            var list = new List<string>(matchCollection.Count);
            foreach (Match item in matchCollection)
            {
                list.Add(item.Groups[1].Value);
            }
            IOrderedEnumerable<string> orderedEnumerable = list.OrderByDescending(s => s.Length);
            foreach (string item2 in orderedEnumerable)
            {
                message = message.Replace(item2, item2.Substring(0, 3));
            }
        }

        void Unmask(ref string message)
        {
            foreach (KeyValuePair<string, string> mask in masks)
            {
                message = message.Replace(mask.Key, mask.Value);
            }
        }

        void HandleBadLanguageAbuse(Player p, ref string message, ref bool stopIt)
        {
            if (ChatFilterSettings.All.BadLanguageAction == BadLanguageAction.DisplaySubstitution)
            {
                message = ChatFilterSettings.All.BadLanguageSubstitution;
            }
            else if (ChatFilterSettings.All.BadLanguageAction == BadLanguageAction.SendWarning)
            {
                IncreaseWarningsAndKick(p);
                Player.SendMessage(p, ChatFilterSettings.All.BadLanguageWarning);
                stopIt = true;
            }
            else
            {
                IncreaseWarningsAndKick(p);
                message = ChatFilterSettings.All.BadLanguageSubstitution;
                Player.SendMessage(p, ChatFilterSettings.All.BadLanguageWarning);
            }
        }

        void FilterBadWords(Player p, ref string message, ref bool stopIt)
        {
            string message2 = message.ToLower();
            RemoveColorCodes(ref message2);
            Unmask(ref message2);
            if (ChatFilterSettings.All.BadLanguageDetectionLevel == BadLanguageDetectionLevel.Normal)
            {
                foreach (string badWord in badWords)
                {
                    if (new Regex("(?:\\s+|^)" + badWord + "(?:\\s+|$)").Matches(message2).Count > 0)
                    {
                        HandleBadLanguageAbuse(p, ref message, ref stopIt);
                        break;
                    }
                }
                return;
            }
            foreach (string whiteWord in whiteWords)
            {
                if (message2.Contains(whiteWord))
                {
                    message2 = message2.Replace(whiteWord, "");
                }
            }
            foreach (string badWord2 in badWords)
            {
                if (message2.Contains(badWord2))
                {
                    HandleBadLanguageAbuse(p, ref message, ref stopIt);
                    break;
                }
            }
        }

        void RemoveColorCodes(ref string message)
        {
            for (int i = 0; i < 10; i++)
            {
                message = message.Replace("%" + i, "");
                message = message.Replace("&" + i, "");
            }
            for (char c2 = 'a'; c2 <= 'f'; c2 = (char)(c2 + 1))
            {
                message = message.Replace("%" + c2, "");
                message = message.Replace("&" + c2, "");
            }
            message = message.Replace("%s", "");
            message = message.Replace("&s", "");
        }

        void CheckForMessageSpam(Player p, ref bool stopIt)
        {
            if (!p.ExtraData.ContainsKey("RecentMessagesDates"))
            {
                p.ExtraData.Add("RecentMessagesDates", new List<DateTime>());
                ((List<DateTime>)p.ExtraData["RecentMessagesDates"]).Add(DateTime.Now);
                return;
            }
            var list = (List<DateTime>)p.ExtraData["RecentMessagesDates"];
            list.Add(DateTime.Now);
            DateTime comparator = DateTime.Now.AddSeconds(-ChatFilterSettings.All.CooldownMaxMessagesSeconds);
            var list2 = list.Where(t => t >= comparator).ToList();
            int num = list2.Count();
            if (num > ChatFilterSettings.All.CooldownMaxMessages)
            {
                stopIt = true;
                Player.SendMessage(p, ChatFilterSettings.All.CooldownMaxWarning);
                if (ChatFilterSettings.All.CooldownTempMute)
                {
                    new CmdTempMute().Use(null, p.name + " 60");
                }
            }
            else
            {
                p.ExtraData["RecentMessagesDates"] = list2;
            }
        }
    }
}