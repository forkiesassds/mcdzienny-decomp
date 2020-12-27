namespace MCDzienny
{
    // Token: 0x020002EA RID: 746
    public class CmdEmote : Command
    {
        // Token: 0x17000847 RID: 2119
        // (get) Token: 0x0600151F RID: 5407 RVA: 0x00074DE8 File Offset: 0x00072FE8
        public override string name
        {
            get { return "emote"; }
        }

        // Token: 0x17000848 RID: 2120
        // (get) Token: 0x06001520 RID: 5408 RVA: 0x00074DF0 File Offset: 0x00072FF0
        public override string shortcut
        {
            get { return "<3"; }
        }

        // Token: 0x17000849 RID: 2121
        // (get) Token: 0x06001521 RID: 5409 RVA: 0x00074DF8 File Offset: 0x00072FF8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700084A RID: 2122
        // (get) Token: 0x06001522 RID: 5410 RVA: 0x00074E00 File Offset: 0x00073000
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700084B RID: 2123
        // (get) Token: 0x06001523 RID: 5411 RVA: 0x00074E04 File Offset: 0x00073004
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x1700084C RID: 2124
        // (get) Token: 0x06001524 RID: 5412 RVA: 0x00074E08 File Offset: 0x00073008
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001526 RID: 5414 RVA: 0x00074E14 File Offset: 0x00073014
        public override void Use(Player p, string message)
        {
            p.parseSmiley = !p.parseSmiley;
            p.smileySaved = false;
            if (p.parseSmiley)
            {
                Player.SendMessage(p, "Emote parsing is enabled.");
                return;
            }

            Player.SendMessage(p, "Emote parsing is disabled.");
        }

        // Token: 0x06001527 RID: 5415 RVA: 0x00074E4C File Offset: 0x0007304C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/emote - Enables or disables emoticon parsing");
        }
    }
}