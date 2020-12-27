namespace MCDzienny
{
    // Token: 0x020000B6 RID: 182
    public class CmdBestScores : Command
    {
        // Token: 0x170002A3 RID: 675
        // (get) Token: 0x0600062B RID: 1579 RVA: 0x00020DD4 File Offset: 0x0001EFD4
        public override string name
        {
            get { return "bestscores"; }
        }

        // Token: 0x170002A4 RID: 676
        // (get) Token: 0x0600062C RID: 1580 RVA: 0x00020DDC File Offset: 0x0001EFDC
        public override string shortcut
        {
            get { return "bs"; }
        }

        // Token: 0x170002A5 RID: 677
        // (get) Token: 0x0600062D RID: 1581 RVA: 0x00020DE4 File Offset: 0x0001EFE4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002A6 RID: 678
        // (get) Token: 0x0600062E RID: 1582 RVA: 0x00020DEC File Offset: 0x0001EFEC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002A7 RID: 679
        // (get) Token: 0x0600062F RID: 1583 RVA: 0x00020DF0 File Offset: 0x0001EFF0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170002A8 RID: 680
        // (get) Token: 0x06000630 RID: 1584 RVA: 0x00020DF4 File Offset: 0x0001EFF4
        public override string CustomName
        {
            get { return Lang.Command.BestScoresName; }
        }

        // Token: 0x170002A9 RID: 681
        // (get) Token: 0x06000631 RID: 1585 RVA: 0x00020DFC File Offset: 0x0001EFFC
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x06000632 RID: 1586 RVA: 0x00020E00 File Offset: 0x0001F000
        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, Lang.Command.BestScoresMessage);
            using (var dataTable = DBInterface.fillData("SELECT * FROM `Players` ORDER BY bestScore DESC LIMIT 10"))
            {
                for (var i = 0; i < dataTable.Rows.Count; i++)
                    Player.SendMessage(p,
                        string.Format(Lang.Command.BestScoresMessage1, i + 1, dataTable.Rows[i]["Name"],
                            dataTable.Rows[i]["bestScore"]));
            }
        }

        // Token: 0x06000633 RID: 1587 RVA: 0x00020EA4 File Offset: 0x0001F0A4
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BestScoresHelp);
        }
    }
}