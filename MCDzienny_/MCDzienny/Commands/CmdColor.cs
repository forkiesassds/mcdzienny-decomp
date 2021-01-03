using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000134 RID: 308
    public class CmdColor : Command
    {
        // Token: 0x1700044A RID: 1098
        // (get) Token: 0x06000936 RID: 2358 RVA: 0x0002DB6C File Offset: 0x0002BD6C
        public override string name
        {
            get { return "color"; }
        }

        // Token: 0x1700044B RID: 1099
        // (get) Token: 0x06000937 RID: 2359 RVA: 0x0002DB74 File Offset: 0x0002BD74
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700044C RID: 1100
        // (get) Token: 0x06000938 RID: 2360 RVA: 0x0002DB7C File Offset: 0x0002BD7C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700044D RID: 1101
        // (get) Token: 0x06000939 RID: 2361 RVA: 0x0002DB84 File Offset: 0x0002BD84
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700044E RID: 1102
        // (get) Token: 0x0600093A RID: 2362 RVA: 0x0002DB88 File Offset: 0x0002BD88
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x0600093C RID: 2364 RVA: 0x0002DB94 File Offset: 0x0002BD94
        public override void Use(Player p, string message)
        {
            if (p != null && !p.boughtColor)
            {
                Player.SendMessage(p, "You have to buy the color first. Check /store.");
                return;
            }

            if (message == "")
            {
                Help(p);
                return;
            }

            var text = c.Parse(message);
            if (text == "")
            {
                Player.SendMessage(p, string.Format("There is no color \"{0}\".", message));
                return;
            }

            if (text == p.color)
            {
                Player.SendMessage(p, "You already have this color.");
                return;
            }

            DBInterface.ExecuteQuery("UPDATE Players SET color = @Color WHERE name = @Name",
                new Dictionary<string, object>
                {
                    {
                        "@Color",
                        c.Name(text)
                    },
                    {
                        "@Name",
                        p.name
                    }
                });
            Player.GlobalChat(p,
                string.Format("{0}*{1} color changed to {2}.", p.color, Name(p.PublicName),
                    text + c.Name(text) + Server.DefaultColor), false);
            p.color = text;
            Player.GlobalDie(p, false);
            Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
            p.SetPrefix();
            p.boughtColor = false;
        }

        // Token: 0x0600093D RID: 2365 RVA: 0x0002DCC4 File Offset: 0x0002BEC4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/color <color> - changes the nick color.");
            Player.SendMessage(p, "&0black &1navy &2green &3teal &4maroon &5purple &6gold &7silver");
            Player.SendMessage(p, "&8gray &9blue &alime &baqua &cred &dpink &eyellow &fwhite");
        }

        // Token: 0x0600093E RID: 2366 RVA: 0x0002DCE8 File Offset: 0x0002BEE8
        private static string Name(string name)
        {
            var a = name[name.Length - 1].ToString().ToLower();
            if (a == "s" || a == "x") return name + Server.DefaultColor + "'";
            return name + Server.DefaultColor + "'s";
        }
    }
}