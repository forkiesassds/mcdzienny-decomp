using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdZoneShow : Command
    {

        readonly List<Vector3> frames = new List<Vector3>();

        BoundingBox boundingBox = default(BoundingBox);
        List<Level.Zone> zonesShowed;

        public override string name { get { return "zoneshow"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            ShowZone(p);
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

        public override void Help(Player p)
        {
            Player.SendMessage(p, "");
        }
    }
}