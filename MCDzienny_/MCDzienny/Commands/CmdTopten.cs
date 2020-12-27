namespace MCDzienny
{
    // Token: 0x02000107 RID: 263
    public class CmdTopten : Command
    {
        // Token: 0x170003A3 RID: 931
        // (get) Token: 0x0600080E RID: 2062 RVA: 0x00028888 File Offset: 0x00026A88
        public override string name
        {
            get { return "topten"; }
        }

        // Token: 0x170003A4 RID: 932
        // (get) Token: 0x0600080F RID: 2063 RVA: 0x00028890 File Offset: 0x00026A90
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003A5 RID: 933
        // (get) Token: 0x06000810 RID: 2064 RVA: 0x00028898 File Offset: 0x00026A98
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003A6 RID: 934
        // (get) Token: 0x06000811 RID: 2065 RVA: 0x000288A0 File Offset: 0x00026AA0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003A7 RID: 935
        // (get) Token: 0x06000812 RID: 2066 RVA: 0x000288A4 File Offset: 0x00026AA4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170003A8 RID: 936
        // (get) Token: 0x06000813 RID: 2067 RVA: 0x000288A8 File Offset: 0x00026AA8
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x06000814 RID: 2068 RVA: 0x000288AC File Offset: 0x00026AAC
        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "The elite of the server:");
            using (var dataTable = DBInterface.fillData("SELECT * FROM `Players` ORDER BY totalScore DESC LIMIT 10"))
            {
                for (var i = 0; i < dataTable.Rows.Count; i++)
                    Player.SendMessage(p,
                        string.Format("%c{0}. {1} - level: {2}", i + 1, dataTable.Rows[i]["Name"],
                            TierSystem.TierCheck(int.Parse(dataTable.Rows[i]["totalScore"].ToString()))));
            }
        }

        // Token: 0x06000815 RID: 2069 RVA: 0x00028960 File Offset: 0x00026B60
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/topten - Displays names of ten best players.");
        }
    }
}