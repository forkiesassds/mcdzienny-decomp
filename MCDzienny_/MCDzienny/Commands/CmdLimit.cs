namespace MCDzienny
{
    // Token: 0x020002CF RID: 719
    public class CmdLimit : Command
    {
        // Token: 0x170007D9 RID: 2009
        // (get) Token: 0x0600145E RID: 5214 RVA: 0x00070CB8 File Offset: 0x0006EEB8
        public override string name
        {
            get { return "limit"; }
        }

        // Token: 0x170007DA RID: 2010
        // (get) Token: 0x0600145F RID: 5215 RVA: 0x00070CC0 File Offset: 0x0006EEC0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007DB RID: 2011
        // (get) Token: 0x06001460 RID: 5216 RVA: 0x00070CC8 File Offset: 0x0006EEC8
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170007DC RID: 2012
        // (get) Token: 0x06001461 RID: 5217 RVA: 0x00070CD0 File Offset: 0x0006EED0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007DD RID: 2013
        // (get) Token: 0x06001462 RID: 5218 RVA: 0x00070CD4 File Offset: 0x0006EED4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x06001464 RID: 5220 RVA: 0x00070CE0 File Offset: 0x0006EEE0
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length != 2)
            {
                Help(p);
                return;
            }

            int num;
            try
            {
                num = int.Parse(message.Split(' ')[1]);
            }
            catch
            {
                Player.SendMessage(p, "Invalid limit amount");
                return;
            }

            if (num < 1)
            {
                Player.SendMessage(p, "Cannot set below 1.");
            }
            else
            {
                var group = Group.Find(message.Split(' ')[0]);
                if (group != null)
                {
                    group.maxBlocks = num;
                    Player.GlobalChat(null,
                        string.Format("{0}'s building limits were set to &b{1}",
                            group.color + group.name + Server.DefaultColor, num), false);
                    Group.saveGroups(Group.groupList);
                    return;
                }

                string a;
                if ((a = message.Split(' ')[0].ToLower()) != null)
                {
                    if (a == "rp" || a == "restartphysics")
                    {
                        Server.rpLimit = num;
                        Player.GlobalMessage(string.Format("Custom /rp's limit was changed to &b{0}", num.ToString()));
                        return;
                    }

                    if (a == "rpnorm" || a == "rpnormal")
                    {
                        Server.rpNormLimit = num;
                        Player.GlobalMessage(string.Format("Normal /rp's limit was changed to &b{0}", num.ToString()));
                        return;
                    }
                }

                Player.SendMessage(p, "No supported /limit");
            }
        }

        // Token: 0x06001465 RID: 5221 RVA: 0x00070E5C File Offset: 0x0006F05C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/limit <type> <amount> - Sets the limit for <type>");
            Player.SendMessage(p, "<types> - " + Group.concatList(true, true) + ", RP, RPNormal");
        }
    }
}