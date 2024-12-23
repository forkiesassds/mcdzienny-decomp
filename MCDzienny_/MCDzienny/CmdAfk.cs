using MCDzienny.Settings;

namespace MCDzienny
{
    class CmdAfk : Command
    {
        public override string name { get { return "afk"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override string CustomName { get { return Lang.Command.AfkName; } }

        public override void Use(Player p, string message)
        {
            if (message != "list")
            {
                if (p.joker)
                {
                    message = "";
                }
                if (Server.voteMode && (p.level.mapType == MapType.Lava && !LavaSettings.All.IsAfkDuringVoteAllowed ||
                        p.level.mapType == MapType.Zombie && !InfectionSettings.All.IsAfkDuringVoteAllowed))
                {
                    Player.SendMessage(p, "You can't use this command during a map vote.");
                }
                else if (!Server.afkset.Contains(p.name))
                {
                    Server.afkset.Add(p.name);
                    if (p.muted || p.IsTempMuted)
                    {
                        Player.SendMessage(p, "You are afk now.");
                        return;
                    }
                    bool stopIt = false;
                    Player.OnPlayerChatEvent(p, ref message, ref stopIt);
                    if (!stopIt)
                    {
                        Player.GlobalMessage(string.Format(Lang.Command.AfkMessage, p.color + p.PublicName + Server.DefaultColor, message));
                        Player.IRCSay(string.Format(Lang.Command.AfkMessage1, p.PublicName, message));
                    }
                }
                else
                {
                    Server.afkset.Remove(p.name);
                    if (!Server.voteMode)
                    {
                        Player.GlobalMessage(string.Format(Lang.Command.AfkMessage2, p.color + p.PublicName + Server.DefaultColor));
                    }
                    Player.IRCSay(string.Format(Lang.Command.AfkMessage3, p.PublicName));
                }
                return;
            }
            foreach (string item in Server.afkset)
            {
                Player.SendMessage(p, item);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.AfkHelp);
        }
    }
}