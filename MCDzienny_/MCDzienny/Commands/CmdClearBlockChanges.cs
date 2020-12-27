namespace MCDzienny
{
    // Token: 0x020002D0 RID: 720
    public class CmdClearBlockChanges : Command
    {
        // Token: 0x170007DE RID: 2014
        // (get) Token: 0x06001466 RID: 5222 RVA: 0x00070E88 File Offset: 0x0006F088
        public override string name
        {
            get { return "clearblockchanges"; }
        }

        // Token: 0x170007DF RID: 2015
        // (get) Token: 0x06001467 RID: 5223 RVA: 0x00070E90 File Offset: 0x0006F090
        public override string shortcut
        {
            get { return "cbc"; }
        }

        // Token: 0x170007E0 RID: 2016
        // (get) Token: 0x06001468 RID: 5224 RVA: 0x00070E98 File Offset: 0x0006F098
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170007E1 RID: 2017
        // (get) Token: 0x06001469 RID: 5225 RVA: 0x00070EA0 File Offset: 0x0006F0A0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170007E2 RID: 2018
        // (get) Token: 0x0600146A RID: 5226 RVA: 0x00070EA4 File Offset: 0x0006F0A4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x0600146C RID: 5228 RVA: 0x00070EB0 File Offset: 0x0006F0B0
        public override void Use(Player p, string message)
        {
            var level = Level.Find(message);
            if (level == null && message != "")
            {
                Player.SendMessage(p, "Could not find level.");
                return;
            }

            if (level == null) level = p.level;
            if (Server.useMySQL)
                DBInterface.ExecuteQuery("TRUNCATE TABLE `Block" + level.name + "`");
            else
                DBInterface.ExecuteQuery("DELETE FROM `Block" + level.name + "`");
            Player.SendMessage(p,
                string.Format("Cleared &cALL{0} recorded block changes in: &d{1}", Server.DefaultColor, level.name));
        }

        // Token: 0x0600146D RID: 5229 RVA: 0x00070F44 File Offset: 0x0006F144
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clearblockchanges <map> - Clears the block changes stored in /about for <map>.");
            Player.SendMessage(p, "&cUSE WITH CAUTION");
        }
    }
}