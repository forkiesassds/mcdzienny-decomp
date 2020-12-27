namespace MCDzienny
{
    // Token: 0x02000108 RID: 264
    public class CmdTpZone : Command
    {
        // Token: 0x170003A9 RID: 937
        // (get) Token: 0x06000817 RID: 2071 RVA: 0x00028978 File Offset: 0x00026B78
        public override string name
        {
            get { return "tpzone"; }
        }

        // Token: 0x170003AA RID: 938
        // (get) Token: 0x06000818 RID: 2072 RVA: 0x00028980 File Offset: 0x00026B80
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003AB RID: 939
        // (get) Token: 0x06000819 RID: 2073 RVA: 0x00028988 File Offset: 0x00026B88
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003AC RID: 940
        // (get) Token: 0x0600081A RID: 2074 RVA: 0x00028990 File Offset: 0x00026B90
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170003AD RID: 941
        // (get) Token: 0x0600081B RID: 2075 RVA: 0x00028994 File Offset: 0x00026B94
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x170003AE RID: 942
        // (get) Token: 0x0600081C RID: 2076 RVA: 0x00028998 File Offset: 0x00026B98
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600081E RID: 2078 RVA: 0x000289A4 File Offset: 0x00026BA4
        public override void Use(Player p, string message)
        {
            if (message == "") message = "list";
            var array = message.Split(' ');
            if (array[0].ToLower() == "list")
            {
                if (array.Length > 1)
                {
                    int num;
                    int num2;
                    try
                    {
                        num = int.Parse(array[1]) * 10;
                        num2 = num - 10;
                    }
                    catch
                    {
                        Help(p);
                        return;
                    }

                    if (num2 < 0)
                    {
                        Player.SendMessage(p, "Must be greater than 0");
                        return;
                    }

                    if (num > p.level.ZoneList.Count) num = p.level.ZoneList.Count;
                    if (num2 > p.level.ZoneList.Count)
                    {
                        Player.SendMessage(p, string.Format("No Zones beyond number {0}", p.level.ZoneList.Count - 1));
                        return;
                    }

                    Player.SendMessage(p, string.Format("Zones ({0} to {1}):", num2, num - 1));
                    for (var i = num2; i < num; i++)
                    {
                        var zone = p.level.ZoneList[i];
                        Player.SendMessage(p,
                            "&c" + i + " &b(" + zone.smallX + "-" + zone.bigX + ", " + zone.smallY + "-" + zone.bigY +
                            ", " + zone.smallZ + "-" + zone.bigZ + ") &f" + zone.Owner);
                    }
                }
                else
                {
                    for (var j = 0; j < p.level.ZoneList.Count; j++)
                    {
                        var zone2 = p.level.ZoneList[j];
                        Player.SendMessage(p,
                            "&c" + j + " &b(" + zone2.smallX + "-" + zone2.bigX + ", " + zone2.smallY + "-" +
                            zone2.bigY + ", " + zone2.smallZ + "-" + zone2.bigZ + ") &f" + zone2.Owner);
                    }

                    Player.SendMessage(p, "For a more structured list, use /tpzone list <1/2/3/..>");
                }
            }
            else
            {
                int num3;
                try
                {
                    num3 = int.Parse(message);
                }
                catch
                {
                    Help(p);
                    return;
                }

                if (num3 < 0 || num3 > p.level.ZoneList.Count)
                {
                    Player.SendMessage(p, "This zone doesn't exist");
                    return;
                }

                var zone3 = p.level.ZoneList[num3];
                p.SendPos(byte.MaxValue, (ushort) (zone3.bigX * 32 + 16), (ushort) (zone3.bigY * 32 + 32),
                    (ushort) (zone3.bigZ * 32 + 16), p.rot[0], p.rot[1]);
                Player.SendMessage(p,
                    string.Format("Teleported to zone &c{0} &b({1}, {2}, {3}) &f{4}", num3, zone3.bigX, zone3.bigY,
                        zone3.bigZ, zone3.Owner));
            }
        }

        // Token: 0x0600081F RID: 2079 RVA: 0x00028DD8 File Offset: 0x00026FD8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tpzone <id> - Teleports to the zone with ID equal to <id>");
            Player.SendMessage(p, "/tpzone list - Lists all zones");
        }
    }
}