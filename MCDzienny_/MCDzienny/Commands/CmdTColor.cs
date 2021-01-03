namespace MCDzienny
{
    // Token: 0x020000E4 RID: 228
    public class CmdTColor : Command
    {
        // Token: 0x1700034C RID: 844
        // (get) Token: 0x0600075A RID: 1882 RVA: 0x000250C4 File Offset: 0x000232C4
        public override string name
        {
            get { return "tcolor"; }
        }

        // Token: 0x1700034D RID: 845
        // (get) Token: 0x0600075B RID: 1883 RVA: 0x000250CC File Offset: 0x000232CC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700034E RID: 846
        // (get) Token: 0x0600075C RID: 1884 RVA: 0x000250D4 File Offset: 0x000232D4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700034F RID: 847
        // (get) Token: 0x0600075D RID: 1885 RVA: 0x000250DC File Offset: 0x000232DC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000350 RID: 848
        // (get) Token: 0x0600075E RID: 1886 RVA: 0x000250E0 File Offset: 0x000232E0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x06000760 RID: 1888 RVA: 0x000250EC File Offset: 0x000232EC
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            message = message.Replace("'", "");
            var array = message.Split(' ');
            var player = Player.Find(array[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player.");
                return;
            }

            if (array.Length == 1)
            {
                player.titlecolor = "";
                Player.GlobalChat(player,
                    string.Format("{0} had their title color removed.",
                        player.color + player.PublicName + Server.DefaultColor), false);
                DBInterface.ExecuteQuery("UPDATE Players SET title_color = '' WHERE Name = '" + player.name + "'");
                player.SetPrefix();
                return;
            }

            var text = c.Parse(array[1]);
            if (text == "")
            {
                Player.SendMessage(p, string.Format("There is no color \"{0}\".", array[1]));
                return;
            }

            if (text == player.titlecolor)
            {
                Player.SendMessage(p, player.PublicName + " already has that title color.");
                return;
            }

            DBInterface.ExecuteQuery(string.Concat("UPDATE Players SET title_color = '", c.Name(text),
                "' WHERE Name = '", player.name, "'"));
            Player.GlobalChat(player,
                string.Format("{0} had their title color changed to {1}.",
                    player.color + player.PublicName + Server.DefaultColor, text + c.Name(text) + Server.DefaultColor),
                false);
            player.titlecolor = text;
            player.SetPrefix();
        }

        // Token: 0x06000761 RID: 1889 RVA: 0x0002527C File Offset: 0x0002347C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tcolor <player> [color] - Gives <player> the title color of [color].");
            Player.SendMessage(p, "If no [color] is specified, title color is removed.");
            Player.SendMessage(p, "&0black &1navy &2green &3teal &4maroon &5purple &6gold &7silver");
            Player.SendMessage(p, "&8gray &9blue &alime &baqua &cred &dpink &eyellow &fwhite");
        }
    }
}