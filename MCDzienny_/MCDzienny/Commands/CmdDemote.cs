namespace MCDzienny
{
    // Token: 0x02000115 RID: 277
    public class CmdDemote : Command
    {
        // Token: 0x170003D0 RID: 976
        // (get) Token: 0x0600085C RID: 2140 RVA: 0x0002A7F4 File Offset: 0x000289F4
        public override string name
        {
            get { return "demote"; }
        }

        // Token: 0x170003D1 RID: 977
        // (get) Token: 0x0600085D RID: 2141 RVA: 0x0002A7FC File Offset: 0x000289FC
        public override string shortcut
        {
            get { return "de"; }
        }

        // Token: 0x170003D2 RID: 978
        // (get) Token: 0x0600085E RID: 2142 RVA: 0x0002A804 File Offset: 0x00028A04
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170003D3 RID: 979
        // (get) Token: 0x0600085F RID: 2143 RVA: 0x0002A80C File Offset: 0x00028A0C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003D4 RID: 980
        // (get) Token: 0x06000860 RID: 2144 RVA: 0x0002A810 File Offset: 0x00028A10
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06000862 RID: 2146 RVA: 0x0002A81C File Offset: 0x00028A1C
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
            var i = Group.groupList.Count - 1;
            while (i >= 0)
            {
                var group3 = Group.groupList[i];
                if (flag)
                {
                    if (group3.Permission > LevelPermission.Banned) group2 = group3;
                    break;
                }

                if (group3 == @group) flag = true;
                i--;
            }

            if (group2 != null)
            {
                all.Find("setrank").Use(p, str + " " + group2.name);
                return;
            }

            Player.SendMessage(p, "No higher ranks exist");
        }

        // Token: 0x06000863 RID: 2147 RVA: 0x0002A8EC File Offset: 0x00028AEC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/demote <name> - Demotes <name> down a rank");
        }
    }
}