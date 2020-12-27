using System;
using System.IO;
using System.Linq;
using MCDzienny.InfectionSystem;

namespace MCDzienny
{
    // Token: 0x02000273 RID: 627
    public class CmdMapInfo : Command
    {
        // Token: 0x1700069B RID: 1691
        // (get) Token: 0x060011FF RID: 4607 RVA: 0x000635B4 File Offset: 0x000617B4
        public override string name
        {
            get { return "mapinfo"; }
        }

        // Token: 0x1700069C RID: 1692
        // (get) Token: 0x06001200 RID: 4608 RVA: 0x000635BC File Offset: 0x000617BC
        public override string shortcut
        {
            get { return "mi"; }
        }

        // Token: 0x1700069D RID: 1693
        // (get) Token: 0x06001201 RID: 4609 RVA: 0x000635C4 File Offset: 0x000617C4
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700069E RID: 1694
        // (get) Token: 0x06001202 RID: 4610 RVA: 0x000635CC File Offset: 0x000617CC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700069F RID: 1695
        // (get) Token: 0x06001203 RID: 4611 RVA: 0x000635D0 File Offset: 0x000617D0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x06001204 RID: 4612 RVA: 0x000635D4 File Offset: 0x000617D4
        private static string FlatBar(string colorCode, double percentage)
        {
            var num = (int) (percentage / 4.0 + 0.5);
            if (num == 25) return "_________________________";
            var text = "_________________________";
            return text.Insert(num, colorCode);
        }

        // Token: 0x06001205 RID: 4613 RVA: 0x00063614 File Offset: 0x00061814
        private static string GrayBar(double percentage)
        {
            return FlatBar("%7", percentage);
        }

        // Token: 0x06001206 RID: 4614 RVA: 0x00063624 File Offset: 0x00061824
        private static string RedBar(double percentage)
        {
            return FlatBar("%c", percentage);
        }

        // Token: 0x06001207 RID: 4615 RVA: 0x00063634 File Offset: 0x00061834
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
                try
                {
                    var infectionMap = InfectionMaps.infectionMaps.SingleOrDefault(m => m.Name == foundLevel.name);
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

            Player.SendMessage(p, "Map name: %b" + Player.RemoveEmailDomain(foundLevel.name));
            if (foundLevel.mapType == MapType.MyMap)
                Player.SendMessage(p, "Owner: %b" + Player.RemoveEmailDomain(foundLevel.Owner));
            Player.SendMessage(p,
                string.Format("Size: Width={0} Height={1} Depth={2}", foundLevel.width, foundLevel.height,
                    foundLevel.depth));
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
                Player.SendMessage(p,
                    string.Format("Build rank = {0}%s : Visit rank = {1}",
                        Group.findPerm(foundLevel.permissionbuild).color +
                        Group.findPerm(foundLevel.permissionbuild).trueName,
                        Group.findPerm(foundLevel.permissionvisit).color +
                        Group.findPerm(foundLevel.permissionvisit).trueName));
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }

            if (Directory.Exists(Server.backupLocation + "/" + foundLevel.name))
            {
                var num = Directory.GetDirectories(Server.backupLocation + "/" + foundLevel.name).Length;
                Player.SendMessage(p,
                    string.Format("Latest backup: &a{0} at &a{1}", num + Server.DefaultColor,
                        Directory.GetCreationTime(string.Concat(Server.backupLocation, "/", foundLevel.name, "/", num))
                            .ToString("yyyy-MM-dd HH:mm:ss")));
                return;
            }

            Player.SendMessage(p, "No backups for this map exist yet.");
        }

        // Token: 0x06001208 RID: 4616 RVA: 0x000639A4 File Offset: 0x00061BA4
        private void MapInfoForZombie(Player p, Level l, InfectionMaps.InfectionMap map)
        {
            var num = 0;
            var num2 = 0;
            using (var dataTable = DBInterface.fillData("SELECT COUNT(*) FROM `Rating" + l.name + "` WHERE Vote = 1"))
            {
                using (var dataTable2 =
                    DBInterface.fillData("SELECT COUNT(*) FROM `Rating" + l.name + "` WHERE Vote = 2"))
                {
                    int.TryParse(dataTable.Rows[0]["COUNT(*)"].ToString(), out num);
                    int.TryParse(dataTable2.Rows[0]["COUNT(*)"].ToString(), out num2);
                }
            }

            var num3 = 0;
            var num4 = 0;
            using (var dataTable3 =
                DBInterface.fillData(
                    string.Format("SELECT COUNT(*) FROM ZombieRounds WHERE MapName = '{0}' AND WhoWon = 0", l.name)))
            {
                using (var dataTable4 = DBInterface.fillData(
                    string.Format("SELECT COUNT(*) FROM ZombieRounds WHERE MapName = '{0}' AND WhoWon = 1", l.name)))
                {
                    int.TryParse(dataTable3.Rows[0]["COUNT(*)"].ToString(), out num3);
                    int.TryParse(dataTable4.Rows[0]["COUNT(*)"].ToString(), out num4);
                }
            }

            Player.SendMessage(p, "_____________ Map Info _____________");
            Player.SendMessage(p, "| Name: %a{0}%s Author: %a{1}", map.Name, map.Author);
            var num5 = num3 + num4;
            Player.SendMessage(p, "| Round time: %a{0}min %sRounds played: %a{1}", map.RoundTimeMinutes, num5);
            var num6 = num + num2;
            if (num6 > 0)
            {
                Player.SendMessage(p, "| [%a{0}%s]", GrayBar(num / (double) num6 * 100.0));
                Player.SendMessage(p, "| Rating: %a{0}%s likes, %7{1}%s dislikes ({2}%%s)", num, num2,
                    (int) (num / (double) num6 * 100.0));
            }
            else
            {
                Player.SendMessage(p, "| Rating: %a{0}%s likes, %7{1}%s dislikes.", num, num2);
            }

            if (num5 > 0)
            {
                var num7 = num4 / (double) num5;
                Player.SendMessage(p, "| [%a{0}%s]", RedBar(num7 * 100.0));
                Player.SendMessage(p, "| Win ratio: %a{0} %shumans, %c{1}%s zombies ({2}%%s)", num4, num3,
                    (int) (num7 * 100.0));
            }
            else
            {
                Player.SendMessage(p, "| Win ratio: %a{0} %shumans, %7{1}%s zombies.", num4, num3);
            }

            Player.SendMessage(p, "| Size (WxHxD): {0}x{1}x{2}", l.width, l.height, l.depth);
        }

        // Token: 0x06001209 RID: 4617 RVA: 0x00063D10 File Offset: 0x00061F10
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mapinfo <map> - Display details of <map>");
        }
    }
}