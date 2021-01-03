using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000285 RID: 645
    public class CmdPortal : Command
    {
        // Token: 0x170006E5 RID: 1765
        // (get) Token: 0x06001294 RID: 4756 RVA: 0x00066458 File Offset: 0x00064658
        public override string name
        {
            get { return "portal"; }
        }

        // Token: 0x170006E6 RID: 1766
        // (get) Token: 0x06001295 RID: 4757 RVA: 0x00066460 File Offset: 0x00064660
        public override string shortcut
        {
            get { return "o"; }
        }

        // Token: 0x170006E7 RID: 1767
        // (get) Token: 0x06001296 RID: 4758 RVA: 0x00066468 File Offset: 0x00064668
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170006E8 RID: 1768
        // (get) Token: 0x06001297 RID: 4759 RVA: 0x00066470 File Offset: 0x00064670
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170006E9 RID: 1769
        // (get) Token: 0x06001298 RID: 4760 RVA: 0x00066474 File Offset: 0x00064674
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170006EA RID: 1770
        // (get) Token: 0x06001299 RID: 4761 RVA: 0x00066478 File Offset: 0x00064678
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600129B RID: 4763 RVA: 0x00066484 File Offset: 0x00064684
        public override void Use(Player p, string message)
        {
            portalPos portalPos;
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
            else if (message.ToLower() == "lava")
            {
                portalPos.type = 162;
            }
            else
            {
                if (message.ToLower() == "show")
                {
                    showPortals(p);
                    return;
                }

                Help(p);
                return;
            }

            p.ClearBlockchange();
            portalPos.port = new List<portPos>();
            p.blockchangeObject = portalPos;
            Player.SendMessage(p, string.Format("Place a the &aEntry block{0} for the portal", Server.DefaultColor));
            p.ClearBlockchange();
            p.Blockchange += EntryChange;
        }

        // Token: 0x0600129C RID: 4764 RVA: 0x000666E8 File Offset: 0x000648E8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/portal [orange/blue/air/water/lava] [multi] - Activates Portal mode.");
            Player.SendMessage(p, "/portal [type] multi - Place Entry blocks until exit is wanted.");
            Player.SendMessage(p, "/portal show - Shows portals, green = in, red = out.");
        }

        // Token: 0x0600129D RID: 4765 RVA: 0x0006670C File Offset: 0x0006490C
        public void EntryChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var portalPos = (portalPos) p.blockchangeObject;
            if (portalPos.multi && type == 21 && portalPos.port.Count > 0)
            {
                ExitChange(p, x, y, z, type);
                return;
            }

            p.level.GetTile(x, y, z);
            p.level.Blockchange(p, x, y, z, portalPos.type);
            p.SendBlockchange(x, y, z, 25);
            portPos item;
            item.portMapName = p.level.name;
            item.x = x;
            item.y = y;
            item.z = z;
            portalPos.port.Add(item);
            p.blockchangeObject = portalPos;
            if (portalPos.multi)
            {
                p.Blockchange += EntryChange;
                Player.SendMessage(p, "&aEntry block placed. &cRed block for exit");
                return;
            }

            Player.SendMessage(p, "&aEntry block placed");
            if (portalPos.lava)
            {
                ExitChange(p, 0, 0, 0, 0);
                return;
            }

            p.Blockchange += ExitChange;
        }

        // Token: 0x0600129E RID: 4766 RVA: 0x00066828 File Offset: 0x00064A28
        public void ExitChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var portalPos = (portalPos) p.blockchangeObject;
            foreach (var portPos in portalPos.port)
            {
                if (p.level.mapType == MapType.MyMap)
                    using (var dataTable = DBInterface.fillData(string.Concat("SELECT * FROM `Portals` WHERE Map=",
                        p.level.MapDbId, " AND EntryX=", portPos.x, " AND EntryY=", portPos.y, " AND EntryZ=",
                        portPos.z)))
                    {
                        if (dataTable.Rows.Count == 0)
                            DBInterface.ExecuteQuery(string.Concat(
                                "INSERT INTO `Portals` (Map, EntryX, EntryY, EntryZ, ExitMap, ExitX, ExitY, ExitZ) VALUES (",
                                p.level.MapDbId, ", ", portPos.x, ", ", portPos.y, ", ", portPos.z, ", '",
                                portalPos.lava ? "lava" : p.level.name, "', ", x, ", ", y, ", ", z, ")"));
                        else
                            DBInterface.ExecuteQuery(string.Concat("UPDATE `Portals` SET ExitMap='",
                                portalPos.lava ? "lava" : p.level.name, "', ExitX=", x, ", ExitY=", y, ", ExitZ=", z,
                                " WHERE Map=", p.level.MapDbId, " AND EntryX=", portPos.x, " AND EntryY=", portPos.y,
                                " AND EntryZ=", portPos.z));
                        goto IL_52D;
                    }

                goto IL_2D0;
                IL_52D:
                if (portPos.portMapName == p.level.name)
                    p.SendBlockchange(portPos.x, portPos.y, portPos.z, portalPos.type);
                continue;
                IL_2D0:
                using (var dataTable2 = DBInterface.fillData(string.Concat("SELECT * FROM `Portals",
                    portPos.portMapName, "` WHERE EntryX=", (int) portPos.x, " AND EntryY=", (int) portPos.y,
                    " AND EntryZ=", (int) portPos.z)))
                {
                    if (dataTable2.Rows.Count == 0)
                        DBInterface.ExecuteQuery(string.Concat("INSERT INTO `Portals", portPos.portMapName,
                            "` (EntryX, EntryY, EntryZ, ExitMap, ExitX, ExitY, ExitZ) VALUES (", (int) portPos.x, ", ",
                            (int) portPos.y, ", ", (int) portPos.z, ", '", portalPos.lava ? "lava" : p.level.name,
                            "', ", (int) x, ", ", (int) y, ", ", (int) z, ")"));
                    else
                        DBInterface.ExecuteQuery(string.Concat("UPDATE `Portals", portPos.portMapName,
                            "` SET ExitMap='", portalPos.lava ? "lava" : p.level.name, "', ExitX=", (int) x, ", ExitY=",
                            (int) y, ", ExitZ=", (int) z, " WHERE EntryX=", (int) portPos.x, " AND EntryY=",
                            (int) portPos.y, " AND EntryZ=", (int) portPos.z));
                }

                goto IL_52D;
            }

            Player.SendMessage(p, string.Format("&3Exit{0} block placed", Server.DefaultColor));
            if (p.staticCommands)
            {
                portalPos.port.Clear();
                p.blockchangeObject = portalPos;
                p.Blockchange += EntryChange;
            }
        }

        // Token: 0x0600129F RID: 4767 RVA: 0x00066E4C File Offset: 0x0006504C
        public void showPortals(Player p)
        {
            p.showPortals = !p.showPortals;
            string queryString;
            if (p.level.mapType == MapType.MyMap)
                queryString = "SELECT * FROM `Portals` WHERE Map=" + p.level.MapDbId;
            else
                queryString = "SELECT * FROM `Portals" + p.level.name + "`";
            using (var dataTable = DBInterface.fillData(queryString))
            {
                if (p.showPortals)
                {
                    int i;
                    for (i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i]["ExitMap"].ToString() == p.level.name)
                            p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["ExitX"].ToString()),
                                ushort.Parse(dataTable.Rows[i]["ExitY"].ToString()),
                                ushort.Parse(dataTable.Rows[i]["ExitZ"].ToString()), 176);
                        p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["EntryX"].ToString()),
                            ushort.Parse(dataTable.Rows[i]["EntryY"].ToString()),
                            ushort.Parse(dataTable.Rows[i]["EntryZ"].ToString()), 175);
                    }

                    Player.SendMessage(p, string.Format("Now showing &a{0} portals.", i + Server.DefaultColor));
                }
                else
                {
                    for (var i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i]["ExitMap"].ToString() == p.level.name)
                            p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["ExitX"].ToString()),
                                ushort.Parse(dataTable.Rows[i]["ExitY"].ToString()),
                                ushort.Parse(dataTable.Rows[i]["ExitZ"].ToString()), 0);
                        p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["EntryX"].ToString()),
                            ushort.Parse(dataTable.Rows[i]["EntryY"].ToString()),
                            ushort.Parse(dataTable.Rows[i]["EntryZ"].ToString()),
                            p.level.GetTile(ushort.Parse(dataTable.Rows[i]["EntryX"].ToString()),
                                ushort.Parse(dataTable.Rows[i]["EntryY"].ToString()),
                                ushort.Parse(dataTable.Rows[i]["EntryZ"].ToString())));
                    }

                    Player.SendMessage(p, "Now hiding portals.");
                }
            }
        }

        // Token: 0x02000286 RID: 646
        public struct portalPos
        {
            // Token: 0x0400092A RID: 2346
            public List<portPos> port;

            // Token: 0x0400092B RID: 2347
            public byte type;

            // Token: 0x0400092C RID: 2348
            public bool multi;

            // Token: 0x0400092D RID: 2349
            public bool lava;
        }

        // Token: 0x02000287 RID: 647
        public struct portPos
        {
            // Token: 0x0400092E RID: 2350
            public ushort x;

            // Token: 0x0400092F RID: 2351
            public ushort y;

            // Token: 0x04000930 RID: 2352
            public ushort z;

            // Token: 0x04000931 RID: 2353
            public string portMapName;
        }
    }
}