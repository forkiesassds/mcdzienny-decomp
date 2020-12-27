namespace MCDzienny
{
    // Token: 0x020002BD RID: 701
    public class CmdInvincible : Command
    {
        // Token: 0x1700079B RID: 1947
        // (get) Token: 0x060013F0 RID: 5104 RVA: 0x0006E63C File Offset: 0x0006C83C
        public override string name
        {
            get { return "invincible"; }
        }

        // Token: 0x1700079C RID: 1948
        // (get) Token: 0x060013F1 RID: 5105 RVA: 0x0006E644 File Offset: 0x0006C844
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700079D RID: 1949
        // (get) Token: 0x060013F2 RID: 5106 RVA: 0x0006E64C File Offset: 0x0006C84C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700079E RID: 1950
        // (get) Token: 0x060013F3 RID: 5107 RVA: 0x0006E654 File Offset: 0x0006C854
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700079F RID: 1951
        // (get) Token: 0x060013F4 RID: 5108 RVA: 0x0006E658 File Offset: 0x0006C858
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060013F6 RID: 5110 RVA: 0x0006E664 File Offset: 0x0006C864
        public override void Use(Player p, string message)
        {
            Player player;
            if (message != "")
                player = Player.Find(message);
            else
                player = p;
            if (player == null)
            {
                Player.SendMessage(p, "Cannot find player.");
                return;
            }

            if (player.group.Permission > p.group.Permission)
            {
                Player.SendMessage(p, "Cannot toggle invincibility for someone of higher rank");
                return;
            }

            if (player.invincible)
            {
                player.invincible = false;
                if (Server.cheapMessage)
                    Player.GlobalChat(p,
                        string.Format("{0} has stopped being immortal",
                            player.color + player.PublicName + Server.DefaultColor), false);
            }
            else
            {
                player.invincible = true;
                if (Server.cheapMessage)
                    Player.GlobalChat(p,
                        string.Concat(player.color, player.PublicName, Server.DefaultColor, " ",
                            Server.cheapMessageGiven), false);
            }
        }

        // Token: 0x060013F7 RID: 5111 RVA: 0x0006E748 File Offset: 0x0006C948
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/invincible [name] - Turns invincible mode on/off.");
            Player.SendMessage(p, "If [name] is given, that player's invincibility is toggled");
        }
    }
}