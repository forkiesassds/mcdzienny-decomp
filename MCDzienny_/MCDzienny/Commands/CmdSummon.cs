namespace MCDzienny
{
    // Token: 0x020002A0 RID: 672
    public class CmdSummon : Command
    {
        // Token: 0x17000743 RID: 1859
        // (get) Token: 0x0600133E RID: 4926 RVA: 0x0006A020 File Offset: 0x00068220
        public override string name
        {
            get { return "summon"; }
        }

        // Token: 0x17000744 RID: 1860
        // (get) Token: 0x0600133F RID: 4927 RVA: 0x0006A028 File Offset: 0x00068228
        public override string shortcut
        {
            get { return "s"; }
        }

        // Token: 0x17000745 RID: 1861
        // (get) Token: 0x06001340 RID: 4928 RVA: 0x0006A030 File Offset: 0x00068230
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000746 RID: 1862
        // (get) Token: 0x06001341 RID: 4929 RVA: 0x0006A038 File Offset: 0x00068238
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000747 RID: 1863
        // (get) Token: 0x06001342 RID: 4930 RVA: 0x0006A03C File Offset: 0x0006823C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x17000748 RID: 1864
        // (get) Token: 0x06001343 RID: 4931 RVA: 0x0006A040 File Offset: 0x00068240
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001345 RID: 4933 RVA: 0x0006A04C File Offset: 0x0006824C
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (message.ToLower() == "all")
            {
                foreach (var player in Player.players)
                    if (player.level == p.level && player != p && player.@group.Permission <= p.@group.Permission)
                    {
                        player.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
                        player.SendMessage(string.Format("You were summoned by {0}.",
                            p.color + p.PublicName + Server.DefaultColor));
                    }

                return;
            }

            var player2 = Player.Find(message);
            if (player2 == null || player2.hidden)
            {
                Player.SendMessage(p, string.Format("There is no player \"{0}\"!", message));
                return;
            }

            if (player2.group.Permission > p.group.Permission && p != null)
            {
                Player.SendMessage(p, "You can't summon a player with a higher rank than yours.");
                return;
            }

            if (p.level != player2.level)
            {
                Player.SendMessage(p, string.Format("{0} is in a different level.", player2.name));
                return;
            }

            player2.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
            player2.SendMessage(
                string.Format("You were summoned by {0}.", p.color + p.PublicName + Server.DefaultColor));
        }

        // Token: 0x06001346 RID: 4934 RVA: 0x0006A214 File Offset: 0x00068414
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/summon <player> - Summons a player to your position.");
            Player.SendMessage(p, "/summon all - Summons all players in the map");
        }
    }
}