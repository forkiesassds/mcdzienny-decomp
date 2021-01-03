namespace MCDzienny
{
    // Token: 0x020002C5 RID: 709
    public class CmdStatic : Command
    {
        // Token: 0x170007B5 RID: 1973
        // (get) Token: 0x0600141F RID: 5151 RVA: 0x0006FAB0 File Offset: 0x0006DCB0
        public override string name
        {
            get { return "static"; }
        }

        // Token: 0x170007B6 RID: 1974
        // (get) Token: 0x06001420 RID: 5152 RVA: 0x0006FAB8 File Offset: 0x0006DCB8
        public override string shortcut
        {
            get { return "t"; }
        }

        // Token: 0x170007B7 RID: 1975
        // (get) Token: 0x06001421 RID: 5153 RVA: 0x0006FAC0 File Offset: 0x0006DCC0
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170007B8 RID: 1976
        // (get) Token: 0x06001422 RID: 5154 RVA: 0x0006FAC8 File Offset: 0x0006DCC8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170007B9 RID: 1977
        // (get) Token: 0x06001423 RID: 5155 RVA: 0x0006FACC File Offset: 0x0006DCCC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170007BA RID: 1978
        // (get) Token: 0x06001424 RID: 5156 RVA: 0x0006FAD0 File Offset: 0x0006DCD0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170007BB RID: 1979
        // (get) Token: 0x06001425 RID: 5157 RVA: 0x0006FAD4 File Offset: 0x0006DCD4
        public override CommandScope Scope
        {
            get { return CommandScope.Freebuild; }
        }

        // Token: 0x06001426 RID: 5158 RVA: 0x0006FAD8 File Offset: 0x0006DCD8
        public override void Use(Player p, string message)
        {
            p.staticCommands = !p.staticCommands;
            p.ClearBlockchange();
            p.BlockAction = 0;
            Player.SendMessage(p, string.Format("Static mode: &a{0}", p.staticCommands.ToString()));
            try
            {
                if (message != "")
                {
                    if (message.IndexOf(' ') == -1)
                    {
                        if (p.group.CanExecute(all.Find(message)))
                            all.Find(message).Use(p, "");
                        else
                            Player.SendMessage(p, "Cannot use that command.");
                    }
                    else if (p.group.CanExecute(all.Find(message.Split(' ')[0])))
                    {
                        all.Find(message.Split(' ')[0]).Use(p, message.Substring(message.IndexOf(' ') + 1));
                    }
                    else
                    {
                        Player.SendMessage(p, "Cannot use that command.");
                    }
                }
            }
            catch
            {
                Player.SendMessage(p, "Could not find specified command");
            }
        }

        // Token: 0x06001427 RID: 5159 RVA: 0x0006FC04 File Offset: 0x0006DE04
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/static [command] - Makes every command a toggle.");
            Player.SendMessage(p, "If [command] is given, then that command is used");
        }
    }
}