using System.Collections.Generic;
using System.Threading;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    public class CTFGame
    {

        public Timer flagReturn = new Timer(1000.0);

        public bool friendlyfire;

        public bool gameOn;

        public Level mapOn;

        public int maxPoints = 3;

        public Timer onTeamCheck = new Timer(500.0);

        public int returnCount;
        public List<Team> teams = new List<Team>();

        public void GameStart()
        {
            mapOn.ChatLevel("Capture the flag game has started!");
            foreach (Team team in teams)
            {
                ReturnFlag(null, team, verbose: false);
                foreach (Player player in team.players)
                {
                    team.SpawnPlayer(player);
                }
            }
            onTeamCheck.Start();
            onTeamCheck.Elapsed += delegate
            {
                foreach (Team team2 in teams)
                {
                    foreach (Player player2 in team2.players)
                    {
                        if (!player2.loggedIn || player2.level != mapOn)
                        {
                            team2.RemoveMember(player2);
                        }
                    }
                }
            };
            flagReturn.Start();
            flagReturn.Elapsed += delegate
            {
                foreach (Team team3 in teams)
                {
                    if (!team3.flagishome && team3.holdingFlag == null)
                    {
                        team3.ftcount++;
                        if (team3.ftcount > 30)
                        {
                            mapOn.ChatLevel("The " + team3.teamstring + " flag has returned to their base.");
                            team3.ftcount = 0;
                            ReturnFlag(null, team3, verbose: false);
                        }
                    }
                }
            };
            Thread thread = new Thread((ThreadStart)delegate
            {
                while (gameOn)
                {
                    foreach (Team team4 in teams)
                    {
                        team4.Drawflag();
                    }
                    Thread.Sleep(200);
                }
            });
            thread.Start();
        }

        public void GameEnd(Team winTeam)
        {
            mapOn.ChatLevel("The game has ended! " + winTeam.teamstring + " has won with " + winTeam.points + " point(s)!");
            foreach (Team team in teams)
            {
                ReturnFlag(null, team, verbose: false);
                foreach (Player player in team.players)
                {
                    player.hasflag = null;
                    player.carryingFlag = false;
                }
                team.points = 0;
            }
            gameOn = false;
        }

        public void GrabFlag(Player p, Team team)
        {
            if (!p.carryingFlag)
            {
                ushort x = (ushort)(p.pos[0] / 32);
                ushort y = (ushort)(p.pos[1] / 32 + 3);
                ushort z = (ushort)(p.pos[2] / 32);
                team.tempFlagblock.x = x;
                team.tempFlagblock.y = y;
                team.tempFlagblock.z = z;
                team.tempFlagblock.type = mapOn.GetTile(x, y, z);
                mapOn.Blockchange(x, y, z, Team.GetColorBlock(team.color));
                mapOn.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + " has stolen the " + team.teamstring + " flag!");
                p.hasflag = team;
                p.carryingFlag = true;
                team.holdingFlag = p;
                team.flagishome = false;
                if (p.aiming)
                {
                    p.ClearBlockchange();
                    p.aiming = false;
                }
            }
        }

        public void CaptureFlag(Player p, Team playerTeam, Team capturedTeam)
        {
            playerTeam.points++;
            mapOn.Blockchange(capturedTeam.tempFlagblock.x, capturedTeam.tempFlagblock.y, capturedTeam.tempFlagblock.z, capturedTeam.tempFlagblock.type);
            mapOn.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + " has captured the " + capturedTeam.teamstring + " flag!");
            if (playerTeam.points >= maxPoints)
            {
                GameEnd(playerTeam);
                return;
            }
            mapOn.ChatLevel(playerTeam.teamstring + " now has " + playerTeam.points + " point(s).");
            p.hasflag = null;
            p.carryingFlag = false;
            ReturnFlag(null, capturedTeam, verbose: false);
        }

        public void DropFlag(Player p, Team team)
        {
            mapOn.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + " has dropped the " + team.teamstring + " flag!");
            ushort num = (ushort)(p.pos[0] / 32);
            ushort num2 = (ushort)(p.pos[1] / 32 - 1);
            ushort num3 = (ushort)(p.pos[2] / 32);
            mapOn.Blockchange(team.tempFlagblock.x, team.tempFlagblock.y, team.tempFlagblock.z, team.tempFlagblock.type);
            team.flagLocation[0] = num;
            team.flagLocation[1] = num2;
            team.flagLocation[2] = num3;
            p.hasflag = null;
            p.carryingFlag = false;
            team.holdingFlag = null;
            team.flagishome = false;
        }

        public void ReturnFlag(Player p, Team team, bool verbose)
        {
            if (p != null && p.spawning)
            {
                return;
            }
            if (verbose)
            {
                if (p != null)
                {
                    mapOn.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + " has returned the " + team.teamstring + " flag!");
                }
                else
                {
                    mapOn.ChatLevel("The " + team.teamstring + " flag has been returned.");
                }
            }
            team.holdingFlag = null;
            team.flagLocation[0] = team.flagBase[0];
            team.flagLocation[1] = team.flagBase[1];
            team.flagLocation[2] = team.flagBase[2];
            team.flagishome = true;
        }

        public void AddTeam(string color)
        {
            char c2 = color[1];
            Team team = new Team();
            team.color = c2;
            team.points = 0;
            team.mapOn = mapOn;
            char[] array = c.Name("&" + c2).ToCharArray();
            array[0] = char.ToUpper(array[0]);
            string text = new string(array);
            team.teamstring = "&" + c2 + text + " team" + Server.DefaultColor;
            teams.Add(team);
            mapOn.ChatLevel(team.teamstring + " has been initialized!");
        }

        public void RemoveTeam(string color)
        {
            char teamCol = color[1];
            Team team2 = teams.Find(team => team.color == teamCol);
            var list = new List<Player>();
            for (int i = 0; i < team2.players.Count; i++)
            {
                list.Add(team2.players[i]);
            }
            foreach (Player item in list)
            {
                team2.RemoveMember(item);
            }
        }
    }
}