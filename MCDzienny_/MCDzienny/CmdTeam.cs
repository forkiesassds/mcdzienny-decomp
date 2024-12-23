namespace MCDzienny
{
    public class CmdTeam : Command
    {
        public override string name { get { return "team"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override void Use(Player p, string message)
        {
            if (!p.level.ctfmode)
            {
                Player.SendMessage(p, "CTF has not been enabled for this map.");
                return;
            }
            if (message.Split(' ')[0].ToLower() == "set" && p.group.Permission >= LevelPermission.Operator)
            {
                string text = message.Split(' ')[1].ToLower();
                string text2 = message.Split(' ')[2].ToLower();
                if (text2 == "none")
                {
                    Player player = Player.Find(text);
                    if (player == null || player.level != p.level)
                    {
                        Player.SendMessage(p, "That player does not exist or is not on your level.");
                    }
                    if (player.team == null)
                    {
                        Player.SendMessage(p, "That player is not on a team.");
                    }
                    player.team.RemoveMember(player);
                    return;
                }
                string text3 = c.Parse(text2);
                if (text3 == "")
                {
                    Player.SendMessage(p, "Invalid team color chosen.");
                    return;
                }
                Player player2 = Player.Find(text);
                if (player2 == null || player2.level != p.level)
                {
                    Player.SendMessage(p, "That player does not exist or is not on your level.");
                }
                char teamCol = text3[1];
                if (p.level.ctfgame.teams.Find(team1 => team1.color == teamCol) == null)
                {
                    Player.SendMessage(p, "Invalid team color chosen.");
                    return;
                }
                Team team2 = p.level.ctfgame.teams.Find(team1 => team1.color == teamCol);
                if (player2.team != null)
                {
                    player2.team.RemoveMember(player2);
                }
                team2.AddMember(player2);
            }
            if (message.Split(' ')[0].ToLower() == "join")
            {
                string text4 = c.Parse(message.Split(' ')[1]);
                if (text4 == "")
                {
                    Player.SendMessage(p, "Invalid team color chosen.");
                    return;
                }
                char teamCol2 = text4[1];
                if (p.level.ctfgame.teams.Find(team => team.color == teamCol2) == null)
                {
                    Player.SendMessage(p, "Invalid team color chosen.");
                    return;
                }
                Team team3 = p.level.ctfgame.teams.Find(team => team.color == teamCol2);
                if (p.team != null)
                {
                    p.team.RemoveMember(p);
                }
                team3.AddMember(p);
            }
            else if (message.Split(' ')[0].ToLower() == "leave")
            {
                if (p.team != null)
                {
                    p.team.RemoveMember(p);
                }
                else
                {
                    Player.SendMessage(p, "You are not on a team.");
                }
            }
            else if (message.Split(' ')[0].ToLower() == "chat")
            {
                if (p.team == null)
                {
                    Player.SendMessage(p, "You must be on a team before you can use team chat.");
                    return;
                }
                p.teamchat = !p.teamchat;
                if (p.teamchat)
                {
                    Player.SendMessage(p, "Team chat has been enabled.");
                }
                else
                {
                    Player.SendMessage(p, "Team chat has been disabled.");
                }
            }
            else
            {
                if (!(message.Split(' ')[0].ToLower() == "scores"))
                {
                    return;
                }
                foreach (Team team4 in p.level.ctfgame.teams)
                {
                    Player.SendMessage(p, team4.teamstring + " has " + team4.points + " point(s).");
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/team join [color] - Joins the team specified by the color.");
            Player.SendMessage(p, "/team leave - Leaves the team you are on.");
            Player.SendMessage(p, "/team set [name] [color] - Op+ - Sets a player to a specified team.");
            Player.SendMessage(p, "/team set [name] none - Op+ - Removes a player from a team.");
            Player.SendMessage(p, "/team scores - Shows the current scores for all teams.");
        }
    }
}