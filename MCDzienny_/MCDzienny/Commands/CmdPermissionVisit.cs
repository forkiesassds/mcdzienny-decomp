namespace MCDzienny
{
    // Token: 0x02000281 RID: 641
    public class CmdPermissionVisit : Command
    {
        // Token: 0x170006D1 RID: 1745
        // (get) Token: 0x06001262 RID: 4706 RVA: 0x00065590 File Offset: 0x00063790
        public override string name
        {
            get { return "pervisit"; }
        }

        // Token: 0x170006D2 RID: 1746
        // (get) Token: 0x06001263 RID: 4707 RVA: 0x00065598 File Offset: 0x00063798
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006D3 RID: 1747
        // (get) Token: 0x06001264 RID: 4708 RVA: 0x000655A0 File Offset: 0x000637A0
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170006D4 RID: 1748
        // (get) Token: 0x06001265 RID: 4709 RVA: 0x000655A8 File Offset: 0x000637A8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170006D5 RID: 1749
        // (get) Token: 0x06001266 RID: 4710 RVA: 0x000655AC File Offset: 0x000637AC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06001268 RID: 4712 RVA: 0x000655B8 File Offset: 0x000637B8
        public override void Use(Player p, string message)
        {
            var num = message.Split(' ').Length;
            if (message == "" || num > 2 || num < 1)
            {
                Help(p);
                return;
            }

            if (num == 1)
            {
                var levelPermission = Level.PermissionFromName(message);
                if (levelPermission == LevelPermission.Null)
                {
                    Player.SendMessage(p, "Not a valid rank");
                    return;
                }

                p.level.permissionvisit = levelPermission;
                Server.s.Log(p.level.name + " visit permission changed to " + message + ".");
                Player.GlobalMessageLevel(p.level, string.Format("visit permission changed to {0}.", message));
            }
            else
            {
                var num2 = message.IndexOf(' ');
                var text = message.Substring(0, num2).ToLower();
                var text2 = message.Substring(num2 + 1).ToLower();
                var levelPermission2 = Level.PermissionFromName(text2);
                if (levelPermission2 == LevelPermission.Null)
                {
                    Player.SendMessage(p, "Not a valid rank");
                    return;
                }

                var level = Level.Find(text);
                if (level != null)
                {
                    level.permissionvisit = levelPermission2;
                    Server.s.Log(level.name + " visit permission changed to " + text2 + ".");
                    Player.GlobalMessageLevel(level, string.Format("visit permission changed to {0}.", text2));
                    if (p != null && p.level != level)
                        Player.SendMessage(p,
                            string.Format("visit permission changed to {0} on {1}.", text2, level.name));
                    return;
                }

                Player.SendMessage(p, string.Format("There is no level \"{0}\" loaded.", text));
            }
        }

        // Token: 0x06001269 RID: 4713 RVA: 0x0006572C File Offset: 0x0006392C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/PerVisit <map> <rank> - Sets visiting permission for a map.");
        }
    }
}