using System.Data;

namespace MCDzienny
{
    class CmdPCount : Command
    {
        public override string name { get { return "pcount"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            int count = Group.findPerm(LevelPermission.Banned).playerList.All().Count;
            using (DataTable dataTable = DBInterface.fillData("SELECT COUNT(Name) FROM Players"))
            {
                Player.SendMessage(p, string.Format("A total of {0} unique players have visited this server.", dataTable.Rows[0]["COUNT(Name)"]));
                Player.SendMessage(p, string.Format("Of these players, {0} have been banned.", count));
            }
            int playerCount = 0;
            int hiddenCount = 0;
            if (p == null)
            {
                Player.players.ForEach(delegate(Player pl)
                {
                    if (!pl.hidden)
                    {
                        playerCount++;
                        if (pl.hidden)
                        {
                            hiddenCount++;
                        }
                    }
                });
            }
            else
            {
                Player.players.ForEach(delegate(Player pl)
                {
                    if (!pl.hidden || p.group.Permission > LevelPermission.AdvBuilder || Server.devs.Contains(p.name.ToLower()))
                    {
                        playerCount++;
                        if (pl.hidden && (p.group.Permission > LevelPermission.AdvBuilder || Server.devs.Contains(p.name.ToLower())))
                        {
                            hiddenCount++;
                        }
                    }
                });
            }
            if (playerCount == 1)
            {
                if (hiddenCount == 0)
                {
                    Player.SendMessage(p, "There is 1 player currently online.");
                }
                else
                {
                    Player.SendMessage(p, string.Format("There is 1 player currently online ({0} hidden).", hiddenCount));
                }
            }
            else if (hiddenCount == 0)
            {
                Player.SendMessage(p, string.Format("There are {0} players online.", playerCount));
            }
            else
            {
                Player.SendMessage(p, string.Format("There are {0} players online ({1} hidden).", playerCount, hiddenCount));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pcount - Displays the number of players online and total.");
        }
    }
}