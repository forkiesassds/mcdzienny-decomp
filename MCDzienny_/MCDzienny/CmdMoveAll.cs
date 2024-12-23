using System;
using System.Threading;

namespace MCDzienny
{
    class CmdMoveAll : Command
    {
        public override string name { get { return "moveall"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == string.Empty || message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }
            if (message.Split(' ').Length == 2)
            {
                string[] map = message.ToLower().Split(' ');
                Level level = Level.Find(map[0]);
                if (level == null)
                {
                    Player.SendMessage(p, "The first map couldn't be found.");
                    return;
                }
                Level level2 = Level.Find(map[1]);
                if (level2 == null)
                {
                    Player.SendMessage(p, "The second map couldn't be found.");
                    return;
                }
                Player.players.ForEachSync(delegate(Player pl)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        try
                        {
                            if (pl.level.name == map[0])
                            {
                                all.Find("goto").Use(pl, map[1]);
                            }
                        }
                        catch (Exception ex)
                        {
                            Server.ErrorLog(ex);
                        }
                    });
                });
                return;
            }
            Level level3 = Level.Find(message.ToLower());
            if (level3 == null)
            {
                Player.SendMessage(p, "The map couldn't be found.");
                return;
            }
            Player.players.ForEachSync(delegate(Player pl)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        all.Find("goto").Use(pl, message.ToLower());
                    }
                    catch (Exception ex2)
                    {
                        Server.ErrorLog(ex2);
                    }
                });
            });
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/moveall [map] - moves all players on the server to the pointed map.");
            Player.SendMessage(p, "/moveall [map1] [map2] - moves all players that are on the map1 to the map2.");
        }
    }
}