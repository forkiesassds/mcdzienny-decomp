namespace MCDzienny
{
    public class CmdReset : Command
    {
        public override string name { get { return "reset"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            message = message.ToLower();
            switch (message)
            {
                case "xp":
                    Player.SendMessage(p, "This command will reset experience for all players.");
                    Player.SendMessage(p, "If you are sure you want to do this write '/reset xp yes.'");
                    break;
                case "xp yes":
                    DBInterface.ExecuteQuery("UPDATE `players` SET `totalScore` = 0");
                    DBInterface.ExecuteQuery("UPDATE `players` SET `bestScore` = 0");
                    Player.SendMessage(p, "Players experience has been reset.");
                    break;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/reset xp - resets experience for all players.");
        }
    }
}