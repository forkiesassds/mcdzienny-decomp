using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000086 RID: 134
    public class CmdZoneShow : Command
    {
        // Token: 0x040001D6 RID: 470
        private BoundingBox boundingBox = default(BoundingBox);

        // Token: 0x040001D7 RID: 471
        private readonly List<Vector3> frames = new List<Vector3>();

        // Token: 0x040001D5 RID: 469
        private List<Level.Zone> zonesShowed;

        // Token: 0x17000112 RID: 274
        // (get) Token: 0x06000386 RID: 902 RVA: 0x00012F3C File Offset: 0x0001113C
        public override string name
        {
            get { return "zoneshow"; }
        }

        // Token: 0x17000113 RID: 275
        // (get) Token: 0x06000387 RID: 903 RVA: 0x00012F44 File Offset: 0x00011144
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000114 RID: 276
        // (get) Token: 0x06000388 RID: 904 RVA: 0x00012F4C File Offset: 0x0001114C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000115 RID: 277
        // (get) Token: 0x06000389 RID: 905 RVA: 0x00012F54 File Offset: 0x00011154
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000116 RID: 278
        // (get) Token: 0x0600038A RID: 906 RVA: 0x00012F58 File Offset: 0x00011158
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000117 RID: 279
        // (get) Token: 0x0600038B RID: 907 RVA: 0x00012F5C File Offset: 0x0001115C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600038D RID: 909 RVA: 0x00012F80 File Offset: 0x00011180
        public override void Use(Player p, string message)
        {
            ShowZone(p);
        }

        // Token: 0x0600038E RID: 910 RVA: 0x00012F8C File Offset: 0x0001118C
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

        // Token: 0x0600038F RID: 911 RVA: 0x000130A4 File Offset: 0x000112A4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "");
        }
    }
}