using System;
using System.Collections.Generic;
using System.Data;
using System.Timers;
using MCDzienny.CpeApi;

namespace MCDzienny.Games
{
    public class Race
    {

        public static readonly string PlayerRaceStateId = "player_race_state_id";

        readonly Timer displayTimeForPlayers = new Timer();

        readonly AABB finishBox = new AABB(9f, 50f, 55f, 19f, 54f, 56f);

        readonly List<string> maps = new List<string>();

        readonly List<Player> players = new List<Player>();

        readonly object playersSyncRoot = new object();

        readonly AABB preStartBox = new AABB(9f, 44f, 61f, 19f, 45f, 65f);

        readonly AABB startBox = new AABB(9f, 44f, 66f, 19f, 46f, 66f);

        public void Start()
        {
            Server.s.Log("Starting in Race Mode!");
            Db.Setup();
            Level level = Level.OpenForRaceMode("levels", "wario_run.lvl");
            level.allowHacks = false;
            level.permissionbuild = LevelPermission.Operator;
            if (level == null)
            {
                throw new NullReferenceException("raceMap == null");
            }
            level.PlayerJoined += raceMap_PlayerJoined;
            Server.mainLevel = level;
            displayTimeForPlayers.Interval = TimeSpan.FromSeconds(1.0).TotalMilliseconds;
            displayTimeForPlayers.Elapsed += displayTimeForPlayers_Elapsed;
            displayTimeForPlayers.Start();
        }

        void displayTimeForPlayers_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (playersSyncRoot)
            {
                players.RemoveAll(p => p.disconnected);
                foreach (Player player in players)
                {
                    if (player.IsCpeSupported)
                    {
                        PlayerRaceState playerRaceState = GetPlayerRaceState(player);
                        string message;
                        if (!playerRaceState.StartTime.HasValue || !playerRaceState.HasRaceStarted)
                        {
                            message = "Not started";
                        }
                        else
                        {
                            TimeSpan timeSpan = DateTime.Now - playerRaceState.StartTime.Value;
                            message = FormatTimeSpanSecondsPrecision(timeSpan) + ".0";
                        }
                        V1.MessageOptions messageOptions = new V1.MessageOptions();
                        messageOptions.MinDisplayTime = TimeSpan.FromSeconds(3.0);
                        V1.SendMessage(player, V1.MessageType.Status1, null, message);
                    }
                }
            }
        }

        void raceMap_PlayerJoined(object sender, PlayerJoinedEventArgs e)
        {
            if (e.Player.IsCpeSupported)
            {
                Cpe.V1.MakeSelection(e.Player, 0, "", 9, 44, 66, 19, 46, 66, 0, 255, 0, 64);
                Cpe.V1.MakeSelection(e.Player, 1, "", 9, 50, 55, 19, 52, 55, 0, 0, 255, 64);
            }
            PlayerRaceState playerRaceState = SetupPlayerRaceState(e.Player);
            playerRaceState.BestTime = Db.GetBestTime(e.Player);
            if (playerRaceState.BestTime.HasValue && e.Player.IsCpeSupported)
            {
                V1.SendMessageTopRight(e.Player, 2, null, "Best time: " + FormatTimeSpan(playerRaceState.BestTime.Value));
            }
            e.Player.PositionChanged += Player_PositionChanged;
            lock (playersSyncRoot)
            {
                players.Add(e.Player);
            }
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer(delegate
            {
                e.Player.SendMessage("This map is called Wario Run and it was made by LeeIzaZombie, Volvagia44 and MineGirlx.");
                e.Player.SendMessage("---------------------");
                e.Player.SendMessage("Welcome to the race!");
                e.Player.SendMessage(
                    "The point of this game is to get from the start through the various obstacles to the finish in the shortest time possible. This game mode is in an early development, so it will get much better in time.");
                e.Player.SendMessage("Tip: If you want to restart the race type: /spawn");
                timer.Dispose();
            }, null, TimeSpan.FromSeconds(5.0), TimeSpan.Zero);
        }

        PlayerRaceState SetupPlayerRaceState(Player player)
        {
            PlayerRaceState playerRaceState = new PlayerRaceState();
            player.ExtraData[PlayerRaceStateId] = playerRaceState;
            return playerRaceState;
        }

        static PlayerRaceState GetPlayerRaceState(Player player)
        {
            return (PlayerRaceState)player.ExtraData[PlayerRaceStateId];
        }

