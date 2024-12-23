using System;

namespace MCDzienny
{
    public class CmdTempMute : Command
    {
        public override string name { get { return "tempmute"; } }

        public override string shortcut { get { return "tmute"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            int num = message.Split(' ').Length;
            string text = message.Trim();
            string text2 = "";
            int result = 0;
            if (num != 1)
            {
                if (num == 2)
                {
                    text = message.Split(' ')[0];
                    if (!int.TryParse(message.Split(' ')[1], out result))
                    {
                        text2 = message.Split(' ')[1];
                    }
                }
                else if (num > 2)
                {
                    text = message.Split(' ')[0];
                    text2 = !int.TryParse(message.Split(' ')[1], out result) ? message.Substring(message.IndexOf(' ') + 1)
                        : message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1) + 1);
                }
            }
            Player player = Player.Find(text);
            if (player == null)
            {
                Player.SendMessage(p, "The player wasn't found.");
                return;
            }
            if (p != null && p.group.Permission <= player.group.Permission)
            {
                Player.SendMessage(p, "You can't tempmute a player with equal or higher rank than you.");
                return;
            }
            if (result == 0)
            {
                result = 60;
            }
            player.muteTime = DateTime.Now.AddSeconds(result);
            string text3 = p == null ? "<Console>" : p.name;
            if (result == 60 && text2 == "")
            {
                Player.SendMessage(player, "You were muted for 60 seconds.");
                Player.SendMessage(p, string.Format("Player {0} was muted for 60 seconds.", player.PublicName));
                Server.s.Log("Player " + player.name + " was muted for 60 seconds by " + text3);
            }
            else if (result == 60 && text2 != "")
            {
                Player.SendMessage(player, string.Format("You were muted for 60 seconds. Reason: {0}", text2));
                Player.SendMessage(p, string.Format("Player {0} was muted for 60 seconds. Reason: {1}", player.PublicName, text2));
                Server.s.Log("Player " + player.name + " was muted for 60 seconds by " + text3 + " Reason: " + text2);
            }
            else if (text2 == "")
            {
                Player.SendMessage(player, string.Format("You were muted for {0} seconds.", result));
                Player.SendMessage(p, string.Format("Player {0} was muted for {1} seconds.", player.PublicName, result));
                Server.s.Log("Player " + player.name + " was muted for " + result + " seconds by " + text3);
            }
            else
            {
                Player.SendMessage(player, string.Format("You were muted for {0} seconds. Reason: {1}", result, text2));
                Player.SendMessage(p, string.Format("Player {0} was muted for {1} seconds. Reason: {2}", player.PublicName, result, text2));
                Server.s.Log("Player " + player.name + " was muted for " + result + " seconds by " + text3 + " Reason: " + text2);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tempmute [player] - mutes [player] for 60 seconds.");
            Player.SendMessage(p, "/tempmute [player] [time] - mutes [player] for [time] seconds.");
            Player.SendMessage(p, "/tempmute [player] [reason] - mutes [player] due to [reason] for 60 seconds.");
            Player.SendMessage(p, "/tempmute [player] [time] [reason]");
        }
    }
}