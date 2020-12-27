namespace MCDzienny
{
    // Token: 0x020000FE RID: 254
    internal class CmdPCount : Command
    {
        // Token: 0x17000377 RID: 887
        // (get) Token: 0x060007C7 RID: 1991 RVA: 0x000278CC File Offset: 0x00025ACC
        public override string name
        {
            get { return "pcount"; }
        }

        // Token: 0x17000378 RID: 888
        // (get) Token: 0x060007C8 RID: 1992 RVA: 0x000278D4 File Offset: 0x00025AD4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000379 RID: 889
        // (get) Token: 0x060007C9 RID: 1993 RVA: 0x000278DC File Offset: 0x00025ADC
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700037A RID: 890
        // (get) Token: 0x060007CA RID: 1994 RVA: 0x000278E4 File Offset: 0x00025AE4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700037B RID: 891
        // (get) Token: 0x060007CB RID: 1995 RVA: 0x000278E8 File Offset: 0x00025AE8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060007CD RID: 1997 RVA: 0x000278F4 File Offset: 0x00025AF4
        public override void Use(Player p, string message)
        {
            var count = Group.findPerm(LevelPermission.Banned).playerList.All().Count;
            using (var dataTable = DBInterface.fillData("SELECT COUNT(Name) FROM Players"))
            {
                Player.SendMessage(p,
                    string.Format("A total of {0} unique players have visited this server.",
                        dataTable.Rows[0]["COUNT(Name)"]));
                Player.SendMessage(p, string.Format("Of these players, {0} have been banned.", count));
            }

            var playerCount = 0;
            var hiddenCount = 0;
            if (p == null)
                Player.players.ForEach(delegate(Player pl)
                {
                    if (!pl.hidden)
                    {
                        playerCount++;
                        if (pl.hidden) hiddenCount++;
                    }
                });
            else
                Player.players.ForEach(delegate(Player pl)
                {
                    if (!pl.hidden || p.group.Permission > LevelPermission.AdvBuilder ||
                        Server.devs.Contains(p.name.ToLower()))
                    {
                        playerCount++;
                        if (pl.hidden && (p.group.Permission > LevelPermission.AdvBuilder ||
                                          Server.devs.Contains(p.name.ToLower()))) hiddenCount++;
                    }
                });
            if (playerCount == 1)
            {
                if (hiddenCount == 0)
                {
                    Player.SendMessage(p, "There is 1 player currently online.");
                    return;
                }

                Player.SendMessage(p, string.Format("There is 1 player currently online ({0} hidden).", hiddenCount));
            }
            else
            {
                if (hiddenCount == 0)
                {
                    Player.SendMessage(p, string.Format("There are {0} players online.", playerCount));
                    return;
                }

                Player.SendMessage(p,
                    string.Format("There are {0} players online ({1} hidden).", playerCount, hiddenCount));
            }
        }

        // Token: 0x060007CE RID: 1998 RVA: 0x00027A90 File Offset: 0x00025C90
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pcount - Displays the number of players online and total.");
        }
    }
}