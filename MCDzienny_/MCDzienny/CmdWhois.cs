using System;
using System.Text;
using MCDzienny.InfectionSystem;

namespace MCDzienny
{
    public class CmdWhois : Command
    {
        public override string name { get { return "whois"; } }

        public override string shortcut { get { return "look"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public string GetEnding(int count)
        {
            int num = 39 - count;
            string text = "";
            for (int i = 0; i < num; i++)
            {
                text += " ";
            }
            return text + "|";
        }

        static string ExperienceBar(double experiencePercentage)
        {
            int num = (int)(experiencePercentage / 100.0 * 12.0 + 0.5);
            StringBuilder stringBuilder = new StringBuilder();
            char[] array = new char[12];
            for (int i = 0; i < array.Length; i += 2)
            {
                array[i] = '\u001e';
                array[i + 1] = '\u001f';
            }
            string text = stringBuilder.Append(array).ToString();
            if (num == 12)
            {
                return text;
            }
            string text2 = text;
            return text2.Insert(num, "%7");
        }

        public override void Use(Player p, string message)
        {
            Player player = null;
            if (message == "")
            {
                if (p == null)
                {
                    Help(p);
                    return;
                }
                player = p;
                message = p.name;
            }
            else
            {
                player = Player.Find(message);
            }
            if (player != null && !player.hidden)
            {
                Player.SendMessage(p, "__________ Player's Info __________");
                Player.SendMessage(
                    p,
                    string.Format("| Name: {0} %sMap: %a{1}", player.color + (p == null || p.group.Permission >= LevelPermission.Admin ? player.name : player.PublicName),
                                  player.level.name));
                if (player.level.mapType == MapType.Zombie)
                {
                    if (player.ZombieTier - 1 < InfectionTiers.tierTreshold.Length)
                    {
                        Player.SendMessage(p, "| Level: %a{0}   %s[%a{1}%s]", player.ZombieTier, ExperienceBar(GetZombieLevelProgress(player)));
                    }
                    else
                    {
                        Player.SendMessage(p, string.Format("| Level: {0}", "%a" + player.ZombieTier));
                    }
                    if (player.ZombieTier - 1 >= InfectionTiers.tierTreshold.Length)
                    {
                        Player.SendMessage(p, string.Format("| Experience: &a{0}", player.PlayerExperienceOnZombie));
                    }
                    else
                    {
                        Player.SendMessage(p,
                                           string.Format("| Experience: %a{0} %7/ %7{1}", player.PlayerExperienceOnZombie,
                                                         InfectionTiers.tierTreshold[player.ZombieTier - 1]));
                    }
                    Player.SendMessage(p, string.Format("| Survived: %a{0} %sInfected: %c{1}", player.WonAsHumanTimes, player.ZombifiedCount));
                }
                else if (player.level.mapType == MapType.Lava)
                {
                    if (player.tier - 1 >= TierSystem.tierTreshold.Length)
                    {
                        Player.SendMessage(p, "| Level: %a{0}", player.tier);
                        Player.SendMessage(p, "| Experience: &a{0}", player.totalScore);
                    }
                    else
                    {
                        Player.SendMessage(p, "| Level: %a{0}   %a{1}", player.tier, ExperienceBar(GetLevelUpProgress(player)));
                        Player.SendMessage(p, "| Experience: &a{0}%7 / {1}", player.totalScore, TierSystem.tierTreshold[player.tier - 1]);
                    }
                    Player.SendMessage(p, string.Format("| %sSurvived: %a{0}%s Best score: &a{1}", player.timesWon, player.bestScore));
                }
                Player.SendMessage(p, string.Format("| Wealth: &a{0} {1}", player.money + Server.DefaultColor, Server.moneys));
                if (p == null || p.group.Permission >= LevelPermission.Admin)
                {
                    Player.SendMessage(
                        p,
                        string.Format("| &bModified: &a{0} blocks, &a{1} since logging in.", player.overallBlocks + Server.DefaultColor,
                                      player.loginBlocks + Server.DefaultColor));
                    string text = Convert.ToDateTime(DateTime.Now.Subtract(player.timeLogged).ToString()).ToString("HH:mm:ss");
                    Player.SendMessage(p, string.Format("| Been logged in for: &a{0}h. and {1}min.", text.Split(':')[0], text.Split(':')[1]));
                    Player.SendMessage(
                        p,
                        string.Format("| First logged into the server on &a{0} at {1}", player.firstLogin.ToString("yyyy-MM-dd"), player.firstLogin.ToString("HH:mm:ss")));
                    Player.SendMessage(
                        p,
                        string.Format("| Logged in &a{0} times, &c{1} of which ended in a kick.", player.totalLogins + Server.DefaultColor,
                                      player.totalKicked + Server.DefaultColor));
                    Player.SendMessage(
                        p,
                        string.Format("| Total time played: &a{0}",
                                      player.TotalMinutesPlayed / 60 > 0 ? string.Format("{0} hours {1} minutes", player.TotalMinutesPlayed / 60,
                                                                                         player.TotalMinutesPlayed % 60)
                                          : string.Format("{0} minutes", player.TotalMinutesPlayed)));
                    string arg = !Server.bannedIP.Contains(player.ip) ? player.ip : string.Format("&8{0}, which is banned", player.ip);
                    Player.SendMessage(p, string.Format("| IP: {0}", arg));
                    if (Server.useWhitelist && Server.whiteList.Contains(player.name))
                    {
                        Player.SendMessage(p, "| Player is &fWhitelisted");
                    }
                }
            }
            else
            {
                Player.SendMessage(p, string.Format("\"{0}\" is offline! Using /whowas instead.", message));
                all.Find("whowas").Use(p, message);
            }
        }

        static double GetZombieLevelProgress(Player who)
        {
            int num = who.ZombieTier > 1 ? InfectionTiers.tierTreshold[who.ZombieTier - 2] : 0;
            int num2 = InfectionTiers.tierTreshold[who.ZombieTier - 1];
            return (who.PlayerExperienceOnZombie - num) / (double)(num2 - num) * 100.0;
        }

        static double GetLevelUpProgress(Player who)
        {
            int num = who.tier > 1 ? TierSystem.tierTreshold[who.tier - 2] : 0;
            int num2 = TierSystem.tierTreshold[who.tier - 1];
            return (who.totalScore - num) / (double)(num2 - num) * 100.0;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whois [player] - Displays information about someone.");
        }
    }
}