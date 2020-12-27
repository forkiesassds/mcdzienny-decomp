using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MCDzienny.Misc;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x02000052 RID: 82
    public class ChatFilter
    {
        // Token: 0x02000054 RID: 84
        [Flags]
        public enum BadLanguageAction
        {
            // Token: 0x04000165 RID: 357
            Null = 0,

            // Token: 0x04000166 RID: 358
            DisplaySubstitution = 1,

            // Token: 0x04000167 RID: 359
            SendWarning = 2
        }

        // Token: 0x02000055 RID: 85
        public enum BadLanguageDetectionLevel
        {
            // Token: 0x04000169 RID: 361
            Normal,

            // Token: 0x0400016A RID: 362
            High
        }

        // Token: 0x02000053 RID: 83
        [Flags]
        public enum CharacterSpamAction
        {
            // Token: 0x04000161 RID: 353
            Null = 0,

            // Token: 0x04000162 RID: 354
            DisplaySubstitution = 1,

            // Token: 0x04000163 RID: 355
            SendWarning = 2
        }

        // Token: 0x04000156 RID: 342
        private const string KeyBadLanguageWarnings = "BadLanguageWarnings";

        // Token: 0x04000157 RID: 343
        private const string KeySpamWarnings = "SpamWarnings";

        // Token: 0x04000158 RID: 344
        private const string KeyRecentMessagesTimes = "RecentMessagesDates";

        // Token: 0x0400015A RID: 346
        private List<string> badWords;

        // Token: 0x0400015D RID: 349
        private readonly string badWordsPath = "text/badwords.txt";

        // Token: 0x04000159 RID: 345
        private readonly Dictionary<string, string> masks;

        // Token: 0x0400015E RID: 350
        private int maxCaps = 3;

        // Token: 0x0400015B RID: 347
        private List<string> whiteWords;

        // Token: 0x0400015C RID: 348
        private readonly string whiteWordsPath = "text/whitewords.txt";

        // Token: 0x060001F7 RID: 503 RVA: 0x0000B9D4 File Offset: 0x00009BD4
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

        // Token: 0x17000070 RID: 112
        // (get) Token: 0x060001F8 RID: 504 RVA: 0x0000BB38 File Offset: 0x00009D38
        // (set) Token: 0x060001F9 RID: 505 RVA: 0x0000BB40 File Offset: 0x00009D40
        public int MaxCaps
        {
            get { return maxCaps; }
            set { maxCaps = value; }
        }

        // Token: 0x060001F6 RID: 502 RVA: 0x0000B950 File Offset: 0x00009B50
        private void IncreaseWarningsAndKick(Player p)
        {
            if (!p.ExtraData.ContainsKey("BadLanguageWarnings"))
            {
                p.ExtraData.Add("BadLanguageWarnings", 1);
                return;
            }

            var num = (int) p.ExtraData["BadLanguageWarnings"] + 1;
            if (num > ChatFilterSettings.All.BadLanguageWarningLimit)
            {
                p.Kick(ChatFilterSettings.All.BadLanguageKickMessage);
                return;
            }

            p.ExtraData["BadLanguageWarnings"] = num;
        }

        // Token: 0x060001FA RID: 506 RVA: 0x0000BB4C File Offset: 0x00009D4C
        public void Initialize()
        {
            LoadBadWords();
            LoadWhiteWords();
            Player.PlayerChatEvent += FilterChat;
        }

        // Token: 0x060001FB RID: 507 RVA: 0x0000BB6C File Offset: 0x00009D6C
        public void LoadBadWords()
        {
            try
            {
                badWords = new List<string>();
                FileUtil.CreateIfNotExists(badWordsPath);
                foreach (var text in File.ReadAllLines(badWordsPath))
                {
                    var text2 = text.Trim();
                    if (!text2.StartsWith("#") && !string.IsNullOrEmpty(text2))
                        badWords.Add(Regex.Escape(text2.ToLower()));
                }
            }
            catch (Exception ex)
            {
                Server.s.Log("Error during bad words loading.");
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060001FC RID: 508 RVA: 0x0000BC10 File Offset: 0x00009E10
        public void LoadWhiteWords()
        {
            try
            {
                whiteWords = new List<string>();
                FileUtil.CreateIfNotExists(whiteWordsPath);
                foreach (var text in File.ReadAllLines(whiteWordsPath))
                {
                    var text2 = text.Trim();
                    if (!text2.StartsWith("#")) whiteWords.Add(text2.ToLower());
                }
            }
            catch (Exception ex)
            {
                Server.s.Log("Error during white words loading.");
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060001FD RID: 509 RVA: 0x0000BCA8 File Offset: 0x00009EA8
        private void FilterChat(Player p, ref string message, ref bool stopIt)
        {
            if (p == null) return;
            if (ChatFilterSettings.All.MessagesCooldown) CheckForMessageSpam(p, ref stopIt);
            if (ChatFilterSettings.All.RemoveCaps) CheckCaps(ref message);
            if (ChatFilterSettings.All.ShortenRepetitions) ShortenRepetitions(p, ref message, ref stopIt);
            if (ChatFilterSettings.All.RemoveBadWords) FilterBadWords(p, ref message, ref stopIt);
        }

        // Token: 0x060001FE RID: 510 RVA: 0x0000BD0C File Offset: 0x00009F0C
        private void CheckCaps(ref string message)
        {
            var num = 0;
            foreach (var c in message)
                if (char.IsUpper(c))
                {
                    num++;
                    if (num > ChatFilterSettings.All.MaxCaps)
                    {
                        message = message.ToLower();
                        break;
                    }
                }
        }

        // Token: 0x060001FF RID: 511 RVA: 0x0000BD5C File Offset: 0x00009F5C
        private void ShortenRepetitions(Player p, ref string message, ref bool stopIt)
        {
            var regex = new Regex("((.)\\2{" + ChatFilterSettings.All.CharSpamMaxChars + ",})");
            var matchCollection = regex.Matches(message);
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
            foreach (var obj in matchCollection)
            {
                var match = (Match) obj;
                list.Add(match.Groups[1].Value);
            }

            var orderedEnumerable = from s in list
                orderby s.Length descending
                select s;
            foreach (var text in orderedEnumerable) message = message.Replace(text, text.Substring(0, 3));
        }

        // Token: 0x06000200 RID: 512 RVA: 0x0000BEDC File Offset: 0x0000A0DC
        private void Unmask(ref string message)
        {
            foreach (var keyValuePair in masks) message = message.Replace(keyValuePair.Key, keyValuePair.Value);
        }

        // Token: 0x06000201 RID: 513 RVA: 0x0000BF40 File Offset: 0x0000A140
        private void HandleBadLanguageAbuse(Player p, ref string message, ref bool stopIt)
        {
            if (ChatFilterSettings.All.BadLanguageAction == BadLanguageAction.DisplaySubstitution)
            {
                message = ChatFilterSettings.All.BadLanguageSubstitution;
                return;
            }

            if (ChatFilterSettings.All.BadLanguageAction == BadLanguageAction.SendWarning)
            {
                IncreaseWarningsAndKick(p);
                Player.SendMessage(p, ChatFilterSettings.All.BadLanguageWarning);
                stopIt = true;
                return;
            }

            IncreaseWarningsAndKick(p);
            message = ChatFilterSettings.All.BadLanguageSubstitution;
            Player.SendMessage(p, ChatFilterSettings.All.BadLanguageWarning);
        }

        // Token: 0x06000202 RID: 514 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
        private void FilterBadWords(Player p, ref string message, ref bool stopIt)
        {
            var text = message.ToLower();
            RemoveColorCodes(ref text);
            Unmask(ref text);
            if (ChatFilterSettings.All.BadLanguageDetectionLevel == BadLanguageDetectionLevel.Normal)
                using (var enumerator = badWords.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var str = enumerator.Current;
                        if (new Regex("(?:\\s+|^)" + str + "(?:\\s+|$)").Matches(text).Count > 0)
                        {
                            HandleBadLanguageAbuse(p, ref message, ref stopIt);
                            break;
                        }
                    }

                    return;
                }

            foreach (var text2 in whiteWords)
                if (text.Contains(text2))
                    text = text.Replace(text2, "");
            foreach (var value in badWords)
                if (text.Contains(value))
                {
                    HandleBadLanguageAbuse(p, ref message, ref stopIt);
                    break;
                }
        }

        // Token: 0x06000203 RID: 515 RVA: 0x0000C0FC File Offset: 0x0000A2FC
        private void RemoveColorCodes(ref string message)
        {
            for (var i = 0; i < 10; i++)
            {
                message = message.Replace("%" + i, "");
                message = message.Replace("&" + i, "");
            }

            for (var c = 'a'; c <= 'f'; c += '\u0001')
            {
                message = message.Replace("%" + c, "");
                message = message.Replace("&" + c, "");
            }

            message = message.Replace("%s", "");
            message = message.Replace("&s", "");
        }

        // Token: 0x06000204 RID: 516 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
        private void CheckForMessageSpam(Player p, ref bool stopIt)
        {
            if (!p.ExtraData.ContainsKey("RecentMessagesDates"))
            {
                p.ExtraData.Add("RecentMessagesDates", new List<DateTime>());
                ((List<DateTime>) p.ExtraData["RecentMessagesDates"]).Add(DateTime.Now);
                return;
            }

            var list = (List<DateTime>) p.ExtraData["RecentMessagesDates"];
            list.Add(DateTime.Now);
            var comparator = DateTime.Now.AddSeconds(-(double) ChatFilterSettings.All.CooldownMaxMessagesSeconds);
            var list2 = (from t in list
                where t >= comparator
                select t).ToList();
            var num = list2.Count();
            if (num > ChatFilterSettings.All.CooldownMaxMessages)
            {
                stopIt = true;
                Player.SendMessage(p, ChatFilterSettings.All.CooldownMaxWarning);
                if (ChatFilterSettings.All.CooldownTempMute) new CmdTempMute().Use(null, p.name + " 60");
                return;
            }

            p.ExtraData["RecentMessagesDates"] = list2;
        }
    }
}