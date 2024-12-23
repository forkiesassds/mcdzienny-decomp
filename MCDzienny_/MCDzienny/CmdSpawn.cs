namespace MCDzienny
{
    public class CmdSpawn : Command
    {
        public override string name { get { return "spawn"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            ushort x = (ushort)((0.5f + p.level.spawnx) * 32f);
            ushort y = (ushort)((0.6f + p.level.spawny) * 32f);
            ushort z = (ushort)((0.5f + p.level.spawnz) * 32f);
            p.SendPos(byte.MaxValue, x, y, z, p.level.rotx, p.level.roty);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spawn - Teleports yourself to the spawn location.");
        }
    }
}