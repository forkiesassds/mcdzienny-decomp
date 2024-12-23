namespace MCDzienny
{
    public class CmdBots : Command
    {
        public override string name { get { return "bots"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override string CustomName { get { return Lang.Command.BotsName; } }

        public override void Use(Player p, string message)
        {
            message = "";
            foreach (PlayerBot playerbot in PlayerBot.playerbots)
            {
                if (playerbot.AIName != "")
                {
                    string text = message;
                    message = text + ", " + playerbot.name + "(" + playerbot.level.name + ")[" + playerbot.AIName + "]";
                }
                else if (playerbot.hunt)
                {
                    string text2 = message;
                    message = text2 + ", " + playerbot.name + "(" + playerbot.level.name + ")[Hunt]";
                }
                else
                {
                    string text3 = message;
                    message = text3 + ", " + playerbot.name + "(" + playerbot.level.name + ")";
                }
                if (playerbot.kill)
                {
                    message += "-kill";
                }
            }
            if (message != "")
            {
                Player.SendMessage(p, string.Format(Lang.Command.BotsMessage, Server.DefaultColor + message.Remove(0, 2)));
            }
            else
            {
                Player.SendMessage(p, Lang.Command.BotsMessage1);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotsHelp);
        }
    }
}