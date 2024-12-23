namespace MCDzienny
{
    public class CmdFreeid : Command
    {
        public override string name { get { return "freeid"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return ""; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            int num = 0;
            for (byte b = 0; b < byte.MaxValue; b++)
            {
                bool flag = false;
                foreach (Player player in Player.players)
                {
                    if (player.id == b)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    num++;
                }
            }
            Player.SendMessage(p, "Total free id's: " + num);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/score - Shows your best score and total experience.");
            Player.SendMessage(p, "/score all - Shows server score stats.");
        }
    }
}