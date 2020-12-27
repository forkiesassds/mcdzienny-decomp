using System;

namespace MCDzienny
{
    // Token: 0x020000F9 RID: 249
    internal class CmdHeartbeat : Command
    {
        // Token: 0x17000367 RID: 871
        // (get) Token: 0x060007A7 RID: 1959 RVA: 0x000271BC File Offset: 0x000253BC
        public override string name
        {
            get { return "heartbeat"; }
        }

        // Token: 0x17000368 RID: 872
        // (get) Token: 0x060007A8 RID: 1960 RVA: 0x000271C4 File Offset: 0x000253C4
        public override string shortcut
        {
            get { return "beat"; }
        }

        // Token: 0x17000369 RID: 873
        // (get) Token: 0x060007A9 RID: 1961 RVA: 0x000271CC File Offset: 0x000253CC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700036A RID: 874
        // (get) Token: 0x060007AA RID: 1962 RVA: 0x000271D4 File Offset: 0x000253D4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700036B RID: 875
        // (get) Token: 0x060007AB RID: 1963 RVA: 0x000271D8 File Offset: 0x000253D8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Nobody; }
        }

        // Token: 0x060007AD RID: 1965 RVA: 0x000271E4 File Offset: 0x000253E4
        public override void Use(Player p, string message)
        {
            try
            {
                Heartbeat.Pump(Beat.MCDzienny);
            }
            catch (Exception ex)
            {
                Server.s.Log("Error with MCDzienny pump.");
                Server.ErrorLog(ex);
            }

            Player.SendMessage(p, "Heartbeat pump sent.");
        }

        // Token: 0x060007AE RID: 1966 RVA: 0x00027230 File Offset: 0x00025430
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/heartbeat - Forces a pump for the MCDzienny heartbeat.  DEBUG PURPOSES ONLY.");
        }
    }
}