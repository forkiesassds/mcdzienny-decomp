using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdCTF : Command
    {

        public override string name { get { return "ctf"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            int num = message.Split(' ').Length;
            switch (num)
            {
                case 3:
                {
                    string[] array = message.Split(' ');
                    for (int j = 0; j < num; j++)
                    {
                        array[j] = array[j].ToLower();
                    }
                    if (!(array[0] == "team"))
                    {
                        break;
                    }
                    if (array[1] == "add")
                    {
                        string text = c.Parse(array[2]);
                        if (text == "")
                        {
                            Player.SendMessage(p, "Invalid team color chosen.");
                            break;
                        }
                        switch (text[1])
                        {
                            case '2':
                            case '5':
                            case '8':
                            case '9':
                            case 'c':
                            case 'e':
                            case 'f':
                                AddTeam(p, text);
                                break;
                            default:
                                Player.SendMessage(p, "Invalid team color chosen.");
                                break;
                        }
                    }
                    else
                    {
                        if (!(array[1] == "remove"))
                        {
                            break;
                        }
                        string text2 = c.Parse(array[2]);
                        if (text2 == "")
                        {
                            Player.SendMessage(p, "Invalid team color chosen.");
                            break;
                        }
                        switch (text2[1])
                        {
                            case '2':
                            case '5':
                            case '8':
                            case '9':
                            case 'c':
                            case 'e':
                            case 'f':
                                RemoveTeam(p, text2);
                                break;
                            default:
                                Player.SendMessage(p, "Invalid team color chosen.");
                                break;
                        }
                    }
                    break;
                }
                case 2:
                {
                    string[] array2 = message.Split(' ');
                    for (int k = 0; k < num; k++)
                    {
                        array2[k] = array2[k].ToLower();
                    }
                    if (array2[0] == "debug")
                    {
                        Debug(p, array2[1]);
                    }
                    else if (array2[0] == "flag")
                    {
                        string text3 = c.Parse(array2[1]);
                        if (text3 == "")
                        {
                            Player.SendMessage(p, "Invalid team color chosen.");
                            break;
                        }
                        char teamCol = text3[1];
                        if (p.level.ctfgame.teams.Find(team => team.color == teamCol) == null)
                        {
                            Player.SendMessage(p, "Invalid team color chosen.");
                            break;
                        }
                        CatchPos catchPos = default(CatchPos);
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
                        string text4 = c.Parse(array2[1]);
                        if (text4 == "")
                        {
                            Player.SendMessage(p, "Invalid team color chosen.");
                            break;
                        }
                        char teamCol2 = text4[1];
                        if (p.level.ctfgame.teams.Find(team => team.color == teamCol2) == null)
                        {
                            Player.SendMessage(p, "Invalid team color chosen.");
                        }
                        else
                        {
                            AddSpawn(p, text4);
                        }
                    }
                    else if (array2[0] == "points")
                    {
                        int result = 0;
                        int.TryParse(array2[1], out result);
                        if (result == 0)
                        {
                            Player.SendMessage(p, "You must choose a points value greater than 0!");
                            break;
                        }
                        p.level.ctfgame.maxPoints = result;
                        Player.SendMessage(p, "Max round points has been set to " + result);
                    }
                    break;
                }
                case 1:
                    if (message.ToLower() == "start")
                    {
                        if (!p.level.ctfmode)
                        {
                            p.level.ctfmode = true;
                        }
                        p.level.ctfgame.gameOn = true;
                        p.level.ctfgame.GameStart();
                    }
                    else if (message.ToLower() == "stop")
                    {
                        if (p.level.ctfmode)
                        {
                            p.level.ctfmode = false;
                        }
                        p.level.ctfmode = false;
                        p.level.ctfgame.gameOn = false;
                        p.level.ChatLevel(p.color + p.name + Server.DefaultColor + " has ended the game");
                    }
                    else if (message.ToLower() == "ff")
                    {
                        if (p.level.ctfgame.friendlyfire)
                        {
                            p.level.ChatLevel("Friendly fire has been disabled.");
                            p.level.ctfgame.friendlyfire = false;
                        }
                        else
                        {
                            p.level.ChatLevel("Friendly fire has been enabled.");
                            p.level.ctfgame.friendlyfire = true;
                        }
                    }
                    else if (message.ToLower() == "clear")
                    {
                        var list = new List<Team>();
                        for (int i = 0; i < p.level.ctfgame.teams.Count; i++)
                        {
                            list.Add(p.level.ctfgame.teams[i]);
                        }
                        foreach (Team item in list)
                        {
                            p.level.ctfgame.RemoveTeam("&" + item.color);
                        }
                        p.level.ctfgame.onTeamCheck.Stop();
                        p.level.ctfgame.onTeamCheck.Dispose();
                        p.level.ctfgame.gameOn = false;
                        p.level.ctfmode = false;
                        p.level.ctfgame = new CTFGame();
                        p.level.ctfgame.mapOn = p.level;
                        Player.SendMessage(p, "CTF data has been cleared.");
                    }
                    else if (message.ToLower() == "")
                    {
                        if (p.level.ctfmode)
                        {
                            p.level.ctfmode = false;
                            p.level.ChatLevel("CTF Mode has been disabled.");
                        }
                        else if (!p.level.ctfmode)
                        {
                            p.level.ctfmode = true;
                            p.level.ChatLevel("CTF Mode has been enabled.");
                        }
                    }
                    break;
            }
        }

        public void AddSpawn(Player p, string color)
        {
            char teamCol = color[1];
            ushort x = (ushort)(p.pos[0] / 32);
            ushort y = (ushort)(p.pos[1] / 32);
            ushort z = (ushort)(p.pos[2] / 32);
            ushort rotx = p.rot[0];
            p.level.ctfgame.teams.Find(team => team.color == teamCol).AddSpawn(x, y, z, rotx, 0);
            Player.SendMessage(p, "Added spawn for " + p.level.ctfgame.teams.Find(team => team.color == teamCol).teamstring);
        }

        public void AddTeam(Player p, string color)
        {
            char teamCol = color[1];
            if (p.level.ctfgame.teams.Find(team => team.color == teamCol) != null)
            {
                Player.SendMessage(p, "That team already exists.");
            }
            else
            {
                p.level.ctfgame.AddTeam(color);
            }
        }

        public void RemoveTeam(Player p, string color)
        {
            char teamCol = color[1];
            if (p.level.ctfgame.teams.Find(team => team.color == teamCol) == null)
            {
                Player.SendMessage(p, "That team does not exist.");
            }
            else
            {
                p.level.ctfgame.RemoveTeam(color);
            }
        }

        public void AddFlag(Player p, string col, ushort x, ushort y, ushort z)
        {
            char teamCol = col[1];
            Team team2 = p.level.ctfgame.teams.Find(team => team.color == teamCol);
            team2.flagBase[0] = x;
            team2.flagBase[1] = y;
            team2.flagBase[2] = z;
            team2.flagLocation[0] = x;
            team2.flagLocation[1] = y;
            team2.flagLocation[2] = z;
            team2.Drawflag();
        }

        public void Debug(Player p, string col)
        {
            if (col.ToLower() == "flags")
            {
                foreach (Team team3 in p.level.ctfgame.teams)
                {
                    Player.SendMessage(p, "Drawing flag for " + team3.teamstring);
                    team3.Drawflag();
                }
                return;
            }
            if (col.ToLower() == "spawn")
            {
                foreach (Team team4 in p.level.ctfgame.teams)
                {
                    foreach (Player player in team4.players)
                    {
                        team4.SpawnPlayer(player);
                    }
                }
                return;
            }
            string text = c.Parse(col);
            char teamCol = text[1];
            Team team2 = p.level.ctfgame.teams.Find(team => team.color == teamCol);
            string text2 = "";
            for (int i = 0; i < p.level.ctfgame.teams.Count; i++)
            {
                text2 = text2 + p.level.ctfgame.teams[i].teamstring + ", ";
            }
            Player.SendMessage(p, "Player Debug: Team: " + p.team.teamstring);
            Player.SendMessage(p, "CTFGame teams: " + text2);
            string text3 = "";
            foreach (Player player2 in team2.players)
            {
                text3 = text3 + player2.name + ", ";
            }
            Player.SendMessage(p, "Player list: " + text3);
            Player.SendMessage(p, "Points: " + team2.points + ", MapOn: " + team2.mapOn.name + ", flagishome: " + team2.flagishome + ", spawnset: " + team2.spawnset);
            Player.SendMessage(p, "FlagBase[0]: " + team2.flagBase[0] + ", [1]: " + team2.flagBase[1] + ", [2]: " + team2.flagBase[2]);
            Player.SendMessage(p, "FlagLocation[0]: " + team2.flagLocation[0] + ", [1]: " + team2.flagLocation[1] + ", [2]: " + team2.flagLocation[2]);
        }

        void FlagBlockChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            p.ClearBlockchange();
            AddFlag(p, catchPos.color, x, y, z);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ctf - Turns CTF mode on for the map.  Must be enabled to play!");
            Player.SendMessage(p, "/ctf start - Starts the game!");
            Player.SendMessage(p, "/ctf stop - Stops the game.");
            Player.SendMessage(p, "/ctf ff - Enables or disables friendly fire.  Default is off.");
            Player.SendMessage(p, "/ctf flag [color] - Sets the flag base for specified team.");
            Player.SendMessage(p, "/ctf spawn [color] - Adds a spawn for the team specified from where you are standing.");
            Player.SendMessage(p, "/ctf points [num] - Sets max round points.  Default is 3.");
            Player.SendMessage(p, "/ctf team add [color] - Initializes team of specified color.");
            Player.SendMessage(p, "/ctf team remove [color] - Removes team of specified color.");
            Player.SendMessage(p, "/ctf clear - Removes all CTF data from map.  Use sparingly.");
        }

        public struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public string color;
        }
    }
}