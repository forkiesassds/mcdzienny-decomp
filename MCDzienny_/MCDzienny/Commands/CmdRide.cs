using System.Threading;

namespace MCDzienny
{
    // Token: 0x020002E5 RID: 741
    public class CmdRide : Command
    {
        // Token: 0x17000830 RID: 2096
        // (get) Token: 0x060014FA RID: 5370 RVA: 0x000745A4 File Offset: 0x000727A4
        public override string name
        {
            get { return "ride"; }
        }

        // Token: 0x17000831 RID: 2097
        // (get) Token: 0x060014FB RID: 5371 RVA: 0x000745AC File Offset: 0x000727AC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000832 RID: 2098
        // (get) Token: 0x060014FC RID: 5372 RVA: 0x000745B4 File Offset: 0x000727B4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000833 RID: 2099
        // (get) Token: 0x060014FD RID: 5373 RVA: 0x000745BC File Offset: 0x000727BC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000834 RID: 2100
        // (get) Token: 0x060014FE RID: 5374 RVA: 0x000745C0 File Offset: 0x000727C0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000835 RID: 2101
        // (get) Token: 0x060014FF RID: 5375 RVA: 0x000745C4 File Offset: 0x000727C4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001501 RID: 5377 RVA: 0x000745D0 File Offset: 0x000727D0
        public override void Use(Player p, string message)
        {
            p.onTrain = !p.onTrain;
            if (!p.onTrain) return;
            var thread = new Thread((ThreadStart) delegate
            {
                while (p.onTrain)
                {
                    Thread.Sleep(3);
                    var num = (ushort) (p.pos[0] / 32);
                    var num2 = (ushort) (p.pos[1] / 32);
                    var num3 = (ushort) (p.pos[2] / 32);
                    var num4 = (ushort) (num - 1);
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
                        for (num5 = (ushort) (num2 - 1); num5 <= num2 + 1; num5 = (ushort) (num5 + 1))
                        {
                            num6 = (ushort) (num3 - 1);
                            while (num6 <= num3 + 1)
                            {
                                if (p.level.GetTile(num4, num5, num6) != 230)
                                {
                                    num6 = (ushort) (num6 + 1);
                                    continue;
                                }

                                goto IL_0086;
                            }
                        }

                        num4 = (ushort) (num4 + 1);
                        continue;
                        IL_0086:
                        p.invincible = true;
                        p.trainGrab = true;
                        byte b = 0;
                        b = (byte) (num2 - num5 == -1 ? 240 : num2 - num5 != 0 ? 8 : 0);
                        if (num - num4 == -1)
                        {
                            if (num3 - num6 == -1)
                                p.SendPos(byte.MaxValue, (ushort) (num4 * 32 + 16), (ushort) ((num5 + 1) * 32 - 2),
                                    (ushort) (num6 * 32 + 16), 96, b);
                            else if (num3 - num6 == 0)
                                p.SendPos(byte.MaxValue, (ushort) (num4 * 32 + 16), (ushort) ((num5 + 1) * 32 - 2),
                                    (ushort) (num6 * 32 + 16), 64, b);
                            else
                                p.SendPos(byte.MaxValue, (ushort) (num4 * 32 + 16), (ushort) ((num5 + 1) * 32 - 2),
                                    (ushort) (num6 * 32 + 16), 32, b);
                        }
                        else if (num - num4 == 0)
                        {
                            if (num3 - num6 == -1)
                                p.SendPos(byte.MaxValue, (ushort) (num4 * 32 + 16), (ushort) ((num5 + 1) * 32 - 2),
                                    (ushort) (num6 * 32 + 16), 128, b);
                            else if (num3 - num6 != 0)
                                p.SendPos(byte.MaxValue, (ushort) (num4 * 32 + 16), (ushort) ((num5 + 1) * 32 - 2),
                                    (ushort) (num6 * 32 + 16), 0, b);
                        }
                        else if (num3 - num6 == -1)
                        {
                            p.SendPos(byte.MaxValue, (ushort) (num4 * 32 + 16), (ushort) ((num5 + 1) * 32 - 2),
                                (ushort) (num6 * 32 + 16), 160, b);
                        }
                        else if (num3 - num6 == 0)
                        {
                            p.SendPos(byte.MaxValue, (ushort) (num4 * 32 + 16), (ushort) ((num5 + 1) * 32 - 2),
                                (ushort) (num6 * 32 + 16), 192, b);
                        }
                        else
                        {
                            p.SendPos(byte.MaxValue, (ushort) (num4 * 32 + 16), (ushort) ((num5 + 1) * 32 - 2),
                                (ushort) (num6 * 32 + 16), 224, b);
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

        // Token: 0x06001502 RID: 5378 RVA: 0x0007463C File Offset: 0x0007283C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ride - Rides a nearby train.");
        }
    }
}