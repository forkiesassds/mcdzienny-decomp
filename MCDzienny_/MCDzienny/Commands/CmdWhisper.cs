namespace MCDzienny
{
    // Token: 0x020002EC RID: 748
    public class CmdWhisper : Command
    {
        // Token: 0x17000852 RID: 2130
        // (get) Token: 0x06001530 RID: 5424 RVA: 0x00074EC4 File Offset: 0x000730C4
        public override string name
        {
            get { return "whisper"; }
        }

        // Token: 0x17000853 RID: 2131
        // (get) Token: 0x06001531 RID: 5425 RVA: 0x00074ECC File Offset: 0x000730CC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000854 RID: 2132
        // (get) Token: 0x06001532 RID: 5426 RVA: 0x00074ED4 File Offset: 0x000730D4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000855 RID: 2133
        // (get) Token: 0x06001533 RID: 5427 RVA: 0x00074EDC File Offset: 0x000730DC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000856 RID: 2134
        // (get) Token: 0x06001534 RID: 5428 RVA: 0x00074EE0 File Offset: 0x000730E0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x06001535 RID: 5429 RVA: 0x00074EE4 File Offset: 0x000730E4
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                p.whisper = !p.whisper;
                p.whisperTo = "";
                if (p.whisper)
                {
                    Player.SendMessage(p, "All messages sent will now auto-whisper");
                    return;
                }

                Player.SendMessage(p, "Whisper chat turned off");
            }
            else
            {
                var player = Player.Find(message);
                if (player == null)
                {
                    p.whisperTo = "";
                    p.whisper = false;
                    Player.SendMessage(p, "Could not find player.");
                    return;
                }

                p.whisper = true;
                p.whisperTo = player.name;
                Player.SendMessage(p,
                    string.Format("Auto-whisper enabled.  All messages will now be sent to {0}.", player.PublicName));
            }
        }

        // Token: 0x06001536 RID: 5430 RVA: 0x00074F8C File Offset: 0x0007318C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whisper <name> - Makes all messages act like whispers");
        }
    }
}