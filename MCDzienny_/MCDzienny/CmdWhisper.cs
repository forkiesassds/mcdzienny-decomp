namespace MCDzienny
{
    public class CmdWhisper : Command
    {
        public override string name { get { return "whisper"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                p.whisper = !p.whisper;
                p.whisperTo = "";
                if (p.whisper)
                {
                    Player.SendMessage(p, "All messages sent will now auto-whisper");
                }
                else
                {
                    Player.SendMessage(p, "Whisper chat turned off");
                }
                return;
            }
            Player player = Player.Find(message);
            if (player == null)
            {
                p.whisperTo = "";
                p.whisper = false;
                Player.SendMessage(p, "Could not find player.");
            }
            else
            {
                p.whisper = true;
                p.whisperTo = player.name;
                Player.SendMessage(p, string.Format("Auto-whisper enabled.  All messages will now be sent to {0}.", player.PublicName));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whisper <name> - Makes all messages act like whispers");
        }
    }
}