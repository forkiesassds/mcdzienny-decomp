namespace MCDzienny
{
    // Token: 0x020000FB RID: 251
    public class CmdTeam : Command
    {
        // Token: 0x17000372 RID: 882
        // (get) Token: 0x060007B9 RID: 1977 RVA: 0x00027400 File Offset: 0x00025600
        public override string name
        {
            get { return "team"; }
        }

        // Token: 0x17000373 RID: 883
        // (get) Token: 0x060007BA RID: 1978 RVA: 0x00027408 File Offset: 0x00025608
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000374 RID: 884
        // (get) Token: 0x060007BB RID: 1979 RVA: 0x00027410 File Offset: 0x00025610
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000375 RID: 885
        // (get) Token: 0x060007BD RID: 1981 RVA: 0x00027420 File Offset: 0x00025620
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000376 RID: 886
        // (get) Token: 0x060007BE RID: 1982 RVA: 0x00027424 File Offset: 0x00025624
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x060007BF RID: 1983 RVA: 0x00027428 File Offset: 0x00025628
        public override void Use(Player p, string message)
        {
            if (!p.level.ctfmode)
            {
                Player.SendMessage(p, "CTF has not been enabled for this map.");
                return;
            }

            if (message.Split(' ')[0].ToLower() == "set" && p.group.Permission >= LevelPermission.Operator)
            {
                var name = message.Split(' ')[1].ToLower();
                var text = message.Split(' ')[2].ToLower();
                if (text == "none")
                {
                    var player = Player.Find(name);
                    if (player == null || player.level != p.level)
                        Player.SendMessage(p, "That player does not exist or is not on your level.");
                    if (player.team == null) Player.SendMessage(p, "That player is not on a team.");
                    player.team.RemoveMember(player);
                    return;
                }

                var text2 = c.Parse(text);
                if (text2 == "")
                {
                    Player.SendMessage(p, "Invalid team color chosen.");
                    return;
                }

                var player2 = Player.Find(name);
                if (player2 == null || player2.level != p.level)
                    Player.SendMessage(p, "That player does not exist or is not on your level.");
                var teamCol = text2[1];
                if (p.level.ctfgame.teams.Find(team1 => team1.color == teamCol) == null)
                {
                    Player.SendMessage(p, "Invalid team color chosen.");
                    return;
                }

                var team4 = p.level.ctfgame.teams.Find(team1 => team1.color == teamCol);
                if (player2.team != null) player2.team.RemoveMember(player2);
                team4.AddMember(player2);
            }

            if (message.Split(' ')[0].ToLower() == "join")
            {
                var text3 = c.Parse(message.Split(' ')[1]);
                if (text3 == "")
                {
                    Player.SendMessage(p, "Invalid team color chosen.");
                    return;
                }

                var teamCol = text3[1];
                if (p.level.ctfgame.teams.Find(team => team.color == teamCol) == null)
                {
                    Player.SendMessage(p, "Invalid team color chosen.");
                    return;
                }

                var team2 = p.level.ctfgame.teams.Find(team => team.color == teamCol);
                if (p.team != null) p.team.RemoveMember(p);
                team2.AddMember(p);
            }
            else if (message.Split(' ')[0].ToLower() == "leave")
            {
                if (p.team != null)
                {
                    p.team.RemoveMember(p);
                    return;
                }

                Player.SendMessage(p, "You are not on a team.");
            }
            else
            {
                if (!(message.Split(' ')[0].ToLower() == "chat"))
                {
                    if (message.Split(' ')[0].ToLower() == "scores")
                        foreach (var team3 in p.level.ctfgame.teams)
                            Player.SendMessage(p, string.Concat(team3.teamstring, " has ", team3.points, " point(s)."));
                    return;
                }

                if (p.team == null)
                {
                    Player.SendMessage(p, "You must be on a team before you can use team chat.");
                    return;
                }

                p.teamchat = !p.teamchat;
                if (p.teamchat)
                {
                    Player.SendMessage(p, "Team chat has been enabled.");
                    return;
                }

                Player.SendMessage(p, "Team chat has been disabled.");
            }
        }

        // Token: 0x060007C0 RID: 1984 RVA: 0x00027840 File Offset: 0x00025A40
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