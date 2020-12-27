using System;
using System.Collections.Generic;
using System.Timers;
using MCDzienny.CpeApi;

namespace MCDzienny.Games
{
    // Token: 0x0200002A RID: 42
    public class Race
    {
        // Token: 0x040000AB RID: 171
        public static readonly string PlayerRaceStateId = "player_race_state_id";

        // Token: 0x040000A5 RID: 165
        private readonly Timer displayTimeForPlayers = new Timer();

        // Token: 0x040000AA RID: 170
        private readonly AABB finishBox = new AABB(9f, 50f, 55f, 19f, 54f, 56f);

        // Token: 0x040000A4 RID: 164
        private readonly List<string> maps = new List<string>();

        // Token: 0x040000A7 RID: 167
        private readonly List<Player> players = new List<Player>();

        // Token: 0x040000A6 RID: 166
        private readonly object playersSyncRoot = new object();

        // Token: 0x040000A9 RID: 169
        private readonly AABB preStartBox = new AABB(9f, 44f, 61f, 19f, 45f, 65f);

        // Token: 0x040000A8 RID: 168
        private readonly AABB startBox = new AABB(9f, 44f, 66f, 19f, 46f, 66f);

        // Token: 0x060000FE RID: 254 RVA: 0x00006DB0 File Offset: 0x00004FB0
        public void Start()
        {
            Server.s.Log("Starting in Race Mode!");
            Db.Setup();
            var level = Level.OpenForRaceMode("levels", "wario_run.lvl");
            level.allowHacks = false;
            level.permissionbuild = LevelPermission.Operator;
            if (level == null) throw new NullReferenceException("raceMap == null");
            level.PlayerJoined += raceMap_PlayerJoined;
            Server.mainLevel = level;
            displayTimeForPlayers.Interval = TimeSpan.FromSeconds(1.0).TotalMilliseconds;
            displayTimeForPlayers.Elapsed += displayTimeForPlayers_Elapsed;
            displayTimeForPlayers.Start();
        }

        // Token: 0x060000FF RID: 255 RVA: 0x00006E5C File Offset: 0x0000505C
        private void displayTimeForPlayers_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (playersSyncRoot)
            {
                players.RemoveAll(p => p.disconnected);
                foreach (var player in players)
                    if (player.IsCpeSupported)
                    {
                        var playerRaceState = GetPlayerRaceState(player);
                        string message;
                        if (playerRaceState.StartTime == null || !playerRaceState.HasRaceStarted)
                        {
                            message = "Not started";
                        }
                        else
                        {
                            var timeSpan = DateTime.Now - playerRaceState.StartTime.Value;
                            message = FormatTimeSpanSecondsPrecision(timeSpan) + ".0";
                        }

                        var messageOptions = new V1.MessageOptions();
                        messageOptions.MinDisplayTime = TimeSpan.FromSeconds(3.0);
                        V1.SendMessage(player, V1.MessageType.Status1, null, message);
                    }
            }
        }

        // Token: 0x06000100 RID: 256 RVA: 0x00006F80 File Offset: 0x00005180
        private void raceMap_PlayerJoined(object sender, PlayerJoinedEventArgs e)
        {
            if (e.Player.IsCpeSupported)
            {
                Cpe.V1.MakeSelection(e.Player, 0, "", 9, 44, 66, 19, 46, 66, 0, 255, 0, 64);
                Cpe.V1.MakeSelection(e.Player, 1, "", 9, 50, 55, 19, 52, 55, 0, 0, 255, 64);
            }

            var playerRaceState = SetupPlayerRaceState(e.Player);
            playerRaceState.BestTime = Db.GetBestTime(e.Player);
            if (playerRaceState.BestTime != null && e.Player.IsCpeSupported)
                V1.SendMessageTopRight(e.Player, 2, null,
                    "Best time: " + FormatTimeSpan(playerRaceState.BestTime.Value));
            e.Player.PositionChanged += Player_PositionChanged;
            lock (playersSyncRoot)
            {
                players.Add(e.Player);
            }

            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer(delegate
            {
                e.Player.SendMessage(
                    "This map is called Wario Run and it was made by LeeIzaZombie, Volvagia44 and MineGirlx.");
                e.Player.SendMessage("---------------------");
                e.Player.SendMessage("Welcome to the race!");
                e.Player.SendMessage(
                    "The point of this game is to get from the start through the various obstacles to the finish in the shortest time possible. This game mode is in an early development, so it will get much better in time.");
                e.Player.SendMessage("Tip: If you want to restart the race type: /spawn");
                timer.Dispose();
            }, null, TimeSpan.FromSeconds(5.0), TimeSpan.Zero);
        }

