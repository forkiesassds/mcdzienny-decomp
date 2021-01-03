namespace MCDzienny
{
    // Token: 0x020002EB RID: 747
    public class CmdOpChat : Command
    {
        // Token: 0x1700084D RID: 2125
        // (get) Token: 0x06001528 RID: 5416 RVA: 0x00074E5C File Offset: 0x0007305C
        public override string name
        {
            get { return "opchat"; }
        }

        // Token: 0x1700084E RID: 2126
        // (get) Token: 0x06001529 RID: 5417 RVA: 0x00074E64 File Offset: 0x00073064
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700084F RID: 2127
        // (get) Token: 0x0600152A RID: 5418 RVA: 0x00074E6C File Offset: 0x0007306C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000850 RID: 2128
        // (get) Token: 0x0600152B RID: 5419 RVA: 0x00074E74 File Offset: 0x00073074
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000851 RID: 2129
        // (get) Token: 0x0600152C RID: 5420 RVA: 0x00074E78 File Offset: 0x00073078
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x0600152E RID: 5422 RVA: 0x00074E84 File Offset: 0x00073084
        public override void Use(Player p, string message)
        {
            p.opchat = !p.opchat;
            if (p.opchat)
            {
                Player.SendMessage(p, "All messages will now be sent to OPs only");
                return;
            }

            Player.SendMessage(p, "OP chat turned off");
        }

        // Token: 0x0600152F RID: 5423 RVA: 0x00074EB4 File Offset: 0x000730B4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/opchat - Makes all messages sent go to OPs by default");
        }
    }
}