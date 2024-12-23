namespace MCDzienny
{
    public class CmdTimeleft : Command
    {
        public override string name { get { return "timeleft"; } }

        public override string shortcut { get { return "tleft"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            int result;
            if (message == "stop")
            {
                LavaSystem.phase1holder = false;
                LavaSystem.phase2holder = false;
            }
            else if (!int.TryParse(message, out result))
            {
                Help(p);
            }
            else
            {
                LavaSystem.time2 = result;
                LavaSystem.phase2holder = true;
                LavaSystem.UpdateTimeStatus();
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/timeleft [time] - Counts down time to the end lava flood round. [time] in minutes");
            Player.SendMessage(p, "/timeleft stop - Stops the countdown");
            Player.SendMessage(p, "/tleft - shortcut");
        }
    }
}