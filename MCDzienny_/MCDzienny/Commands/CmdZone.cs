using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002D9 RID: 729
    public class CmdZone : Command
    {
        // Token: 0x040009AF RID: 2479
        private BoundingBox boundingBox = default(BoundingBox);

        // Token: 0x040009B0 RID: 2480
        private readonly List<Vector3> frames = new List<Vector3>();

        // Token: 0x040009AE RID: 2478
        private List<Level.Zone> zonesShowed;

        // Token: 0x17000801 RID: 2049
        // (get) Token: 0x060014A7 RID: 5287 RVA: 0x00071EE8 File Offset: 0x000700E8
        public override string name
        {
            get { return "zone"; }
        }

        // Token: 0x17000802 RID: 2050
        // (get) Token: 0x060014A8 RID: 5288 RVA: 0x00071EF0 File Offset: 0x000700F0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000803 RID: 2051
        // (get) Token: 0x060014A9 RID: 5289 RVA: 0x00071EF8 File Offset: 0x000700F8
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000804 RID: 2052
        // (get) Token: 0x060014AA RID: 5290 RVA: 0x00071F00 File Offset: 0x00070100
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000805 RID: 2053
        // (get) Token: 0x060014AB RID: 5291 RVA: 0x00071F04 File Offset: 0x00070104
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000806 RID: 2054
        // (get) Token: 0x060014AC RID: 5292 RVA: 0x00071F08 File Offset: 0x00070108
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060014AD RID: 5293 RVA: 0x00071F0C File Offset: 0x0007010C
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                p.ZoneCheck = true;
                Player.SendMessage(p, "Place a block where you would like to check for zones.");
                return;
            }

            if (p.group.Permission < LevelPermission.Operator)
            {
                Player.SendMessage(p, "Reserved for OP+");
                return;
            }

            message = message.Replace("'", "");
            if (message.IndexOf(' ') == -1)
            {
                string a;
                if ((a = message.ToLower()) != null)
                {
                    if (a == "del")
                    {
                        p.zoneDel = true;
                        Player.SendMessage(p, "Place a block where you would like to delete a zone.");
                        return;
                    }

                    if (a == "show")
                    {
                        ShowZone(p);
                        return;
                    }
                }

                Help(p);
                return;
            }

            if (message.ToLower() == "del all")
            {
                if (p.group.Permission < LevelPermission.Admin)
                {
                    Player.SendMessage(p, "Only a SuperOP may delete all zones at once");
                    return;
                }

                if (p.level.ZoneList.Count == 0)
                {
                    Player.SendMessage(p, "There are no zones on this map.");
                    return;
                }

                for (var i = 0; i < p.level.ZoneList.Count; i++)
                {
                    var zone = p.level.ZoneList[i];
                    if (p.level.mapType == MapType.MyMap)
                        DBInterface.ExecuteQuery(string.Format(
                            "DELETE FROM `Zones` WHERE Map={0} AND Owner='{1}' AND SmallX='{2}' AND SMALLY='{3}' AND SMALLZ='{4}' AND BIGX='{5}' AND BIGY='{6}' AND BIGZ='{7}'",
                            p.level.MapDbId, zone.Owner, zone.smallX, zone.smallY, zone.smallZ, zone.bigX, zone.bigY,
                            zone.bigZ));
                    else
                        DBInterface.ExecuteQuery(string.Format(
                            "DELETE FROM `Zone{0}` WHERE Owner='{1}' AND SmallX='{2}' AND SMALLY='{3}' AND SMALLZ='{4}' AND BIGX='{5}' AND BIGY='{6}' AND BIGZ='{7}'",
                            p.level.name, zone.Owner, zone.smallX, zone.smallY, zone.smallZ, zone.bigX, zone.bigY,
                            zone.bigZ));
                    Player.SendMessage(p, string.Format("Zone deleted for &b{0}", zone.Owner));
                    p.level.ZoneList.Remove(p.level.ZoneList[i]);
                    if (i == p.level.ZoneList.Count)
                    {
                        Player.SendMessage(p, "Finished removing all zones");
                        return;
                    }

                    i--;
                }
            }

            if (p.group.Permission < LevelPermission.Operator)
            {
                Player.SendMessage(p, "Setting zones is reserved for OP+");
                return;
            }

            if (Group.Find(message.Split(' ')[1]) != null)
                message = message.Split(' ')[0] + " grp" + Group.Find(message.Split(' ')[1]).name;
            if (!(message.Split(' ')[0].ToLower() == "add"))
            {
                Help(p);
                return;
            }

            var player = Player.Find(message.Split(' ')[1]);
            CatchPos catchPos;
            if (player == null)
                catchPos.Owner = message.Split(' ')[1];
            else
                catchPos.Owner = player.name;
            if (!Player.ValidName(catchPos.Owner))
            {
                Player.SendMessage(p, "INVALID NAME.");
                return;
            }

            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            Player.SendMessage(p, string.Format("Zone for: &b{0}.", catchPos.Owner));
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x060014AE RID: 5294 RVA: 0x0007235C File Offset: 0x0007055C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/zone [add] [name] - Creates a zone only [name] can build in");
            Player.SendMessage(p, "/zone [add] [rank] - Creates a zone only [rank]+ can build in");
            Player.SendMessage(p, "/zone del - Deletes the zone clicked");
            Player.SendMessage(p, "/zone show - Shows all zones");
        }

        // Token: 0x060014AF RID: 5295 RVA: 0x0007238C File Offset: 0x0007058C
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        // Token: 0x060014B0 RID: 5296 RVA: 0x00072400 File Offset: 0x00070600
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            Level.Zone item;
            item.smallX = Math.Min(catchPos.x, x);
            item.smallY = Math.Min(catchPos.y, y);
            item.smallZ = Math.Min(catchPos.z, z);
            item.bigX = Math.Max(catchPos.x, x);
            item.bigY = Math.Max(catchPos.y, y);
            item.bigZ = Math.Max(catchPos.z, z);
            item.Owner = catchPos.Owner;
            p.level.ZoneList.Add(item);
            if (p.level.mapType == MapType.MyMap)
                DBInterface.ExecuteQuery(string.Format(
                    "INSERT INTO Zones (Map, SmallX, SmallY, SmallZ, BigX, BigY, BigZ, Owner) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, '{7}')",
                    p.level.MapDbId, item.smallX, item.smallY, item.smallZ, item.bigX, item.bigY, item.bigZ,
                    item.Owner));
            else
                DBInterface.ExecuteQuery(string.Concat("INSERT INTO `Zone", p.level.name,
                    "` (SmallX, SmallY, SmallZ, BigX, BigY, BigZ, Owner) VALUES (", item.smallX, ", ", item.smallY,
                    ", ", item.smallZ, ", ", item.bigX, ", ", item.bigY, ", ", item.bigZ, ", '", item.Owner, "')"));
            Player.SendMessage(p, string.Format("Added zone for &b{0}", catchPos.Owner));
        }

        // Token: 0x060014B1 RID: 5297 RVA: 0x00072678 File Offset: 0x00070878
        private void ShowZone(Player p)
        {
            if (zonesShowed == null)
            {
                Player.SendMessage(p, "Showing all zones.");
                zonesShowed = new List<Level.Zone>();
                p.level.ZoneList.ForEach(delegate(Level.Zone z) { zonesShowed.Add(z); });
                zonesShowed.ForEach(delegate(Level.Zone z)
                {
                    boundingBox = new BoundingBox(new Vector3(z.smallX, z.smallY, z.smallZ),
                        new Vector3(z.bigX, z.bigY, z.bigZ));
                    frames.AddRange(boundingBox.BoxOutline());
                });
                frames.ForEach(delegate(Vector3 frame)
                {
                    p.AddVirtualBlock((ushort) frame.X, (ushort) frame.Y, (ushort) frame.Z, 14);
                });
                p.CommitVirtual();
                return;
            }

            Player.SendMessage(p, "Hiding all zones.");
            frames.ForEach(delegate(Vector3 frame)
            {
                p.AddVirtualBlock((ushort) frame.X, (ushort) frame.Y, (ushort) frame.Z,
                    p.level.GetTile((ushort) frame.X, (ushort) frame.Y, (ushort) frame.Z));
            });
            p.CommitVirtual();
            frames.Clear();
            zonesShowed.Clear();
            zonesShowed = null;
        }

        // Token: 0x020002DA RID: 730
        private struct CatchPos
        {
            // Token: 0x040009B1 RID: 2481
            public ushort x;

            // Token: 0x040009B2 RID: 2482
            public ushort y;

            // Token: 0x040009B3 RID: 2483
            public ushort z;

            // Token: 0x040009B4 RID: 2484
            public string Owner;
        }
    }
}