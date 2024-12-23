namespace MCDzienny
{
    public class CmdSummonSpawn : Command
    {
        public override string name { get { return "summonspawn"; } }

        public override string shortcut { get { return "ss"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            Command sum = new CmdSummon();
            Player.players.ForEach(delegate(Player pl)
            {
                if (pl.level == p.level && !LavaSystem.CheckSpawn(pl))
                {
                    sum.Use(p, pl.name);
                }
            });
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/summonspawn - Summons all the players that are close to spawn.");
        }
    }
}