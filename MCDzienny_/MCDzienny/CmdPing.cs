namespace MCDzienny
{
    public class CmdPing : Command
    {
        public override string name { get { return "ping"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "info"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Player.players.ForEach(delegate(Player who)
                {
                    if (who.connectionSpeed == -2)
                    {
                        Player.SendMessage(p, who.name + " ping: greenlist");
                    }
                    else if (who.connectionSpeed == -1)
                    {
                        Player.SendMessage(p, who.name + " ping: is being tested");
                    }
                    else
                    {
                        Player.SendMessage(p, who.name + " ping: " + who.connectionSpeed);
                    }
                });
            }
            else
            {
                Player player = Player.Find(message);
                if (player == null)
                {
                    Help(p);
                }
                else
                {
                    player.TestConnection();
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ping - Connection speed.");
        }
    }
}