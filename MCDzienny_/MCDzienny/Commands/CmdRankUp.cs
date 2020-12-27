using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000121 RID: 289
    public class CmdRankUp : Command
    {
        // Token: 0x170003F6 RID: 1014
        // (get) Token: 0x060008A7 RID: 2215 RVA: 0x0002BE20 File Offset: 0x0002A020
        public override string name
        {
            get { return "rankup"; }
        }

        // Token: 0x170003F7 RID: 1015
        // (get) Token: 0x060008A8 RID: 2216 RVA: 0x0002BE28 File Offset: 0x0002A028
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003F8 RID: 1016
        // (get) Token: 0x060008A9 RID: 2217 RVA: 0x0002BE30 File Offset: 0x0002A030
        public override string type
        {
            get { return ""; }
        }

        // Token: 0x170003F9 RID: 1017
        // (get) Token: 0x060008AA RID: 2218 RVA: 0x0002BE38 File Offset: 0x0002A038
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003FA RID: 1018
        // (get) Token: 0x060008AB RID: 2219 RVA: 0x0002BE3C File Offset: 0x0002A03C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060008AC RID: 2220 RVA: 0x0002BE40 File Offset: 0x0002A040
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var player = Player.Find(message.ToLower());
            if (player == null)
            {
                Player.SendMessage(p, "Error during auto-promotion, player not found.");
                return;
            }

            var list = new List<Group>();
            list.AddRange(Group.groupList);
            var i = 0;
            while (i < list.Count)
                if (list[i].Permission == player.group.Permission)
                {
                    if (list.Count <= i + 1)
                    {
                        Player.SendMessage(p, "You have the highest rank possible.");
                        return;
                    }

                    if (list[i + 1].Permission < LevelPermission.Operator)
                    {
                        all.Find("setrank").Use(null, player.name + " " + list[i + 1].name);
                        return;
                    }

                    Player.SendMessage(p, "Higher rank you can obtain only from Server Masters.");
                    return;
                }
                else
                {
                    i++;
                }
        }

        // Token: 0x060008AD RID: 2221 RVA: 0x0002BF28 File Offset: 0x0002A128
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rankup [name] - ranks up the [player].");
        }
    }
}