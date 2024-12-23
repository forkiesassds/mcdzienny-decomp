using System.Threading;

namespace MCDzienny
{
    public class CmdRide : Command
    {
        public override string name { get { return "ride"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            p.onTrain = !p.onTrain;
            if (!p.onTrain)
            {
                return;
            }
            Thread thread = new Thread((ThreadStart)delegate
            {
                while (p.onTrain)
                {
                    Thread.Sleep(3);
                    ushort num = (ushort)(p.pos[0] / 32);
                    ushort num2 = (ushort)(p.pos[1] / 32);
                    ushort num3 = (ushort)(p.pos[2] / 32);
                    ushort num4 = (ushort)(num - 1);
                    while (true)
                    {
                        if (num4 > num + 1)
                        {
                            Thread.Sleep(3);
                            p.invincible = false;
                            p.trainGrab = false;
                            break;
                        }
                        ushort num5;
                        ushort num6;
                        for (num5 = (ushort)(num2 - 1); num5 <= num2 + 1; num5++)
                        {
                            num6 = (ushort)(num3 - 1);
                            while (num6 <= num3 + 1)
                            {
                                if (p.level.GetTile(num4, num5, num6) != 230)
                                {
                                    num6++;
                                    continue;
                                }
                                goto IL_0086;
                            }
                        }
                        num4++;
                        continue;
                        IL_0086:
                        p.invincible = true;
                        p.trainGrab = true;
                        byte b = 0;
                        b = (byte)(num2 - num5 == -1 ? 240 : num2 - num5 != 0 ? 8 : 0);
                        if (num - num4 == -1)
                        {
                            if (num3 - num6 == -1)
                            {
                                p.SendPos(byte.MaxValue, (ushort)(num4 * 32 + 16), (ushort)((num5 + 1) * 32 - 2), (ushort)(num6 * 32 + 16), 96, b);
                            }
                            else if (num3 - num6 == 0)
                            {
                                p.SendPos(byte.MaxValue, (ushort)(num4 * 32 + 16), (ushort)((num5 + 1) * 32 - 2), (ushort)(num6 * 32 + 16), 64, b);
                            }
                            else
                            {
                                p.SendPos(byte.MaxValue, (ushort)(num4 * 32 + 16), (ushort)((num5 + 1) * 32 - 2), (ushort)(num6 * 32 + 16), 32, b);
                            }
                        }
                        else if (num - num4 == 0)
                        {
                            if (num3 - num6 == -1)
                            {
                                p.SendPos(byte.MaxValue, (ushort)(num4 * 32 + 16), (ushort)((num5 + 1) * 32 - 2), (ushort)(num6 * 32 + 16), 128, b);
                            }
                            else if (num3 - num6 != 0)
                            {
                                p.SendPos(byte.MaxValue, (ushort)(num4 * 32 + 16), (ushort)((num5 + 1) * 32 - 2), (ushort)(num6 * 32 + 16), 0, b);
                            }
                        }
                        else if (num3 - num6 == -1)
                        {
                            p.SendPos(byte.MaxValue, (ushort)(num4 * 32 + 16), (ushort)((num5 + 1) * 32 - 2), (ushort)(num6 * 32 + 16), 160, b);
                        }
                        else if (num3 - num6 == 0)
                        {
                            p.SendPos(byte.MaxValue, (ushort)(num4 * 32 + 16), (ushort)((num5 + 1) * 32 - 2), (ushort)(num6 * 32 + 16), 192, b);
                        }
                        else
                        {
                            p.SendPos(byte.MaxValue, (ushort)(num4 * 32 + 16), (ushort)((num5 + 1) * 32 - 2), (ushort)(num6 * 32 + 16), 224, b);
                        }
                        break;
                    }
                }
                Player.SendMessage(p, "Dismounted");
                Thread.Sleep(1000);
                p.invincible = false;
                p.trainGrab = false;
            });
            thread.Start();
            Player.SendMessage(p, "Stand near a train to mount it");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ride - Rides a nearby train.");
        }
    }
}