        void Player_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            Player player = (Player)sender;
            if (startBox.intersects(player.bb))
            {
                PlayerRaceState playerRaceState = GetPlayerRaceState(player);
                if (!playerRaceState.HasRaceStarted)
                {
                    playerRaceState.HasRaceStarted = true;
                    playerRaceState.StartTime = DateTime.Now;
                    if (player.IsCpeSupported)
                    {
                        V1.MessageOptions messageOptions = new V1.MessageOptions();
                        messageOptions.DisplayTime = TimeSpan.FromSeconds(2.0);
                        messageOptions.IsBlinking = true;
                        V1.MessageOptions options = messageOptions;
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
                PlayerRaceState playerRaceState2 = GetPlayerRaceState(player);
                if (playerRaceState2.HasRaceStarted)
                {
                    playerRaceState2.HasRaceStarted = false;
                    playerRaceState2.StartTime = null;
                    player.SendMessage("Ready, set ...");
                }
            }
            else
            {
                if (!finishBox.intersects(player.bb))
                {
                    return;
                }
                PlayerRaceState playerRaceState3 = GetPlayerRaceState(player);
                if (!playerRaceState3.HasRaceStarted)
                {
                    return;
                }
                playerRaceState3.HasRaceStarted = false;
                TimeSpan timeSpan = DateTime.Now - playerRaceState3.StartTime.Value;
                if (!playerRaceState3.BestTime.HasValue)
                {
                    Db.SetBestTime(player, timeSpan);
                    playerRaceState3.BestTime = timeSpan;
                    Server.s.Log("Player " + player.name + " has finished race for the first time.");
                    if (player.IsCpeSupported)
                    {
                        V1.SendMessageTopRight(player, 2, null, "Best time: " + FormatTimeSpan(timeSpan));
                    }
                }
                player.SendMessage("You finished the race. Congratulations!");
                if (playerRaceState3.BestTime.Value > timeSpan)
                {
                    player.SendMessage("You beat your previous best time!");
                    Db.SetBestTime(player, timeSpan);
                    playerRaceState3.BestTime = timeSpan;
                    Server.s.Log("Player " + player.name + " has beat his best time.");
                    if (player.IsCpeSupported)
                    {
                        V1.SendMessageTopRight(player, 2, null, "Best time: " + FormatTimeSpan(timeSpan));
                    }
                }
                player.SendMessage(string.Format("Your time: " + FormatTimeSpan(timeSpan)));
                Server.s.Log("Player " + player.name + " has finished with time: " + FormatTimeSpan(timeSpan));
            }
        }

        static string FormatTimeSpan(TimeSpan timeSpan)
        {
            return string.Format("{0}:{1}.{2}", AddLeadingZero((int)timeSpan.TotalMinutes), AddLeadingZero(timeSpan.Seconds), timeSpan.Milliseconds / 100);
        }

        static string FormatTimeSpanSecondsPrecision(TimeSpan timeSpan)
        {
            return string.Format("{0}:{1}", AddLeadingZero((int)timeSpan.TotalMinutes), AddLeadingZero(timeSpan.Seconds));
        }

        static string AddLeadingZero(int value)
        {
            if (value >= 10)
            {
                return value.ToString();
            }
            return "0" + value;
        }

        public class PlayerRaceState
        {
            public bool HasRaceStarted { get; set; }

            public DateTime? StartTime { get; set; }

            public TimeSpan? BestTime { get; set; }
        }

        public static class Db
        {
            public static readonly int MockupMapId = -1;

            public static void Setup()
            {
                DBInterface.ExecuteQuery("CREATE TABLE if not exists PlayersBestRunTimes (MapId INTEGER NOT NULL, PlayerId INTEGER NOT NULL, BestTime INTEGER)");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists PlayersBestRunTimes_PlayerMapIdIndex ON PlayersBestRunTimes(MapId, PlayerId)");
                DBInterface.ExecuteQuery("CREATE TABLE if not exists MapsBestRunTimes (MapId INTEGER NOT NULL PRIMARY KEY, PlayerId INTEGER NOT NULL, BestTime INTEGER)");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists MapsBestRunTimes_MapIdIndex ON MapsBestRunTimes(MapId)");
            }

            public static void SetBestTime(Player player, TimeSpan bestTime)
            {
                PlayerRaceState playerRaceState = GetPlayerRaceState(player);
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("@mapid", MockupMapId);
                dictionary.Add("@playerid", player.DbId);
                dictionary.Add("@besttime", bestTime.Ticks);
                Dictionary<string, object> parameters = dictionary;
                if (!playerRaceState.BestTime.HasValue)
                {
                    DBInterface.ExecuteQuery("INSERT INTO PlayersBestRunTimes (MapId, PlayerId, BestTime) VALUES (@mapid, @playerid, @besttime)", parameters);
                }
                else
                {
                    DBInterface.ExecuteQuery("UPDATE PlayersBestRunTimes SET BestTime=@besttime WHERE MapId=@mapid AND PlayerId=@playerid", parameters);
                }
            }

            public static TimeSpan? GetBestTime(Player player)
            {
                using (DataTable dataTable = DBInterface.fillData("SELECT BestTime FROM PlayersBestRunTimes WHERE MapId=" + MockupMapId + " AND PlayerId=" + player.DbId))
                {
                    if (dataTable.Rows.Count == 0)
                    {
                        return null;
                    }
                    return new TimeSpan((long)dataTable.Rows[0][0]);
                }
            }

            public static TimeSpan? GetMapBestTime(Level level)
            {
                throw new NotImplementedException();
            }
        }
    }
}