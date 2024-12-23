using System;
using System.Threading;

namespace MCDzienny
{
    public class CmdTimedMessage : Command
    {

        public override string name { get { return "tmessage"; } }

        public override string shortcut { get { return "tmsg"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            int num = message.Split(' ').Length;
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
                string message2 = message.Substring(message.IndexOf(' '));
                Holder.Tester = true;
                Timed @object = new Timed(message2, time);
                Thread thread = new Thread(@object.Fetch);
                thread.Start();
            }
            if (num == 1)
            {
                if (message == "stop")
                {
                    Holder.Tester = false;
                }
                else
                {
                    Help(p);
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tmessage [time] [message] - show your message continuously every [time] seconds.");
        }

        public class Timed
        {
            readonly string message;

            readonly int time;

            public Timed(string message, int time)
            {
                this.message = message;
                this.time = time;
            }

            public void Fetch()
            {
                new Holder();
                while (Holder.Tester)
                {
                    Player.GlobalChatWorld(null, message, showname: false);
                    Thread.Sleep(time * 1000);
                }
            }
        }

        class Holder
        {

            public static bool Tester { get; set; }
        }
    }
}