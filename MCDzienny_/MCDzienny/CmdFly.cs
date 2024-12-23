using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    public class CmdFly : Command
    {

        public override string name { get { return "fly"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            p.isFlying = !p.isFlying;
            if (!p.isFlying)
            {
                return;
            }
            Player.SendMessage(p, "You are now flying. &cJump!");
            Thread thread = new Thread((ThreadStart)delegate
            {
                var list = new List<Pos>();
                Pos item = default(Pos);
                while (p.isFlying)
                {
                    Thread.Sleep(20);
                    try
                    {
                        var list2 = new List<Pos>();
                        ushort num = (ushort)(p.pos[0] / 32);
                        ushort num2 = (ushort)((p.pos[1] - 60) / 32);
                        ushort num3 = (ushort)(p.pos[2] / 32);
                        try
                        {
                            for (ushort num4 = (ushort)(num - 2); num4 <= num + 2; num4++)
                            {
                                for (ushort num5 = (ushort)(num2 - 1); num5 <= num2; num5++)
                                {
                                    for (ushort num6 = (ushort)(num3 - 2); num6 <= num3 + 2; num6++)
                                    {
                                        if (p.level.GetTile(num4, num5, num6) == 0)
                                        {
                                            item.x = num4;
                                            item.y = num5;
                                            item.z = num6;
                                            list2.Add(item);
                                        }
                                    }
                                }
                            }
                            var list3 = new List<Pos>();
                            foreach (Pos item2 in list)
                            {
                                if (!list2.Contains(item2))
                                {
                                    p.SendBlockchange(item2.x, item2.y, item2.z, 0);
                                    list3.Add(item2);
                                }
                            }
                            foreach (Pos item3 in list3)
                            {
                                list.Remove(item3);
                            }
                            foreach (Pos item4 in list2)
                            {
                                if (!list.Contains(item4))
                                {
                                    list.Add(item4);
                                    p.SendBlockchange(item4.x, item4.y, item4.z, 20);
                                }
                            }
                            list2.Clear();
                            list3.Clear();
                        }
                        catch {}
                    }
                    catch {}
                }
                foreach (Pos item5 in list)
                {
                    p.SendBlockchange(item5.x, item5.y, item5.z, 0);
                }
                Player.SendMessage(p, "Stopped flying");
            });
            thread.Start();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fly - Allows you to fly");
        }

        struct Pos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}