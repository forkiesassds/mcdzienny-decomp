using System.Threading;

namespace MCDzienny
{
    // Token: 0x02000123 RID: 291
    public class CmdFetch : Command
    {
        // Token: 0x17000400 RID: 1024
        // (get) Token: 0x060008BB RID: 2235 RVA: 0x0002C0DC File Offset: 0x0002A2DC
        public override string name
        {
            get { return "fetch"; }
        }

        // Token: 0x17000401 RID: 1025
        // (get) Token: 0x060008BC RID: 2236 RVA: 0x0002C0E4 File Offset: 0x0002A2E4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000402 RID: 1026
        // (get) Token: 0x060008BD RID: 2237 RVA: 0x0002C0EC File Offset: 0x0002A2EC
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000403 RID: 1027
        // (get) Token: 0x060008BE RID: 2238 RVA: 0x0002C0F4 File Offset: 0x0002A2F4
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000404 RID: 1028
        // (get) Token: 0x060008BF RID: 2239 RVA: 0x0002C0F8 File Offset: 0x0002A2F8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000405 RID: 1029
        // (get) Token: 0x060008C0 RID: 2240 RVA: 0x0002C0FC File Offset: 0x0002A2FC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060008C1 RID: 2241 RVA: 0x0002C100 File Offset: 0x0002A300
        public override void Use(Player p, string message)
        {
            if (p == null)
            {
                Player.SendMessage(p, "Console cannot use this command. Try using /move instead.");
                return;
            }

            if (message.ToLower() == "all")
            {
                Player.players.ForEachSync(delegate(Player pl)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        if (pl != p)
                        {
                            if (p.level != pl.level)
                            {
                                Player.SendMessage(p,
                                    string.Format("{0} is in a different Level. Forcefetching has started!", pl.name));
                                var level2 = p.level;
                                pl.ignorePermission = true;
                                all.Find("goto").Use(pl, level2.name);
                                pl.ignorePermission = false;
                                Thread.Sleep(250);
                                while (pl.Loading) Thread.Sleep(250);
                            }

                            pl.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
                        }
                    });
                });
                return;
            }

            var player = Player.Find(message);
            if (player == null || player.hidden)
            {
                Player.SendMessage(p, "Could not find player.");
                return;
            }

            if (p.level != player.level)
            {
                Player.SendMessage(p,
                    string.Format("{0} is in a different Level. Forcefetching has started!", player.name));
                var level = p.level;
                all.Find("goto").Use(player, level.name);
                Thread.Sleep(1000);
                while (player.Loading) Thread.Sleep(250);
            }

            player.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
        }

        // Token: 0x060008C2 RID: 2242 RVA: 0x0002C240 File Offset: 0x0002A440
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fetch [player] - fetches [player] forced!");
            Player.SendMessage(p, "Moves [player] to your Level first");
            Player.SendMessage(p, "/fetch all - fetches all players online");
        }
    }
}