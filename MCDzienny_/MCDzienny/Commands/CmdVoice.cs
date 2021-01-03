namespace MCDzienny
{
    // Token: 0x020002AC RID: 684
    public class CmdVoice : Command
    {
        // Token: 0x1700077A RID: 1914
        // (get) Token: 0x0600139B RID: 5019 RVA: 0x0006BF18 File Offset: 0x0006A118
        public override string name
        {
            get { return "voice"; }
        }

        // Token: 0x1700077B RID: 1915
        // (get) Token: 0x0600139C RID: 5020 RVA: 0x0006BF20 File Offset: 0x0006A120
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700077C RID: 1916
        // (get) Token: 0x0600139D RID: 5021 RVA: 0x0006BF28 File Offset: 0x0006A128
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700077D RID: 1917
        // (get) Token: 0x0600139E RID: 5022 RVA: 0x0006BF30 File Offset: 0x0006A130
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700077E RID: 1918
        // (get) Token: 0x0600139F RID: 5023 RVA: 0x0006BF34 File Offset: 0x0006A134
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060013A1 RID: 5025 RVA: 0x0006BF40 File Offset: 0x0006A140
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, string.Format("There is no player online named \"{0}\"", message));
                return;
            }

            if (player.voice)
            {
                player.voice = false;
                Player.SendMessage(p, string.Format("Removing voice status from {0}", player.PublicName));
                player.SendMessage("Your voice status has been revoked.");
                player.voicestring = "";
                return;
            }

            player.voice = true;
            Player.SendMessage(p, string.Format("Giving voice status to {0}", player.PublicName));
            player.SendMessage("You have received voice status.");
            player.voicestring = "&f+";
        }

        // Token: 0x060013A2 RID: 5026 RVA: 0x0006BFF0 File Offset: 0x0006A1F0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/voice <name> - Toggles voice status on or off for specified player.");
        }
    }
}