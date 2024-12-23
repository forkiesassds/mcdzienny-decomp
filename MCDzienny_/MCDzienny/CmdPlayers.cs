using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCDzienny
{
    class CmdPlayers : Command
    {
        public override string name { get { return "players"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            if (string.Equals(message, "cpe", StringComparison.OrdinalIgnoreCase))
            {
                ShowPlayersCpeSupport(p);
                return;
            }
            try
            {
                var source = new List<Player>(Player.players);
                IEnumerable<IGrouping<Group, Player>> enumerable = p != null && p.group.Permission < LevelPermission.Operator ? from pl in source
                    where !pl.hidden || pl.IsRefree
                    group pl by pl.@group
                    into g
                    orderby g.Key.Permission descending
                    select g : from pl in source
                    group pl by pl.@group
                    into g
                    orderby g.Key.Permission descending
                    select g;
                StringBuilder stringBuilder = new StringBuilder();
                foreach (IGrouping<Group, Player> item in enumerable)
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(" ");
                    }
                    stringBuilder.Append(item.Key.color + "(" + item.Key.name + ") " + string.Join(", ", (from pla in item
                                                                                                       select pla.PublicName +
                                                                                                           (Server.afkset.Contains(pla.name) ? "/afk" : "") +
                                                                                                           (!pla.hidden ? "" : pla.IsRefree ? "/ref" : "/hidden")
                                                                                                       into pla
                                                                                                       orderby pla
                                                                                                       select pla).ToArray()));
                }
                int num = source.Count(pl => !pl.hidden || pl.IsRefree);
                int num2 = source.Count(pl => pl.hidden && !pl.IsRefree);
                Player.SendMessage(
                    p,
                    string.Format("{0} players online: {1}", num + ((p == null || p.group.Permission >= LevelPermission.Operator) && num2 > 0 ? "+" + num2 : ""),
                                  stringBuilder));
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        void ShowPlayersCpeSupport(Player player)
        {
            List<Player> copy = Player.players.GetCopy();
            IEnumerable<Player> source = from p in copy
                where p.IsCpeSupported
                orderby p.PublicName
                select p;
            IEnumerable<Player> source2 = from p in copy
                where !p.IsCpeSupported
                orderby p.PublicName
                select p;
            if (player != null && player.group.Permission < LevelPermission.Operator)
            {
                source = source.Where(p => !p.hidden);
                source2 = source2.Where(p => !p.hidden);
            }
            string[] array = source.Select(p => p.PublicName).ToArray();
            string[] array2 = source2.Select(p => p.PublicName).ToArray();
            if (array2.Length == 0 && array.Length == 0)
            {
                Player.SendMessage(player, "There are no players online.");
                return;
            }
            if (array.Length > 0)
            {
                Player.SendMessage(player, "Players that support CPE:");
                Player.SendMessage(player, string.Join(", ", array));
            }
            if (array2.Length == 0)
            {
                Player.SendMessage(player, "All the players online support CPE.");
            }
            else
            {
                Player.SendMessage(player, "Players that don't support CPE:");
                Player.SendMessage(player, string.Join(", ", array2));
            }
            if (array.Length == 0)
            {
                Player.SendMessage(player, "None of the players online supports CPE.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/players - Shows name and general rank of all players.");
        }
    }
}