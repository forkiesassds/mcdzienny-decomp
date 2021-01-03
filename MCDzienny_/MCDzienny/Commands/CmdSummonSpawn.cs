namespace MCDzienny
{
    // Token: 0x020000E2 RID: 226
    public class CmdSummonSpawn : Command
    {
        // Token: 0x17000347 RID: 839
        // (get) Token: 0x06000750 RID: 1872 RVA: 0x00025014 File Offset: 0x00023214
        public override string name
        {
            get { return "summonspawn"; }
        }

        // Token: 0x17000348 RID: 840
        // (get) Token: 0x06000751 RID: 1873 RVA: 0x0002501C File Offset: 0x0002321C
        public override string shortcut
        {
            get { return "ss"; }
        }

        // Token: 0x17000349 RID: 841
        // (get) Token: 0x06000752 RID: 1874 RVA: 0x00025024 File Offset: 0x00023224
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700034A RID: 842
        // (get) Token: 0x06000753 RID: 1875 RVA: 0x0002502C File Offset: 0x0002322C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700034B RID: 843
        // (get) Token: 0x06000754 RID: 1876 RVA: 0x00025030 File Offset: 0x00023230
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06000756 RID: 1878 RVA: 0x0002503C File Offset: 0x0002323C
        public override void Use(Player p, string message)
        {
            Command sum = new CmdSummon();
            Player.players.ForEach(delegate(Player pl)
            {
                if (pl.level == p.level && !LavaSystem.CheckSpawn(pl)) sum.Use(p, pl.name);
            });
        }

        // Token: 0x06000757 RID: 1879 RVA: 0x00025078 File Offset: 0x00023278
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/summonspawn - Summons all the players that are close to spawn.");
        }
    }
}