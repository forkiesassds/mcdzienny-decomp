namespace MCDzienny
{
    // Token: 0x02000135 RID: 309
    public class CmdTitleColor : Command
    {
        // Token: 0x1700044F RID: 1103
        // (get) Token: 0x0600093F RID: 2367 RVA: 0x0002DD4C File Offset: 0x0002BF4C
        public override string name
        {
            get { return "titlecolor"; }
        }

        // Token: 0x17000450 RID: 1104
        // (get) Token: 0x06000940 RID: 2368 RVA: 0x0002DD54 File Offset: 0x0002BF54
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000451 RID: 1105
        // (get) Token: 0x06000941 RID: 2369 RVA: 0x0002DD5C File Offset: 0x0002BF5C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000452 RID: 1106
        // (get) Token: 0x06000942 RID: 2370 RVA: 0x0002DD64 File Offset: 0x0002BF64
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000453 RID: 1107
        // (get) Token: 0x06000943 RID: 2371 RVA: 0x0002DD68 File Offset: 0x0002BF68
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000454 RID: 1108
        // (get) Token: 0x06000944 RID: 2372 RVA: 0x0002DD6C File Offset: 0x0002BF6C
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x06000946 RID: 2374 RVA: 0x0002DD78 File Offset: 0x0002BF78
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            message = message.Replace("'", "");
            if (p == null)
            {
                Player.SendMessage(p, "You can't use this command from console.");
                return;
            }

            if (!p.boughtTColor)
            {
                Player.SendMessage(p, "You have to buy /titlecolor before you can use it.");
                return;
            }

            var text = c.Parse(message);
            if (text == "")
            {
                Player.SendMessage(p, string.Format("There is no color \"{0}\".", message));
                return;
            }

            if (text == p.titlecolor)
            {
                Player.SendMessage(p, "You already have that title color.");
                return;
            }

            DBInterface.ExecuteQuery(string.Concat("UPDATE Players SET title_color = '", c.Name(text),
                "' WHERE Name = '", p.name, "'"));
            Player.GlobalChat(p,
                string.Format("{0} had their title color changed to {1}.", p.color + p.PublicName + Server.DefaultColor,
                    text + c.Name(text) + Server.DefaultColor), false);
            p.titlecolor = text;
            p.SetPrefix();
            p.boughtTColor = false;
        }

        // Token: 0x06000947 RID: 2375 RVA: 0x0002DE98 File Offset: 0x0002C098
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/titlecolor [color] - gives you the title color of [color].");
            Player.SendMessage(p, "&0black &1navy &2green &3teal &4maroon &5purple &6gold &7silver");
            Player.SendMessage(p, "&8gray &9blue &alime &baqua &cred &dpink &eyellow &fwhite");
        }
    }
}