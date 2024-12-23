namespace MCDzienny
{
    public class CmdSetspawn : Command
    {
        public override string name { get { return "setspawn"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            Player.SendMessage(p, "Spawn location changed.");
            p.level.spawnx = (ushort)(p.pos[0] / 32);
            p.level.spawny = (ushort)(p.pos[1] / 32);
            p.level.spawnz = (ushort)(p.pos[2] / 32);
            p.level.rotx = p.rot[0];
            p.level.roty = 0;
            p.level.changed = true;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setspawn - Set the default spawn location.");
        }
    }
}