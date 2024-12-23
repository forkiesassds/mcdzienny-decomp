namespace MCDzienny
{
    public class CmdVoteBan : Command
    {
        public override string name { get { return "voteban"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if (p != null && (p.muted || p.IsTempMuted))
            {
                Player.SendMessage(p, "You can't use this command while muted.");
                return;
            }
            bool stopIt = false;
            Player.OnPlayerChatEvent(p, ref message, ref stopIt);
            if (stopIt)
            {
                return;
            }
            string text = message.Trim();
            string text2 = "";
            if (message.Split(' ').Length >= 2)
            {
                text = message.Split(' ')[0];
                text2 = message.Substring(message.IndexOf(' ') + 1);
            }
            if (VotingSystem.votingInProgress)
            {
                Player.SendMessage(p, "Please wait till the end of the current voting!");
                return;
            }
            Player player = Player.Find(text);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player specified!");
                return;
            }
            if (p != null && player.group.Permission > p.group.Permission)
            {
                Player.GlobalChat(p,
                                  string.Format("{0} {1}tried to voteban {2} {3}and failed!", p.color + p.PublicName, Server.DefaultColor, player.color + player.name,
                                                Server.DefaultColor),
                                  showname: false);
                return;
            }
            string text3 = p == null ? Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 1) : p.color + p.PublicName;
            if (text2 == "")
            {
                Server.s.Log("Vote to tempban " + player.name + " was called by " + text3);
                Player.GlobalMessage(string.Format("&cA vote to temp-ban {0} {1}has been called by {2}!", player.color + player.PublicName, Server.DefaultColor,
                                                   text3 + Server.DefaultColor));
                Player.GlobalMessage(string.Format("Type &cY {0}or &cN {1}to vote.", Server.DefaultColor, Server.DefaultColor));
            }
            else
            {
                Server.s.Log("Vote to tempban " + player.name + " was called by " + text3 + " reason: " + text2);
                Player.GlobalMessage("&cA vote to temp-ban " + player.color + player.PublicName + " " + Server.DefaultColor + "has been called by " + text3 +
                                     Server.DefaultColor + "!");
                Player.GlobalMessage("%cGiven reason: " + Server.DefaultColor + text2);
                Player.GlobalMessage("Type &cY " + Server.DefaultColor + "or &cN " + Server.DefaultColor + "to vote.");
            }
            VotingSystem.StartVote(player, "", VotingSystem.TypeOfVote.VoteBan, 30000);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/voteban <player> - Calls a 30sec vote to tempban <player>");
            Player.SendMessage(p, "/voteban <player> [reason]");
        }
    }
}