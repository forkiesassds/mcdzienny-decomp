using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdZone : Command
    {

        readonly List<Vector3> frames = new List<Vector3>();

        BoundingBox boundingBox = default(BoundingBox);

        List<Level.Zone> zonesShowed;

        public override string name { get { return "zone"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

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
                switch (message.ToLower())
                {
                    case "del":
                        p.zoneDel = true;
                        Player.SendMessage(p, "Place a block where you would like to delete a zone.");
                        break;
                    case "show":
                        ShowZone(p);
                        break;
                    default:
                        Help(p);
                        break;
                }
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
                int num;
                for (num = 0; num < p.level.ZoneList.Count; num++)
                {
                    Level.Zone zone = p.level.ZoneList[num];
                    if (p.level.mapType == MapType.MyMap)
                    {
                        DBInterface.ExecuteQuery(
                            string.Format(
                                "DELETE FROM `Zones` WHERE Map={0} AND Owner='{1}' AND SmallX='{2}' AND SMALLY='{3}' AND SMALLZ='{4}' AND BIGX='{5}' AND BIGY='{6}' AND BIGZ='{7}'",
                                p.level.MapDbId, zone.Owner, zone.smallX, zone.smallY, zone.smallZ, zone.bigX, zone.bigY, zone.bigZ));
                    }
                    else
                    {
                        DBInterface.ExecuteQuery(
                            string.Format(
                                "DELETE FROM `Zone{0}` WHERE Owner='{1}' AND SmallX='{2}' AND SMALLY='{3}' AND SMALLZ='{4}' AND BIGX='{5}' AND BIGY='{6}' AND BIGZ='{7}'",
                                p.level.name, zone.Owner, zone.smallX, zone.smallY, zone.smallZ, zone.bigX, zone.bigY, zone.bigZ));
                    }
                    Player.SendMessage(p, string.Format("Zone deleted for &b{0}", zone.Owner));
                    p.level.ZoneList.Remove(p.level.ZoneList[num]);
                    if (num == p.level.ZoneList.Count)
                    {
                        Player.SendMessage(p, "Finished removing all zones");
                        return;
                    }
                    num--;
                }
            }
            if (p.group.Permission < LevelPermission.Operator)
            {
                Player.SendMessage(p, "Setting zones is reserved for OP+");
                return;
            }
            if (Group.Find(message.Split(' ')[1]) != null)
            {
                message = message.Split(' ')[0] + " grp" + Group.Find(message.Split(' ')[1]).name;
            }
            if (message.Split(' ')[0].ToLower() == "add")
            {
                Player player = Player.Find(message.Split(' ')[1]);
                CatchPos catchPos = default(CatchPos);
                if (player == null)
                {
                    catchPos.Owner = message.Split(' ')[1];
                }
                else
                {
                    catchPos.Owner = player.name;
                }
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
            else
            {
                Help(p);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/zone [add] [name] - Creates a zone only [name] can build in");
            Player.SendMessage(p, "/zone [add] [rank] - Creates a zone only [rank]+ can build in");
            Player.SendMessage(p, "/zone del - Deletes the zone clicked");
            Player.SendMessage(p, "/zone show - Shows all zones");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            Level.Zone item = default(Level.Zone);
            item.smallX = Math.Min(catchPos.x, x);
            item.smallY = Math.Min(catchPos.y, y);
            item.smallZ = Math.Min(catchPos.z, z);
            item.bigX = Math.Max(catchPos.x, x);
            item.bigY = Math.Max(catchPos.y, y);
            item.bigZ = Math.Max(catchPos.z, z);
            item.Owner = catchPos.Owner;
            p.level.ZoneList.Add(item);
            if (p.level.mapType == MapType.MyMap)
            {
                DBInterface.ExecuteQuery(
                    string.Format("INSERT INTO Zones (Map, SmallX, SmallY, SmallZ, BigX, BigY, BigZ, Owner) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, '{7}')",
                                  p.level.MapDbId, item.smallX, item.smallY, item.smallZ, item.bigX, item.bigY, item.bigZ, item.Owner));
            }
            else
            {
                DBInterface.ExecuteQuery("INSERT INTO `Zone" + p.level.name + "` (SmallX, SmallY, SmallZ, BigX, BigY, BigZ, Owner) VALUES (" + item.smallX + ", " +
                                         item.smallY + ", " + item.smallZ + ", " + item.bigX + ", " + item.bigY + ", " + item.bigZ + ", '" + item.Owner + "')");
            }
            Player.SendMessage(p, string.Format("Added zone for &b{0}", catchPos.Owner));
        }

        void ShowZone(Player p)
        {
            if (zonesShowed == null)
            {
                Player.SendMessage(p, "Showing all zones.");
                zonesShowed = new List<Level.Zone>();
                p.level.ZoneList.ForEach(delegate(Level.Zone z) { zonesShowed.Add(z); });
                zonesShowed.ForEach(delegate(Level.Zone z)
                {
                    boundingBox = new BoundingBox(new Vector3(z.smallX, z.smallY, z.smallZ), new Vector3(z.bigX, z.bigY, z.bigZ));
                    frames.AddRange(boundingBox.BoxOutline());
                });
                frames.ForEach(delegate(Vector3 frame) { p.AddVirtualBlock((ushort)frame.X, (ushort)frame.Y, (ushort)frame.Z, 14); });
                p.CommitVirtual();
            }
            else
            {
                Player.SendMessage(p, "Hiding all zones.");
                frames.ForEach(delegate(Vector3 frame)
                {
                    p.AddVirtualBlock((ushort)frame.X, (ushort)frame.Y, (ushort)frame.Z,
                                      p.level.GetTile((ushort)frame.X, (ushort)frame.Y, (ushort)frame.Z));
                });
                p.CommitVirtual();
                frames.Clear();
                zonesShowed.Clear();
                zonesShowed = null;
            }
        }

        struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public string Owner;
        }
    }
}