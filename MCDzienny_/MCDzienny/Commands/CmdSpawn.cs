namespace MCDzienny
{
    // Token: 0x02000299 RID: 665
    public class CmdSpawn : Command
    {
        // Token: 0x1700072A RID: 1834
        // (get) Token: 0x0600130F RID: 4879 RVA: 0x00068F64 File Offset: 0x00067164
        public override string name
        {
            get { return "spawn"; }
        }

        // Token: 0x1700072B RID: 1835
        // (get) Token: 0x06001310 RID: 4880 RVA: 0x00068F6C File Offset: 0x0006716C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700072C RID: 1836
        // (get) Token: 0x06001311 RID: 4881 RVA: 0x00068F74 File Offset: 0x00067174
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700072D RID: 1837
        // (get) Token: 0x06001312 RID: 4882 RVA: 0x00068F7C File Offset: 0x0006717C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700072E RID: 1838
        // (get) Token: 0x06001313 RID: 4883 RVA: 0x00068F80 File Offset: 0x00067180
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x1700072F RID: 1839
        // (get) Token: 0x06001314 RID: 4884 RVA: 0x00068F84 File Offset: 0x00067184
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001316 RID: 4886 RVA: 0x00068F90 File Offset: 0x00067190
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            var x = (ushort) ((0.5f + p.level.spawnx) * 32f);
            var y = (ushort) ((0.6f + p.level.spawny) * 32f);
            var z = (ushort) ((0.5f + p.level.spawnz) * 32f);
            p.SendPos(byte.MaxValue, x, y, z, p.level.rotx, p.level.roty);
        }

        // Token: 0x06001317 RID: 4887 RVA: 0x00069024 File Offset: 0x00067224
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spawn - Teleports yourself to the spawn location.");
        }
    }
}