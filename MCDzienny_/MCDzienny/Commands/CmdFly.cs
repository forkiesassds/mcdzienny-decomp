using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x0200010A RID: 266
    public class CmdFly : Command
    {
        // Token: 0x170003B4 RID: 948
        // (get) Token: 0x06000828 RID: 2088 RVA: 0x00028EE8 File Offset: 0x000270E8
        public override string name
        {
            get { return "fly"; }
        }

        // Token: 0x170003B5 RID: 949
        // (get) Token: 0x06000829 RID: 2089 RVA: 0x00028EF0 File Offset: 0x000270F0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003B6 RID: 950
        // (get) Token: 0x0600082A RID: 2090 RVA: 0x00028EF8 File Offset: 0x000270F8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003B7 RID: 951
        // (get) Token: 0x0600082B RID: 2091 RVA: 0x00028F00 File Offset: 0x00027100
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003B8 RID: 952
        // (get) Token: 0x0600082C RID: 2092 RVA: 0x00028F04 File Offset: 0x00027104
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x170003B9 RID: 953
        // (get) Token: 0x0600082D RID: 2093 RVA: 0x00028F08 File Offset: 0x00027108
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600082F RID: 2095 RVA: 0x00028F14 File Offset: 0x00027114
        public override void Use(Player p, string message)
        {
            p.isFlying = !p.isFlying;
            if (!p.isFlying) return;
            Player.SendMessage(p, "You are now flying. &cJump!");
            var thread = new Thread((ThreadStart) delegate
            {
                var list = new List<Pos>();
                var item = default(Pos);
                while (p.isFlying)
                {
                    Thread.Sleep(20);
                    try
                    {
                        var list2 = new List<Pos>();
                        var num = (ushort) (p.pos[0] / 32);
                        var num2 = (ushort) ((p.pos[1] - 60) / 32);
                        var num3 = (ushort) (p.pos[2] / 32);
                        try
                        {
                            for (var num4 = (ushort) (num - 2); num4 <= num + 2; num4 = (ushort) (num4 + 1))
                            for (var num5 = (ushort) (num2 - 1); num5 <= num2; num5 = (ushort) (num5 + 1))
                            for (var num6 = (ushort) (num3 - 2); num6 <= num3 + 2; num6 = (ushort) (num6 + 1))
                                if (p.level.GetTile(num4, num5, num6) == 0)
                                {
                                    item.x = num4;
                                    item.y = num5;
                                    item.z = num6;
                                    list2.Add(item);
                                }

                            var list3 = new List<Pos>();
                            foreach (var item2 in list)
                                if (!list2.Contains(item2))
                                {
                                    p.SendBlockchange(item2.x, item2.y, item2.z, 0);
                                    list3.Add(item2);
                                }

                            foreach (var item3 in list3) list.Remove(item3);
                            foreach (var item4 in list2)
                                if (!list.Contains(item4))
                                {
                                    list.Add(item4);
                                    p.SendBlockchange(item4.x, item4.y, item4.z, 20);
                                }

                            list2.Clear();
                            list3.Clear();
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                    }
                }

                foreach (var item5 in list) p.SendBlockchange(item5.x, item5.y, item5.z, 0);
                Player.SendMessage(p, "Stopped flying");
            });
            thread.Start();
        }

        // Token: 0x06000830 RID: 2096 RVA: 0x00028F80 File Offset: 0x00027180
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fly - Allows you to fly");
        }

        // Token: 0x0200010B RID: 267
        private struct Pos
        {
            // Token: 0x040003BD RID: 957
            public ushort x;

            // Token: 0x040003BE RID: 958
            public ushort y;

            // Token: 0x040003BF RID: 959
            public ushort z;
        }
    }
}