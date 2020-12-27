using System.Data;
using MCDzienny.Misc;

namespace MCDzienny
{
    // Token: 0x0200008D RID: 141
    internal class CmdStars : Command
    {
        // Token: 0x1700011E RID: 286
        // (get) Token: 0x060003B9 RID: 953 RVA: 0x00013B8C File Offset: 0x00011D8C
        public override string name
        {
            get { return "stars"; }
        }

        // Token: 0x1700011F RID: 287
        // (get) Token: 0x060003BA RID: 954 RVA: 0x00013B94 File Offset: 0x00011D94
        public override string shortcut
        {
            get { return "star"; }
        }

        // Token: 0x17000120 RID: 288
        // (get) Token: 0x060003BB RID: 955 RVA: 0x00013B9C File Offset: 0x00011D9C
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x17000121 RID: 289
        // (get) Token: 0x060003BC RID: 956 RVA: 0x00013BA4 File Offset: 0x00011DA4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000122 RID: 290
        // (get) Token: 0x060003BD RID: 957 RVA: 0x00013BA8 File Offset: 0x00011DA8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000123 RID: 291
        // (get) Token: 0x060003BE RID: 958 RVA: 0x00013BAC File Offset: 0x00011DAC
        public override bool ConsoleAccess
        {
            get { return true; }
        }

        // Token: 0x17000124 RID: 292
        // (get) Token: 0x060003BF RID: 959 RVA: 0x00013BB0 File Offset: 0x00011DB0
        public override CommandScope Scope
        {
            get { return CommandScope.Zombie; }
        }

        // Token: 0x060003C0 RID: 960 RVA: 0x00013BB4 File Offset: 0x00011DB4
        public override void Use(Player p, string message)
        {
            if (message == "" && p == null)
            {
                Player.SendMessage(p, "Type /stars [player] to check player's stars count.");
                Player.SendMessage(p, "Or /stars top to see best players.");
                return;
            }

            message = message.ToLower();
            if (message == "top")
            {
                ShowBestPlayers(p);
                return;
            }

            if (message == "top zombies")
            {
                ShowBestZombies(p);
                return;
            }

            if (message == "top humans")
            {
                ShowBestHumans(p);
                return;
            }

            Player player;
            if (message == "")
            {
                player = p;
            }
            else
            {
                player = Player.Find(message);
                if (player == null)
                {
                    Player.SendMessage(p, "Could not find the player named " + message);
                    return;
                }
            }

            Player.SendMessage(p, "--------------------------");
            if (player == p)
                Player.SendMessage(p, "Your stars count:");
            else
                Player.SendMessage(p, player.color + player.PublicName + "%s stars count:");
            var num = (int) player.ExtraData["gold_stars_count"] + (int) player.ExtraData["silver_stars_count"] +
                      (int) player.ExtraData["bronze_stars_count"] + (int) player.ExtraData["rotten_stars_count"];
            if (num == 0)
            {
                Player.SendMessage(p, "%c0");
                return;
            }

            if ((int) player.ExtraData["rotten_stars_count"] > 0)
                Player.SendMessage(p,
                    string.Concat("Rotten stars ", MCColor.DarkGreen, "* ", player.ExtraData["rotten_stars_count"]));
            if ((int) player.ExtraData["bronze_stars_count"] > 0)
                Player.SendMessage(p, "Bronze stars %4* " + player.ExtraData["bronze_stars_count"]);
            if ((int) player.ExtraData["silver_stars_count"] > 0)
                Player.SendMessage(p, "Silver stars %7* " + player.ExtraData["silver_stars_count"]);
            if ((int) player.ExtraData["gold_stars_count"] > 0)
                Player.SendMessage(p, "Gold stars %6* " + player.ExtraData["gold_stars_count"]);
            Player.SendMessage(p, "Total: " + num);
        }

