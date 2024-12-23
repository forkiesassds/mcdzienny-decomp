using System.Data;

namespace MCDzienny
{
    public class CmdResults : Command
    {
        public override string name { get { return "maprating"; } }

        public override string shortcut { get { return "mr"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (p == null)
            {
                Player.SendMessage(null, "You cannot use this command from console.");
                return;
            }
            string text = null;
            string text2 = null;
            if (p.level.mapType == MapType.MyMap)
            {
                text = "SELECT COUNT(*) FROM `Rating" + p.level.name + "` WHERE Vote = 1 AND WHERE Map=" + p.level.MapDbId;
                text2 = "SELECT COUNT(*) FROM `Rating" + p.level.name + "` WHERE Vote = 2 AND WHERE Map=" + p.level.MapDbId;
            }
            else
            {
                text = "SELECT COUNT(*) FROM `Rating" + p.level.name + "` WHERE Vote = 1";
                text2 = "SELECT COUNT(*) FROM `Rating" + p.level.name + "` WHERE Vote = 2";
            }
            using (DataTable dataTable = DBInterface.fillData(text))
            {
                using (DataTable dataTable2 = DBInterface.fillData(text2))
                {
                    Player.SendMessage(
                        p, string.Format("The rating for this map is: %a{0}%s likes, %7{1}%s dislikes.", dataTable.Rows[0]["COUNT(*)"], dataTable2.Rows[0]["COUNT(*)"]));
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/maprating (/mr) - show the rating of the map.");
        }
    }
}