using MCDzienny.MultiMessages;
using MCDzienny.Settings;

namespace MCDzienny
{
    public class CmdSetLava : Command
    {
        public override string name { get { return "setlava"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            message = message.ToLower();
            if (message.Split(' ').Length == 2)
            {
                switch (message.Split(' ')[0])
                {
                    case "map":
                    {
                        string mapName = message.Split(' ')[1];
                        int num = LavaSystem.lavaMaps.FindIndex(map => map.Name == mapName ? true : false);
                        if (num != -1)
                        {
                            LavaSystem.SetMapIndex(num);
                            LavaSystem.skipVoting = true;
                            LavaSystem.phase1holder = false;
                            LavaSystem.phase2holder = false;
                            LavaSystem.nextMap = true;
                            Server.s.Log("The level was changed to " + LavaSystem.lavaMaps[num].Name + ".");
                            Player.SendMessage(p, string.Format("The level was changed to {0}.", LavaSystem.lavaMaps[num].Name));
                        }
                        else
                        {
                            Player.SendMessage(p, string.Format("Map: {0} wasn't found.", mapName));
                        }
                        break;
                    }
                    case "mood":
                    case "state":
                        switch (message.Split(' ')[1])
                        {
                            case "1":
                            case "calm":
                                LavaSettings.All.LavaState = LavaState.Calm;
                                Player.GlobalMessageLevel(Server.LavaLevel, "%bLava is in a %dCALM%b state now.");
                                break;
                            case "2":
                            case "disturbed":
                                LavaSettings.All.LavaState = LavaState.Disturbed;
                                Player.GlobalMessageLevel(Server.LavaLevel, "%bLava is %cDISTURBED%b now.");
                                break;
                            case "3":
                            case "furious":
                                LavaSettings.All.LavaState = LavaState.Furious;
                                Player.GlobalMessageLevel(Server.LavaLevel, "%bLava becomes %cFURIOUS");
                                break;
                            case "4":
                            case "wild":
                                LavaSettings.All.LavaState = LavaState.Wild;
                                Player.GlobalMessageLevel(Server.LavaLevel, "%bLava gets %cWILD !!!");
                                break;
                            default:
                                Player.SendMessage(p, "Unknown lava state.");
                                break;
                        }
                        break;
                    case "reload":
                        switch (message.Split(' ')[1])
                        {
                            case "maps":
                            case "map":
                                LavaSystem.LoadLavaMapsXML();
                                Player.SendMessage(p, "List of lava maps reloaded.");
                                if (p != null)
                                {
                                    Server.s.Log("List of lava maps reloaded.");
                                }
                                break;
                            case "price":
                            case "prices":
                            case "store":
                                Store.LoadPricesXML();
                                Player.SendMessage(p, "Store prices reloaded.");
                                break;
                            case "levels":
                                TierSystem.ReloadLevels();
                                Player.SendMessage(p, "levelsystem.txt reloaded.");
                                break;
                            case "text":
                            case "texts":
                            case "messages":
                                MessagesManager.Reload();
                                Player.SendMessage(p, "Custom messages reloaded.");
                                break;
                            case "cmdblocks":
                            case "commandblocks":
                            case "cmdblock":
                            case "commandblock":
                                if (p == null)
                                {
                                    Player.SendMessage(p, "You can't use this option from console.");
                                    break;
                                }
                                p.level.ReloadSettings();
                                Player.SendMessage(p, string.Format("Command blocks were reloaded for {0} map.", p.level.name));
                                break;
                            default:
                                Player.SendMessage(p, "You can reload: maps, prices, levels, texts, cmdblocks.");
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
                case "up":
                    Server.LavaLevel.hardcore = !Server.LavaLevel.hardcore;
                    if (Server.LavaLevel.hardcore)
                    {
                        Player.SendMessage(p, "Now lava goes up as well.");
                        Server.s.Log("Now lava goes up as well.");
                    }
                    else
                    {
                        Player.SendMessage(p, "Lava goes up is off now.");
                        Server.s.Log("Lava goes up mode is off now.");
                    }
                    break;
                case "protection":
                    LavaSettings.All.OverloadProtection = !LavaSettings.All.OverloadProtection;
                    if (LavaSettings.All.OverloadProtection)
                    {
                        Player.SendMessage(p, "Overload protection is ON now.");
                    }
                    else
                    {
                        Player.SendMessage(p, "Overload protection is OFF now.");
                    }
                    break;
                case "reload":
                case "reloadmaps":
                    LavaSystem.LoadLavaMapsXML();
                    Player.SendMessage(p, "List of lava maps reloaded.");
                    if (p != null)
                    {
                        Server.s.Log("List of lava maps reloaded.");
                    }
                    break;
                case "reloadprices":
                    Store.LoadPricesXML();
                    Player.SendMessage(p, "Store prices reloaded.");
                    break;
                case "reloadlevels":
                    TierSystem.ReloadLevels();
                    Player.SendMessage(p, "levelsystem.txt reloaded.");
                    break;
                case "next":
                    LavaSystem.skipVoting = true;
                    LavaSystem.phase1holder = false;
                    LavaSystem.phase2holder = false;
                    LavaSystem.nextMap = true;
                    Server.s.Log("The level was skipped..");
                    Player.SendMessage(p, "The level was skipped");
                    break;
                case "reloadtexts":
                    MessagesManager.Reload();
                    Player.SendMessage(p, "Custom messages reloaded.");
                    break;
                case "reloadcmdblocks":
                    if (p == null)
                    {
                        Player.SendMessage(p, "You can't use this option from console.");
                        break;
                    }
                    p.level.ReloadSettings();
                    Player.SendMessage(p, string.Format("Command blocks were reloaded for {0} map.", p.level.name));
                    break;
                case "debug":
                    Player.SendMessage(p, LavaSystem.GetCheckBlocksCount());
                    break;
                case "speedtest":
                    LavaSettings.All.ConnectionSpeedTest = !LavaSettings.All.ConnectionSpeedTest;
                    if (LavaSettings.All.ConnectionSpeedTest)
                    {
                        Player.SendMessage(p, "Speed test is now ON.");
                        Server.s.Log("Speed test was turned ON.");
                    }
                    else
                    {
                        Player.SendMessage(p, "Speed test is now OFF.");
                        Server.s.Log("Speed test was turned OFF.");
                    }
                    break;
                case "water":
                    LavaSystem.waterFlood = !LavaSystem.waterFlood;
                    if (LavaSystem.waterFlood)
                    {
                        Player.GlobalChatWorld(p, "%cThis round is Water Flood round.", showname: false);
                    }
                    else
                    {
                        Player.GlobalChatWorld(p, "%cThis round is Lava Flood round.", showname: false);
                    }
                    break;
                case "rage":
                    Player.SendMessage(p, "Use '/setlava state furious' instead.");
                    break;
                default:
                    Help(p);
                    break;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setlava next - jumps to next map.");
            Player.SendMessage(p, "/setlava map [map name] - goes to chosen map.");
            Player.SendMessage(p, "/setlava state [lava state] - sets lava to chosen state.");
            Player.SendMessage(p, "/setlava reloadmaps - reloads list of lava maps.");
            Player.SendMessage(p, "/setlava reloadtexts - reloads textdata.");
            Player.SendMessage(p, "/setlava reloadprices - reloads list of store items.");
            Player.SendMessage(p, "More uses are listed on http://mcdzienny.cba.pl");
        }
    }
}