using System.Threading;

namespace MCDzienny
{
    public class CmdFetch : Command
    {
        public override string name { get { return "fetch"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

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
                                Player.SendMessage(p, string.Format("{0} is in a different Level. Forcefetching has started!", pl.name));
                                Level level = p.level;
                                pl.ignorePermission = true;
                                all.Find("goto").Use(pl, level.name);
                                pl.ignorePermission = false;
                                Thread.Sleep(250);
                                while (pl.Loading)
                                {
                                    Thread.Sleep(250);
                                }
                            }
                            pl.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
                        }
                    });
                });
                return;
            }
            Player player = Player.Find(message);
            if (player == null || player.hidden)
            {
                Player.SendMessage(p, "Could not find player.");
                return;
            }
            if (p.level != player.level)
            {
                Player.SendMessage(p, string.Format("{0} is in a different Level. Forcefetching has started!", player.name));
                Level level2 = p.level;
                all.Find("goto").Use(player, level2.name);
                Thread.Sleep(1000);
                while (player.Loading)
                {
                    Thread.Sleep(250);
                }
            }
            player.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fetch [player] - fetches [player] forced!");
            Player.SendMessage(p, "Moves [player] to your Level first");
            Player.SendMessage(p, "/fetch all - fetches all players online");
        }
    }
}