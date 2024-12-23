namespace MCDzienny
{
    public class CmdKill : Command
    {
        public override string name { get { return "kill"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            int num = 0;
            Player player;
            string customMessage;
            if (message.IndexOf(' ') == -1)
            {
                player = Player.Find(message);
                customMessage = p == null ? " %cwas ambushed and killed." : string.Format(" was killed by {0}", p.color + p.PublicName);
            }
            else
            {
                player = Player.Find(message.Split(' ')[0]);
                message = message.Substring(message.IndexOf(' ') + 1);
                if (message.IndexOf(' ') == -1)
                {
                    if (message.ToLower() == "explode")
                    {
                        customMessage = p != null ? " was exploded by " + p.color + p.PublicName : "was exploded.";
                        num = 1;
                    }
                    else
                    {
                        customMessage = " " + message;
                    }
                }
                else
                {
                    if (message.Split(' ')[0].ToLower() == "explode")
                    {
                        num = 1;
                        message = message.Substring(message.IndexOf(' ') + 1);
                    }
                    customMessage = " " + message;
                }
            }
            if (player == null)
            {
                if (p != null)
                {
                    p.HandleDeath(1, " killed itself in its confusion");
                }
                Player.SendMessage(p, "Could not find player");
                return;
            }
            if (p != null && player.group.Permission > p.group.Permission)
            {
                p.HandleDeath(1, string.Format(" was killed by {0}", player.color + player.PublicName));
                Player.SendMessage(p, "Cannot kill someone of higher rank");
                return;
            }
            player.invincible = false;
            if (num == 1)
            {
                player.HandleDeath(1, customMessage, explode: true);
            }
            else
            {
                player.HandleDeath(1, customMessage);
            }
            player.SubtractLife();
            player.invincible = false;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kill <name> [explode] <message>");
            Player.SendMessage(p, "Kills <name> with <message>. Causes explosion if [explode] is written");
        }
    }
}