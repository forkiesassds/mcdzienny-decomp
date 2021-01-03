namespace MCDzienny.StoreSystem
{
    // Token: 0x02000238 RID: 568
    public class Title : Item
    {
        // Token: 0x170005F0 RID: 1520
        // (get) Token: 0x06001095 RID: 4245 RVA: 0x0005604C File Offset: 0x0005424C
        public override string Name
        {
            get { return "Title"; }
        }

        // Token: 0x170005F1 RID: 1521
        // (get) Token: 0x06001096 RID: 4246 RVA: 0x00056054 File Offset: 0x00054254
        public override int ListPosition
        {
            get { return 9; }
        }

        // Token: 0x06001097 RID: 4247 RVA: 0x00056058 File Offset: 0x00054258
        public override int GetAmount(Player p)
        {
            return 1;
        }

        // Token: 0x06001098 RID: 4248 RVA: 0x0005605C File Offset: 0x0005425C
        public override int GetPrice(Player p)
        {
            return 280;
        }

        // Token: 0x06001099 RID: 4249 RVA: 0x00056064 File Offset: 0x00054264
        public override bool GetIsListed(Player p)
        {
            return true;
        }

        // Token: 0x0600109A RID: 4250 RVA: 0x00056068 File Offset: 0x00054268
        public override string GetDescription(Player p)
        {
            return " - use /title [your title] to set a new title,";
        }

        // Token: 0x0600109B RID: 4251 RVA: 0x00056070 File Offset: 0x00054270
        public override string GetHelp(Player p)
        {
            return "This item lets you change your title.";
        }

        // Token: 0x0600109C RID: 4252 RVA: 0x00056078 File Offset: 0x00054278
        public override bool OnBuying(Player p)
        {
            if (p.boughtTitle)
            {
                Player.SendMessage(p,
                    "You have already bought this item. In order to use it write: /title [your title]");
                return false;
            }

            return true;
        }

        // Token: 0x0600109D RID: 4253 RVA: 0x00056090 File Offset: 0x00054290
        public override void OnBought(Player p)
        {
            p.boughtTitle = true;
            Player.SendMessage(p, "In order to change your title use the command: /title");
        }
    }
}