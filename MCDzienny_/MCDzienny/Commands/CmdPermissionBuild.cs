namespace MCDzienny
{
    // Token: 0x02000280 RID: 640
    public class CmdPermissionBuild : Command
    {
        // Token: 0x170006CC RID: 1740
        // (get) Token: 0x0600125A RID: 4698 RVA: 0x000653E4 File Offset: 0x000635E4
        public override string name
        {
            get { return "perbuild"; }
        }

        // Token: 0x170006CD RID: 1741
        // (get) Token: 0x0600125B RID: 4699 RVA: 0x000653EC File Offset: 0x000635EC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006CE RID: 1742
        // (get) Token: 0x0600125C RID: 4700 RVA: 0x000653F4 File Offset: 0x000635F4
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170006CF RID: 1743
        // (get) Token: 0x0600125D RID: 4701 RVA: 0x000653FC File Offset: 0x000635FC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170006D0 RID: 1744
        // (get) Token: 0x0600125E RID: 4702 RVA: 0x00065400 File Offset: 0x00063600
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06001260 RID: 4704 RVA: 0x0006540C File Offset: 0x0006360C
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

                p.level.permissionbuild = levelPermission;
                Server.s.Log(p.level.name + " build permission changed to " + message + ".");
                Player.GlobalMessageLevel(p.level, string.Format("build permission changed to {0}.", message));
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
                    level.permissionbuild = levelPermission2;
                    Server.s.Log(level.name + " build permission changed to " + text2 + ".");
                    Player.GlobalMessageLevel(level, string.Format("build permission changed to {0}.", text2));
                    if (p != null && p.level != level)
                        Player.SendMessage(p,
                            string.Format("build permission changed to {0} on {1}.", text2, level.name));
                    return;
                }

                Player.SendMessage(p, string.Format("There is no level \"{0}\" loaded.", text));
            }
        }

        // Token: 0x06001261 RID: 4705 RVA: 0x00065580 File Offset: 0x00063780
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/PerBuild <map> <rank> - Sets build permission for a map.");
        }
    }
}