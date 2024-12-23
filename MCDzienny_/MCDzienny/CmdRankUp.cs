using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdRankUp : Command
    {
        public override string name { get { return "rankup"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return ""; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message.ToLower());
            if (player == null)
            {
                Player.SendMessage(p, "Error during auto-promotion, player not found.");
                return;
            }
            var list = new List<Group>();
            list.AddRange(Group.groupList);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Permission != player.group.Permission)
                {
                    continue;
                }
                if (list.Count > i + 1)
                {
                    if (list[i + 1].Permission < LevelPermission.Operator)
                    {
                        all.Find("setrank").Use(null, player.name + " " + list[i + 1].name);
                    }
                    else
                    {
                        Player.SendMessage(p, "Higher rank you can obtain only from Server Masters.");
                    }
                }
                else
                {
                    Player.SendMessage(p, "You have the highest rank possible.");
                }
                break;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rankup [name] - ranks up the [player].");
        }
    }
}