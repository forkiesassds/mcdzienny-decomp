using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x02000261 RID: 609
    public class CmdGoto : Command
    {
        // Token: 0x1700065B RID: 1627
        // (get) Token: 0x0600118A RID: 4490 RVA: 0x00060ADC File Offset: 0x0005ECDC
        public override string name
        {
            get { return "goto"; }
        }

        // Token: 0x1700065C RID: 1628
        // (get) Token: 0x0600118B RID: 4491 RVA: 0x00060AE4 File Offset: 0x0005ECE4
        public override string shortcut
        {
            get { return "g"; }
        }

        // Token: 0x1700065D RID: 1629
        // (get) Token: 0x0600118C RID: 4492 RVA: 0x00060AEC File Offset: 0x0005ECEC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700065E RID: 1630
        // (get) Token: 0x0600118D RID: 4493 RVA: 0x00060AF4 File Offset: 0x0005ECF4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700065F RID: 1631
        // (get) Token: 0x0600118E RID: 4494 RVA: 0x00060AF8 File Offset: 0x0005ECF8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000660 RID: 1632
        // (get) Token: 0x0600118F RID: 4495 RVA: 0x00060AFC File Offset: 0x0005ECFC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001190 RID: 4496 RVA: 0x00060B00 File Offset: 0x0005ED00
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (message.ToLower() == "lava" && Server.LavaLevel != null &&
                Level.FindExact(Server.LavaLevel.name) != null) message = Server.LavaLevel.name;
            if (Server.heavenMap != null && p.group.Permission < LevelPermission.Operator)
            {
                if (message == Server.heavenMap.name && p.lives > 0)
                {
                    Player.SendMessage(p, "You aren't dead, yet.");
                    return;
                }

                if (p.inHeaven && message != Server.heavenMap.name)
                {
                    Player.SendMessage(p, "You have to buy a life in order to get back to the arena.");
                    return;
                }
            }

            Level level;
            if (message.Contains("/") || message.Contains("\\"))
            {
                var array = message.Split(new[]
                {
                    '/',
                    '\\'
                }, StringSplitOptions.RemoveEmptyEntries);
                if (array.Length != 2)
                {
                    Player.SendMessage(p, "Incorrect map path.");
                    return;
                }

                var owner = array[0].Trim().ToLower();
                var mapName = array[1].Trim().ToLower();
                new List<Level>(Server.levels);
                if (owner.EndsWith("@"))
                {
                    var path = new CmdMyMap().GetMyMapDirectoryPath(owner).Replace(owner, "");
                    if (!Directory.Exists(path))
                    {
                        Player.SendMessage(p, string.Format("Player named {0} doesn't own any maps.", owner));
                        return;
                    }

                    var text = Directory.GetDirectories(path).FirstOrDefault(d => d.Contains(owner));
                    if (text == null)
                    {
                        Player.SendMessage(p, string.Format("Player named {0} doesn't own any maps.", owner));
                        return;
                    }

                    owner = new CmdMyMap().GetPathTopElement(text);
                }

                level = Server.levels.SingleOrDefault(l =>
                    l.mapType == MapType.MyMap && l.name == mapName && CmdMyMap.IsMapOwnedBy(l, p));
                if (level == null)
                {
                    var path2 = string.Concat(new CmdMyMap().GetMyMapDirectoryPath(owner), Path.DirectorySeparatorChar,
                        mapName, ".lvl");
                    if (File.Exists(path2))
                    {
                        new CmdMyMap().LoadAndSendToMap(p, mapName, owner);
                        return;
                    }

                    level = Server.levels.FirstOrDefault(l =>
                        l.mapType == MapType.MyMap && l.Owner.Contains(owner.ToLower()) &&
                        l.name.Contains(mapName.ToLower()));
                    if (level == null)
                    {
                        if (p.name == owner)
                        {
                            Player.SendMessage(p, string.Format("You don't have a map named {0}.", mapName));
                            return;
                        }

                        Player.SendMessage(p,
                            string.Format("Player {0} doesn't have a map named {1}.", owner, mapName));
                        return;
                    }
                }
            }
            else
            {
                level = Level.FindExact(message);
                if (level == null) level = Level.Find(message);
            }

            if (level == null)
            {
                if (!Server.AutoLoad)
                {
                    Player.SendMessage(p, string.Format("There is no level \"{0}\" loaded.", message));
                    return;
                }

                var level2 = Level.Load(message, true);
                if (level2 == null)
                {
                    Player.SendMessage(p, string.Format("A map named \"{0}\" doesn't exist.", message));
                    return;
                }

                Server.AddLevel(level2);
                level = level2;
            }

            var level3 = p.level;
            if (level.mapType == MapType.Lava)
            {
                if (LavaSettings.All.LavaMapPlayerLimit > 0 && p.group.Permission < LevelPermission.Operator &&
                    LavaSettings.All.LavaMapPlayerLimit <= level.PlayersCount)
                {
                    Player.SendMessage(p, "You can't go there, the lava map is currently full.");
                    return;
                }
            }
            else if (level.playerLimit > 0 && p.group.Permission < LevelPermission.Operator &&
                     level.playerLimit <= level.PlayersCount)
            {
                Player.SendMessage(p, "You can't go there, the map is currently full.");
                return;
            }

            if (!p.ignorePermission && p.group.Permission < level.permissionvisit)
            {
                Player.SendMessage(p, string.Format("You're not allowed to go to {0}.", level.PublicName));
                return;
            }

            p.SendToMap(level);
            if (!p.hidden && p.level.mapType != MapType.Lava && p.level.mapType != MapType.Zombie)
                Player.GlobalChat(p,
                    string.Format("{0}*{1} went to &b{2}", p.color, p.PublicName + Server.DefaultColor,
                        level.PublicName), false);
        }

        // Token: 0x06001191 RID: 4497 RVA: 0x00061018 File Offset: 0x0005F218
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/goto [map] - sends you to the map.");
        }
    }
}