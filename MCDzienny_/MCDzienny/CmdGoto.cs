using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MCDzienny.Settings;

namespace MCDzienny
{
    public class CmdGoto : Command
    {
        public override string name { get { return "goto"; } }

        public override string shortcut { get { return "g"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if (message.ToLower() == "lava" && Server.LavaLevel != null && Level.FindExact(Server.LavaLevel.name) != null)
            {
                message = Server.LavaLevel.name;
            }
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
            Level level = null;
            if (message.Contains("/") || message.Contains("\\"))
            {
                string[] array = message.Split(new char[2]
                {
                    '/', '\\'
                }, StringSplitOptions.RemoveEmptyEntries);
                if (array.Length != 2)
                {
                    Player.SendMessage(p, "Incorrect map path.");
                    return;
                }
                string owner = array[0].Trim().ToLower();
                string mapName = array[1].Trim().ToLower();
                new List<Level>(Server.levels);
                if (owner.EndsWith("@"))
                {
                    string path = new CmdMyMap().GetMyMapDirectoryPath(owner).Replace(owner, "");
                    if (!Directory.Exists(path))
                    {
                        Player.SendMessage(p, string.Format("Player named {0} doesn't own any maps.", owner));
                        return;
                    }
                    string text = Directory.GetDirectories(path).FirstOrDefault(d => d.Contains(owner));
                    if (text == null)
                    {
                        Player.SendMessage(p, string.Format("Player named {0} doesn't own any maps.", owner));
                        return;
                    }
                    owner = new CmdMyMap().GetPathTopElement(text);
                }
                level = Server.levels.SingleOrDefault(l => l.mapType == MapType.MyMap && l.name == mapName && CmdMyMap.IsMapOwnedBy(l, p));
                if (level == null)
                {
                    string path2 = new CmdMyMap().GetMyMapDirectoryPath(owner) + Path.DirectorySeparatorChar + mapName + ".lvl";
                    if (File.Exists(path2))
                    {
                        new CmdMyMap().LoadAndSendToMap(p, mapName, owner);
                        return;
                    }
                    level = Server.levels.FirstOrDefault(l => l.mapType == MapType.MyMap && l.Owner.Contains(owner.ToLower()) && l.name.Contains(mapName.ToLower()));
                    if (level == null)
                    {
                        if (p.name == owner)
                        {
                            Player.SendMessage(p, string.Format("You don't have a map named {0}.", mapName));
                        }
                        else
                        {
                            Player.SendMessage(p, string.Format("Player {0} doesn't have a map named {1}.", owner, mapName));
                        }
                        return;
                    }
                }
            }
            else
            {
                level = Level.FindExact(message);
                if (level == null)
                {
                    level = Level.Find(message);
                }
            }
            if (level == null)
            {
                if (!Server.AutoLoad)
                {
                    Player.SendMessage(p, string.Format("There is no level \"{0}\" loaded.", message));
                    return;
                }
                Level level2 = Level.Load(message, autoUnload: true);
                if (level2 == null)
                {
                    Player.SendMessage(p, string.Format("A map named \"{0}\" doesn't exist.", message));
                    return;
                }
                Server.AddLevel(level2);
                level = level2;
            }
            if (level.mapType == MapType.Lava)
            {
                if (LavaSettings.All.LavaMapPlayerLimit > 0 && p.group.Permission < LevelPermission.Operator && LavaSettings.All.LavaMapPlayerLimit <= level.PlayersCount)
                {
                    Player.SendMessage(p, "You can't go there, the lava map is currently full.");
                    return;
                }
            }
            else if (level.playerLimit > 0 && p.group.Permission < LevelPermission.Operator && level.playerLimit <= level.PlayersCount)
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
            {
                Player.GlobalChat(p, string.Format("{0}*{1} went to &b{2}", p.color, p.PublicName + Server.DefaultColor, level.PublicName), showname: false);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/goto [map] - sends you to the map.");
        }
    }
}