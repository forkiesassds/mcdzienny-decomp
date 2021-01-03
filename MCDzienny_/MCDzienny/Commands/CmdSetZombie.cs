using System;
using MCDzienny.InfectionSystem;

namespace MCDzienny
{
    // Token: 0x02000141 RID: 321
    public class CmdSetZombie : Command
    {
        // Token: 0x17000468 RID: 1128
        // (get) Token: 0x06000983 RID: 2435 RVA: 0x0002E98C File Offset: 0x0002CB8C
        public override string name
        {
            get { return "setzombie"; }
        }

        // Token: 0x17000469 RID: 1129
        // (get) Token: 0x06000984 RID: 2436 RVA: 0x0002E994 File Offset: 0x0002CB94
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700046A RID: 1130
        // (get) Token: 0x06000985 RID: 2437 RVA: 0x0002E99C File Offset: 0x0002CB9C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700046B RID: 1131
        // (get) Token: 0x06000986 RID: 2438 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700046C RID: 1132
        // (get) Token: 0x06000987 RID: 2439 RVA: 0x0002E9A8 File Offset: 0x0002CBA8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x1700046D RID: 1133
        // (get) Token: 0x06000988 RID: 2440 RVA: 0x0002E9AC File Offset: 0x0002CBAC
        public override CommandScope Scope
        {
            get { return CommandScope.Zombie; }
        }

        // Token: 0x06000989 RID: 2441 RVA: 0x0002E9B0 File Offset: 0x0002CBB0
        public override void Use(Player p, string message)
        {
            message = message.ToLower();
            if (message.Split(' ').Length == 2)
            {
                string a;
                if ((a = message.Split(' ')[0]) != null)
                {
                    if (!(a == "zombie"))
                    {
                        if (a == "time")
                        {
                            var num = int.Parse(message.Split(' ')[1]);
                            InfectionUtils.EndTime = DateTime.Now.Add(new TimeSpan(0, num, 0));
                            Player.SendMessage(p, "%7Time left: {0}min", num);
                            return;
                        }

                        if (!(a == "map") && !(a == "queue"))
                        {
                            if (a == "reload")
                            {
                                string a2;
                                if ((a2 = message.Split(' ')[1]) == null || !(a2 == "maps") && !(a2 == "map"))
                                {
                                    Help(p);
                                    return;
                                }

                                InfectionMaps.LoadInfectionMapsXML();
                                Player.SendMessage(p, "List of zombie maps was reloaded.");
                                if (p != null)
                                {
                                    Server.s.Log("List of zombie maps was reloaded.");
                                    return;
                                }

                                return;
                            }
                        }
                        else
                        {
                            var mapName = message.Split(' ')[1];
                            var num2 = InfectionMaps.infectionMaps.FindIndex(map => map.Name == mapName);
                            if (num2 != -1)
                            {
                                Player.GlobalMessageLevel(InfectionSystem.InfectionSystem.currentInfectionLevel,
                                    "Queued map: %b" + InfectionMaps.infectionMaps[num2].Name);
                                InfectionSystem.InfectionSystem.skipVoting = true;
                                InfectionSystem.InfectionSystem.selectedMapIndex = num2;
                                return;
                            }

                            Player.SendMessage(p, string.Format("Map named %c{0}%s wasn't found.", mapName));
                            return;
                        }
                    }
                    else
                    {
                        var text = message.Split(' ')[1];
                        var player = Player.Find(text);
                        if (player == null)
                        {
                            Player.SendMessage(p, "Couldn't find a player named " + text);
                            return;
                        }

                        InfectionSystem.InfectionSystem.NextZombie = player.name;
                        Player.GlobalMessageLevel(InfectionSystem.InfectionSystem.currentInfectionLevel,
                            "Player %c" + player.PublicName + "%s will start the infection next round.");
                        return;
                    }
                }

                Help(p);
                return;
            }

            string a3;
            if ((a3 = message) != null && (a3 == "reload" || a3 == "reloadmaps" || a3 == "reloadmap"))
            {
                InfectionMaps.LoadInfectionMapsXML();
                Player.SendMessage(p, "List of zombie maps was reloaded.");
                if (p != null) Server.s.Log("List of zombie maps was reloaded.");
            }
            else
            {
                Help(p);
            }
        }

        // Token: 0x0600098A RID: 2442 RVA: 0x0002EC68 File Offset: 0x0002CE68
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setzombie map [map name] - switches to the chosen map.");
            Player.SendMessage(p, "/setzombie reloadmaps - reloads the list of zombie maps.");
            Player.SendMessage(p, "/setzombie zombie [player] - player starts as zombie next round.");
        }
    }
}