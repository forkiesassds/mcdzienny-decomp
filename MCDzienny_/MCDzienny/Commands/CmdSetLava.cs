using MCDzienny.MultiMessages;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x020000D6 RID: 214
    public class CmdSetLava : Command
    {
        // Token: 0x1700030F RID: 783
        // (get) Token: 0x060006F2 RID: 1778 RVA: 0x00023898 File Offset: 0x00021A98
        public override string name
        {
            get { return "setlava"; }
        }

        // Token: 0x17000310 RID: 784
        // (get) Token: 0x060006F3 RID: 1779 RVA: 0x000238A0 File Offset: 0x00021AA0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000311 RID: 785
        // (get) Token: 0x060006F4 RID: 1780 RVA: 0x000238A8 File Offset: 0x00021AA8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000312 RID: 786
        // (get) Token: 0x060006F5 RID: 1781 RVA: 0x000238B0 File Offset: 0x00021AB0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000313 RID: 787
        // (get) Token: 0x060006F6 RID: 1782 RVA: 0x000238B4 File Offset: 0x00021AB4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060006F8 RID: 1784 RVA: 0x000238C0 File Offset: 0x00021AC0
        public override void Use(Player p, string message)
        {
            message = message.ToLower();
            if (message.Split(' ').Length == 2)
            {
                string a;
                if ((a = message.Split(' ')[0]) != null)
                {
                    if (!(a == "map"))
                    {
                        if (a == "mood" || a == "state")
                        {
                            var text = message.Split(' ')[1];
                            string key;
                            switch (key = text)
                            {
                                case "1":
                                case "calm":
                                    LavaSettings.All.LavaState = LavaState.Calm;
                                    Player.GlobalMessageLevel(Server.LavaLevel, "%bLava is in a %dCALM%b state now.");
                                    return;
                                case "2":
                                case "disturbed":
                                    LavaSettings.All.LavaState = LavaState.Disturbed;
                                    Player.GlobalMessageLevel(Server.LavaLevel, "%bLava is %cDISTURBED%b now.");
                                    return;
                                case "3":
                                case "furious":
                                    LavaSettings.All.LavaState = LavaState.Furious;
                                    Player.GlobalMessageLevel(Server.LavaLevel, "%bLava becomes %cFURIOUS");
                                    return;
                                case "4":
                                case "wild":
                                    LavaSettings.All.LavaState = LavaState.Wild;
                                    Player.GlobalMessageLevel(Server.LavaLevel, "%bLava gets %cWILD !!!");
                                    return;
                            }

                            Player.SendMessage(p, "Unknown lava state.");
                            return;
                        }

                        if (a == "reload")
                        {
                            string key2;
                            switch (key2 = message.Split(' ')[1])
                            {
                                case "maps":
                                case "map":
                                    LavaSystem.LoadLavaMapsXML();
                                    Player.SendMessage(p, "List of lava maps reloaded.");
                                    if (p != null)
                                    {
                                        Server.s.Log("List of lava maps reloaded.");
                                        return;
                                    }

                                    return;
                                case "price":
                                case "prices":
                                case "store":
                                    Store.LoadPricesXML();
                                    Player.SendMessage(p, "Store prices reloaded.");
                                    return;
                                case "levels":
                                    TierSystem.ReloadLevels();
                                    Player.SendMessage(p, "levelsystem.txt reloaded.");
                                    return;
                                case "text":
                                case "texts":
                                case "messages":
                                    MessagesManager.Reload();
                                    Player.SendMessage(p, "Custom messages reloaded.");
                                    return;
                                case "cmdblocks":
                                case "commandblocks":
                                case "cmdblock":
                                case "commandblock":
                                    if (p == null)
                                    {
                                        Player.SendMessage(p, "You can't use this option from console.");
                                        return;
                                    }

                                    p.level.ReloadSettings();
                                    Player.SendMessage(p,
                                        string.Format("Command blocks were reloaded for {0} map.", p.level.name));
                                    return;
                            }

                            Player.SendMessage(p, "You can reload: maps, prices, levels, texts, cmdblocks.");
                            return;
                        }
                    }
                    else
                    {
                        var mapName = message.Split(' ')[1];
                        var num3 = LavaSystem.lavaMaps.FindIndex(map => map.Name == mapName);
                        if (num3 != -1)
                        {
                            LavaSystem.SetMapIndex(num3);
                            LavaSystem.skipVoting = true;
                            LavaSystem.phase1holder = false;
                            LavaSystem.phase2holder = false;
                            LavaSystem.nextMap = true;
                            Server.s.Log("The level was changed to " + LavaSystem.lavaMaps[num3].Name + ".");
                            Player.SendMessage(p,
                                string.Format("The level was changed to {0}.", LavaSystem.lavaMaps[num3].Name));
                            return;
                        }

                        Player.SendMessage(p, string.Format("Map: {0} wasn't found.", mapName));
                        return;
                    }
                }

                Help(p);
                return;
            }

            string key3;
            switch (key3 = message)
            {
                case "up":
                    Server.LavaLevel.hardcore = !Server.LavaLevel.hardcore;
                    if (Server.LavaLevel.hardcore)
                    {
                        Player.SendMessage(p, "Now lava goes up as well.");
                        Server.s.Log("Now lava goes up as well.");
                        return;
                    }

                    Player.SendMessage(p, "Lava goes up is off now.");
                    Server.s.Log("Lava goes up mode is off now.");
                    return;
                case "protection":
                    LavaSettings.All.OverloadProtection = !LavaSettings.All.OverloadProtection;
                    if (LavaSettings.All.OverloadProtection)
                    {
                        Player.SendMessage(p, "Overload protection is ON now.");
                        return;
                    }

                    Player.SendMessage(p, "Overload protection is OFF now.");
                    return;
                case "reload":
                case "reloadmaps":
                    LavaSystem.LoadLavaMapsXML();
                    Player.SendMessage(p, "List of lava maps reloaded.");
                    if (p != null)
                    {
                        Server.s.Log("List of lava maps reloaded.");
                        return;
                    }

                    return;
                case "reloadprices":
                    Store.LoadPricesXML();
                    Player.SendMessage(p, "Store prices reloaded.");
                    return;
                case "reloadlevels":
                    TierSystem.ReloadLevels();
                    Player.SendMessage(p, "levelsystem.txt reloaded.");
                    return;
                case "next":
                    LavaSystem.skipVoting = true;
                    LavaSystem.phase1holder = false;
                    LavaSystem.phase2holder = false;
                    LavaSystem.nextMap = true;
                    Server.s.Log("The level was skipped..");
                    Player.SendMessage(p, "The level was skipped");
                    return;
                case "reloadtexts":
                    MessagesManager.Reload();
                    Player.SendMessage(p, "Custom messages reloaded.");
                    return;
                case "reloadcmdblocks":
                    if (p == null)
                    {
                        Player.SendMessage(p, "You can't use this option from console.");
                        return;
                    }

                    p.level.ReloadSettings();
                    Player.SendMessage(p, string.Format("Command blocks were reloaded for {0} map.", p.level.name));
                    return;
                case "debug":
                    Player.SendMessage(p, LavaSystem.GetCheckBlocksCount());
                    return;
                case "speedtest":
                    LavaSettings.All.ConnectionSpeedTest = !LavaSettings.All.ConnectionSpeedTest;
                    if (LavaSettings.All.ConnectionSpeedTest)
                    {
                        Player.SendMessage(p, "Speed test is now ON.");
                        Server.s.Log("Speed test was turned ON.");
                        return;
                    }

                    Player.SendMessage(p, "Speed test is now OFF.");
                    Server.s.Log("Speed test was turned OFF.");
                    return;
                case "water":
                    LavaSystem.waterFlood = !LavaSystem.waterFlood;
                    if (LavaSystem.waterFlood)
                    {
                        Player.GlobalChatWorld(p, "%cThis round is Water Flood round.", false);
                        return;
                    }

                    Player.GlobalChatWorld(p, "%cThis round is Lava Flood round.", false);
                    return;
                case "rage":
                    Player.SendMessage(p, "Use '/setlava state furious' instead.");
                    return;
            }

            Help(p);
        }

        // Token: 0x060006F9 RID: 1785 RVA: 0x0002405C File Offset: 0x0002225C
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