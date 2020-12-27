namespace MCDzienny
{
    // Token: 0x02000114 RID: 276
    public class CmdPromote : Command
    {
        // Token: 0x170003CB RID: 971
        // (get) Token: 0x06000854 RID: 2132 RVA: 0x0002A6EC File Offset: 0x000288EC
        public override string name
        {
            get { return "promote"; }
        }

        // Token: 0x170003CC RID: 972
        // (get) Token: 0x06000855 RID: 2133 RVA: 0x0002A6F4 File Offset: 0x000288F4
        public override string shortcut
        {
            get { return "pr"; }
        }

        // Token: 0x170003CD RID: 973
        // (get) Token: 0x06000856 RID: 2134 RVA: 0x0002A6FC File Offset: 0x000288FC
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170003CE RID: 974
        // (get) Token: 0x06000857 RID: 2135 RVA: 0x0002A704 File Offset: 0x00028904
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003CF RID: 975
        // (get) Token: 0x06000858 RID: 2136 RVA: 0x0002A708 File Offset: 0x00028908
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600085A RID: 2138 RVA: 0x0002A714 File Offset: 0x00028914
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }

            var player = Player.Find(message);
            string str;
            Group group;
            if (player == null)
            {
                str = message;
                group = Group.findPlayerGroup(message);
            }
            else
            {
                str = player.name;
                group = player.group;
            }

            Group group2 = null;
            var flag = false;
            var i = 0;
            while (i < Group.groupList.Count)
            {
                var group3 = Group.groupList[i];
                if (flag)
                {
                    if (group3.Permission < LevelPermission.Nobody) group2 = group3;
                    break;
                }

                if (group3 == @group) flag = true;
                i++;
            }

            if (group2 != null)
            {
                all.Find("setrank").Use(p, str + " " + group2.name);
                return;
            }

            Player.SendMessage(p, "No higher ranks exist");
        }

        // Token: 0x0600085B RID: 2139 RVA: 0x0002A7E4 File Offset: 0x000289E4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/promote <name> - Promotes <name> up a rank");
        }
    }
}