using System;
using System.Text;
using MCDzienny.InfectionSystem;

namespace MCDzienny
{
    // Token: 0x020002AE RID: 686
    public class CmdWhois : Command
    {
        // Token: 0x17000784 RID: 1924
        // (get) Token: 0x060013AB RID: 5035 RVA: 0x0006C334 File Offset: 0x0006A534
        public override string name
        {
            get { return "whois"; }
        }

        // Token: 0x17000785 RID: 1925
        // (get) Token: 0x060013AC RID: 5036 RVA: 0x0006C33C File Offset: 0x0006A53C
        public override string shortcut
        {
            get { return "look"; }
        }

        // Token: 0x17000786 RID: 1926
        // (get) Token: 0x060013AD RID: 5037 RVA: 0x0006C344 File Offset: 0x0006A544
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000787 RID: 1927
        // (get) Token: 0x060013AE RID: 5038 RVA: 0x0006C34C File Offset: 0x0006A54C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000788 RID: 1928
        // (get) Token: 0x060013AF RID: 5039 RVA: 0x0006C350 File Offset: 0x0006A550
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060013B1 RID: 5041 RVA: 0x0006C35C File Offset: 0x0006A55C
        public string GetEnding(int count)
        {
            var num = 39 - count;
            var str = "";
            for (var i = 0; i < num; i++) str += " ";
            return str + "|";
        }

        // Token: 0x060013B2 RID: 5042 RVA: 0x0006C39C File Offset: 0x0006A59C
        private static string ExperienceBar(double experiencePercentage)
        {
            var num = (int) (experiencePercentage / 100.0 * 12.0 + 0.5);
            var stringBuilder = new StringBuilder();
            var array = new char[12];
            for (var i = 0; i < array.Length; i += 2)
            {
                array[i] = '\u001e';
                array[i + 1] = '\u001f';
            }

            var text = stringBuilder.Append(array).ToString();
            if (num == 12) return text;
            var text2 = text;
            return text2.Insert(num, "%7");
        }

        // Token: 0x060013B3 RID: 5043 RVA: 0x0006C41C File Offset: 0x0006A61C
        public override void Use(Player p, string message)
        {
            Player player;
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
                Player.SendMessage(p,
                    string.Format("| Name: {0} %sMap: %a{1}",
                        player.color + (p == null || p.group.Permission >= LevelPermission.Admin
                            ? player.name
                            : player.PublicName), player.level.name));
                if (player.level.mapType == MapType.Zombie)
                {
                    if (player.ZombieTier - 1 < InfectionTiers.tierTreshold.Length)
                        Player.SendMessage(p, "| Level: %a{0}   %s[%a{1}%s]", player.ZombieTier,
                            ExperienceBar(GetZombieLevelProgress(player)));
                    else
                        Player.SendMessage(p, string.Format("| Level: {0}", "%a" + player.ZombieTier));
                    if (player.ZombieTier - 1 >= InfectionTiers.tierTreshold.Length)
                        Player.SendMessage(p, string.Format("| Experience: &a{0}", player.PlayerExperienceOnZombie));
                    else
                        Player.SendMessage(p,
                            string.Format("| Experience: %a{0} %7/ %7{1}", player.PlayerExperienceOnZombie,
                                InfectionTiers.tierTreshold[player.ZombieTier - 1]));
                    Player.SendMessage(p,
                        string.Format("| Survived: %a{0} %sInfected: %c{1}", player.WonAsHumanTimes,
                            player.ZombifiedCount));
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
                        Player.SendMessage(p, "| Level: %a{0}   %a{1}", player.tier,
                            ExperienceBar(GetLevelUpProgress(player)));
                        Player.SendMessage(p, "| Experience: &a{0}%7 / {1}", player.totalScore,
                            TierSystem.tierTreshold[player.tier - 1]);
                    }

                    Player.SendMessage(p,
                        string.Format("| %sSurvived: %a{0}%s Best score: &a{1}", player.timesWon, player.bestScore));
                }

                Player.SendMessage(p,
                    string.Format("| Wealth: &a{0} {1}", player.money + Server.DefaultColor, Server.moneys));
                if (p == null || p.group.Permission >= LevelPermission.Admin)
                {
                    Player.SendMessage(p,
                        string.Format("| &bModified: &a{0} blocks, &a{1} since logging in.",
                            player.overallBlocks + Server.DefaultColor, player.loginBlocks + Server.DefaultColor));
                    var text = Convert.ToDateTime(DateTime.Now.Subtract(player.timeLogged).ToString())
                        .ToString("HH:mm:ss");
                    Player.SendMessage(p,
                        string.Format("| Been logged in for: &a{0}h. and {1}min.", text.Split(':')[0],
                            text.Split(':')[1]));
                    Player.SendMessage(p,
                        string.Format("| First logged into the server on &a{0} at {1}",
                            player.firstLogin.ToString("yyyy-MM-dd"), player.firstLogin.ToString("HH:mm:ss")));
                    Player.SendMessage(p,
                        string.Format("| Logged in &a{0} times, &c{1} of which ended in a kick.",
                            player.totalLogins + Server.DefaultColor, player.totalKicked + Server.DefaultColor));
                    Player.SendMessage(p,
                        string.Format("| Total time played: &a{0}",
                            player.TotalMinutesPlayed / 60 > 0
                                ? string.Format("{0} hours {1} minutes", player.TotalMinutesPlayed / 60,
                                    player.TotalMinutesPlayed % 60)
                                : string.Format("{0} minutes", player.TotalMinutesPlayed)));
                    string arg;
                    if (Server.bannedIP.Contains(player.ip))
                        arg = string.Format("&8{0}, which is banned", player.ip);
                    else
                        arg = player.ip;
                    Player.SendMessage(p, string.Format("| IP: {0}", arg));
                    if (Server.useWhitelist && Server.whiteList.Contains(player.name))
                        Player.SendMessage(p, "| Player is &fWhitelisted");
                }
            }
            else
            {
                Player.SendMessage(p, string.Format("\"{0}\" is offline! Using /whowas instead.", message));
                all.Find("whowas").Use(p, message);
            }
        }

        // Token: 0x060013B4 RID: 5044 RVA: 0x0006C8F4 File Offset: 0x0006AAF4
        private static double GetZombieLevelProgress(Player who)
        {
            var num = who.ZombieTier > 1 ? InfectionTiers.tierTreshold[who.ZombieTier - 2] : 0;
            var num2 = InfectionTiers.tierTreshold[who.ZombieTier - 1];
            return (who.PlayerExperienceOnZombie - num) / (double) (num2 - num) * 100.0;
        }

        // Token: 0x060013B5 RID: 5045 RVA: 0x0006C944 File Offset: 0x0006AB44
        private static double GetLevelUpProgress(Player who)
        {
            var num = who.tier > 1 ? TierSystem.tierTreshold[who.tier - 2] : 0;
            var num2 = TierSystem.tierTreshold[who.tier - 1];
            return (who.totalScore - num) / (double) (num2 - num) * 100.0;
        }

        // Token: 0x060013B6 RID: 5046 RVA: 0x0006C994 File Offset: 0x0006AB94
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whois [player] - Displays information about someone.");
        }
    }
}