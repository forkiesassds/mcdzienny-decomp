namespace MCDzienny
{
    public class CmdDelete : Command
    {
        public override string name { get { return "delete"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Freebuild | CommandScope.Lava | CommandScope.Home; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            p.deleteMode = !p.deleteMode;
            Player.SendMessage(p, string.Format("Delete mode: &a{0}", p.deleteMode));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/delete - Deletes any block you click");
            Player.SendMessage(p, "\"any block\" meaning door_air, portals, mb's, etc");
        }
    }
}