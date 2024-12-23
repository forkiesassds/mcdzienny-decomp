namespace MCDzienny
{
    public class CmdCountdown : Command
    {
        public override string name { get { return "countdown"; } }

        public override string shortcut { get { return "cdown"; } }

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
                LavaSystem.time = result;
                LavaSystem.phase1holder = true;
                LavaSystem.UpdateTimeStatus();
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/countdown [time] - Counts down time to the lava flood. [time] in minutes");
            Player.SendMessage(p, "/countdown stop - Stops the countdown");
            Player.SendMessage(p, "/cdown - shortcut");
        }
    }
}