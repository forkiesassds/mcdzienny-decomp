using System;
using System.Linq;

namespace MCDzienny
{
    public class CmdAccept : Command
    {
        readonly TimeSpan expirationTime = new TimeSpan(0, 5, 0);

        public override string name { get { return "accept"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (!p.ExtraData.ContainsKey("invitation"))
            {
                Player.SendMessage(p, "You don't have any invitation pending.");
                return;
            }
            object[] array = (object[])p.ExtraData["invitation"];
            DateTime dateTime = (DateTime)array[2];
            string inviter = array[1].ToString();
            if (dateTime < DateTime.Now.Subtract(expirationTime))
            {
                Player.SendMessage(p, string.Format("The invitation from {0} expired.", Player.RemoveEmailDomain(array[1].ToString())));
                p.ExtraData.Remove("invitation");
                return;
            }
            string mapName = array[0].ToString();
            Level level = Server.levels.SingleOrDefault(l => l.mapType == MapType.MyMap && l.name == mapName.ToLower() && l.Owner == inviter.ToLower());
            if (level == null)
            {
                Player.SendMessage(p, "The map is no longer loaded.");
                p.ExtraData.Remove("invitation");
                return;
            }
            Level level2 = p.level;
            p.level = level;
            p.SendUserMOTD();
            p.SendMap();
            p.SendPos(byte.MaxValue, (ushort)((0.5f + p.level.spawnx) * 32f), (ushort)((0.6f + p.level.spawny) * 32f), (ushort)((0.5f + p.level.spawnz) * 32f),
                      p.level.rotx, p.level.roty);
            level2.NotifyPopulationChanged();
            Player.SendMessage(p, "You accepted the invitation.");
            Player player = Player.Find(inviter);
            if (player != null)
            {
                Player.SendMessage(player, p.PublicName + " accepted your invitation.");
            }
            p.ExtraData.Remove("invitation");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/accept - accepts the invitation.");
        }
    }
}