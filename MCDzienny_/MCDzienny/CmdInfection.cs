using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using MCDzienny.Settings;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    class CmdInfection : Command
    {

        static InfectionMode infectionMode = InfectionMode.Zombie;

        public static Timer MainLoop = new Timer(500.0);

        public static Timer TimeDisplay = new Timer(1000.0);

        static readonly Random random = new Random();

        public static List<Player> infected = new List<Player>();

        public static List<Player> notInfected = new List<Player>();

        public static DateTime endTime;

        public static TimeSpan timeToEnd;

        public static int display = 15;

        public static Level infectionLevel;

        static readonly string trueColorKey = "true_color";

        static readonly string killsKey = "kills";

        static int time;

        public string gameMode = "";

        public override string name { get { return "infection"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

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
                Player.SendMessage(p, string.Format("There is {0}min {1}s left to the end of this round!", timeToEnd.Minutes, timeToEnd.Seconds));
                return;
            }
            if (int.TryParse(message.Trim(), out time))
            {
                endTime = DateTime.Now.AddMinutes(time);
            }
            else
            {
                endTime = DateTime.Now.AddMinutes(InfectionSettings.All.RoundTime);
            }
            Player.SendMessage(p, "Preparing the infection.");
            if (p.level.PlayersCount < 2)
            {
                Player.SendMessage(p, string.Format("You can't play now, there has to be at least {0} more players on the map.", 3 - p.level.PlayersCount));
                Player.SendMessage(p, "Game stopped.");
                return;
            }
            try
            {
                Player.players.ForEach(delegate(Player pl)
                {
                    if (pl.level == infectionLevel)
                    {
                        all.Find("spawn").Use(pl, "");
                    }
                });
            }
            catch {}
            gameMode = "ZOMBIE";
            TimeDisplay.Elapsed += DisplayTime;
            TimeDisplay.Enabled = true;
            infectionLevel = p.level;
            Player.GlobalMessage(string.Format("%cInfection starts on the map: {0}", p.level.name));
            try
            {
                Player.players.ForEach(delegate(Player player)
                {
                    if (player.level == p.level)
                    {
                        notInfected.Add(player);
                    }
                });
            }
            catch {}
            Player.GlobalMessageLevel(infectionLevel, Language.GetText(23));
            int num = 10;
            while (num > 0)
            {
                Player.GlobalMessageLevel(infectionLevel, "%d" + num);
                num--;
                Thread.Sleep(1000);
            }
            int index = random.Next(notInfected.Count);
            Player player2 = notInfected[index];
            Player.GlobalMessage("%c" + player2.name + Language.GetText(24));
            Player.GlobalMessageLevel(infectionLevel, Language.GetText(25));
            player2.extraData.Add(trueColorKey, player2.color);
            player2.extraData.Add(killsKey, 0);
            Player.GlobalDie(player2, self: false);
            player2.color = "&c";
            player2.isZombie = true;
            player2.GlobalSpawn(InfectionSettings.All.ZombieAlias);
            infected.Add(player2);
            notInfected.Remove(player2);
            MainLoop.Elapsed += InfectionCore;
            MainLoop.Enabled = true;
        }

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
                Player.GlobalMessageLevel(
                    infectionLevel, string.Format("There is {0}:{1} left to the end of the infection.", timeToEnd.Minutes, timeToEnd.Seconds.ToString("00")));
                display = 55;
            }
            display--;
        }

        public static void InfectionCore(object sender, ElapsedEventArgs e)
        {
            infected.ForEach(delegate(Player player1)
            {
                Player.players.ForEach(delegate(Player player2)
                {
                    if (player2.level == infectionLevel && !player2.isZombie && Math.Abs(player2.pos[0] / 32 - player1.pos[0] / 32) <= 1 &&
                        Math.Abs(player2.pos[1] / 32 - player1.pos[1] / 32) <= 1 && Math.Abs(player2.pos[2] / 32 - player1.pos[2] / 32) <= 1)
                    {
                        Player.GlobalMessageLevel(infectionLevel, player2.color + player2.name + Language.GetText(26) + player1.color + player1.name);
                        Player.GlobalMessageLevel(infectionLevel, "%d" + player2.name + Language.GetText(24));
                        if (player1.extraData.ContainsKey(killsKey))
                        {
                            player1.extraData[killsKey] = (int)player1.extraData[killsKey] + 1;
                            if ((int)player1.extraData[killsKey] % 5 == 0)
                            {
                                Player.GlobalMessageLevel(infectionLevel, string.Format("%a{0} is on {1}x killing spree!!!", player1.name, player1.extraData[killsKey]));
                            }
                        }
                        else
                        {
                            player1.extraData.Add(killsKey, 1);
                        }
                        player2.extraData.Add(trueColorKey, player2.color);
                        player2.color = "&c";
                        Player.GlobalDie(player2, self: false);
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
                if (pl.level == infectionLevel && !pl.isZombie && !notInfected.Contains(pl))
                {
                    notInfected.Add(pl);
                }
            });
            if (notInfected.Count <= 0)
            {
                EndInfection();
            }
        }

        public static void EndInfection()
        {
            Player.GlobalMessage("The infection ended!");
            TimeDisplay.Enabled = false;
            TimeDisplay.Elapsed -= DisplayTime;
            MainLoop.Enabled = false;
            MainLoop.Elapsed -= InfectionCore;
            int winnersCount = 0;
            Player.GlobalMessageLevel(infectionLevel, "The winners are");
            notInfected.ForEach(delegate(Player p)
            {
                if (p != null && p.level == infectionLevel)
                {
                    winnersCount++;
                }
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
                            Player.SendMessage(p, string.Format("You were rewarded {0} {1}", InfectionSettings.All.RewardForHumansFixed, Server.moneys));
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
                            Player.GlobalMessageLevel(infectionLevel, p.color + p.name + " (" + p.extraData[killsKey] + ")");
                            int num = InfectionSettings.All.RewardForZombiesFixed;
                            if (p.extraData.ContainsKey(killsKey))
                            {
                                num += (int)p.extraData[killsKey] * InfectionSettings.All.RewardForZombiesMultipiler;
                            }
                            p.money += num;
                            Player.SendMessage(p, string.Format("%bYou were rewarded {0} {1}!", num, Server.moneys));
                        }
                    });
                }
            }
            catch {}
            infectionMode = InfectionMode.Zombie;
            infected.ForEach(delegate(Player p) { ResetData(p, resetname: true); });
            infected.Clear();
            notInfected.ForEach(delegate(Player p) { ResetData(p, resetname: false); });
            notInfected.Clear();
        }

        static void ResetData(Player player, bool resetname)
        {
            if (resetname)
            {
                if (player.extraData.ContainsKey(trueColorKey))
                {
                    player.color = (string)player.extraData[trueColorKey];
                    player.extraData.Remove(trueColorKey);
                }
                if (player.extraData.ContainsKey(killsKey))
                {
                    player.extraData.Remove(killsKey);
                }
                player.isZombie = false;
                Player.GlobalDie(player, self: false);
                Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1], self: false);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/infection - starts the infection game mode.");
            Player.SendMessage(p, "/infection [time] - starts the infection that will last [time] minutes.");
            Player.SendMessage(p, "/infection stop - stops the infection.");
        }

        enum InfectionMode
        {
            Zombie,
            Other
        }
    }
}