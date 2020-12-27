namespace MCDzienny
{
    // Token: 0x020000C1 RID: 193
    public class CmdFreeid : Command
    {
        // Token: 0x170002DC RID: 732
        // (get) Token: 0x06000684 RID: 1668 RVA: 0x00021BD8 File Offset: 0x0001FDD8
        public override string name
        {
            get { return "freeid"; }
        }

        // Token: 0x170002DD RID: 733
        // (get) Token: 0x06000685 RID: 1669 RVA: 0x00021BE0 File Offset: 0x0001FDE0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002DE RID: 734
        // (get) Token: 0x06000686 RID: 1670 RVA: 0x00021BE8 File Offset: 0x0001FDE8
        public override string type
        {
            get { return ""; }
        }

        // Token: 0x170002DF RID: 735
        // (get) Token: 0x06000687 RID: 1671 RVA: 0x00021BF0 File Offset: 0x0001FDF0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002E0 RID: 736
        // (get) Token: 0x06000688 RID: 1672 RVA: 0x00021BF4 File Offset: 0x0001FDF4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x0600068A RID: 1674 RVA: 0x00021C00 File Offset: 0x0001FE00
        public override void Use(Player p, string message)
        {
            var num = 0;
            for (byte b = 0; b < 255; b += 1)
            {
                var flag = false;
                foreach (var player in Player.players)
                    if (player.id == b)
                        flag = true;
                if (!flag) num++;
            }

            Player.SendMessage(p, "Total free id's: " + num);
        }

        // Token: 0x0600068B RID: 1675 RVA: 0x00021C8C File Offset: 0x0001FE8C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/score - Shows your best score and total experience.");
            Player.SendMessage(p, "/score all - Shows server score stats.");
        }
    }
}