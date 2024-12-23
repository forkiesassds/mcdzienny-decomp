using System.Text;
using MCDzienny.Misc;

namespace MCDzienny
{
    public class CmdImpersonate : Command
    {
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool museumUsable { get { return true; } }

        public override string name { get { return "impersonate"; } }

        public override string shortcut { get { return "imp"; } }

        public override string type { get { return "other"; } }

        public void SendIt(Player p, string message, Player targetPlayer)
        {
            if (targetPlayer != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                message = stringBuilder.Append(targetPlayer.IronChallengeTag).Append(targetPlayer.color).Append(targetPlayer.voicestring)
                    .Append(targetPlayer.Tag)
                    .Append(targetPlayer.color)
                    .Append(targetPlayer.prefix)
                    .Append(targetPlayer.Tier)
                    .Append(targetPlayer.LavaPrefix)
                    .Append(" ")
                    .Append(targetPlayer.color)
                    .Append(targetPlayer.PublicName)
                    .Append(": ")
                    .Replace("  ", " ")
                    .Append(MCColor.White)
                    .Append(message)
                    .ToString();
                Player.GlobalMessage(message);
            }
            else
            {
                Player.SendMessage(p, "The player wasn't found.");
            }
        }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if (message.Split(' ').Length < 2)
            {
                Player.SendMessage(p, "No message was given.");
                return;
            }
            Player player = Player.Find(message.Split(' ')[0]);
            message = message.Substring(message.IndexOf(' ') + 1);
            if (player != null)
            {
                if (p == null)
                {
                    SendIt(p, message, player);
                }
                else if (player == p)
                {
                    SendIt(p, message, player);
                }
                else if (p.group.Permission > player.group.Permission)
                {
                    SendIt(p, message, player);
                }
                else
                {
                    Player.SendMessage(p, "You cannot impersonate a player of equal or greater rank.");
                }
            }
            else if (p != null)
            {
                if (p.group.Permission >= LevelPermission.Admin)
                {
                    if (Group.findPlayerGroup(message.Split(' ')[0]).Permission < p.group.Permission)
                    {
                        SendIt(p, message, null);
                    }
                    else
                    {
                        Player.SendMessage(p, "You cannot impersonate a player of equal or greater rank.");
                    }
                }
                else
                {
                    Player.SendMessage(p, "You are not allowed to impersonate offline players");
                }
            }
            else
            {
                SendIt(p, message, null);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/impersonate <player> <message> - Sends a message as if it came from <player>");
        }
    }
}