using System.Collections.Generic;
using System.Threading;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    // Token: 0x02000242 RID: 578
    public class CTFGame
    {
        // Token: 0x040008B7 RID: 2231
        public Timer flagReturn = new Timer(1000.0);

        // Token: 0x040008B5 RID: 2229
        public bool friendlyfire;

        // Token: 0x040008B4 RID: 2228
        public bool gameOn;

        // Token: 0x040008B2 RID: 2226
        public Level mapOn;

        // Token: 0x040008B3 RID: 2227
        public int maxPoints = 3;

        // Token: 0x040008B6 RID: 2230
        public Timer onTeamCheck = new Timer(500.0);

        // Token: 0x040008B8 RID: 2232
        public int returnCount;

        // Token: 0x040008B1 RID: 2225
        public List<Team> teams = new List<Team>();

        // Token: 0x060010C8 RID: 4296 RVA: 0x000575C8 File Offset: 0x000557C8
        public void GameStart()
        {
            mapOn.ChatLevel("Capture the flag game has started!");
            foreach (var team in teams)
            {
                ReturnFlag(null, team, false);
                foreach (var p in team.players) team.SpawnPlayer(p);
            }

            onTeamCheck.Start();
            onTeamCheck.Elapsed += delegate
            {
                foreach (var team2 in teams)
                foreach (var player in team2.players)
                    if (!player.loggedIn || player.level != mapOn)
                        team2.RemoveMember(player);
            };
            flagReturn.Start();
            flagReturn.Elapsed += delegate
            {
                foreach (var team2 in teams)
                    if (!team2.flagishome && team2.holdingFlag == null)
                    {
                        team2.ftcount++;
                        if (team2.ftcount > 30)
                        {
                            mapOn.ChatLevel("The " + team2.teamstring + " flag has returned to their base.");
                            team2.ftcount = 0;
                            ReturnFlag(null, team2, false);
                        }
                    }
            };
            var thread = new Thread(delegate()
            {
                while (gameOn)
                {
                    foreach (var team2 in teams) team2.Drawflag();
                    Thread.Sleep(200);
                }
            });
            thread.Start();
        }

        // Token: 0x060010C9 RID: 4297 RVA: 0x000576CC File Offset: 0x000558CC
        public void GameEnd(Team winTeam)
        {
            mapOn.ChatLevel(string.Concat("The game has ended! ", winTeam.teamstring, " has won with ", winTeam.points,
                " point(s)!"));
            foreach (var team in teams)
            {
                ReturnFlag(null, team, false);
                foreach (var player in team.players)
                {
                    player.hasflag = null;
                    player.carryingFlag = false;
                }

                team.points = 0;
            }

            gameOn = false;
        }

        // Token: 0x060010CA RID: 4298 RVA: 0x000577C0 File Offset: 0x000559C0
        public void GrabFlag(Player p, Team team)
        {
            if (!p.carryingFlag)
            {
                var x = (ushort) (p.pos[0] / 32);
                var y = (ushort) (p.pos[1] / 32 + 3);
                var z = (ushort) (p.pos[2] / 32);
                team.tempFlagblock.x = x;
                team.tempFlagblock.y = y;
                team.tempFlagblock.z = z;
                team.tempFlagblock.type = mapOn.GetTile(x, y, z);
                mapOn.Blockchange(x, y, z, Team.GetColorBlock(team.color));
                mapOn.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + " has stolen the " +
                                team.teamstring + " flag!");
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

        // Token: 0x060010CB RID: 4299 RVA: 0x000578E8 File Offset: 0x00055AE8
        public void CaptureFlag(Player p, Team playerTeam, Team capturedTeam)
        {
            playerTeam.points++;
            mapOn.Blockchange(capturedTeam.tempFlagblock.x, capturedTeam.tempFlagblock.y, capturedTeam.tempFlagblock.z,
                capturedTeam.tempFlagblock.type);
            mapOn.ChatLevel(string.Concat(p.color, p.prefix, p.name, Server.DefaultColor, " has captured the ",
                capturedTeam.teamstring, " flag!"));
            if (playerTeam.points >= maxPoints)
            {
                GameEnd(playerTeam);
                return;
            }

            mapOn.ChatLevel(string.Concat(playerTeam.teamstring, " now has ", playerTeam.points, " point(s)."));
            p.hasflag = null;
            p.carryingFlag = false;
            ReturnFlag(null, capturedTeam, false);
        }

        // Token: 0x060010CC RID: 4300 RVA: 0x00057A00 File Offset: 0x00055C00
        public void DropFlag(Player p, Team team)
        {
            mapOn.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + " has dropped the " + team.teamstring +
                            " flag!");
            var num = (ushort) (p.pos[0] / 32);
            var num2 = (ushort) (p.pos[1] / 32 - 1);
            var num3 = (ushort) (p.pos[2] / 32);
            mapOn.Blockchange(team.tempFlagblock.x, team.tempFlagblock.y, team.tempFlagblock.z,
                team.tempFlagblock.type);
            team.flagLocation[0] = num;
            team.flagLocation[1] = num2;
            team.flagLocation[2] = num3;
            p.hasflag = null;
            p.carryingFlag = false;
            team.holdingFlag = null;
            team.flagishome = false;
        }

        // Token: 0x060010CD RID: 4301 RVA: 0x00057B04 File Offset: 0x00055D04
        public void ReturnFlag(Player p, Team team, bool verbose)
        {
            if (p != null && p.spawning) return;
            if (verbose)
            {
                if (p != null)
                    mapOn.ChatLevel(string.Concat(p.color, p.prefix, p.name, Server.DefaultColor, " has returned the ",
                        team.teamstring, " flag!"));
                else
                    mapOn.ChatLevel("The " + team.teamstring + " flag has been returned.");
            }

            team.holdingFlag = null;
            team.flagLocation[0] = team.flagBase[0];
            team.flagLocation[1] = team.flagBase[1];
            team.flagLocation[2] = team.flagBase[2];
            team.flagishome = true;
        }

        // Token: 0x060010CE RID: 4302 RVA: 0x00057BD8 File Offset: 0x00055DD8
        public void AddTeam(string color)
        {
            var c = color[1];
            var team = new Team();
            team.color = c;
            team.points = 0;
            team.mapOn = mapOn;
            var array = MCDzienny.c.Name("&" + c).ToCharArray();
            array[0] = char.ToUpper(array[0]);
            var text = new string(array);
            team.teamstring = "&" + c + text + " team" + Server.DefaultColor;
            teams.Add(team);
            mapOn.ChatLevel(team.teamstring + " has been initialized!");
        }

        // Token: 0x060010CF RID: 4303 RVA: 0x00057CA0 File Offset: 0x00055EA0
        public void RemoveTeam(string color)
        {
            var teamCol = color[1];
            var team2 = teams.Find(team => team.color == teamCol);
            var list = new List<Player>();
            for (var i = 0; i < team2.players.Count; i++) list.Add(team2.players[i]);
            foreach (var p in list) team2.RemoveMember(p);
        }
    }
}