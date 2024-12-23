using System;

namespace MCDzienny
{
    class CmdHeartbeat : Command
    {
        public override string name { get { return "heartbeat"; } }

        public override string shortcut { get { return "beat"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }

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

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/heartbeat - Forces a pump for the MCDzienny heartbeat.  DEBUG PURPOSES ONLY.");
        }
    }
}