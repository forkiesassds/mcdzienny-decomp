namespace MCDzienny
{
    // Token: 0x02000136 RID: 310
    public class CmdLavaPortal : Command
    {
        // Token: 0x17000455 RID: 1109
        // (get) Token: 0x06000948 RID: 2376 RVA: 0x0002DEBC File Offset: 0x0002C0BC
        public override string name
        {
            get { return "lavaportal"; }
        }

        // Token: 0x17000456 RID: 1110
        // (get) Token: 0x06000949 RID: 2377 RVA: 0x0002DEC4 File Offset: 0x0002C0C4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000457 RID: 1111
        // (get) Token: 0x0600094A RID: 2378 RVA: 0x0002DECC File Offset: 0x0002C0CC
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000458 RID: 1112
        // (get) Token: 0x0600094B RID: 2379 RVA: 0x0002DED4 File Offset: 0x0002C0D4
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000459 RID: 1113
        // (get) Token: 0x0600094C RID: 2380 RVA: 0x0002DED8 File Offset: 0x0002C0D8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x1700045A RID: 1114
        // (get) Token: 0x0600094D RID: 2381 RVA: 0x0002DEDC File Offset: 0x0002C0DC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600094F RID: 2383 RVA: 0x0002DEE8 File Offset: 0x0002C0E8
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (message.ToLower() == "show")
            {
                all.Find("portal").Use(p, message);
                return;
            }

            all.Find("portal").Use(p, message + " lavamap");
        }

        // Token: 0x06000950 RID: 2384 RVA: 0x0002DF54 File Offset: 0x0002C154
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/lavaportal [orange/blue/air/water/lava] [multi] - Activates Portal mode.");
            Player.SendMessage(p, "/lavaportal [type] multi - Place Entry blocks until exit is wanted.");
            Player.SendMessage(p, "/lavaportal show - Shows portals, green = in, red = out.");
        }
    }
}