using System;

namespace MCDzienny
{
    public class CmdRoll : Command
    {
        public override string name { get { return "roll"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            Random random = new Random();
            int val;
            try
            {
                val = int.Parse(message.Split(' ')[0]);
            }
            catch
            {
                val = 1;
            }
            int val2;
            try
            {
                val2 = int.Parse(message.Split(' ')[1]);
            }
            catch
            {
                val2 = 7;
            }
            Player.GlobalMessage(
                string.Format("{0} rolled a &a{1} ({2}|{3})", p.color + p.PublicName + Server.DefaultColor,
                              random.Next(Math.Min(val, val2), Math.Max(val, val2) + 1) + Server.DefaultColor, Math.Min(val, val2), Math.Max(val, val2)));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/roll [min] [max] - Rolls a random number between [min] and [max].");
        }
    }
}