namespace MCDzienny
{
    public class CmdKickAll : Command
    {
        public override string name { get { return "kickall"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kickall - kicks all players from the server.");
        }

        public override void Use(Player p, string message)
        {
            int count = Player.players.Count;
            if (p != null)
            {
                for (int num = count - 1; num >= 0; num--)
                {
                    try
                    {
                        Player.players[num].disconnectionReason = DisconnectionReason.Kicked;
                        Player.players[num].Kick(string.Format("You were kicked by {0}", p.PublicName));
                    }
                    catch {}
                }
                return;
            }
            for (int num2 = count - 1; num2 >= 0; num2--)
            {
                try
                {
                    Player.players[num2].disconnectionReason = DisconnectionReason.Kicked;
                    Player.players[num2].Kick("You were kicked.");
                }
                catch {}
            }
        }
    }
}