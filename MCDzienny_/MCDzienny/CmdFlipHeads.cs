namespace MCDzienny
{
    public class CmdFlipHeads : Command
    {
        public override string name { get { return "flipheads"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            Server.flipHead = !Server.flipHead;
            if (Server.flipHead)
            {
                foreach (Player player in Player.players)
                {
                    player.flipHead = true;
                }
                Player.GlobalChat(p, "All necks were broken", showname: false);
                return;
            }
            foreach (Player player2 in Player.players)
            {
                player2.flipHead = false;
            }
            Player.GlobalChat(p, "All necks were mended", showname: false);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/flipheads - Does as it says on the tin");
        }
    }
}