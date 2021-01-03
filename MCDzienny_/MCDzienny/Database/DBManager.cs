using System;
using System.Collections.Generic;
using System.Data;

namespace MCDzienny.Database
{
    // Token: 0x02000092 RID: 146
    public static class DBManager
    {
        // Token: 0x040001E5 RID: 485
        public static readonly string[] KeyType_wonAsZombieTimes =
        {
            "wonAsZombieTimes",
            "MEDIUMINT"
        };

        // Token: 0x040001E6 RID: 486
        public static readonly string[] KeyType_wonAsHumanTimes =
        {
            "wonAsHumanTimes",
            "MEDIUMINT"
        };

        // Token: 0x040001E7 RID: 487
        public static readonly string[] KeyType_zombifiedCount =
        {
            "zombifiedCount",
            "MEDIUMINT"
        };

        // Token: 0x060003DE RID: 990 RVA: 0x00014574 File Offset: 0x00012774
        public static void Initialization()
        {
            try
            {
                if (Server.useMySQL)
                {
                    try
                    {
                        DBInterface.ExecuteQuery("CREATE DATABASE if not exists `" + Server.MySQLDatabaseName + "`",
                            true);
                    }
                    catch (Exception ex)
                    {
                        Server.s.Log(
                            "MySQL settings have not been set! Please reference the MySQL_Setup.txt file on setting up MySQL!");
                        Server.ErrorLog(ex);
                        return;
                    }

                    CreateAlterPlayersTableMySQL();
                    DBInterface.ExecuteQuery(
                        "CREATE TABLE if not exists Stars (Id INTEGER NOT NULL AUTO_INCREMENT, Name VARCHAR(64), GoldStars MEDIUMINT, SilverStars MEDIUMINT, BronzeStars MEDIUMINT, RottenStars MEDIUMINT, PRIMARY KEY (Id), INDEX (Name), INDEX (BronzeStars), INDEX (SilverStars), INDEX (GoldStars), INDEX (RottenStars));");
                    DBInterface.ExecuteQuery(
                        "CREATE TABLE if not exists ZombieRounds ( Id BIGINT NOT NULL PRIMARY KEY AUTO_INCREMENT, DateTime DATETIME, MapName VARCHAR(32), RoundTime SMALLINT, BuildingAllowed TINYINT(1), PillaringAllowed TINYINT(1), Duration SMALLINT, PlayersCountOnStart SMALLINT, PlayersCountOnEnd SMALLINT, WhoWon TINYINT, WinnersCount SMALLINT, INDEX (MapName))");
                    DBInterface.ExecuteQuery(
                        "CREATE TABLE if not exists MapList (Id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, MapName VARCHAR(32), Owner VARCHAR(64), INDEX (MapName, Owner))");
                    DBInterface.ExecuteQuery(
                        "CREATE TABLE if not exists Blocks (Username CHAR(64), Map INT UNSIGNED NOT NULL, TimePerformed DATETIME, X SMALLINT UNSIGNED, Y SMALLINT UNSIGNED, Z SMALLINT UNSIGNED, Type TINYINT UNSIGNED, Deleted BOOL, INDEX (Map,X,Y,Z), INDEX(Map), FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE)");
                    DBInterface.ExecuteQuery(
                        "CREATE TABLE if not exists Portals (Map INT UNSIGNED NOT NULL, EntryX SMALLINT UNSIGNED, EntryY SMALLINT UNSIGNED, EntryZ SMALLINT UNSIGNED, ExitMap CHAR(64), ExitX SMALLINT UNSIGNED, ExitY SMALLINT UNSIGNED, ExitZ SMALLINT UNSIGNED, INDEX(Map), FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE)");
                    DBInterface.ExecuteQuery(
                        "CREATE TABLE if not exists `Messages` (Map INT UNSIGNED NOT NULL, X SMALLINT UNSIGNED, Y SMALLINT UNSIGNED, Z SMALLINT UNSIGNED, Message CHAR(255), INDEX(Map), FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE);");
                    DBInterface.ExecuteQuery(
                        "CREATE TABLE if not exists `Zones` (Map INT UNSIGNED NOT NULL, SmallX SMALLINT UNSIGNED, SmallY SMALLINT UNSIGNED, SmallZ SMALLINT UNSIGNED, BigX SMALLINT UNSIGNED, BigY SMALLINT UNSIGNED, BigZ SMALLINT UNSIGNED, Owner VARCHAR(64), INDEX(Map), FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE);");
                    DBInterface.ExecuteQuery(
                        "CREATE TABLE if not exists `Ratings` (Map INT UNSIGNED NOT NULL, Username CHAR(64), Vote TINYINT, INDEX(Map, Vote), INDEX(Map, Username), INDEX(Map), FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE)");
                    DBInterface.ExecuteQuery(
                        "CREATE TABLE if not exists `PlayerAppearance` (Player MEDIUMINT NOT NULL, Model VARCHAR(64), Skin VARCHAR(64), FOREIGN KEY (Player) REFERENCES Players(ID) ON DELETE CASCADE ON UPDATE CASCADE);");
                    using (var dataTable = DBInterface.fillData("SHOW INDEX FROM Players"))
                    {
                        var flag = true;
                        foreach (DataRow row in dataTable.Rows)
                            if (row["Key_name"].ToString() == "PlayerNameIdx")
                                flag = false;
                        if (flag) DBInterface.ExecuteQuery("CREATE INDEX PlayerNameIdx ON Players (Name)");
                    }

                    return;
                }

                CreateAlterPlayersTableSQLite();
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists Stars (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name VARCHAR(64), GoldStars MEDIUMINT, SilverStars MEDIUMINT, BronzeStars MEDIUMINT, RottenStars MEDIUMINT);");
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists ZombieRounds (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, DateTime DATETIME, MapName VARCHAR(32), RoundTime INTEGER, BuildingAllowed INTEGER, PillaringAllowed INTEGER,  Duration INTEGER, PlayersCountOnStart INTEGER, PlayersCountOnEnd INTEGER, WhoWon INTEGER, WinnersCount INTEGER)");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists PlayersIdx ON Players (Name)");
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists MapList (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, MapName VARCHAR(32), Owner VARCHAR(64))");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists MapNameOwner ON MapList(MapName, Owner)");
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists Blocks (Username CHAR(64), Map INTEGER NOT NULL, TimePerformed DATETIME, X SMALLINT UNSIGNED, Y SMALLINT UNSIGNED, Z SMALLINT UNSIGNED, Type TINYINT UNSIGNED, Deleted BOOL, FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE)");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists BlocksMapIdx ON Blocks(Map)");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists BlocksIndex ON Blocks(Map,X,Y,Z)");
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists `Portals` (Map INTEGER NOT NULL, EntryX SMALLINT UNSIGNED, EntryY SMALLINT UNSIGNED, EntryZ SMALLINT UNSIGNED, ExitMap CHAR(64), ExitX SMALLINT UNSIGNED, ExitY SMALLINT UNSIGNED, ExitZ SMALLINT UNSIGNED, FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE)");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists PortalsMapIdx ON Portals(Map)");
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists `Messages` (Map INTEGER NOT NULL, X SMALLINT UNSIGNED, Y SMALLINT UNSIGNED, Z SMALLINT UNSIGNED, Message CHAR(255), FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE);");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists MessagesMapIdx ON Messages(Map)");
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists `Zones` (Map INTEGER NOT NULL, SmallX SMALLINT UNSIGNED, SmallY SMALLINT UNSIGNED, SmallZ SMALLINT UNSIGNED, BigX SMALLINT UNSIGNED, BigY SMALLINT UNSIGNED, BigZ SMALLINT UNSIGNED, Owner VARCHAR(64), FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE);");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists ZonesMapIdx ON Zones(Map)");
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists `Ratings` (Map INTEGER NOT NULL, Username CHAR(64), Vote TINYINT, FOREIGN KEY (Map) REFERENCES MapList(Id) ON DELETE CASCADE ON UPDATE CASCADE)");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists RatingsVotesIdx ON Ratings (Map, Vote)");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists RatingsMapUsernameIdx ON Ratings (Map, Username)");
                DBInterface.ExecuteQuery("CREATE INDEX if not exists RatingsMapIdx ON Ratings (Map)");
                DBInterface.ExecuteQuery(
                    "CREATE TABLE if not exists `PlayerAppearance` (Player INTEGER NOT NULL, Model VARCHAR(64), Skin VARCHAR(64), FOREIGN KEY (Player) REFERENCES Players(ID) ON DELETE CASCADE ON UPDATE CASCADE);");
                DBInterface.ExecuteQuery(
                    "CREATE INDEX if not exists `PlayerAppearancePlayerIdx` ON PlayerAppearance(Player);");
                using (var dataTable2 = DBInterface.fillData("SELECT * FROM Sqlite_Master WHERE Type = 'index'"))
                {
                    var flag2 = true;
                    var flag3 = true;
                    var flag4 = true;
                    foreach (DataRow row2 in dataTable2.Rows)
                    {
                        if (row2["Name"].ToString() == "StarsGoldStarsIdx") flag2 = false;
                        if (row2["Name"].ToString() == "PlayerNameIdx") flag3 = false;
                        var flag5 = row2["Name"].ToString() == "NotesNameIdx";
                        if (row2["Name"].ToString() == "ZombieRoundsMapNameIdx") flag4 = false;
                        var flag6 = row2["Name"].ToString() == "StarsByWeekGoldStarsIdx";
                    }

                    if (flag2)
                        DBInterface.ExecuteQuery(
                            "CREATE INDEX StarsGoldStarsIdx ON Stars (GoldStars); CREATE INDEX StarsSilverStarsIdx ON Stars (SilverStars); CREATE INDEX StarsBronzeStarsIdx ON Stars (BronzeStars); CREATE INDEX StarsRottenStarsIdx ON Stars (RottenStars); CREATE INDEX StarsNameIdx ON Stars (Name);");
                    if (flag3) DBInterface.ExecuteQuery("CREATE INDEX PlayerNameIdx ON Players (Name);");
                    if (flag4)
                        DBInterface.ExecuteQuery("CREATE INDEX ZombieRoundsMapNameIdx ON ZombieRounds (MapName);");
                }
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }

        // Token: 0x060003DF RID: 991 RVA: 0x0001493C File Offset: 0x00012B3C
        private static void CreateAlterPlayersTableSQLite()
        {
            DBInterface.ExecuteQuery(
                "CREATE TABLE if not exists Players ( ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name VARCHAR(64), IP CHAR(15), FirstLogin DATETIME, LastLogin DATETIME, totalLogin MEDIUMINT, Title CHAR(20), TotalDeaths SMALLINT, Money MEDIUMINT UNSIGNED, totalBlocks BIGINT, totalKicked MEDIUMINT, color VARCHAR(6), title_color VARCHAR(6), totalScore BIGINT, bestScore MEDIUMINT UNSIGNED, timesWon MEDIUMINT UNSIGNED, welcomeMessage VARCHAR(37), farewellMessage VARCHAR(37));");
            using (var dataTable = DBInterface.fillData("PRAGMA table_info(Players)"))
            {
                var list = new List<string>();
                foreach (var obj in dataTable.Rows)
                {
                    var dataRow = (DataRow) obj;
                    list.Add(dataRow["name"].ToString());
                }

                if (!list.Contains("ID"))
                {
                    DBInterface.BeginTransaction();
                    try
                    {
                        DBInterface.TransQuery(
                            "CREATE TABLE Players_ID ( ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name VARCHAR(64), IP CHAR(15), FirstLogin DATETIME, LastLogin DATETIME, totalLogin MEDIUMINT, Title CHAR(20), TotalDeaths SMALLINT, Money MEDIUMINT UNSIGNED, totalBlocks BIGINT, totalKicked MEDIUMINT, color VARCHAR(6), title_color VARCHAR(6), totalScore BIGINT, bestScore MEDIUMINT UNSIGNED, timesWon MEDIUMINT UNSIGNED, welcomeMessage VARCHAR(37), farewellMessage VARCHAR(37), totalMinutesPlayed MEDIUMINT, flags MEDIUMINT, playerExperienceOnZombie BIGINT, wonAsHumanTimes MEDIUMINT, wonAsZombieTimes MEDIUMINT, zombifiedCount MEDIUMINT, roundsOnZombie MEDIUMINT);INSERT INTO Players_ID (Name, IP, FirstLogin, LastLogin, totalLogin, Title, TotalDeaths, Money, totalBlocks, totalKicked, color, title_color, totalScore, bestScore, timesWon, welcomeMessage, farewellMessage, totalMinutesPlayed, flags, playerExperienceOnZombie, wonAsHumanTimes, wonAsZombieTimes, zombifiedCount, roundsOnZombie) SELECT * FROM Players;DROP TABLE Players;ALTER TABLE Players_ID RENAME TO Players;");
                        DBInterface.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        Server.s.Log("Transaction error: " + ex.Message);
                        Server.ErrorLog(ex);
                    }
                    finally
                    {
                        DBInterface.EndTransaction();
                    }
                }

                if (!list.Contains("totalMinutesPlayed"))
                    DBInterface.ExecuteQuery(
                        "ALTER TABLE Players ADD COLUMN totalMinutesPlayed MEDIUMINT AFTER bestScore");
                if (!list.Contains("flags"))
                    DBInterface.ExecuteQuery("ALTER TABLE Players ADD COLUMN flags MEDIUMINT AFTER totalMinutesPlayed");
                if (!list.Contains("playerExperienceOnZombie"))
                    DBInterface.ExecuteQuery(
                        "ALTER TABLE Players ADD COLUMN playerExperienceOnZombie BIGINT AFTER flags");
                SQLiteCreateColumnIfNotExists(KeyType_wonAsHumanTimes[0], KeyType_wonAsHumanTimes[1], list);
                SQLiteCreateColumnIfNotExists(KeyType_wonAsZombieTimes[0], KeyType_wonAsZombieTimes[1], list);
                SQLiteCreateColumnIfNotExists(KeyType_zombifiedCount[0], KeyType_zombifiedCount[1], list);
                SQLiteCreateColumnIfNotExists(DBPlayerColumns.RoundsOnZombie.Name, DBPlayerColumns.RoundsOnZombie.Type,
                    list);
            }
        }

        // Token: 0x060003E0 RID: 992 RVA: 0x00014B10 File Offset: 0x00012D10
        private static void CreateAlterPlayersTableMySQL()
        {
            DBInterface.ExecuteQuery(
                "CREATE TABLE if not exists Players (ID MEDIUMINT not null auto_increment, Name VARCHAR(64), IP CHAR(15), FirstLogin DATETIME, LastLogin DATETIME, totalLogin MEDIUMINT, Title CHAR(20), TotalDeaths SMALLINT, Money MEDIUMINT UNSIGNED, totalBlocks BIGINT, totalKicked MEDIUMINT, color VARCHAR(6), title_color VARCHAR(6), totalScore BIGINT, bestScore MEDIUMINT UNSIGNED, timesWon MEDIUMINT UNSIGNED, welcomeMessage VARCHAR(37), farewellMessage VARCHAR(37), PRIMARY KEY (ID));");
            DBInterface.ExecuteQuery("ALTER TABLE Players MODIFY Name varchar(64);");
            var dataTable = DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field`='timesWon'");
            if (dataTable.Rows.Count == 0)
                DBInterface.ExecuteQuery("ALTER TABLE PLayers ADD COLUMN timesWon MEDIUMINT UNSIGNED AFTER bestScore");
            dataTable.Dispose();
            var dataTable2 = DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field`='welcomeMessage'");
            if (dataTable2.Rows.Count == 0)
                DBInterface.ExecuteQuery("ALTER TABLE PLayers ADD COLUMN welcomeMessage VARCHAR(37) AFTER timesWon");
            dataTable2.Dispose();
            var dataTable3 = DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field`='farewellMessage'");
            if (dataTable3.Rows.Count == 0)
                DBInterface.ExecuteQuery(
                    "ALTER TABLE PLayers ADD COLUMN farewellMessage VARCHAR(37) AFTER welcomeMessage");
            dataTable3.Dispose();
            var dataTable4 = DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field`='totalScore'");
            if (dataTable4.Rows.Count == 0)
                DBInterface.ExecuteQuery("ALTER TABLE PLayers ADD COLUMN totalScore BIGINT AFTER title_color");
            dataTable4.Dispose();
            using (var dataTable5 = DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field` = 'bestScore'"))
            {
                if (dataTable5.Rows.Count == 0)
                    DBInterface.ExecuteQuery("ALTER TABLE Players ADD COLUMN bestScore MEDIUMINT AFTER totalScore");
            }

            using (var dataTable6 =
                DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field` = 'totalMinutesPlayed'"))
            {
                if (dataTable6.Rows.Count == 0)
                    DBInterface.ExecuteQuery(
                        "ALTER TABLE Players ADD COLUMN totalMinutesPlayed MEDIUMINT AFTER bestScore");
            }

            using (var dataTable7 = DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field` = 'flags'"))
            {
                if (dataTable7.Rows.Count == 0)
                    DBInterface.ExecuteQuery("ALTER TABLE Players ADD COLUMN flags MEDIUMINT AFTER totalMinutesPlayed");
            }

            using (var dataTable8 =
                DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field` = 'playerExperienceOnZombie'"))
            {
                if (dataTable8.Rows.Count == 0)
                    DBInterface.ExecuteQuery(
                        "ALTER TABLE Players ADD COLUMN playerExperienceOnZombie BIGINT AFTER totalScore");
            }

            using (var dataTable9 =
                DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field` = 'playerExperienceOnZombie'"))
            {
                if (dataTable9.Rows.Count == 0)
                    DBInterface.ExecuteQuery(
                        "ALTER TABLE Players ADD COLUMN playerExperienceOnZombie BIGINT AFTER totalScore");
            }

            MySQLCreateColumnIfNotExists(KeyType_wonAsHumanTimes[0], KeyType_wonAsHumanTimes[1]);
            MySQLCreateColumnIfNotExists(KeyType_wonAsZombieTimes[0], KeyType_wonAsZombieTimes[1]);
            MySQLCreateColumnIfNotExists(KeyType_zombifiedCount[0], KeyType_zombifiedCount[1]);
            MySQLCreateColumnIfNotExists(DBPlayerColumns.RoundsOnZombie.Name, DBPlayerColumns.RoundsOnZombie.Type);
        }

        // Token: 0x060003E1 RID: 993 RVA: 0x00014D60 File Offset: 0x00012F60
        private static void MySQLCreateColumnIfNotExists(string columnName, string columnType)
        {
            using (var dataTable =
                DBInterface.fillData("SHOW COLUMNS FROM Players WHERE `Field` = '" + columnName + "'"))
            {
                if (dataTable.Rows.Count == 0)
                    DBInterface.ExecuteQuery(string.Concat("ALTER TABLE Players ADD COLUMN ", columnName, " ",
                        columnType, " AFTER flags"));
            }
        }

        // Token: 0x060003E2 RID: 994 RVA: 0x00014DE0 File Offset: 0x00012FE0
        private static void SQLiteCreateColumnIfNotExists(string columnName, string columnType,
            List<string> currentColumnNames)
        {
            if (!currentColumnNames.Contains(columnName))
                DBInterface.ExecuteQuery(string.Concat("ALTER TABLE Players ADD COLUMN ", columnName, " ", columnType,
                    " AFTER flags"));
        }
    }
}