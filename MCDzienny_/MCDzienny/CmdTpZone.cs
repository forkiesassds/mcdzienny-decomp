namespace MCDzienny
{
    public class CmdTpZone : Command
    {
        public override string name { get { return "tpzone"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                message = "list";
            }
            string[] array = message.Split(' ');
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
                    if (num > p.level.ZoneList.Count)
                    {
                        num = p.level.ZoneList.Count;
                    }
                    if (num2 > p.level.ZoneList.Count)
                    {
                        Player.SendMessage(p, string.Format("No Zones beyond number {0}", p.level.ZoneList.Count - 1));
                        return;
                    }
                    Player.SendMessage(p, string.Format("Zones ({0} to {1}):", num2, num - 1));
                    for (int i = num2; i < num; i++)
                    {
                        Level.Zone zone = p.level.ZoneList[i];
                        Player.SendMessage(
                            p,
                            "&c" + i + " &b(" + zone.smallX + "-" + zone.bigX + ", " + zone.smallY + "-" + zone.bigY + ", " + zone.smallZ + "-" + zone.bigZ + ") &f" +
                            zone.Owner);
                    }
                }
                else
                {
                    for (int j = 0; j < p.level.ZoneList.Count; j++)
                    {
                        Level.Zone zone2 = p.level.ZoneList[j];
                        Player.SendMessage(
                            p,
                            "&c" + j + " &b(" + zone2.smallX + "-" + zone2.bigX + ", " + zone2.smallY + "-" + zone2.bigY + ", " + zone2.smallZ + "-" + zone2.bigZ +
                            ") &f" + zone2.Owner);
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
                Level.Zone zone3 = p.level.ZoneList[num3];
                p.SendPos(byte.MaxValue, (ushort)(zone3.bigX * 32 + 16), (ushort)(zone3.bigY * 32 + 32), (ushort)(zone3.bigZ * 32 + 16), p.rot[0], p.rot[1]);
                Player.SendMessage(p, string.Format("Teleported to zone &c{0} &b({1}, {2}, {3}) &f{4}", num3, zone3.bigX, zone3.bigY, zone3.bigZ, zone3.Owner));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tpzone <id> - Teleports to the zone with ID equal to <id>");
            Player.SendMessage(p, "/tpzone list - Lists all zones");
        }
    }
}