namespace MCDzienny
{
    class CmdPause : Command
    {
        public override string name { get { return "pause"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            Server.pause = !Server.pause;
            if (Server.pause)
            {
                Player.SendMessage(p, "Physics was stopped.");
            }
            else
            {
                Player.SendMessage(p, "Physics is on.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pause - pauses physics");
        }
    }
}