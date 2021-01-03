using System.Collections.Generic;
using MCDzienny.Misc;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x02000061 RID: 97
    internal class CmdHome : Command
    {
        // Token: 0x04000179 RID: 377
        private readonly List<CaseAction> caseActionList;

        // Token: 0x04000178 RID: 376
        private readonly SyntaxCaseSolver caseSolver;

        // Token: 0x06000266 RID: 614 RVA: 0x0000D1BC File Offset: 0x0000B3BC
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

        // Token: 0x170000A1 RID: 161
        // (get) Token: 0x0600025F RID: 607 RVA: 0x0000D190 File Offset: 0x0000B390
        public override string name
        {
            get { return "home"; }
        }

        // Token: 0x170000A2 RID: 162
        // (get) Token: 0x06000260 RID: 608 RVA: 0x0000D198 File Offset: 0x0000B398
        public override string shortcut
        {
            get { return "pmap"; }
        }

        // Token: 0x170000A3 RID: 163
        // (get) Token: 0x06000261 RID: 609 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000A4 RID: 164
        // (get) Token: 0x06000262 RID: 610 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170000A5 RID: 165
        // (get) Token: 0x06000263 RID: 611 RVA: 0x0000D1AC File Offset: 0x0000B3AC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x170000A6 RID: 166
        // (get) Token: 0x06000264 RID: 612 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170000A7 RID: 167
        // (get) Token: 0x06000265 RID: 613 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
        public override string CustomName
        {
            get { return ""; }
        }

        // Token: 0x06000267 RID: 615 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
        public override void Use(Player p, string message)
        {
            caseSolver.Process(p, message.ToLower());
        }

        // Token: 0x06000268 RID: 616 RVA: 0x0000D3DC File Offset: 0x0000B5DC
        public void Default(Player p, string message)
        {
            var level = Level.FindExact(p.name.ToLower());
            if (level != null)
            {
                all.Find("goto").Use(p, p.name);
            }
            else
            {
                level = Level.Load(p.name.ToLower(), 4, MapType.Home, true);
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

        // Token: 0x06000269 RID: 617 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
        public void BackToMain(Player p, string message)
        {
            all.Find("goto").Use(p, Server.DefaultLevel.name);
        }

        // Token: 0x0600026A RID: 618 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
        public void SetPassword(Player p, string message)
        {
        }

        // Token: 0x0600026B RID: 619 RVA: 0x0000D4CC File Offset: 0x0000B6CC
        public void NewMap(Player p, string message)
        {
            var flag = false;
            var flag2 = false;
            string text;
            if (message.Split(' ').Length > 1)
            {
                if (message.Split(' ')[1].Trim().ToLower() == "yes") flag = true;
                text = message.Split(' ')[0].Trim().ToLower();
            }
            else
            {
                text = message.Trim().ToLower();
            }

            var type = "flat";
            string key;
            switch (key = text)
            {
                case "flat":
                case "pixel":
                case "island":
                case "mountains":
                case "ocean":
                case "forest":
                case "desert":
                    type = text;
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

            if (p.level.name.ToLower() == p.name.ToLower() && p.level.mapType == MapType.Home) flag2 = true;
            var level = Level.FindExact(p.name.ToLower());
            if (level != null) all.Find("unload").Use(null, p.name.ToLower());
            DBInterface.ExecuteQuery("DROP TABLE IF EXISTS `Block" + p.name.ToLower() + "`");
            DBInterface.ExecuteQuery("DROP TABLE IF EXISTS `Portals" + p.name.ToLower() + "`");
            DBInterface.ExecuteQuery("DROP TABLE IF EXISTS `Messages" + p.name.ToLower() + "`");
            DBInterface.ExecuteQuery("DROP TABLE IF EXISTS `Zone" + p.name.ToLower() + "`");
            var x = (ushort) GeneralSettings.All.HomeMapWidth;
            var y = (ushort) GeneralSettings.All.HomeMapHeight;
            var z = (ushort) GeneralSettings.All.HomeMapDepth;
            new Level(p.name, x, y, z, type)
            {
                mapType = MapType.Home,
                directoryPath = "maps/home",
                IsMapBeingBackuped = true
            }.Save(true);
            var level2 = Level.Load(p.name.ToLower(), 4, MapType.Home, true);
            Player.SendMessage(p, "A new home map was created for you.");
            if (flag2) p.SendToMap(level2);
        }

        // Token: 0x0600026C RID: 620 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
        public void Kick(Player p, string message)
        {
            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Player wasn't found.");
                return;
            }

            if (player.group.Permission >= LevelPermission.Operator)
            {
                Player.SendMessage(p, "You can't kick OP+");
                return;
            }

            if (player.level.name.ToLower() == p.name.ToLower() && player.level.mapType == MapType.Home)
            {
                all.Find("move").Use(null, player.name + " " + Server.DefaultLevel.name);
                Player.SendMessage(p, "%c" + p.name + " kicked you out of his home.");
                Player.SendMessage(p, "You kicked " + player.name);
                return;
            }

            Player.SendMessage(p, "The player isn't on your home map. You can't kick him.");
        }

        // Token: 0x0600026D RID: 621 RVA: 0x0000D898 File Offset: 0x0000BA98
        public void Ban(Player p, string message)
        {
        }

        // Token: 0x0600026E RID: 622 RVA: 0x0000D89C File Offset: 0x0000BA9C
        public void PerBuild(Player p, string message)
        {
        }

        // Token: 0x0600026F RID: 623 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
        public void PerVisit(Player p, string message)
        {
        }

        // Token: 0x06000270 RID: 624 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
        public void Delete(Player p, string message)
        {
        }

        // Token: 0x06000271 RID: 625 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
        public void Help(Player p, string message)
        {
            Help(p);
        }

        // Token: 0x06000272 RID: 626 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
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