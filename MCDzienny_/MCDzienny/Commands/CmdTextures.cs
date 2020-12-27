namespace MCDzienny.Commands
{
    // Token: 0x0200007A RID: 122
    public class CmdTextures : Command
    {
        // Token: 0x170000DE RID: 222
        // (get) Token: 0x06000321 RID: 801 RVA: 0x00011B80 File Offset: 0x0000FD80
        public override string name
        {
            get { return "textures"; }
        }

        // Token: 0x170000DF RID: 223
        // (get) Token: 0x06000322 RID: 802 RVA: 0x00011B88 File Offset: 0x0000FD88
        public override string shortcut
        {
            get { return "texture"; }
        }

        // Token: 0x170000E0 RID: 224
        // (get) Token: 0x06000323 RID: 803 RVA: 0x00011B90 File Offset: 0x0000FD90
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000E1 RID: 225
        // (get) Token: 0x06000324 RID: 804 RVA: 0x00011B98 File Offset: 0x0000FD98
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000E2 RID: 226
        // (get) Token: 0x06000325 RID: 805 RVA: 0x00011B9C File Offset: 0x0000FD9C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170000E3 RID: 227
        // (get) Token: 0x06000326 RID: 806 RVA: 0x00011BA0 File Offset: 0x0000FDA0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000328 RID: 808 RVA: 0x00011BAC File Offset: 0x0000FDAC
        public override void Use(Player p, string message)
        {
            Player player;
            if (message != "")
            {
                if (p != null && p.group.Permission < LevelPermission.Operator)
                {
                    Player.SendMessage(p, "@ Your level is not high enough for using this command on other players.");
                    return;
                }

                player = Player.Find(message.Trim());
            }
            else
            {
                if (p == null)
                {
                    Help(p);
                    return;
                }

                player = p;
            }

            if (player != null && player.IsUsingWom)
            {
                if (player.IsUsingTextures)
                {
                    player.RemoveTextures();
                    Player.SendMessage(p, string.Format("@ {0} stops using custom texture pack.", player.PublicName));
                    return;
                }

                player.SendTextures();
                Player.SendMessage(p, string.Format("@ {0} starts using custom texture pack.", player.PublicName));
            }
        }

        // Token: 0x06000329 RID: 809 RVA: 0x00011C50 File Offset: 0x0000FE50
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/textures - toggles the use of custom texture pack.");
        }
    }
}