        // Token: 0x06000101 RID: 257 RVA: 0x00007108 File Offset: 0x00005308
        private PlayerRaceState SetupPlayerRaceState(Player player)
        {
            var playerRaceState = new PlayerRaceState();
            player.ExtraData[PlayerRaceStateId] = playerRaceState;
            return playerRaceState;
        }

        // Token: 0x06000102 RID: 258 RVA: 0x00007130 File Offset: 0x00005330
        private static PlayerRaceState GetPlayerRaceState(Player player)
        {
            return (PlayerRaceState) player.ExtraData[PlayerRaceStateId];
        }

        // Token: 0x06000103 RID: 259 RVA: 0x00007148 File Offset: 0x00005348
        private void Player_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            var player = (Player) sender;
            if (startBox.intersects(player.bb))
            {
                var playerRaceState = GetPlayerRaceState(player);
                if (!playerRaceState.HasRaceStarted)
                {
                    playerRaceState.HasRaceStarted = true;
                    playerRaceState.StartTime = DateTime.Now;
                    if (player.IsCpeSupported)
                    {
                        var options = new V1.MessageOptions
                        {
                            DisplayTime = TimeSpan.FromSeconds(2.0),
                            IsBlinking = true
                        };
                        V1.SendMessage(player, V1.MessageType.Announcement, options, "Go!");
                    }
                    else
                    {
                        player.SendMessage("Go!");
                    }

                    Server.s.Log("Player " + player.name + " has started.");
                }
            }
            else if (preStartBox.intersects(player.bb))
            {
                var playerRaceState2 = GetPlayerRaceState(player);
                if (playerRaceState2.HasRaceStarted)
                {
                    playerRaceState2.HasRaceStarted = false;
                    playerRaceState2.StartTime = null;
                    player.SendMessage("Ready, set ...");
                }
            }
            else if (finishBox.intersects(player.bb))
            {
                var playerRaceState3 = GetPlayerRaceState(player);
                if (playerRaceState3.HasRaceStarted)
                {
                    playerRaceState3.HasRaceStarted = false;
                    var timeSpan = DateTime.Now - playerRaceState3.StartTime.Value;
                    if (playerRaceState3.BestTime == null)
                    {
                        Db.SetBestTime(player, timeSpan);
                        playerRaceState3.BestTime = timeSpan;
                        Server.s.Log("Player " + player.name + " has finished race for the first time.");
                        if (player.IsCpeSupported)
                            V1.SendMessageTopRight(player, 2, null, "Best time: " + FormatTimeSpan(timeSpan));
                    }

                    player.SendMessage("You finished the race. Congratulations!");
                    if (playerRaceState3.BestTime.Value > timeSpan)
                    {
                        player.SendMessage("You beat your previous best time!");
                        Db.SetBestTime(player, timeSpan);
                        playerRaceState3.BestTime = timeSpan;
                        Server.s.Log("Player " + player.name + " has beat his best time.");
                        if (player.IsCpeSupported)
                            V1.SendMessageTopRight(player, 2, null, "Best time: " + FormatTimeSpan(timeSpan));
                    }

                    player.SendMessage(string.Format("Your time: " + FormatTimeSpan(timeSpan)));
                    Server.s.Log("Player " + player.name + " has finished with time: " + FormatTimeSpan(timeSpan));
                }
            }
        }

        // Token: 0x06000104 RID: 260 RVA: 0x000073D8 File Offset: 0x000055D8
        private static string FormatTimeSpan(TimeSpan timeSpan)
        {
            return string.Format("{0}:{1}.{2}", AddLeadingZero((int) timeSpan.TotalMinutes),
                AddLeadingZero(timeSpan.Seconds), timeSpan.Milliseconds / 100);
        }

        // Token: 0x06000105 RID: 261 RVA: 0x0000740C File Offset: 0x0000560C
        private static string FormatTimeSpanSecondsPrecision(TimeSpan timeSpan)
        {
            return string.Format("{0}:{1}", AddLeadingZero((int) timeSpan.TotalMinutes),
                AddLeadingZero(timeSpan.Seconds));
        }

        // Token: 0x06000106 RID: 262 RVA: 0x00007434 File Offset: 0x00005634
        private static string AddLeadingZero(int value)
        {
            if (value >= 10) return value.ToString();
            return "0" + value;
        }

        // Token: 0x0200002B RID: 43
        public class PlayerRaceState
        {
            // Token: 0x17000043 RID: 67
            // (get) Token: 0x0600010A RID: 266 RVA: 0x00007528 File Offset: 0x00005728
            // (set) Token: 0x0600010B RID: 267 RVA: 0x00007530 File Offset: 0x00005730
            public bool HasRaceStarted { get; set; }

            // Token: 0x17000044 RID: 68
            // (get) Token: 0x0600010C RID: 268 RVA: 0x0000753C File Offset: 0x0000573C
            // (set) Token: 0x0600010D RID: 269 RVA: 0x00007544 File Offset: 0x00005744
            public DateTime? StartTime { get; set; }

            // Token: 0x17000045 RID: 69
            // (get) Token: 0x0600010E RID: 270 RVA: 0x00007550 File Offset: 0x00005750
            // (set) Token: 0x0600010F RID: 271 RVA: 0x00007558 File Offset: 0x00005758
            public TimeSpan? BestTime { get; set; }
        }

        // Token: 0x0200002C RID: 44
        public static class Db
        {
            // Token: 0x040000B0 RID: 176
            public static readonly int MockupMapId = -1;

            // Token: 0x06000111 RID: 273 RVA: 0x0000756C File Offset: 0x0000576C
            public static void Setup()
            {
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists PlayersBestRunTimes (MapId INTEGER NOT NULL, PlayerId INTEGER NOT NULL, BestTime INTEGER)");
                DBInterface.ExecuteQuery(
                    "CREATE INDEX if not exists PlayersBestRunTimes_PlayerMapIdIndex ON PlayersBestRunTimes(MapId, PlayerId)");
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists MapsBestRunTimes (MapId INTEGER NOT NULL PRIMARY KEY, PlayerId INTEGER NOT NULL, BestTime INTEGER)");
                DBInterface.ExecuteQuery(
                    "CREATE INDEX if not exists MapsBestRunTimes_MapIdIndex ON MapsBestRunTimes(MapId)");
            }

            // Token: 0x06000112 RID: 274 RVA: 0x00007598 File Offset: 0x00005798
            public static void SetBestTime(Player player, TimeSpan bestTime)
            {
                var playerRaceState = GetPlayerRaceState(player);
                var parameters = new Dictionary<string, object>
                {
                    {
                        "@mapid",
                        MockupMapId
                    },
                    {
                        "@playerid",
                        player.DbId
                    },
                    {
                        "@besttime",
                        bestTime.Ticks
                    }
                };
                if (playerRaceState.BestTime == null)
                {
                    DBInterface.ExecuteQuery(
                        "INSERT INTO PlayersBestRunTimes (MapId, PlayerId, BestTime) VALUES (@mapid, @playerid, @besttime)",
                        parameters);
                    return;
                }

                DBInterface.ExecuteQuery(
                    "UPDATE PlayersBestRunTimes SET BestTime=@besttime WHERE MapId=@mapid AND PlayerId=@playerid",
                    parameters);
            }

            // Token: 0x06000113 RID: 275 RVA: 0x00007620 File Offset: 0x00005820
            public static TimeSpan? GetBestTime(Player player)
            {
                TimeSpan? result;
                using (var dataTable = DBInterface.fillData(string.Concat(
                    "SELECT BestTime FROM PlayersBestRunTimes WHERE MapId=", MockupMapId, " AND PlayerId=",
                    player.DbId)))
                {
                    if (dataTable.Rows.Count == 0)
                        result = null;
                    else
                        result = new TimeSpan((long) dataTable.Rows[0][0]);
                }

                return result;
            }

            // Token: 0x06000114 RID: 276 RVA: 0x000076C4 File Offset: 0x000058C4
            public static TimeSpan? GetMapBestTime(Level level)
            {
                throw new NotImplementedException();
            }
        }
    }
}