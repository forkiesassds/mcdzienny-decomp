namespace MCDzienny
{
    // Token: 0x020002DF RID: 735
    public class CmdJail : Command
    {
        // Token: 0x17000817 RID: 2071
        // (get) Token: 0x060014D1 RID: 5329 RVA: 0x00072C18 File Offset: 0x00070E18
        public override string name
        {
            get { return "jail"; }
        }

        // Token: 0x17000818 RID: 2072
        // (get) Token: 0x060014D2 RID: 5330 RVA: 0x00072C20 File Offset: 0x00070E20
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000819 RID: 2073
        // (get) Token: 0x060014D3 RID: 5331 RVA: 0x00072C28 File Offset: 0x00070E28
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700081A RID: 2074
        // (get) Token: 0x060014D4 RID: 5332 RVA: 0x00072C30 File Offset: 0x00070E30
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700081B RID: 2075
        // (get) Token: 0x060014D5 RID: 5333 RVA: 0x00072C34 File Offset: 0x00070E34
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060014D6 RID: 5334 RVA: 0x00072C38 File Offset: 0x00070E38
        public override void Use(Player p, string message)
        {
            if ((message.ToLower() == "create" || message.ToLower() == "") && p != null)
            {
                p.level.jailx = p.pos[0];
                p.level.jaily = p.pos[1];
                p.level.jailz = p.pos[2];
                p.level.jailrotx = p.rot[0];
                p.level.jailroty = p.rot[1];
                Player.SendMessage(p, "Set Jail point.");
                return;
            }

            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find specified player.");
                return;
            }

            if (!player.jailed)
            {
                if (p != null)
                {
                    if (player.group.Permission >= p.group.Permission)
                    {
                        Player.SendMessage(p, "Cannot jail someone of equal or greater rank.");
                        return;
                    }

                    if (player.level != p.level) all.Find("goto").Use(player, p.level.name);
                    Player.GlobalDie(player, false);
                    Player.GlobalSpawn(player, p.level.jailx, p.level.jaily, p.level.jailz, p.level.jailrotx,
                        p.level.jailroty, true);
                }
                else
                {
                    Player.GlobalDie(player, false);
                    Player.GlobalSpawn(player, player.level.jailx, player.level.jaily, player.level.jailz,
                        player.level.jailrotx, player.level.jailroty, true);
                }

                player.jailed = true;
                Player.GlobalChat(null,
                    string.Format("{0} was &8jailed", player.color + player.PublicName + Server.DefaultColor), false);
                return;
            }

            player.jailed = false;
            Player.GlobalChat(null,
                string.Format("{0} was &afreed{1} from jail", player.color + player.PublicName + Server.DefaultColor,
                    Server.DefaultColor), false);
        }

        // Token: 0x060014D7 RID: 5335 RVA: 0x00072E48 File Offset: 0x00071048
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/jail [user] - Places [user] in jail unable to use commands.");
            Player.SendMessage(p, "/jail [create] - Creates the jail point for the map.");
        }
    }
}