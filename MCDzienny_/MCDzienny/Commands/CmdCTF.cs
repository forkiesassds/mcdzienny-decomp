using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020000E6 RID: 230
    public class CmdCTF : Command
    {
        // Token: 0x17000356 RID: 854
        // (get) Token: 0x0600076A RID: 1898 RVA: 0x000253B8 File Offset: 0x000235B8
        public override string name
        {
            get { return "ctf"; }
        }

        // Token: 0x17000357 RID: 855
        // (get) Token: 0x0600076B RID: 1899 RVA: 0x000253C0 File Offset: 0x000235C0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000358 RID: 856
        // (get) Token: 0x0600076C RID: 1900 RVA: 0x000253C8 File Offset: 0x000235C8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000359 RID: 857
        // (get) Token: 0x0600076D RID: 1901 RVA: 0x000253D0 File Offset: 0x000235D0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700035A RID: 858
        // (get) Token: 0x0600076F RID: 1903 RVA: 0x000253DC File Offset: 0x000235DC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x06000770 RID: 1904 RVA: 0x000253E0 File Offset: 0x000235E0
        public override void Use(Player p, string message)
        {
            var num = message.Split(' ').Length;
            if (num == 3)
            {
                var array = message.Split(' ');
                for (var i = 0; i < num; i++) array[i] = array[i].ToLower();
                if (array[0] == "team")
                {
                    if (array[1] == "add")
                    {
                        var text = MCDzienny.c.Parse(array[2]);
                        if (text == "")
                        {
                            Player.SendMessage(p, "Invalid team color chosen.");
                            return;
                        }

                        var c = text[1];
                        var c2 = c;
                        if (c2 != '2')
                        {
                            switch (c2)
                            {
                                case '5':
                                case '8':
                                case '9':
                                    goto IL_E1;
                                case '6':
                                case '7':
                                    break;
                                default:
                                    switch (c2)
                                    {
                                        case 'c':
                                        case 'e':
                                        case 'f':
                                            goto IL_E1;
                                    }

                                    break;
                            }

                            Player.SendMessage(p, "Invalid team color chosen.");
                            return;
                        }

                        IL_E1:
                        AddTeam(p, text);
                        return;
                    }
                    else if (array[1] == "remove")
                    {
                        var text2 = c.Parse(array[2]);
                        if (text2 == "")
                        {
                            Player.SendMessage(p, "Invalid team color chosen.");
                            return;
                        }

                        var c3 = text2[1];
                        var c4 = c3;
                        if (c4 != '2')
                        {
                            switch (c4)
                            {
                                case '5':
                                case '8':
                                case '9':
                                    goto IL_17A;
                                case '6':
                                case '7':
                                    break;
                                default:
                                    switch (c4)
                                    {
                                        case 'c':
                                        case 'e':
                                        case 'f':
                                            goto IL_17A;
                                    }

                                    break;
                            }

                            Player.SendMessage(p, "Invalid team color chosen.");
                            return;
                        }

                        IL_17A:
                        RemoveTeam(p, text2);
                    }
                }
            }
            else if (num == 2)
            {
                var array2 = message.Split(' ');
                for (var j = 0; j < num; j++) array2[j] = array2[j].ToLower();
                if (array2[0] == "debug")
                {
                    Debug(p, array2[1]);
                    return;
                }

                if (array2[0] == "flag")
                {
                    var text3 = c.Parse(array2[1]);
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

                    CatchPos catchPos;
                    catchPos.x = 0;
                    catchPos.y = 0;
                    catchPos.z = 0;
                    catchPos.color = text3;
                    p.blockchangeObject = catchPos;
                    Player.SendMessage(p, "Place a block to determine where to place the flag.");
                    p.ClearBlockchange();
                    p.Blockchange += FlagBlockChange;
                }
                else if (array2[0] == "spawn")
                {
                    var text4 = c.Parse(array2[1]);
                    if (text4 == "")
                    {
                        Player.SendMessage(p, "Invalid team color chosen.");
                        return;
                    }

                    var teamCol = text4[1];
                    if (p.level.ctfgame.teams.Find(team => team.color == teamCol) == null)
                    {
                        Player.SendMessage(p, "Invalid team color chosen.");
                        return;
                    }

                    AddSpawn(p, text4);
                }
                else if (array2[0] == "points")
                {
                    var num2 = 0;
                    int.TryParse(array2[1], out num2);
                    if (num2 == 0)
                    {
                        Player.SendMessage(p, "You must choose a points value greater than 0!");
                        return;
                    }

                    p.level.ctfgame.maxPoints = num2;
                    Player.SendMessage(p, "Max round points has been set to " + num2);
                }
            }
            else if (num == 1)
            {
                if (message.ToLower() == "start")
                {
                    if (!p.level.ctfmode) p.level.ctfmode = true;
                    p.level.ctfgame.gameOn = true;
                    p.level.ctfgame.GameStart();
                    return;
                }

                if (message.ToLower() == "stop")
                {
                    if (p.level.ctfmode) p.level.ctfmode = false;
                    p.level.ctfmode = false;
                    p.level.ctfgame.gameOn = false;
                    p.level.ChatLevel(p.color + p.name + Server.DefaultColor + " has ended the game");
                    return;
                }

                if (message.ToLower() == "ff")
                {
                    if (p.level.ctfgame.friendlyfire)
                    {
                        p.level.ChatLevel("Friendly fire has been disabled.");
                        p.level.ctfgame.friendlyfire = false;
                        return;
                    }

                    p.level.ChatLevel("Friendly fire has been enabled.");
                    p.level.ctfgame.friendlyfire = true;
                }
                else
                {
                    if (message.ToLower() == "clear")
                    {
                        var list = new List<Team>();
                        for (var k = 0; k < p.level.ctfgame.teams.Count; k++) list.Add(p.level.ctfgame.teams[k]);
                        foreach (var team2 in list) p.level.ctfgame.RemoveTeam("&" + team2.color);
                        p.level.ctfgame.onTeamCheck.Stop();
                        p.level.ctfgame.onTeamCheck.Dispose();
                        p.level.ctfgame.gameOn = false;
                        p.level.ctfmode = false;
                        p.level.ctfgame = new CTFGame();
                        p.level.ctfgame.mapOn = p.level;
                        Player.SendMessage(p, "CTF data has been cleared.");
                        return;
                    }

                    if (message.ToLower() == "")
                    {
                        if (p.level.ctfmode)
                        {
                            p.level.ctfmode = false;
                            p.level.ChatLevel("CTF Mode has been disabled.");
                            return;
                        }

                        if (!p.level.ctfmode)
                        {
                            p.level.ctfmode = true;
                            p.level.ChatLevel("CTF Mode has been enabled.");
                        }
                    }
                }
            }
        }

        // Token: 0x06000771 RID: 1905 RVA: 0x00025A54 File Offset: 0x00023C54
        public void AddSpawn(Player p, string color)
        {
            var teamCol = color[1];
            var x = (ushort) (p.pos[0] / 32);
            var y = (ushort) (p.pos[1] / 32);
            var z = (ushort) (p.pos[2] / 32);
            ushort rotx = p.rot[0];
            p.level.ctfgame.teams.Find(team => team.color == teamCol).AddSpawn(x, y, z, rotx, 0);
            Player.SendMessage(p,
                "Added spawn for " + p.level.ctfgame.teams.Find(team => team.color == teamCol).teamstring);
        }

        // Token: 0x06000772 RID: 1906 RVA: 0x00025B10 File Offset: 0x00023D10
        public void AddTeam(Player p, string color)
        {
            var teamCol = color[1];
            if (p.level.ctfgame.teams.Find(team => team.color == teamCol) != null)
            {
                Player.SendMessage(p, "That team already exists.");
                return;
            }

            p.level.ctfgame.AddTeam(color);
        }

        // Token: 0x06000773 RID: 1907 RVA: 0x00025B70 File Offset: 0x00023D70
        public void RemoveTeam(Player p, string color)
        {
            var teamCol = color[1];
            if (p.level.ctfgame.teams.Find(team => team.color == teamCol) == null)
            {
                Player.SendMessage(p, "That team does not exist.");
                return;
            }

            p.level.ctfgame.RemoveTeam(color);
        }

        // Token: 0x06000774 RID: 1908 RVA: 0x00025BD0 File Offset: 0x00023DD0
        public void AddFlag(Player p, string col, ushort x, ushort y, ushort z)
        {
            var teamCol = col[1];
            var team2 = p.level.ctfgame.teams.Find(team => team.color == teamCol);
            team2.flagBase[0] = x;
            team2.flagBase[1] = y;
            team2.flagBase[2] = z;
            team2.flagLocation[0] = x;
            team2.flagLocation[1] = y;
            team2.flagLocation[2] = z;
            team2.Drawflag();
        }

        // Token: 0x06000775 RID: 1909 RVA: 0x00025C54 File Offset: 0x00023E54
        public void Debug(Player p, string col)
        {
            if (col.ToLower() == "flags")
            {
                foreach (var team4 in p.level.ctfgame.teams)
                {
                    Player.SendMessage(p, "Drawing flag for " + team4.teamstring);
                    team4.Drawflag();
                }

                return;
            }

            if (col.ToLower() == "spawn")
            {
                foreach (var team2 in p.level.ctfgame.teams)
                foreach (var p2 in team2.players)
                    team2.SpawnPlayer(p2);
                return;
            }

            var text = c.Parse(col);
            var teamCol = text[1];
            var team3 = p.level.ctfgame.teams.Find(team => team.color == teamCol);
            var text2 = "";
            for (var i = 0; i < p.level.ctfgame.teams.Count; i++)
                text2 = text2 + p.level.ctfgame.teams[i].teamstring + ", ";
            Player.SendMessage(p, "Player Debug: Team: " + p.team.teamstring);
            Player.SendMessage(p, "CTFGame teams: " + text2);
            var text3 = "";
            foreach (var player in team3.players) text3 = text3 + player.name + ", ";
            Player.SendMessage(p, "Player list: " + text3);
            Player.SendMessage(p,
                string.Concat("Points: ", team3.points, ", MapOn: ", team3.mapOn.name, ", flagishome: ",
                    team3.flagishome, ", spawnset: ", team3.spawnset));
            Player.SendMessage(p,
                string.Concat("FlagBase[0]: ", team3.flagBase[0], ", [1]: ", team3.flagBase[1], ", [2]: ",
                    team3.flagBase[2]));
            Player.SendMessage(p,
                string.Concat("FlagLocation[0]: ", team3.flagLocation[0], ", [1]: ", team3.flagLocation[1], ", [2]: ",
                    team3.flagLocation[2]));
        }

        // Token: 0x06000776 RID: 1910 RVA: 0x00025FEC File Offset: 0x000241EC
        private void FlagBlockChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            var catchPos = (CatchPos) p.blockchangeObject;
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            p.ClearBlockchange();
            AddFlag(p, catchPos.color, x, y, z);
        }

        // Token: 0x06000777 RID: 1911 RVA: 0x00026038 File Offset: 0x00024238
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ctf - Turns CTF mode on for the map.  Must be enabled to play!");
            Player.SendMessage(p, "/ctf start - Starts the game!");
            Player.SendMessage(p, "/ctf stop - Stops the game.");
            Player.SendMessage(p, "/ctf ff - Enables or disables friendly fire.  Default is off.");
            Player.SendMessage(p, "/ctf flag [color] - Sets the flag base for specified team.");
            Player.SendMessage(p,
                "/ctf spawn [color] - Adds a spawn for the team specified from where you are standing.");
            Player.SendMessage(p, "/ctf points [num] - Sets max round points.  Default is 3.");
            Player.SendMessage(p, "/ctf team add [color] - Initializes team of specified color.");
            Player.SendMessage(p, "/ctf team remove [color] - Removes team of specified color.");
            Player.SendMessage(p, "/ctf clear - Removes all CTF data from map.  Use sparingly.");
        }

        // Token: 0x020000E7 RID: 231
        public struct CatchPos
        {
            // Token: 0x04000390 RID: 912
            public ushort x;

            // Token: 0x04000391 RID: 913
            public ushort y;

            // Token: 0x04000392 RID: 914
            public ushort z;

            // Token: 0x04000393 RID: 915
            public string color;
        }
    }
}