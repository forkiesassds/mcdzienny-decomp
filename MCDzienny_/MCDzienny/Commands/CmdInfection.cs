using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using MCDzienny.Settings;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    // Token: 0x020000C6 RID: 198
    internal class CmdInfection : Command
    {
        // Token: 0x04000362 RID: 866
        private static InfectionMode infectionMode = InfectionMode.Zombie;

        // Token: 0x04000363 RID: 867
        public static Timer MainLoop = new Timer(500.0);

        // Token: 0x04000364 RID: 868
        public static Timer TimeDisplay = new Timer(1000.0);

        // Token: 0x04000365 RID: 869
        private static readonly Random random = new Random();

        // Token: 0x04000366 RID: 870
        public static List<Player> infected = new List<Player>();

        // Token: 0x04000367 RID: 871
        public static List<Player> notInfected = new List<Player>();

        // Token: 0x04000369 RID: 873
        public static DateTime endTime;

        // Token: 0x0400036A RID: 874
        public static TimeSpan timeToEnd;

        // Token: 0x0400036B RID: 875
        public static int display = 15;

        // Token: 0x0400036C RID: 876
        public static Level infectionLevel;

        // Token: 0x0400036D RID: 877
        private static readonly string trueColorKey = "true_color";

        // Token: 0x0400036E RID: 878
        private static readonly string killsKey = "kills";

        // Token: 0x0400036F RID: 879
        private static int time;

        // Token: 0x04000368 RID: 872
        public string gameMode = "";

        // Token: 0x170002E8 RID: 744
        // (get) Token: 0x0600069B RID: 1691 RVA: 0x00022098 File Offset: 0x00020298
        public override string name
        {
            get { return "infection"; }
        }

        // Token: 0x170002E9 RID: 745
        // (get) Token: 0x0600069C RID: 1692 RVA: 0x000220A0 File Offset: 0x000202A0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002EA RID: 746
        // (get) Token: 0x0600069D RID: 1693 RVA: 0x000220A8 File Offset: 0x000202A8
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170002EB RID: 747
        // (get) Token: 0x0600069E RID: 1694 RVA: 0x000220B0 File Offset: 0x000202B0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002EC RID: 748
        // (get) Token: 0x0600069F RID: 1695 RVA: 0x000220B4 File Offset: 0x000202B4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060006A0 RID: 1696 RVA: 0x000220B8 File Offset: 0x000202B8
        public override void Use(Player p, string message)
        {
            if (p == null && message.ToLower() != "stop")
            {
                Help(p);
                return;
            }

            if (message.ToLower() == "stop")
            {
                Player.SendMessage(p, "Infection was stopped.");
                MainLoop.Enabled = false;
                TimeDisplay.Enabled = false;
                EndInfection();
                return;
            }

            if (message.ToLower() == "time")
            {
                Player.SendMessage(p,
                    string.Format("There is {0}min {1}s left to the end of this round!", timeToEnd.Minutes,
                        timeToEnd.Seconds));
                return;
            }

            if (int.TryParse(message.Trim(), out time))
                endTime = DateTime.Now.AddMinutes(time);
            else
                endTime = DateTime.Now.AddMinutes(InfectionSettings.All.RoundTime);
            Player.SendMessage(p, "Preparing the infection.");
            if (p.level.PlayersCount < 2)
            {
                Player.SendMessage(p,
                    string.Format("You can't play now, there has to be at least {0} more players on the map.",
                        3 - p.level.PlayersCount));
                Player.SendMessage(p, "Game stopped.");
                return;
            }

            try
            {
                Player.players.ForEach(delegate(Player pl)
                {
                    if (pl.level == infectionLevel) all.Find("spawn").Use(pl, "");
                });
            }
            catch
            {
            }

            gameMode = "ZOMBIE";
            TimeDisplay.Elapsed += DisplayTime;
            TimeDisplay.Enabled = true;
            infectionLevel = p.level;
            Player.GlobalMessage(string.Format("%cInfection starts on the map: {0}", p.level.name));
            try
            {
                Player.players.ForEach(delegate(Player player)
                {
                    if (player.level == p.level) notInfected.Add(player);
                });
            }
            catch
            {
            }

            Player.GlobalMessageLevel(infectionLevel, Language.GetText(23));
            var i = 10;
            while (i > 0)
            {
                Player.GlobalMessageLevel(infectionLevel, "%d" + i);
                i--;
                Thread.Sleep(1000);
            }

            var index = random.Next(notInfected.Count);
            var player2 = notInfected[index];
            Player.GlobalMessage("%c" + player2.name + Language.GetText(24));
            Player.GlobalMessageLevel(infectionLevel, Language.GetText(25));
            player2.extraData.Add(trueColorKey, player2.color);
            player2.extraData.Add(killsKey, 0);
            Player.GlobalDie(player2, false);
            player2.color = "&c";
            player2.isZombie = true;
            player2.GlobalSpawn(InfectionSettings.All.ZombieAlias);
            infected.Add(player2);
            notInfected.Remove(player2);
            MainLoop.Elapsed += InfectionCore;
            MainLoop.Enabled = true;
        }

        // Token: 0x060006A1 RID: 1697 RVA: 0x00022418 File Offset: 0x00020618
        public static void DisplayTime(object sender, ElapsedEventArgs e)
        {
            timeToEnd = endTime.Subtract(DateTime.Now);
            if (timeToEnd.TotalSeconds <= 0.0)
            {
                MainLoop.Enabled = false;
                EndInfection();
                return;
            }

            if (timeToEnd.TotalSeconds <= 10.0)
            {
                Player.GlobalMessageLevel(infectionLevel, timeToEnd.Seconds.ToString());
            }
            else if (display <= 0)
            {
                Player.GlobalMessageLevel(infectionLevel,
                    string.Format("There is {0}:{1} left to the end of the infection.", timeToEnd.Minutes,
                        timeToEnd.Seconds.ToString("00")));
                display = 55;
            }

            display--;
        }

        // Token: 0x060006A2 RID: 1698 RVA: 0x000224E8 File Offset: 0x000206E8
        public static void InfectionCore(object sender, ElapsedEventArgs e)
        {
            infected.ToList().ForEach(delegate(Player player1)
            {
                Player.players.ForEach(delegate(Player player2)
                {
                    if (player2.level == infectionLevel && !player2.isZombie &&
                        Math.Abs(player2.pos[0] / 32 - player1.pos[0] / 32) <= 1 &&
                        Math.Abs(player2.pos[1] / 32 - player1.pos[1] / 32) <= 1 &&
                        Math.Abs(player2.pos[2] / 32 - player1.pos[2] / 32) <= 1)
                    {
                        Player.GlobalMessageLevel(infectionLevel,
                            string.Concat(player2.color, player2.name, Language.GetText(26), player1.color,
                                player1.name));
                        Player.GlobalMessageLevel(infectionLevel, "%d" + player2.name + Language.GetText(24));
                        if (player1.extraData.ContainsKey(killsKey))
                        {
                            player1.extraData[killsKey] = (int) player1.extraData[killsKey] + 1;
                            if ((int) player1.extraData[killsKey] % 5 == 0)
                                Player.GlobalMessageLevel(infectionLevel,
                                    string.Format("%a{0} is on {1}x killing spree!!!", player1.name,
                                        player1.extraData[killsKey]));
                        }
                        else
                        {
                            player1.extraData.Add(killsKey, 1);
                        }

                        player2.extraData.Add(trueColorKey, player2.color);
                        player2.color = "&c";
                        Player.GlobalDie(player2, false);
                        player2.extraData.Add(killsKey, 0);
                        player2.GlobalSpawn(InfectionSettings.All.ZombieAlias);
                        player2.isZombie = true;
                        infected.Add(player2);
                        notInfected.Remove(player2);
                    }
                });
            });
            Player.players.ForEach(delegate(Player pl)
            {
                if (pl.level == infectionLevel && !pl.isZombie && !notInfected.Contains(pl)) notInfected.Add(pl);
            });
            if (notInfected.Count <= 0) EndInfection();
        }

        // Token: 0x060006A3 RID: 1699 RVA: 0x00022558 File Offset: 0x00020758
        public static void EndInfection()
        {
            Player.GlobalMessage("The infection ended!");
            TimeDisplay.Enabled = false;
            TimeDisplay.Elapsed -= DisplayTime;
            MainLoop.Enabled = false;
            MainLoop.Elapsed -= InfectionCore;
            var winnersCount = 0;
            Player.GlobalMessageLevel(infectionLevel, "The winners are");
            notInfected.ForEach(delegate(Player p)
            {
                if (p != null && p.level == infectionLevel) winnersCount++;
            });
            try
            {
                if (winnersCount > 0)
                {
                    Player.GlobalMessageLevel(infectionLevel, "Humans:");
                    notInfected.ForEach(delegate(Player p)
                    {
                        if (p != null && p.level == infectionLevel)
                        {
                            Player.GlobalMessageLevel(infectionLevel, p.color + p.name);
                            Thread.Sleep(500);
                            p.money += InfectionSettings.All.RewardForHumansFixed;
                            Player.SendMessage(p,
                                string.Format("You were rewarded {0} {1}", InfectionSettings.All.RewardForHumansFixed,
                                    Server.moneys));
                        }
                    });
                }
                else
                {
                    Player.GlobalMessageLevel(infectionLevel, "Zombies:");
                    infected.ForEach(delegate(Player p)
                    {
                        if (p != null && p.level == infectionLevel)
                        {
                            Player.GlobalMessageLevel(infectionLevel,
                                string.Concat(p.color, p.name, " (", p.extraData[killsKey].ToString(), ")"));
                            var num = InfectionSettings.All.RewardForZombiesFixed;
                            if (p.extraData.ContainsKey(killsKey))
                                num += (int) p.extraData[killsKey] * InfectionSettings.All.RewardForZombiesMultipiler;
                            p.money += num;
                            Player.SendMessage(p, string.Format("%bYou were rewarded {0} {1}!", num, Server.moneys));
                        }
                    });
                }
            }
            catch
            {
            }

            infectionMode = InfectionMode.Zombie;
            infected.ForEach(delegate(Player p) { ResetData(p, true); });
            infected.Clear();
            notInfected.ForEach(delegate(Player p) { ResetData(p, false); });
            notInfected.Clear();
        }

        // Token: 0x060006A4 RID: 1700 RVA: 0x000226D8 File Offset: 0x000208D8
        private static void ResetData(Player player, bool resetname)
        {
            if (resetname)
            {
                if (player.extraData.ContainsKey(trueColorKey))
                {
                    player.color = (string) player.extraData[trueColorKey];
                    player.extraData.Remove(trueColorKey);
                }

                if (player.extraData.ContainsKey(killsKey)) player.extraData.Remove(killsKey);
                player.isZombie = false;
                Player.GlobalDie(player, false);
                Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1],
                    false);
            }
        }

        // Token: 0x060006A5 RID: 1701 RVA: 0x00022790 File Offset: 0x00020990
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/infection - starts the infection game mode.");
            Player.SendMessage(p, "/infection [time] - starts the infection that will last [time] minutes.");
            Player.SendMessage(p, "/infection stop - stops the infection.");
        }

        // Token: 0x020000C7 RID: 199
        private enum InfectionMode
        {
            // Token: 0x04000378 RID: 888
            Zombie,

            // Token: 0x04000379 RID: 889
            Other
        }
    }
}