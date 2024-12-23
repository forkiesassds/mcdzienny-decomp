using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    public class CmdPortal : Command
    {

        public override string name { get { return "portal"; } }

        public override string shortcut { get { return "o"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            portalPos portalPos = default(portalPos);
            portalPos.multi = false;
            portalPos.lava = false;
            if (message.IndexOf(' ') != -1)
            {
                if (message.Split(' ').Length == 3 && message.Split(' ')[2].ToLower() == "lavamap")
                {
                    portalPos.lava = true;
                    message = message.Split(' ')[0] + " " + message.Split(' ')[1];
                }
                if (message.Split(' ')[1].ToLower() == "multi")
                {
                    portalPos.multi = true;
                    message = message.Split(' ')[0];
                }
                else if (message.Split(' ')[1].ToLower() == "lavamap")
                {
                    portalPos.lava = true;
                    message = message.Split(' ')[0];
                }
            }
            if (message.ToLower() == "blue" || message == "")
            {
                portalPos.type = 175;
            }
            else if (message.ToLower() == "orange")
            {
                portalPos.type = 176;
            }
            else if (message.ToLower() == "air")
            {
                portalPos.type = 160;
            }
            else if (message.ToLower() == "water")
            {
                portalPos.type = 161;
            }
            else
            {
                if (!(message.ToLower() == "lava"))
                {
                    if (message.ToLower() == "show")
                    {
                        showPortals(p);
                    }
                    else
                    {
                        Help(p);
                    }
                    return;
                }
                portalPos.type = 162;
            }
            p.ClearBlockchange();
            portalPos.port = new List<portPos>();
            p.blockchangeObject = portalPos;
            Player.SendMessage(p, string.Format("Place a the &aEntry block{0} for the portal", Server.DefaultColor));
            p.ClearBlockchange();
            p.Blockchange += EntryChange;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/portal [orange/blue/air/water/lava] [multi] - Activates Portal mode.");
            Player.SendMessage(p, "/portal [type] multi - Place Entry blocks until exit is wanted.");
            Player.SendMessage(p, "/portal show - Shows portals, green = in, red = out.");
        }

        public void EntryChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            portalPos portalPos = (portalPos)p.blockchangeObject;
            if (portalPos.multi && type == 21 && portalPos.port.Count > 0)
            {
                ExitChange(p, x, y, z, type);
                return;
            }
            p.level.GetTile(x, y, z);
            p.level.Blockchange(p, x, y, z, portalPos.type);
            p.SendBlockchange(x, y, z, 25);
            portPos item = default(portPos);
            item.portMapName = p.level.name;
            item.x = x;
            item.y = y;
            item.z = z;
            portalPos.port.Add(item);
            p.blockchangeObject = portalPos;
            if (!portalPos.multi)
            {
                Player.SendMessage(p, "&aEntry block placed");
                if (portalPos.lava)
                {
                    ExitChange(p, 0, 0, 0, 0);
                }
                else
                {
                    p.Blockchange += ExitChange;
                }
            }
            else
            {
                p.Blockchange += EntryChange;
                Player.SendMessage(p, "&aEntry block placed. &cRed block for exit");
            }
        }

        public void ExitChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            portalPos portalPos = (portalPos)p.blockchangeObject;
            foreach (portPos item in portalPos.port)
            {
                if (p.level.mapType == MapType.MyMap)
                {
                    using (DataTable dataTable = DBInterface.fillData("SELECT * FROM `Portals` WHERE Map=" + p.level.MapDbId + " AND EntryX=" + item.x + " AND EntryY=" +
                                                                      item.y + " AND EntryZ=" + item.z))
                    {
                        if (dataTable.Rows.Count == 0)
                        {
                            DBInterface.ExecuteQuery("INSERT INTO `Portals` (Map, EntryX, EntryY, EntryZ, ExitMap, ExitX, ExitY, ExitZ) VALUES (" + p.level.MapDbId +
                                                     ", " + item.x + ", " + item.y + ", " + item.z + ", '" + (portalPos.lava ? "lava" : p.level.name) + "', " + x + ", " +
                                                     y + ", " + z + ")");
                        }
                        else
                        {
                            DBInterface.ExecuteQuery("UPDATE `Portals` SET ExitMap='" + (portalPos.lava ? "lava" : p.level.name) + "', ExitX=" + x + ", ExitY=" + y +
                                                     ", ExitZ=" + z + " WHERE Map=" + p.level.MapDbId + " AND EntryX=" + item.x + " AND EntryY=" + item.y +
                                                     " AND EntryZ=" + item.z);
                        }
                    }
                }
                else
                {
                    using (DataTable dataTable2 = DBInterface.fillData("SELECT * FROM `Portals" + item.portMapName + "` WHERE EntryX=" + (int)item.x + " AND EntryY=" +
                                                                       (int)item.y + " AND EntryZ=" + (int)item.z))
                    {
                        if (dataTable2.Rows.Count == 0)
                        {
                            DBInterface.ExecuteQuery("INSERT INTO `Portals" + item.portMapName + "` (EntryX, EntryY, EntryZ, ExitMap, ExitX, ExitY, ExitZ) VALUES (" +
                                                     (int)item.x + ", " + (int)item.y + ", " + (int)item.z + ", '" + (portalPos.lava ? "lava" : p.level.name) + "', " +
                                                     (int)x + ", " + (int)y + ", " + (int)z + ")");
                        }
                        else
                        {
                            DBInterface.ExecuteQuery("UPDATE `Portals" + item.portMapName + "` SET ExitMap='" + (portalPos.lava ? "lava" : p.level.name) + "', ExitX=" +
                                                     (int)x + ", ExitY=" + (int)y + ", ExitZ=" + (int)z + " WHERE EntryX=" + (int)item.x + " AND EntryY=" + (int)item.y +
                                                     " AND EntryZ=" + (int)item.z);
                        }
                    }
                }
                if (item.portMapName == p.level.name)
                {
                    p.SendBlockchange(item.x, item.y, item.z, portalPos.type);
                }
            }
            Player.SendMessage(p, string.Format("&3Exit{0} block placed", Server.DefaultColor));
            if (p.staticCommands)
            {
                portalPos.port.Clear();
                p.blockchangeObject = portalPos;
                p.Blockchange += EntryChange;
            }
        }

        public void showPortals(Player p)
        {
            p.showPortals = !p.showPortals;
            string text = null;
            text = p.level.mapType != MapType.MyMap ? "SELECT * FROM `Portals" + p.level.name + "`" : "SELECT * FROM `Portals` WHERE Map=" + p.level.MapDbId;
            using (DataTable dataTable = DBInterface.fillData(text))
            {
                if (p.showPortals)
                {
                    int i;
                    for (i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i]["ExitMap"].ToString() == p.level.name)
                        {
                            p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["ExitX"].ToString()), ushort.Parse(dataTable.Rows[i]["ExitY"].ToString()),
                                              ushort.Parse(dataTable.Rows[i]["ExitZ"].ToString()), 176);
                        }
                        p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["EntryX"].ToString()), ushort.Parse(dataTable.Rows[i]["EntryY"].ToString()),
                                          ushort.Parse(dataTable.Rows[i]["EntryZ"].ToString()), 175);
                    }
                    Player.SendMessage(p, string.Format("Now showing &a{0} portals.", i + Server.DefaultColor));
                    return;
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i]["ExitMap"].ToString() == p.level.name)
                    {
                        p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["ExitX"].ToString()), ushort.Parse(dataTable.Rows[i]["ExitY"].ToString()),
                                          ushort.Parse(dataTable.Rows[i]["ExitZ"].ToString()), 0);
                    }
                    p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["EntryX"].ToString()), ushort.Parse(dataTable.Rows[i]["EntryY"].ToString()),
                                      ushort.Parse(dataTable.Rows[i]["EntryZ"].ToString()),
                                      p.level.GetTile(ushort.Parse(dataTable.Rows[i]["EntryX"].ToString()), ushort.Parse(dataTable.Rows[i]["EntryY"].ToString()),
                                                      ushort.Parse(dataTable.Rows[i]["EntryZ"].ToString())));
                }
                Player.SendMessage(p, "Now hiding portals.");
            }
        }

        public struct portalPos
        {
            public List<portPos> port;

            public byte type;

            public bool multi;

            public bool lava;
        }

        public struct portPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public string portMapName;
        }
    }
}