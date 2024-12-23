namespace MCDzienny
{
    public class CmdStore : Command
    {
        public override string name { get { return "store"; } }

        public override string shortcut { get { return "shop"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Lava | CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Zombie)
            {
                StoreSystem.Store.ZombieStore.DisplayItems(p, 0);
                return;
            }
            Player.SendMessage(p, "You can buy:");
            if (message.ToLower() == "more")
            {
                Store.PrintListMore(p);
            }
            else
            {
                Store.PrintList(p);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/store - shows you the list of the stuff available.");
        }
    }
}