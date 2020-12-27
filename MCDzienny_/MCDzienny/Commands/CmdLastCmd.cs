namespace MCDzienny
{
    // Token: 0x020002F3 RID: 755
    public class CmdLastCmd : Command
    {
        // Token: 0x1700086D RID: 2157
        // (get) Token: 0x06001560 RID: 5472 RVA: 0x00075BF8 File Offset: 0x00073DF8
        public override string name
        {
            get { return "lastcmd"; }
        }

        // Token: 0x1700086E RID: 2158
        // (get) Token: 0x06001561 RID: 5473 RVA: 0x00075C00 File Offset: 0x00073E00
        public override string shortcut
        {
            get { return "last"; }
        }

        // Token: 0x1700086F RID: 2159
        // (get) Token: 0x06001562 RID: 5474 RVA: 0x00075C08 File Offset: 0x00073E08
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x17000870 RID: 2160
        // (get) Token: 0x06001563 RID: 5475 RVA: 0x00075C10 File Offset: 0x00073E10
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000871 RID: 2161
        // (get) Token: 0x06001564 RID: 5476 RVA: 0x00075C14 File Offset: 0x00073E14
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000872 RID: 2162
        // (get) Token: 0x06001565 RID: 5477 RVA: 0x00075C18 File Offset: 0x00073E18
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001567 RID: 5479 RVA: 0x00075C24 File Offset: 0x00073E24
        public override void Use(Player p, string message)
        {
            if (message == "")
                using (var enumerator = Player.players.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var player = enumerator.Current;
                        Player.SendMessage(p,
                            string.Format("{0} last used \"{1}\"", player.color + player.name + Server.DefaultColor,
                                player.lastCMD));
                    }

                    return;
                }

            var player2 = Player.Find(message);
            if (player2 == null)
            {
                Player.SendMessage(p, "Could not find player entered");
                return;
            }

            Player.SendMessage(p,
                string.Format("{0} last used \"{1}\"", player2.color + player2.name + Server.DefaultColor,
                    player2.lastCMD));
        }

        // Token: 0x06001568 RID: 5480 RVA: 0x00075CEC File Offset: 0x00073EEC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/last [user] - Shows last command used by [user]");
            Player.SendMessage(p, "/last by itself will show all last commands (SPAMMY)");
        }
    }
}