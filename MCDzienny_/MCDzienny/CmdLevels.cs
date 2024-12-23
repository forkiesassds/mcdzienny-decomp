using System.Collections.Generic;
using System.Linq;

namespace MCDzienny
{
    public class CmdLevels : Command
    {
        public override string name { get { return "levels"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            var source = new List<Level>(Server.levels);
            IEnumerable<string> source2 = source.Select(delegate(Level l)
            {
                if (l == null || p != null && l.permissionvisit <= p.group.Permission)
                {
                    if (Group.findPerm(l.permissionbuild) != null)
                    {
                        return Group.findPerm(l.permissionbuild).color + Owner(l) + l.PublicName + " &b[" + l.physics + "]";
                    }
                    return Owner(l) + l.PublicName + " &b[" + l.physics + "]";
                }
                return Group.findPerm(l.permissionvisit) != null ? Group.findPerm(l.permissionvisit).color + Owner(l) + l.PublicName + " &b[" + l.physics + "]"
                    : Owner(l) + l.PublicName + " &b[" + l.physics + "]";
            });
            Player.SendMessage(p, string.Format("Loaded: {0}", string.Join(", ", source2.ToArray())));
            Player.SendMessage(p, "Use &4/unloaded for unloaded levels.");
        }

        static string Owner(Level l)
        {
            if (l.mapType != MapType.MyMap)
            {
                return "";
            }
            return Player.RemoveEmailDomain(l.Owner) + "/";
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/levels - Lists all loaded levels and their physics levels.");
        }
    }
}