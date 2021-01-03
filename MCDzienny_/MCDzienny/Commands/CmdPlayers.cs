using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCDzienny
{
    // Token: 0x02000284 RID: 644
    internal class CmdPlayers : Command
    {
        // Token: 0x170006E0 RID: 1760
        // (get) Token: 0x0600127A RID: 4730 RVA: 0x00065E84 File Offset: 0x00064084
        public override string name
        {
            get { return "players"; }
        }

        // Token: 0x170006E1 RID: 1761
        // (get) Token: 0x0600127B RID: 4731 RVA: 0x00065E8C File Offset: 0x0006408C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006E2 RID: 1762
        // (get) Token: 0x0600127C RID: 4732 RVA: 0x00065E94 File Offset: 0x00064094
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x170006E3 RID: 1763
        // (get) Token: 0x0600127D RID: 4733 RVA: 0x00065E9C File Offset: 0x0006409C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170006E4 RID: 1764
        // (get) Token: 0x0600127E RID: 4734 RVA: 0x00065EA0 File Offset: 0x000640A0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x0600127F RID: 4735 RVA: 0x00065EA4 File Offset: 0x000640A4
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
                IEnumerable<IGrouping<Group, Player>> enumerable;
                if (p == null || p.group.Permission >= LevelPermission.Operator)
                    enumerable = from pl in source
                        group pl by pl.@group
                        into g
                        orderby g.Key.Permission descending
                        select g;
                else
                    enumerable = from pl in source
                        where !pl.hidden || pl.IsRefree
                        group pl by pl.@group
                        into g
                        orderby g.Key.Permission descending
                        select g;
                var stringBuilder = new StringBuilder();
                foreach (var grouping in enumerable)
                {
                    if (stringBuilder.Length > 0) stringBuilder.Append(" ");
                    var stringBuilder2 = stringBuilder;
                    var array = new string[5];
                    array[0] = grouping.Key.color;
                    array[1] = "(";
                    array[2] = grouping.Key.name;
                    array[3] = ") ";
                    array[4] = string.Join(", ", (from pla in grouping
                        select pla.PublicName + (Server.afkset.Contains(pla.name) ? "/afk" : "") +
                               (pla.hidden ? pla.IsRefree ? "/ref" : "/hidden" : "")
                        into pla
                        orderby pla
                        select pla).ToArray());
                    stringBuilder2.Append(string.Concat(array));
                }

                var num = source.Count(pl => !pl.hidden || pl.IsRefree);
                var num2 = source.Count(pl => pl.hidden && !pl.IsRefree);
                Player.SendMessage(p,
                    string.Format("{0} players online: {1}",
                        num + ((p == null || p.group.Permission >= LevelPermission.Operator) && num2 > 0
                            ? "+" + num2
                            : ""), stringBuilder));
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001280 RID: 4736 RVA: 0x00066158 File Offset: 0x00064358
        private void ShowPlayersCpeSupport(Player player)
        {
            var copy = Player.players.GetCopy();
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
                source = from p in source
                    where !p.hidden
                    select p;
                source2 = from p in source2
                    where !p.hidden
                    select p;
            }

            var array = (from p in source
                select p.PublicName).ToArray();
            var array2 = (from p in source2
                select p.PublicName).ToArray();
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

            if (array.Length == 0) Player.SendMessage(player, "None of the players online supports CPE.");
        }

        // Token: 0x06001281 RID: 4737 RVA: 0x00066324 File Offset: 0x00064524
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/players - Shows name and general rank of all players.");
        }
    }
}