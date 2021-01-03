using System;
using System.Linq;

namespace MCDzienny
{
    // Token: 0x02000057 RID: 87
    public class CmdAccept : Command
    {
        // Token: 0x0400016C RID: 364
        private readonly TimeSpan expirationTime = new TimeSpan(0, 5, 0);

        // Token: 0x17000071 RID: 113
        // (get) Token: 0x06000208 RID: 520 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
        public override string name
        {
            get { return "accept"; }
        }

        // Token: 0x17000072 RID: 114
        // (get) Token: 0x06000209 RID: 521 RVA: 0x0000C300 File Offset: 0x0000A500
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000073 RID: 115
        // (get) Token: 0x0600020A RID: 522 RVA: 0x0000C308 File Offset: 0x0000A508
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000074 RID: 116
        // (get) Token: 0x0600020B RID: 523 RVA: 0x0000C310 File Offset: 0x0000A510
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000075 RID: 117
        // (get) Token: 0x0600020C RID: 524 RVA: 0x0000C314 File Offset: 0x0000A514
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000076 RID: 118
        // (get) Token: 0x0600020D RID: 525 RVA: 0x0000C318 File Offset: 0x0000A518
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600020E RID: 526 RVA: 0x0000C31C File Offset: 0x0000A51C
        public override void Use(Player p, string message)
        {
            if (!p.ExtraData.ContainsKey("invitation"))
            {
                Player.SendMessage(p, "You don't have any invitation pending.");
                return;
            }

            var array = (object[]) p.ExtraData["invitation"];
            var t = (DateTime) array[2];
            var inviter = array[1].ToString();
            if (t < DateTime.Now.Subtract(expirationTime))
            {
                Player.SendMessage(p,
                    string.Format("The invitation from {0} expired.", Player.RemoveEmailDomain(array[1].ToString())));
                p.ExtraData.Remove("invitation");
                return;
            }

            var mapName = array[0].ToString();
            var level = Server.levels.SingleOrDefault(l =>
                l.mapType == MapType.MyMap && l.name == mapName.ToLower() && l.Owner == inviter.ToLower());
            if (level == null)
            {
                Player.SendMessage(p, "The map is no longer loaded.");
                p.ExtraData.Remove("invitation");
                return;
            }

            var level2 = p.level;
            p.level = level;
            p.SendUserMOTD();
            p.SendMap();
            p.SendPos(byte.MaxValue, (ushort) ((0.5f + p.level.spawnx) * 32f), (ushort) ((0.6f + p.level.spawny) * 32f),
                (ushort) ((0.5f + p.level.spawnz) * 32f), p.level.rotx, p.level.roty);
            level2.NotifyPopulationChanged();
            Player.SendMessage(p, "You accepted the invitation.");
            var player = Player.Find(inviter);
            if (player != null) Player.SendMessage(player, p.PublicName + " accepted your invitation.");
            p.ExtraData.Remove("invitation");
        }

        // Token: 0x0600020F RID: 527 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/accept - accepts the invitation.");
        }
    }
}