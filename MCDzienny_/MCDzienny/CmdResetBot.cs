namespace MCDzienny
{
    class CmdResetBot : Command
    {
        public override string name { get { return "resetbot"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            IRCBot.Reset();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/resetbot - reloads the IRCBot. FOR EMERGENCIES ONLY!");
        }
    }
}