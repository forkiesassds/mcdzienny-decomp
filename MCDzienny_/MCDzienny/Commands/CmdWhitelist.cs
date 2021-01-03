namespace MCDzienny
{
    // Token: 0x020002AD RID: 685
    public class CmdWhitelist : Command
    {
        // Token: 0x1700077F RID: 1919
        // (get) Token: 0x060013A3 RID: 5027 RVA: 0x0006C000 File Offset: 0x0006A200
        public override string name
        {
            get { return "whitelist"; }
        }

        // Token: 0x17000780 RID: 1920
        // (get) Token: 0x060013A4 RID: 5028 RVA: 0x0006C008 File Offset: 0x0006A208
        public override string shortcut
        {
            get { return "w"; }
        }

        // Token: 0x17000781 RID: 1921
        // (get) Token: 0x060013A5 RID: 5029 RVA: 0x0006C010 File Offset: 0x0006A210
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000782 RID: 1922
        // (get) Token: 0x060013A6 RID: 5030 RVA: 0x0006C018 File Offset: 0x0006A218
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000783 RID: 1923
        // (get) Token: 0x060013A7 RID: 5031 RVA: 0x0006C01C File Offset: 0x0006A21C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060013A9 RID: 5033 RVA: 0x0006C028 File Offset: 0x0006A228
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var num = message.IndexOf(' ');
            if (num != -1)
            {
                var text = message.Substring(0, num);
                var text2 = message.Substring(num + 1);
                string a;
                if ((a = text) != null)
                {
                    if (!(a == "add"))
                    {
                        if (!(a == "del"))
                        {
                            if (a == "list")
                            {
                                var text3 = "Whitelist:&f";
                                foreach (var str in Server.whiteList.All()) text3 = text3 + " " + str + ",";
                                text3 = text3.Substring(0, text3.Length - 1);
                                Player.SendMessage(p, text3);
                                return;
                            }
                        }
                        else
                        {
                            if (!Server.whiteList.Contains(text2))
                            {
                                Player.SendMessage(p, "&f" + text2 + Server.DefaultColor + " is not on the whitelist!");
                                return;
                            }

                            Server.whiteList.Remove(text2);
                            Player.GlobalMessageOps(string.Concat(p.color, p.prefix, p.name, Server.DefaultColor,
                                " removed &f", text2, Server.DefaultColor, " from the whitelist."));
                            Server.whiteList.Save("whitelist.txt");
                            Server.s.Log("WHITELIST: Removed " + text2);
                            return;
                        }
                    }
                    else
                    {
                        if (Server.whiteList.Contains(text2))
                        {
                            Player.SendMessage(p, "&f" + text2 + Server.DefaultColor + " is already on the whitelist!");
                            return;
                        }

                        Server.whiteList.Add(text2);
                        Player.GlobalMessageOps(string.Concat(p.color, p.prefix, p.name, Server.DefaultColor,
                            " added &f", text2, Server.DefaultColor, " to the whitelist."));
                        Server.whiteList.Save("whitelist.txt");
                        Server.s.Log("WHITELIST: Added " + text2);
                        return;
                    }
                }

                Help(p);
                return;
            }

            if (message == "list")
            {
                var text4 = "Whitelist:&f";
                foreach (var str2 in Server.whiteList.All()) text4 = text4 + " " + str2 + ",";
                text4 = text4.Substring(0, text4.Length - 1);
                Player.SendMessage(p, text4);
                return;
            }

            Help(p);
        }

        // Token: 0x060013AA RID: 5034 RVA: 0x0006C324 File Offset: 0x0006A524
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/whitelist <add/del/list> [player] - Handles whitelist entry for [player], or lists all entries.");
        }
    }
}