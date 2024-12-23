using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdAwards : Command
    {
        public override string name { get { return "awards"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }
            int num = 0;
            string text = "";
            if (message != "")
            {
                if (message.Split(' ').Length == 2)
                {
                    text = message.Split(' ')[0];
                    Player player = Player.Find(text);
                    if (player != null)
                    {
                        text = player.name;
                    }
                    try
                    {
                        num = int.Parse(message.Split(' ')[1]);
                    }
                    catch
                    {
                        Help(p);
                        return;
                    }
                }
                else if (message.Length <= 3)
                {
                    try
                    {
                        num = int.Parse(message);
                    }
                    catch
                    {
                        text = message;
                        Player player2 = Player.Find(text);
                        if (player2 != null)
                        {
                            text = player2.name;
                        }
                    }
                }
                else
                {
                    text = message;
                    Player player3 = Player.Find(text);
                    if (player3 != null)
                    {
                        text = player3.name;
                    }
                }
            }
            if (num < 0)
            {
                Player.SendMessage(p, "Cannot display pages less than 0");
                return;
            }
            var list = new List<Awards.awardData>();
            if (text == "")
            {
                list = Awards.allAwards;
            }
            else
            {
                foreach (string playersAward in Awards.getPlayersAwards(text))
                {
                    Awards.awardData awardData = new Awards.awardData();
                    awardData.awardName = playersAward;
                    awardData.description = Awards.getDescription(playersAward);
                    list.Add(awardData);
                }
            }
            if (list.Count == 0)
            {
                if (text != "")
                {
                    Player.SendMessage(p, "The player has no awards!");
                }
                else
                {
                    Player.SendMessage(p, "There are no awards in this server yet");
                }
                return;
            }
            int num2 = num * 5;
            int num3 = (num - 1) * 5;
            if (num3 > list.Count)
            {
                Player.SendMessage(p, "There aren't that many awards. Enter a smaller number");
                return;
            }
            if (num2 > list.Count)
            {
                num2 = list.Count;
            }
            if (text != "")
            {
                Player.SendMessage(p, string.Format("{0} has the following awards:", Server.FindColor(text) + text + Server.DefaultColor));
            }
            else
            {
                Player.SendMessage(p, "Awards available: ");
            }
            if (num == 0)
            {
                foreach (Awards.awardData item in list)
                {
                    Player.SendMessage(p, string.Format("&6{0}:%7 {1}", item.awardName, item.description));
                }
                if (list.Count > 8)
                {
                    Player.SendMessage(p, string.Format("&5Use &b/awards {0} 1/2/3/... &5for a more ordered list", message));
                }
            }
            else
            {
                for (int i = num3; i < num2; i++)
                {
                    Awards.awardData awardData2 = list[i];
                    Player.SendMessage(p, "&6" + awardData2.awardName + ": &7" + awardData2.description);
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/awards [player] - Gives a full list of awards");
            Player.SendMessage(p, "If [player] is specified, shows awards for that player");
            Player.SendMessage(p, "Use 1/2/3/... to get an ordered list");
        }
    }
}