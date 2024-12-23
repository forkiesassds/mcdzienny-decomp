namespace MCDzienny
{
    public class CmdBotAdd : Command
    {
        public override string name { get { return "botadd"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override string CustomName { get { return Lang.Command.BotAddName; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
            }
            else if (!PlayerBot.ValidName(message))
            {
                Player.SendMessage(p, string.Format(Lang.Command.BotAddMessage, message));
            }
            else
            {
                PlayerBot.playerbots.Add(new PlayerBot(message, p.level, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotAddHelp);
        }
    }
}