namespace MCDzienny
{
    public class CmdDemote : Command
    {
        public override string name { get { return "demote"; } }

        public override string shortcut { get { return "de"; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message);
            string text;
            Group group;
            if (player == null)
            {
                text = message;
                group = Group.findPlayerGroup(message);
            }
            else
            {
                text = player.name;
                group = player.group;
            }
            Group group2 = null;
            bool flag = false;
            for (int num = Group.groupList.Count - 1; num >= 0; num--)
            {
                Group group3 = Group.groupList[num];
                if (flag)
                {
                    if (group3.Permission > LevelPermission.Banned)
                    {
                        group2 = group3;
                    }
                    break;
                }
                if (group3 == group)
                {
                    flag = true;
                }
            }
            if (group2 != null)
            {
                all.Find("setrank").Use(p, text + " " + group2.name);
            }
            else
            {
                Player.SendMessage(p, "No higher ranks exist");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/demote <name> - Demotes <name> down a rank");
        }
    }
}