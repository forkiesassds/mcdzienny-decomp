using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x02000119 RID: 281
    public class Awards
    {
        // Token: 0x040003D9 RID: 985
        public static List<playerAwards> playersAwards = new List<playerAwards>();

        // Token: 0x040003DA RID: 986
        public static List<awardData> allAwards = new List<awardData>();

        // Token: 0x0600086E RID: 2158 RVA: 0x0002AC38 File Offset: 0x00028E38
        public static void Load()
        {
            if (!File.Exists("text/awardsList.txt"))
            {
                var streamWriter = new StreamWriter(File.Create("text/awardsList.txt"));
                streamWriter.WriteLine(
                    "#This is a full list of awards. The server will load these and they can be awarded as you please");
                streamWriter.WriteLine("#Format is:");
                streamWriter.WriteLine("# awardName : Description of award goes after the colon");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Gotta start somewhere : Built your first house");
                streamWriter.WriteLine("Climbing the ladder : Earned a rank advancement");
                streamWriter.WriteLine("Do you live here? : Joined the server a huge bunch of times");
                streamWriter.Flush();
                streamWriter.Close();
            }

            allAwards = new List<awardData>();
            foreach (var text in File.ReadAllLines("text/awardsList.txt"))
                if (!(text == "") && text[0] != '#' && text.IndexOf(" : ") != -1)
                {
                    var awardData = new awardData();
                    awardData.setAward(text.Split(new[]
                    {
                        " : "
                    }, StringSplitOptions.None)[0]);
                    awardData.description = text.Split(new[]
                    {
                        " : "
                    }, StringSplitOptions.None)[1];
                    allAwards.Add(awardData);
                }

            playersAwards = new List<playerAwards>();
            if (File.Exists("text/playerAwards.txt"))
                foreach (var text2 in File.ReadAllLines("text/playerAwards.txt"))
                    if (text2.IndexOf(" : ") != -1)
                    {
                        playerAwards item;
                        item.playerName = text2.Split(new[]
                        {
                            " : "
                        }, StringSplitOptions.None)[0].ToLower();
                        var text3 = text2.Split(new[]
                        {
                            " : "
                        }, StringSplitOptions.None)[1];
                        item.awards = new List<string>();
                        if (text3.IndexOf(',') != -1)
                            foreach (var givenName in text3.Split(','))
                                item.awards.Add(camelCase(givenName));
                        else if (text3.Trim() != "") item.awards.Add(camelCase(text3));
                        playersAwards.Add(item);
                    }

            Save();
        }

        // Token: 0x0600086F RID: 2159 RVA: 0x0002AE98 File Offset: 0x00029098
        public static void Save()
        {
            var streamWriter = new StreamWriter(File.Create("text/awardsList.txt"));
            streamWriter.WriteLine(
                "#This is a full list of awards. The server will load these and they can be awarded as you please");
            streamWriter.WriteLine("#Format is:");
            streamWriter.WriteLine("# awardName : Description of award goes after the colon");
            streamWriter.WriteLine();
            foreach (var awardData in allAwards)
                streamWriter.WriteLine(camelCase(awardData.awardName) + " : " + awardData.description);
            streamWriter.Flush();
            streamWriter.Close();
            streamWriter = new StreamWriter(File.Create("text/playerAwards.txt"));
            foreach (var playerAwards in playersAwards)
                streamWriter.WriteLine(playerAwards.playerName.ToLower() + " : " +
                                       string.Join(",", playerAwards.awards.ToArray()));
            streamWriter.Flush();
            streamWriter.Close();
        }

        // Token: 0x06000870 RID: 2160 RVA: 0x0002AFD0 File Offset: 0x000291D0
        public static bool giveAward(string playerName, string awardName)
        {
            foreach (var playerAwards in playersAwards)
                if (playerAwards.playerName == playerName.ToLower())
                {
                    if (playerAwards.awards.Contains(camelCase(awardName))) return false;
                    playerAwards.awards.Add(camelCase(awardName));
                    return true;
                }

            playerAwards item;
            item.playerName = playerName.ToLower();
            item.awards = new List<string>();
            item.awards.Add(camelCase(awardName));
            playersAwards.Add(item);
            return true;
        }

        // Token: 0x06000871 RID: 2161 RVA: 0x0002B098 File Offset: 0x00029298
        public static bool takeAward(string playerName, string awardName)
        {
            foreach (var playerAwards in playersAwards)
                if (playerAwards.playerName == playerName.ToLower())
                {
                    if (!playerAwards.awards.Contains(camelCase(awardName))) return false;
                    playerAwards.awards.Remove(camelCase(awardName));
                    return true;
                }

            return false;
        }

        // Token: 0x06000872 RID: 2162 RVA: 0x0002B12C File Offset: 0x0002932C
        public static List<string> getPlayersAwards(string playerName)
        {
            foreach (var playerAwards in playersAwards)
                if (playerAwards.playerName == playerName.ToLower())
                    return playerAwards.awards;
            return new List<string>();
        }

        // Token: 0x06000873 RID: 2163 RVA: 0x0002B19C File Offset: 0x0002939C
        public static string getDescription(string awardName)
        {
            foreach (var awardData in allAwards)
                if (camelCase(awardData.awardName) == camelCase(awardName))
                    return awardData.description;
            return "";
        }

        // Token: 0x06000874 RID: 2164 RVA: 0x0002B210 File Offset: 0x00029410
        public static string awardAmount(string playerName)
        {
            foreach (var playerAwards in playersAwards)
                if (playerAwards.playerName == playerName.ToLower())
                    return string.Concat("&f", playerAwards.awards.Count, "/", allAwards.Count, " (",
                        Math.Round(playerAwards.awards.Count / (double) allAwards.Count * 100.0, 2), "%)",
                        Server.DefaultColor);
            return string.Concat("&f0/", allAwards.Count, " (0%)", Server.DefaultColor);
        }

        // Token: 0x06000875 RID: 2165 RVA: 0x0002B348 File Offset: 0x00029548
        public static bool addAward(string awardName, string awardDescription)
        {
            if (awardExists(awardName)) return false;
            var awardData = new awardData();
            awardData.awardName = camelCase(awardName);
            awardData.description = awardDescription;
            allAwards.Add(awardData);
            return true;
        }

        // Token: 0x06000876 RID: 2166 RVA: 0x0002B384 File Offset: 0x00029584
        public static bool removeAward(string awardName)
        {
            foreach (var awardData in allAwards)
                if (camelCase(awardData.awardName) == camelCase(awardName))
                {
                    allAwards.Remove(awardData);
                    return true;
                }

            return false;
        }

        // Token: 0x06000877 RID: 2167 RVA: 0x0002B3FC File Offset: 0x000295FC
        public static bool awardExists(string awardName)
        {
            foreach (var awardData in allAwards)
                if (camelCase(awardData.awardName) == camelCase(awardName))
                    return true;
            return false;
        }

        // Token: 0x06000878 RID: 2168 RVA: 0x0002B468 File Offset: 0x00029668
        public static string camelCase(string givenName)
        {
            var text = "";
            if (givenName != "")
                foreach (var text2 in givenName.Split(' '))
                    if (text2.Length > 1)
                        text = text + text2[0].ToString().ToUpper() + text2.Substring(1).ToLower() + " ";
                    else
                        text = text + text2.ToUpper() + " ";
            return text.Trim();
        }

        // Token: 0x0200011A RID: 282
        public struct playerAwards
        {
            // Token: 0x040003DB RID: 987
            public string playerName;

            // Token: 0x040003DC RID: 988
            public List<string> awards;
        }

        // Token: 0x0200011B RID: 283
        public class awardData
        {
            // Token: 0x040003DD RID: 989
            public string awardName;

            // Token: 0x040003DE RID: 990
            public string description;

            // Token: 0x0600087B RID: 2171 RVA: 0x0002B524 File Offset: 0x00029724
            public void setAward(string name)
            {
                awardName = camelCase(name);
            }
        }
    }
}