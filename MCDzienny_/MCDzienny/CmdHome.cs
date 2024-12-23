using System.Collections.Generic;
using MCDzienny.Misc;
using MCDzienny.Settings;

namespace MCDzienny
{
    class CmdHome : Command
    {

        readonly List<CaseAction> caseActionList;
        readonly SyntaxCaseSolver caseSolver;

        public CmdHome()
        {
            caseActionList = new List<CaseAction>();
            caseActionList.Add(new CaseAction(Default, ""));
            caseActionList.Add(new CaseAction(SetPassword, "password"));
            caseActionList.Add(new CaseAction(NewMap, "new", "create"));
            caseActionList.Add(new CaseAction(Kick, "kick"));
            caseActionList.Add(new CaseAction(Ban, "ban"));
            caseActionList.Add(new CaseAction(PerBuild, "perbuild"));
            caseActionList.Add(new CaseAction(PerVisit, "pervisit"));
            caseActionList.Add(new CaseAction(Delete, "delete", "remove"));
            caseActionList.Add(new CaseAction(BackToMain, "exit", "return", "mainmap", "leave", "back"));
            caseSolver = new SyntaxCaseSolver(caseActionList, Help);
        }

        public override string name { get { return "home"; } }

        public override string shortcut { get { return "pmap"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override string CustomName { get { return ""; } }

        public override void Use(Player p, string message)
        {
            caseSolver.Process(p, message.ToLower());
        }

        public void Default(Player p, string message)
        {
            Level level = Level.FindExact(p.name.ToLower());
            if (level != null)
            {
                all.Find("goto").Use(p, p.name);
            }
            else
            {
                level = Level.Load(p.name.ToLower(), 4, MapType.Home, autoUnload: true);
                if (level != null)
                {
                    Server.AddLevel(level);
                    all.Find("goto").Use(p, p.name);
                }
                else
                {
                    NewMap(p, "create yes");
                    level = Level.FindExact(p.name);
                    if (level == null)
                    {
                        Player.SendMessage(p, "Error: Can't create a home map for you!");
                        return;
                    }
                    all.Find("goto").Use(p, p.name);
                }
            }
            Player.SendMessage(p, "Welcome to your home map.");
        }

        public void BackToMain(Player p, string message)
        {
            all.Find("goto").Use(p, Server.DefaultLevel.name);
        }

        public void SetPassword(Player p, string message) {}

        public void NewMap(Player p, string message)
        {
            bool flag = false;
            bool flag2 = false;
            string text = "";
            if (message.Split(' ').Length > 1)
            {
                if (message.Split(' ')[1].Trim().ToLower() == "yes")
                {
                    flag = true;
                }
                text = message.Split(' ')[0].Trim().ToLower();
            }
            else
            {
                text = message.Trim().ToLower();
            }
            string text2 = "flat";
            switch (text)
            {
                case "flat":
                case "pixel":
                case "island":
                case "mountains":
                case "ocean":
                case "forest":
                case "desert":
                    text2 = text;
                    break;
                case "yes":
                    flag = true;
                    break;
            }
            if (!flag)
            {
                Player.SendMessage(p, "Creating a new home map will delete your current home map!");
                Player.SendMessage(p, "Are you sure you want to create a new one?");
                Player.SendMessage(p, "If yes write '/home new yes' or '/home new [theme] yes'");
                return;
            }
            if (p.level.name.ToLower() == p.name.ToLower() && p.level.mapType == MapType.Home)
            {
                flag2 = true;
            }
            Level level = Level.FindExact(p.name.ToLower());
            if (level != null)
            {
                all.Find("unload").Use(null, p.name.ToLower());
            }
            DBInterface.ExecuteQuery("DROP TABLE IF EXISTS `Block" + p.name.ToLower() + "`");
            DBInterface.ExecuteQuery("DROP TABLE IF EXISTS `Portals" + p.name.ToLower() + "`");
            DBInterface.ExecuteQuery("DROP TABLE IF EXISTS `Messages" + p.name.ToLower() + "`");
            DBInterface.ExecuteQuery("DROP TABLE IF EXISTS `Zone" + p.name.ToLower() + "`");
            ushort x = (ushort)GeneralSettings.All.HomeMapWidth;
            ushort y = (ushort)GeneralSettings.All.HomeMapHeight;
            ushort z = (ushort)GeneralSettings.All.HomeMapDepth;
            Level level2 = new Level(p.name, x, y, z, text2);
            level2.mapType = MapType.Home;
            level2.directoryPath = "maps/home";
            level2.IsMapBeingBackuped = true;
            level2.Save(Override: true);
            Level level3 = Level.Load(p.name.ToLower(), 4, MapType.Home, autoUnload: true);
            Player.SendMessage(p, "A new home map was created for you.");
            if (flag2)
            {
                p.SendToMap(level3);
            }
        }

        public void Kick(Player p, string message)
        {
            Player player = Player.Find(message);
            if (player != null)
            {
                if (player.group.Permission >= LevelPermission.Operator)
                {
                    Player.SendMessage(p, "You can't kick OP+");
                }
                else if (player.level.name.ToLower() == p.name.ToLower() && player.level.mapType == MapType.Home)
                {
                    all.Find("move").Use(null, player.name + " " + Server.DefaultLevel.name);
                    Player.SendMessage(p, "%c" + p.name + " kicked you out of his home.");
                    Player.SendMessage(p, "You kicked " + player.name);
                }
                else
                {
                    Player.SendMessage(p, "The player isn't on your home map. You can't kick him.");
                }
            }
            else
            {
                Player.SendMessage(p, "Player wasn't found.");
            }
        }

        public void Ban(Player p, string message) {}

        public void PerBuild(Player p, string message) {}

        public void PerVisit(Player p, string message) {}

        public void Delete(Player p, string message) {}

        public void Help(Player p, string message)
        {
            Help(p);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/home - teleports you to your home map.");
            Player.SendMessage(p, "/home back - teleports you back to the main map.");
            Player.SendMessage(p, "/home new [theme] - replaces your current home map with a new one.");
            Player.SendMessage(p, "Available [theme] is: flat, mountains, ocean, forest, desert, pixel.");
            Player.SendMessage(p, "/home kick [player] - kicks a player from your home map.");
        }
    }
}