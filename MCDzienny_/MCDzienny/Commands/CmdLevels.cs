using System.Collections.Generic;
using System.Linq;

namespace MCDzienny
{
    // Token: 0x02000270 RID: 624
    public class CmdLevels : Command
    {
        // Token: 0x17000691 RID: 1681
        // (get) Token: 0x060011EC RID: 4588 RVA: 0x00062F70 File Offset: 0x00061170
        public override string name
        {
            get { return "levels"; }
        }

        // Token: 0x17000692 RID: 1682
        // (get) Token: 0x060011ED RID: 4589 RVA: 0x00062F78 File Offset: 0x00061178
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000693 RID: 1683
        // (get) Token: 0x060011EE RID: 4590 RVA: 0x00062F80 File Offset: 0x00061180
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x17000694 RID: 1684
        // (get) Token: 0x060011EF RID: 4591 RVA: 0x00062F88 File Offset: 0x00061188
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000695 RID: 1685
        // (get) Token: 0x060011F0 RID: 4592 RVA: 0x00062F8C File Offset: 0x0006118C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x060011F1 RID: 4593 RVA: 0x00062F90 File Offset: 0x00061190
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            var source = new List<Level>(Server.levels);
            var source2 = source.Select(delegate(Level l)
            {
                if (l == null || p != null && l.permissionvisit <= p.@group.Permission)
                {
                    if (Group.findPerm(l.permissionbuild) != null)
                        return string.Concat(Group.findPerm(l.permissionbuild).color, Owner(l), l.PublicName, " &b[",
                            l.physics, "]");
                    return string.Concat(Owner(l), l.PublicName, " &b[", l.physics, "]");
                }

                if (Group.findPerm(l.permissionvisit) != null)
                    return string.Concat(Group.findPerm(l.permissionvisit).color, Owner(l), l.PublicName, " &b[",
                        l.physics, "]");
                return string.Concat(Owner(l), l.PublicName, " &b[", l.physics, "]");
            });
            Player.SendMessage(p, string.Format("Loaded: {0}", string.Join(", ", source2.ToArray())));
            Player.SendMessage(p, "Use &4/unloaded for unloaded levels.");
        }

        // Token: 0x060011F2 RID: 4594 RVA: 0x00063018 File Offset: 0x00061218
        private static string Owner(Level l)
        {
            if (l.mapType != MapType.MyMap) return "";
            return Player.RemoveEmailDomain(l.Owner) + "/";
        }

        // Token: 0x060011F3 RID: 4595 RVA: 0x00063040 File Offset: 0x00061240
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/levels - Lists all loaded levels and their physics levels.");
        }
    }
}