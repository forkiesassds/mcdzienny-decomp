namespace MCDzienny
{
    public class CmdClearBlockChanges : Command
    {
        public override string name { get { return "clearblockchanges"; } }

        public override string shortcut { get { return "cbc"; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            Level level = Level.Find(message);
            if (level == null && message != "")
            {
                Player.SendMessage(p, "Could not find level.");
                return;
            }
            if (level == null)
            {
                level = p.level;
            }
            if (Server.useMySQL)
            {
                DBInterface.ExecuteQuery("TRUNCATE TABLE `Block" + level.name + "`");
            }
            else
            {
                DBInterface.ExecuteQuery("DELETE FROM `Block" + level.name + "`");
            }
            Player.SendMessage(p, string.Format("Cleared &cALL{0} recorded block changes in: &d{1}", Server.DefaultColor, level.name));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clearblockchanges <map> - Clears the block changes stored in /about for <map>.");
            Player.SendMessage(p, "&cUSE WITH CAUTION");
        }
    }
}