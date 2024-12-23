using System;
using MCDzienny.InfectionSystem;

namespace MCDzienny
{
    public class CmdSetZombie : Command
    {
        public override string name { get { return "setzombie"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override CommandScope Scope { get { return CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            message = message.ToLower();
            if (message.Split(' ').Length == 2)
            {
                switch (message.Split(' ')[0])
                {
                    case "zombie":
                    {
                        string text = message.Split(' ')[1];
                        Player player = Player.Find(text);
                        if (player == null)
                        {
                            Player.SendMessage(p, "Couldn't find a player named " + text);
                            break;
                        }
                        InfectionSystem.InfectionSystem.NextZombie = player.name;
                        Player.GlobalMessageLevel(InfectionSystem.InfectionSystem.currentInfectionLevel,
                                                  "Player %c" + player.PublicName + "%s will start the infection next round.");
                        break;
                    }
                    case "time":
                    {
                        int num2 = int.Parse(message.Split(' ')[1]);
                        InfectionUtils.EndTime = DateTime.Now.Add(new TimeSpan(0, num2, 0));
                        Player.SendMessage(p, "%7Time left: {0}min", num2);
                        break;
                    }
                    case "map":
                    case "queue":
                    {
                        string mapName = message.Split(' ')[1];
                        int num = InfectionMaps.infectionMaps.FindIndex(map => map.Name == mapName ? true : false);
                        if (num != -1)
                        {
                            Player.GlobalMessageLevel(InfectionSystem.InfectionSystem.currentInfectionLevel, "Queued map: %b" + InfectionMaps.infectionMaps[num].Name);
                            InfectionSystem.InfectionSystem.skipVoting = true;
                            InfectionSystem.InfectionSystem.selectedMapIndex = num;
                        }
                        else
                        {
                            Player.SendMessage(p, string.Format("Map named %c{0}%s wasn't found.", mapName));
                        }
                        break;
                    }
                    case "reload":
                        switch (message.Split(' ')[1])
                        {
                            case "maps":
                            case "map":
                                InfectionMaps.LoadInfectionMapsXML();
                                Player.SendMessage(p, "List of zombie maps was reloaded.");
                                if (p != null)
                                {
                                    Server.s.Log("List of zombie maps was reloaded.");
                                }
                                break;
                            default:
                                Help(p);
                                break;
                        }
                        break;
                    default:
                        Help(p);
                        break;
                }
                return;
            }
            switch (message)
            {
                case "reload":
                case "reloadmaps":
                case "reloadmap":
                    InfectionMaps.LoadInfectionMapsXML();
                    Player.SendMessage(p, "List of zombie maps was reloaded.");
                    if (p != null)
                    {
                        Server.s.Log("List of zombie maps was reloaded.");
                    }
                    break;
                default:
                    Help(p);
                    break;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setzombie map [map name] - switches to the chosen map.");
            Player.SendMessage(p, "/setzombie reloadmaps - reloads the list of zombie maps.");
            Player.SendMessage(p, "/setzombie zombie [player] - player starts as zombie next round.");
        }
    }
}