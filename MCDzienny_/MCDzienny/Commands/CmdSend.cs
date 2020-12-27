namespace MCDzienny
{
    // Token: 0x020002C6 RID: 710
    public class CmdSend : Command
    {
        // Token: 0x170007BC RID: 1980
        // (get) Token: 0x06001429 RID: 5161 RVA: 0x0006FC24 File Offset: 0x0006DE24
        public override string name
        {
            get { return "send"; }
        }

        // Token: 0x170007BD RID: 1981
        // (get) Token: 0x0600142A RID: 5162 RVA: 0x0006FC2C File Offset: 0x0006DE2C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007BE RID: 1982
        // (get) Token: 0x0600142B RID: 5163 RVA: 0x0006FC34 File Offset: 0x0006DE34
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170007BF RID: 1983
        // (get) Token: 0x0600142C RID: 5164 RVA: 0x0006FC3C File Offset: 0x0006DE3C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007C0 RID: 1984
        // (get) Token: 0x0600142D RID: 5165 RVA: 0x0006FC40 File Offset: 0x0006DE40
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x0600142F RID: 5167 RVA: 0x0006FC4C File Offset: 0x0006DE4C
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }

            var player = Player.Find(message.Split(' ')[0]);
            string text;
            if (player != null)
                text = player.name;
            else
                text = message.Split(' ')[0];
            var message2 = message.Substring(message.IndexOf(' ') + 1);
            Player.SendToInbox(p.name, text, message2);
            Player.SendMessage(p, string.Format("Message sent to &5{0}.", text));
            if (player != null)
                player.SendMessage(string.Format("Message recieved from &5{0}.", p.name + Server.DefaultColor));
        }

        // Token: 0x06001430 RID: 5168 RVA: 0x0006FD0C File Offset: 0x0006DF0C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/send [name] <message> - Sends <message> to [name].");
        }
    }
}