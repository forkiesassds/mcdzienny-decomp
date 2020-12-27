namespace MCDzienny
{
    // Token: 0x02000298 RID: 664
    public class CmdSetspawn : Command
    {
        // Token: 0x17000725 RID: 1829
        // (get) Token: 0x06001307 RID: 4871 RVA: 0x00068E88 File Offset: 0x00067088
        public override string name
        {
            get { return "setspawn"; }
        }

        // Token: 0x17000726 RID: 1830
        // (get) Token: 0x06001308 RID: 4872 RVA: 0x00068E90 File Offset: 0x00067090
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000727 RID: 1831
        // (get) Token: 0x06001309 RID: 4873 RVA: 0x00068E98 File Offset: 0x00067098
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000728 RID: 1832
        // (get) Token: 0x0600130A RID: 4874 RVA: 0x00068EA0 File Offset: 0x000670A0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000729 RID: 1833
        // (get) Token: 0x0600130B RID: 4875 RVA: 0x00068EA4 File Offset: 0x000670A4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600130C RID: 4876 RVA: 0x00068EA8 File Offset: 0x000670A8
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            Player.SendMessage(p, "Spawn location changed.");
            p.level.spawnx = (ushort) (p.pos[0] / 32);
            p.level.spawny = (ushort) (p.pos[1] / 32);
            p.level.spawnz = (ushort) (p.pos[2] / 32);
            p.level.rotx = p.rot[0];
            p.level.roty = 0;
            p.level.changed = true;
        }

        // Token: 0x0600130D RID: 4877 RVA: 0x00068F4C File Offset: 0x0006714C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setspawn - Set the default spawn location.");
        }
    }
}