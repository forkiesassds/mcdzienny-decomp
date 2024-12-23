namespace MCDzienny
{
    public class CmdXundo : Command
    {
        public override string name { get { return "xundo"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            Player player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find the player specified.");
            }
            else if (p != null && player.group.Permission >= p.group.Permission)
            {
                Player.SendMessage(p, "You don't have the permission to undo this player actions.");
            }
            else
            {
                all.Find("undo").Use(null, message + " all");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/xundo [player] - undoes all player actions.");
        }
    }
}