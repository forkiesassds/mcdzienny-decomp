namespace MCDzienny.Commands
{
    // Token: 0x0200006F RID: 111
    public class CmdPoke : Command
    {
        // Token: 0x170000BB RID: 187
        // (get) Token: 0x060002D2 RID: 722 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
        public override string name
        {
            get { return "poke"; }
        }

        // Token: 0x170000BC RID: 188
        // (get) Token: 0x060002D3 RID: 723 RVA: 0x0000FFBC File Offset: 0x0000E1BC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170000BD RID: 189
        // (get) Token: 0x060002D4 RID: 724 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000BE RID: 190
        // (get) Token: 0x060002D5 RID: 725 RVA: 0x0000FFCC File Offset: 0x0000E1CC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000BF RID: 191
        // (get) Token: 0x060002D6 RID: 726 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170000C0 RID: 192
        // (get) Token: 0x060002D7 RID: 727 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060002D8 RID: 728 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var player = Player.Find(message);
            if (player == null || player.hidden)
            {
                Player.SendMessage(p, "Couldn't find the player.");
                return;
            }

            Player.SendMessage(player, "You were poked by {0}!", p.color + p.PublicName + Server.DefaultColor);
            Player.SendMessage(p, "You just poked {0}.", player.color + player.PublicName + Server.DefaultColor);
        }

        // Token: 0x060002D9 RID: 729 RVA: 0x00010070 File Offset: 0x0000E270
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/poke [player] - pokes the [player].");
        }
    }
}