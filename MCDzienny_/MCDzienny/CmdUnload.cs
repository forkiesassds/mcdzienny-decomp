using System;

namespace MCDzienny
{
    public class CmdUnload : Command
    {
        public override string name { get { return "unload"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message.ToLower() == "empty")
            {
                bool Empty = true;
                foreach (Level l in Server.levels)
                {
                    Empty = true;
                    PlayerCollection players = Player.players;
                    Action<Player> action = delegate(Player pl)
                    {
                        if (pl.level == l)
                        {
                            Empty = false;
                        }
                    };
                    players.ForEach(action);
                    if (Empty && l.unload)
                    {
                        l.Unload();
                        return;
                    }
                }
                Player.SendMessage(p, "No levels were empty.");
                return;
            }
            Level level = Level.Find(message);
            if (level != null)
            {
                if (!level.Unload())
                {
                    Player.SendMessage(p, "You cannot unload the main level.");
                }
            }
            else
            {
                Player.SendMessage(p, string.Format("There is no level \"{0}\" loaded.", message));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unload [level] - Unloads a level.");
            Player.SendMessage(p, "/unload empty - Unloads an empty level.");
        }
    }
}