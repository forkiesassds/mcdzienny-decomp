namespace MCDzienny
{
    public class CmdBotSummon : Command
    {
        public override string name { get { return "botsummon"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override bool ConsoleAccess { get { return false; } }

        public override string CustomName { get { return Lang.Command.BotSummonName; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            PlayerBot playerBot = PlayerBot.Find(message);
            if (playerBot == null)
            {
                Player.SendMessage(p, string.Format(Lang.Command.BotSummonMessage, message));
            }
            else if (p.level != playerBot.level)
            {
                Player.SendMessage(p, string.Format(Lang.Command.BotSummonMessage1, playerBot.name));
            }
            else
            {
                playerBot.SetPos(p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotSummonHelp);
        }
    }
}