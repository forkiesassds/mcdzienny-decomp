using System;

namespace MCDzienny
{
    // Token: 0x020002DC RID: 732
    public class CmdRoll : Command
    {
        // Token: 0x17000807 RID: 2055
        // (get) Token: 0x060014B8 RID: 5304 RVA: 0x000728B0 File Offset: 0x00070AB0
        public override string name
        {
            get { return "roll"; }
        }

        // Token: 0x17000808 RID: 2056
        // (get) Token: 0x060014B9 RID: 5305 RVA: 0x000728B8 File Offset: 0x00070AB8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000809 RID: 2057
        // (get) Token: 0x060014BA RID: 5306 RVA: 0x000728C0 File Offset: 0x00070AC0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700080A RID: 2058
        // (get) Token: 0x060014BB RID: 5307 RVA: 0x000728C8 File Offset: 0x00070AC8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700080B RID: 2059
        // (get) Token: 0x060014BC RID: 5308 RVA: 0x000728CC File Offset: 0x00070ACC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060014BE RID: 5310 RVA: 0x000728D8 File Offset: 0x00070AD8
        public override void Use(Player p, string message)
        {
            var random = new Random();
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

            Player.GlobalMessage(string.Format("{0} rolled a &a{1} ({2}|{3})",
                p.color + p.PublicName + Server.DefaultColor,
                random.Next(Math.Min(val, val2), Math.Max(val, val2) + 1) + Server.DefaultColor, Math.Min(val, val2),
                Math.Max(val, val2)));
        }

        // Token: 0x060014BF RID: 5311 RVA: 0x000729D0 File Offset: 0x00070BD0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/roll [min] [max] - Rolls a random number between [min] and [max].");
        }
    }
}