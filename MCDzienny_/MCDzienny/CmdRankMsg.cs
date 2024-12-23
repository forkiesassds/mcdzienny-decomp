namespace MCDzienny
{
    public class CmdRankMsg : Command
    {
        public override string name { get { return "rankmsg"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return ""; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2)
            {
                Help(p);
                return;
            }
            Group grp = Group.Find(message.Split(' ')[0].ToLower());
            if (grp == null)
            {
                Player.SendMessage(p, "The group wasn't found.");
                return;
            }
            string msg = message.Substring(message.IndexOf(' ') + 1);
            string playerName = p == null ? Server.ConsoleRealName : p.PublicName;
            int count = 0;
            Player.players.ForEach(delegate(Player pl)
            {
                if (pl.group == grp)
                {
                    Player.SendMessage(pl, grp.color + "(" + playerName + " to " + grp.name + ") " + Server.DefaultColor + msg);
                    count++;
                }
            });
            if (count == 0)
            {
                Player.SendMessage(p, string.Format("There is no player online with a rank of {0}", message.Split(' ')[0]));
            }
            else if (count == 1)
            {
                Player.SendMessage(p, string.Format("The message was sent to {0} player with a rank of {1}", count, message.Split(' ')[0]));
                Server.s.Log("(" + playerName + " to " + grp.name + ") " + msg);
            }
            else
            {
                Player.SendMessage(p, string.Format("The message was sent to {0} players with a rank of {1}", count, message.Split(' ')[0]));
                Server.s.Log("(" + playerName + " to " + grp.name + ") " + msg);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rankmsg [rank] [message] - sends to all players that have a certain [rank] a [message].");
        }
    }
}