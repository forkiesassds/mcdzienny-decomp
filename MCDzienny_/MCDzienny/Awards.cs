using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    public class Awards
    {

        public static List<playerAwards> playersAwards = new List<playerAwards>();

        public static List<awardData> allAwards = new List<awardData>();

        public static void Load()
        {
            if (!File.Exists("text/awardsList.txt"))
            {
                StreamWriter streamWriter = new StreamWriter(File.Create("text/awardsList.txt"));
                streamWriter.WriteLine("#This is a full list of awards. The server will load these and they can be awarded as you please");
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
            string[] array = File.ReadAllLines("text/awardsList.txt");
            foreach (string text in array)
            {
                if (!(text == "") && text[0] != '#' && text.IndexOf(" : ") != -1)
                {
                    awardData awardData = new awardData();
                    awardData.setAward(text.Split(new string[1]
                    {
                        " : "
                    }, StringSplitOptions.None)[0]);
                    awardData.description = text.Split(new string[1]
                    {
                        " : "
                    }, StringSplitOptions.None)[1];
                    allAwards.Add(awardData);
                }
            }
            playersAwards = new List<playerAwards>();
            if (File.Exists("text/playerAwards.txt"))
            {
                string[] array2 = File.ReadAllLines("text/playerAwards.txt");
                playerAwards item = default(playerAwards);
                foreach (string text2 in array2)
                {
                    if (text2.IndexOf(" : ") == -1)
                    {
                        continue;
                    }
                    item.playerName = text2.Split(new string[1]
                    {
                        " : "
                    }, StringSplitOptions.None)[0].ToLower();
                    string text3 = text2.Split(new string[1]
                    {
                        " : "
                    }, StringSplitOptions.None)[1];
                    item.awards = new List<string>();
                    if (text3.IndexOf(',') != -1)
                    {
                        string[] array3 = text3.Split(',');
                        foreach (string givenName in array3)
                        {
                            item.awards.Add(camelCase(givenName));
                        }
                    }
                    else if (text3.Trim() != "")
                    {
                        item.awards.Add(camelCase(text3));
                    }
                    playersAwards.Add(item);
                }
            }
            Save();
        }

        public static void Save()
        {
            StreamWriter streamWriter = new StreamWriter(File.Create("text/awardsList.txt"));
            streamWriter.WriteLine("#This is a full list of awards. The server will load these and they can be awarded as you please");
            streamWriter.WriteLine("#Format is:");
            streamWriter.WriteLine("# awardName : Description of award goes after the colon");
            streamWriter.WriteLine();
            foreach (awardData allAward in allAwards)
            {
                streamWriter.WriteLine(camelCase(allAward.awardName) + " : " + allAward.description);
            }
            streamWriter.Flush();
            streamWriter.Close();
            streamWriter = new StreamWriter(File.Create("text/playerAwards.txt"));
            foreach (playerAwards playersAward in playersAwards)
            {
                streamWriter.WriteLine(playersAward.playerName.ToLower() + " : " + string.Join(",", playersAward.awards.ToArray()));
            }
            streamWriter.Flush();
            streamWriter.Close();
        }

        public static bool giveAward(string playerName, string awardName)
        {
            foreach (playerAwards playersAward in playersAwards)
            {
                if (playersAward.playerName == playerName.ToLower())
                {
                    if (playersAward.awards.Contains(camelCase(awardName)))
                    {
                        return false;
                    }
                    playersAward.awards.Add(camelCase(awardName));
                    return true;
                }
            }
            playerAwards item = default(playerAwards);
            item.playerName = playerName.ToLower();
            item.awards = new List<string>();
            item.awards.Add(camelCase(awardName));
            playersAwards.Add(item);
            return true;
        }

        public static bool takeAward(string playerName, string awardName)
        {
            foreach (playerAwards playersAward in playersAwards)
            {
                if (playersAward.playerName == playerName.ToLower())
                {
                    if (!playersAward.awards.Contains(camelCase(awardName)))
                    {
                        return false;
                    }
                    playersAward.awards.Remove(camelCase(awardName));
                    return true;
                }
            }
            return false;
        }

        public static List<string> getPlayersAwards(string playerName)
        {
            foreach (playerAwards playersAward in playersAwards)
            {
                if (playersAward.playerName == playerName.ToLower())
                {
                    return playersAward.awards;
                }
            }
            return new List<string>();
        }

        public static string getDescription(string awardName)
        {
            foreach (awardData allAward in allAwards)
            {
                if (camelCase(allAward.awardName) == camelCase(awardName))
                {
                    return allAward.description;
                }
            }
            return "";
        }

        public static string awardAmount(string playerName)
        {
            foreach (playerAwards playersAward in playersAwards)
            {
                if (playersAward.playerName == playerName.ToLower())
                {
                    return "&f" + playersAward.awards.Count + "/" + allAwards.Count + " (" + Math.Round(playersAward.awards.Count / (double)allAwards.Count * 100.0, 2) +
                        "%)" + Server.DefaultColor;
                }
            }
            return "&f0/" + allAwards.Count + " (0%)" + Server.DefaultColor;
        }

        public static bool addAward(string awardName, string awardDescription)
        {
            if (awardExists(awardName))
            {
                return false;
            }
            awardData awardData = new awardData();
            awardData.awardName = camelCase(awardName);
            awardData.description = awardDescription;
            allAwards.Add(awardData);
            return true;
        }

        public static bool removeAward(string awardName)
        {
            foreach (awardData allAward in allAwards)
            {
                if (camelCase(allAward.awardName) == camelCase(awardName))
                {
                    allAwards.Remove(allAward);
                    return true;
                }
            }
            return false;
        }

        public static bool awardExists(string awardName)
        {
            foreach (awardData allAward in allAwards)
            {
                if (camelCase(allAward.awardName) == camelCase(awardName))
                {
                    return true;
                }
            }
            return false;
        }

        public static string camelCase(string givenName)
        {
            string text = "";
            if (givenName != "")
            {
                string[] array = givenName.Split(' ');
                foreach (string text2 in array)
                {
                    text = text2.Length <= 1 ? text + text2.ToUpper() + " " : text + text2[0].ToString().ToUpper() + text2.Substring(1).ToLower() + " ";
                }
            }
            return text.Trim();
        }

        public struct playerAwards
        {
            public string playerName;

            public List<string> awards;
        }

        public class awardData
        {
            public string awardName;

            public string description;

            public void setAward(string name)
            {
                awardName = camelCase(name);
            }
        }
    }
}