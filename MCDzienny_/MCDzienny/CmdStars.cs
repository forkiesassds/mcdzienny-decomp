using System.Data;
using MCDzienny.Misc;

namespace MCDzienny
{
    class CmdStars : Command
    {
        public override string name { get { return "stars"; } }

        public override string shortcut { get { return "star"; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return true; } }

        public override CommandScope Scope { get { return CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            if (message == "" && p == null)
            {
                Player.SendMessage(p, "Type /stars [player] to check player's stars count.");
                Player.SendMessage(p, "Or /stars top to see best players.");
                return;
            }
            message = message.ToLower();
            switch (message)
            {
                case "top":
                    ShowBestPlayers(p);
                    return;
                case "top zombies":
                    ShowBestZombies(p);
                    return;
                case "top humans":
                    ShowBestHumans(p);
                    return;
            }
            Player player = null;
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
            {
                Player.SendMessage(p, "Your stars count:");
            }
            else
            {
                Player.SendMessage(p, player.color + player.PublicName + "%s stars count:");
            }
            int num = (int)player.ExtraData["gold_stars_count"] + (int)player.ExtraData["silver_stars_count"] + (int)player.ExtraData["bronze_stars_count"] +
                (int)player.ExtraData["rotten_stars_count"];
            if (num == 0)
            {
                Player.SendMessage(p, "%c0");
                return;
            }
            if ((int)player.ExtraData["rotten_stars_count"] > 0)
            {
                Player.SendMessage(p, "Rotten stars " + MCColor.DarkGreen + "* " + player.ExtraData["rotten_stars_count"]);
            }
            if ((int)player.ExtraData["bronze_stars_count"] > 0)
            {
                Player.SendMessage(p, "Bronze stars %4* " + player.ExtraData["bronze_stars_count"]);
            }
            if ((int)player.ExtraData["silver_stars_count"] > 0)
            {
                Player.SendMessage(p, "Silver stars %7* " + player.ExtraData["silver_stars_count"]);
            }
            if ((int)player.ExtraData["gold_stars_count"] > 0)
            {
                Player.SendMessage(p, "Gold stars %6* " + player.ExtraData["gold_stars_count"]);
            }
            Player.SendMessage(p, "Total: " + num);
        }

        void ShowBestHumans(Player p)
        {
            Player.SendMessage(p, "--------------------------");
            using (DataTable dataTable = DBInterface.fillData("SELECT * FROM Stars ORDER BY GoldStars DESC, SilverStars DESC, BronzeStars DESC LIMIT 10"))
            {
                Player.SendMessage(p, "Best Humans:");
                int num = 1;
                foreach (DataRow row in dataTable.Rows)
                {
                    Player.SendMessage(
                        p,
                        string.Format("{0}. {1} %6* {2} %7* {3} %4* {4}", num, RemoveDomain(row["Name"].ToString()), row["GoldStars"], row["SilverStars"],
                                      row["BronzeStars"]));
                    num++;
                }
            }
        }

        void ShowBestZombies(Player p)
        {
            Player.SendMessage(p, "--------------------------");
            using (DataTable dataTable = DBInterface.fillData("SELECT * FROM Stars ORDER BY RottenStars DESC LIMIT 10"))
            {
                Player.SendMessage(p, "Best Zombies:");
                int num = 1;
                foreach (DataRow row in dataTable.Rows)
                {
                    Player.SendMessage(p, string.Format("{0}. {1} %2* {2}", num, RemoveDomain(row["Name"].ToString()), row["RottenStars"]));
                    num++;
                }
            }
        }

        void ShowBestPlayers(Player p)
        {
            Player.SendMessage(p, "--------------------------");
            using (DataTable dataTable = DBInterface.fillData("SELECT * FROM Stars ORDER BY GoldStars DESC, SilverStars DESC, BronzeStars DESC LIMIT 3"))
            {
                Player.SendMessage(p, "Best Humans:");
                int num = 1;
                foreach (DataRow row in dataTable.Rows)
                {
                    Player.SendMessage(
                        p,
                        string.Format("{0}. {1} %6* {2} %7* {3} %4* {4}", num, RemoveDomain(row["Name"].ToString()), row["GoldStars"], row["SilverStars"],
                                      row["BronzeStars"]));
                    num++;
                }
            }
            Player.SendMessage(p, "--------------------------");
            using (DataTable dataTable2 = DBInterface.fillData("SELECT * FROM Stars ORDER BY RottenStars DESC LIMIT 3"))
            {
                Player.SendMessage(p, "Best Zombies:");
                int num2 = 1;
                foreach (DataRow row2 in dataTable2.Rows)
                {
                    Player.SendMessage(p, string.Format("{0}. {1} %2* {2}", num2, RemoveDomain(row2["Name"].ToString()), row2["RottenStars"]));
                    num2++;
                }
            }
        }

        static string RemoveDomain(string name)
        {
            int num = name.IndexOf('@');
            if (num == -1)
            {
                return name;
            }
            return name.Substring(0, num + 1);
        }

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