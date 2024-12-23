using System;
using MCDzienny.Settings;

namespace MCDzienny
{
    class CmdAka : Command
    {
        public override string name { get { return "aka"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            Func<Player, string> getName = null;
            if (p.ShowAlias)
            {
                getName = GetTrueName;
                p.ShowAlias = false;
            }
            else
            {
                getName = GetMaskedName;
                p.ShowAlias = true;
            }
            p.DespawnAll();
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (p.level == pl.level && p != pl && !pl.hidden)
                {
                    p.SendSpawn(pl, getName(pl));
                }
            });
        }

        string GetTrueName(Player p)
        {
            return (p.isZombie ? "&c" : "") + p.PublicName;
        }

        string GetMaskedName(Player p)
        {
            if (!p.isZombie)
            {
                return p.color + (p.IsRefree ? "[REF]" : "") + p.PublicName;
            }
            return InfectionSettings.All.ZombieAlias;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/aka - switches between players' real name and temporary aliases.");
        }
    }
}