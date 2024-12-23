using System;
using System.Data;
using System.IO;
using System.Linq;
using MCDzienny.InfectionSystem;

namespace MCDzienny
{
    public class CmdMapInfo : Command
    {
        public override string name { get { return "mapinfo"; } }

        public override string shortcut { get { return "mi"; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        static string FlatBar(string colorCode, double percentage)
        {
            int num = (int)(percentage / 4.0 + 0.5);
            if (num == 25)
            {
                return "_________________________";
            }
            string text = "_________________________";
            return text.Insert(num, colorCode);
        }

        static string GrayBar(double percentage)
        {
            return FlatBar("%7", percentage);
        }

        static string RedBar(double percentage)
        {
            return FlatBar("%c", percentage);
        }

        public override void Use(Player p, string message)
        {
            Level foundLevel;
            if (message == "")
            {
                if (p == null)
                {
                    Help(p);
                    return;
                }
                foundLevel = p.level;
            }
            else
            {
                foundLevel = Level.Find(message);
            }
            if (foundLevel == null)
            {
                Player.SendMessage(p, "Could not find specified level.");
                return;
            }
            if (foundLevel.mapType == MapType.Zombie)
            {
                try
                {
                    InfectionMaps.InfectionMap infectionMap = InfectionMaps.infectionMaps.SingleOrDefault(m => m.Name == foundLevel.name);
                    if (infectionMap != null)
                    {
                        MapInfoForZombie(p, foundLevel, infectionMap);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                    return;
                }
            }
            Player.SendMessage(p, "Map name: %b" + Player.RemoveEmailDomain(foundLevel.name));
            if (foundLevel.mapType == MapType.MyMap)
            {
                Player.SendMessage(p, "Owner: %b" + Player.RemoveEmailDomain(foundLevel.Owner));
            }
            Player.SendMessage(p, string.Format("Size: Width={0} Height={1} Depth={2}", foundLevel.width, foundLevel.height, foundLevel.depth));
            switch (foundLevel.physics)
            {
                case 0:
                    Player.SendMessage(p, string.Format("Physics are &cOFF%s on &b{0}", foundLevel.name));
                    break;
                case 1:
                    Player.SendMessage(p, string.Format("Physics are &aNormal%s on &b{0}", foundLevel.name));
                    break;
                case 2:
                    Player.SendMessage(p, string.Format("Physics are &aAdvanced%s on &b{0}", foundLevel.name));
                    break;
                case 3:
                    Player.SendMessage(p, string.Format("Physics are &aHardcore%s on &b{0}", foundLevel.name));
                    break;
                case 4:
                    Player.SendMessage(p, string.Format("Physics are &aInstant%s on &b{0}", foundLevel.name));
                    break;
            }
            try
            {
                Player.SendMessage(
                    p,
                    string.Format("Build rank = {0}%s : Visit rank = {1}",
                                  Group.findPerm(foundLevel.permissionbuild).color + Group.findPerm(foundLevel.permissionbuild).trueName,
                                  Group.findPerm(foundLevel.permissionvisit).color + Group.findPerm(foundLevel.permissionvisit).trueName));
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
            if (Directory.Exists(Server.backupLocation + "/" + foundLevel.name))
            {
                int num = Directory.GetDirectories(Server.backupLocation + "/" + foundLevel.name).Length;
                Player.SendMessage(
                    p,
                    string.Format("Latest backup: &a{0} at &a{1}", num + Server.DefaultColor,
                                  Directory.GetCreationTime(Server.backupLocation + "/" + foundLevel.name + "/" + num).ToString("yyyy-MM-dd HH:mm:ss")));
            }
            else
            {
                Player.SendMessage(p, "No backups for this map exist yet.");
            }
        }

        void MapInfoForZombie(Player p, Level l, InfectionMaps.InfectionMap map)
        {
            int result = 0;
            int result2 = 0;
            using (DataTable dataTable = DBInterface.fillData("SELECT COUNT(*) FROM `Rating" + l.name + "` WHERE Vote = 1"))
            {
                using (DataTable dataTable2 = DBInterface.fillData("SELECT COUNT(*) FROM `Rating" + l.name + "` WHERE Vote = 2"))
                {
                    int.TryParse(dataTable.Rows[0]["COUNT(*)"].ToString(), out result);
                    int.TryParse(dataTable2.Rows[0]["COUNT(*)"].ToString(), out result2);
                }
            }
            int result3 = 0;
            int result4 = 0;
            using (DataTable dataTable3 = DBInterface.fillData(string.Format("SELECT COUNT(*) FROM ZombieRounds WHERE MapName = '{0}' AND WhoWon = 0", l.name)))
            {
                using (DataTable dataTable4 = DBInterface.fillData(string.Format("SELECT COUNT(*) FROM ZombieRounds WHERE MapName = '{0}' AND WhoWon = 1", l.name)))
                {
                    int.TryParse(dataTable3.Rows[0]["COUNT(*)"].ToString(), out result3);
                    int.TryParse(dataTable4.Rows[0]["COUNT(*)"].ToString(), out result4);
                }
            }
            Player.SendMessage(p, "_____________ Map Info _____________");
            Player.SendMessage(p, "| Name: %a{0}%s Author: %a{1}", map.Name, map.Author);
            int num = result3 + result4;
            Player.SendMessage(p, "| Round time: %a{0}min %sRounds played: %a{1}", map.RoundTimeMinutes, num);
            int num2 = result + result2;
            if (num2 > 0)
            {
                Player.SendMessage(p, "| [%a{0}%s]", GrayBar(result / (double)num2 * 100.0));
                Player.SendMessage(p, "| Rating: %a{0}%s likes, %7{1}%s dislikes ({2}%%s)", result, result2, (int)(result / (double)num2 * 100.0));
            }
            else
            {
                Player.SendMessage(p, "| Rating: %a{0}%s likes, %7{1}%s dislikes.", result, result2);
            }
            if (num > 0)
            {
                double num3 = result4 / (double)num;
                Player.SendMessage(p, "| [%a{0}%s]", RedBar(num3 * 100.0));
                Player.SendMessage(p, "| Win ratio: %a{0} %shumans, %c{1}%s zombies ({2}%%s)", result4, result3, (int)(num3 * 100.0));
            }
            else
            {
                Player.SendMessage(p, "| Win ratio: %a{0} %shumans, %7{1}%s zombies.", result4, result3);
            }
            Player.SendMessage(p, "| Size (WxHxD): {0}x{1}x{2}", l.width, l.height, l.depth);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mapinfo <map> - Display details of <map>");
        }
    }
}