        // Token: 0x060003C1 RID: 961 RVA: 0x00013E18 File Offset: 0x00012018
        private void ShowBestHumans(Player p)
        {
            Player.SendMessage(p, "--------------------------");
            using (var dataTable =
                DBInterface.fillData(
                    "SELECT * FROM Stars ORDER BY GoldStars DESC, SilverStars DESC, BronzeStars DESC LIMIT 10"))
            {
                Player.SendMessage(p, "Best Humans:");
                var num = 1;
                foreach (var obj in dataTable.Rows)
                {
                    var dataRow = (DataRow) obj;
                    Player.SendMessage(p,
                        string.Format("{0}. {1} %6* {2} %7* {3} %4* {4}", num, RemoveDomain(dataRow["Name"].ToString()),
                            dataRow["GoldStars"], dataRow["SilverStars"], dataRow["BronzeStars"]));
                    num++;
                }
            }
        }

        // Token: 0x060003C2 RID: 962 RVA: 0x00013F1C File Offset: 0x0001211C
        private void ShowBestZombies(Player p)
        {
            Player.SendMessage(p, "--------------------------");
            using (var dataTable = DBInterface.fillData("SELECT * FROM Stars ORDER BY RottenStars DESC LIMIT 10"))
            {
                Player.SendMessage(p, "Best Zombies:");
                var num = 1;
                foreach (var obj in dataTable.Rows)
                {
                    var dataRow = (DataRow) obj;
                    Player.SendMessage(p,
                        string.Format("{0}. {1} %2* {2}", num, RemoveDomain(dataRow["Name"].ToString()),
                            dataRow["RottenStars"]));
                    num++;
                }
            }
        }

        // Token: 0x060003C3 RID: 963 RVA: 0x00013FE8 File Offset: 0x000121E8
        private void ShowBestPlayers(Player p)
        {
            Player.SendMessage(p, "--------------------------");
            using (var dataTable =
                DBInterface.fillData(
                    "SELECT * FROM Stars ORDER BY GoldStars DESC, SilverStars DESC, BronzeStars DESC LIMIT 3"))
            {
                Player.SendMessage(p, "Best Humans:");
                var num = 1;
                foreach (var obj in dataTable.Rows)
                {
                    var dataRow = (DataRow) obj;
                    Player.SendMessage(p,
                        string.Format("{0}. {1} %6* {2} %7* {3} %4* {4}", num, RemoveDomain(dataRow["Name"].ToString()),
                            dataRow["GoldStars"], dataRow["SilverStars"], dataRow["BronzeStars"]));
                    num++;
                }
            }

            Player.SendMessage(p, "--------------------------");
            using (var dataTable2 = DBInterface.fillData("SELECT * FROM Stars ORDER BY RottenStars DESC LIMIT 3"))
            {
                Player.SendMessage(p, "Best Zombies:");
                var num2 = 1;
                foreach (var obj2 in dataTable2.Rows)
                {
                    var dataRow2 = (DataRow) obj2;
                    Player.SendMessage(p,
                        string.Format("{0}. {1} %2* {2}", num2, RemoveDomain(dataRow2["Name"].ToString()),
                            dataRow2["RottenStars"]));
                    num2++;
                }
            }
        }

        // Token: 0x060003C4 RID: 964 RVA: 0x000141B4 File Offset: 0x000123B4
        private static string RemoveDomain(string name)
        {
            var num = name.IndexOf('@');
            if (num == -1) return name;
            return name.Substring(0, num + 1);
        }

        // Token: 0x060003C5 RID: 965 RVA: 0x000141DC File Offset: 0x000123DC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/stars - displays your stars count.");
            Player.SendMessage(p, "/stars [player] - checks stars of a player.");
            Player.SendMessage(p, "/stars top - shows the best players.");
            Player.SendMessage(p, "/stars top zombies - top 10 zombies,");
            Player.SendMessage(p, "/stars top humans - top 10 humans.");
        }
    }
}