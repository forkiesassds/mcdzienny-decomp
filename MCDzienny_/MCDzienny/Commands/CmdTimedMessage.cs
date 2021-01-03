using System;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x02000129 RID: 297
    public class CmdTimedMessage : Command
    {
        // Token: 0x17000415 RID: 1045
        // (get) Token: 0x060008E1 RID: 2273 RVA: 0x0002C99C File Offset: 0x0002AB9C
        public override string name
        {
            get { return "tmessage"; }
        }

        // Token: 0x17000416 RID: 1046
        // (get) Token: 0x060008E2 RID: 2274 RVA: 0x0002C9A4 File Offset: 0x0002ABA4
        public override string shortcut
        {
            get { return "tmsg"; }
        }

        // Token: 0x17000417 RID: 1047
        // (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002C9AC File Offset: 0x0002ABAC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000418 RID: 1048
        // (get) Token: 0x060008E4 RID: 2276 RVA: 0x0002C9B4 File Offset: 0x0002ABB4
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000419 RID: 1049
        // (get) Token: 0x060008E5 RID: 2277 RVA: 0x0002C9B8 File Offset: 0x0002ABB8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060008E7 RID: 2279 RVA: 0x0002C9C4 File Offset: 0x0002ABC4
        public override void Use(Player p, string message)
        {
            var num = message.Split(' ').Length;
            if (num > 1)
            {
                int time;
                try
                {
                    time = Convert.ToInt32(message.Split(' ')[0]);
                }
                catch
                {
                    Help(p);
                    return;
                }

                var message2 = message.Substring(message.IndexOf(' '));
                Holder.Tester = true;
                var @object = new Timed(message2, time);
                var thread = new Thread(@object.Fetch);
                thread.Start();
            }

            if (num == 1)
            {
                if (message == "stop")
                {
                    Holder.Tester = false;
                    return;
                }

                Help(p);
            }
        }

        // Token: 0x060008E8 RID: 2280 RVA: 0x0002CA80 File Offset: 0x0002AC80
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tmessage [time] [message] - show your message continuously every [time] seconds.");
        }

        // Token: 0x0200012A RID: 298
        public class Timed
        {
            // Token: 0x040003E4 RID: 996
            private readonly string message;

            // Token: 0x040003E5 RID: 997
            private readonly int time;

            // Token: 0x060008E9 RID: 2281 RVA: 0x0002CA90 File Offset: 0x0002AC90
            public Timed(string message, int time)
            {
                this.message = message;
                this.time = time;
            }

            // Token: 0x060008EA RID: 2282 RVA: 0x0002CAA8 File Offset: 0x0002ACA8
            public void Fetch()
            {
                new Holder();
                while (Holder.Tester)
                {
                    Player.GlobalChatWorld(null, message, false);
                    Thread.Sleep(time * 1000);
                }
            }
        }

        // Token: 0x0200012B RID: 299
        private class Holder
        {
            // Token: 0x040003E6 RID: 998

            // Token: 0x1700041A RID: 1050
            // (get) Token: 0x060008EB RID: 2283 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
            // (set) Token: 0x060008EC RID: 2284 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
            public static bool Tester { get; set; }
        }
    }
}