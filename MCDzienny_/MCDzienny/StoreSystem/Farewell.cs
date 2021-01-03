namespace MCDzienny.StoreSystem
{
    // Token: 0x02000234 RID: 564
    public class Farewell : Item
    {
        // Token: 0x170005E6 RID: 1510
        // (get) Token: 0x06001072 RID: 4210 RVA: 0x00055DBC File Offset: 0x00053FBC
        public override string Name
        {
            get { return "Farewell"; }
        }

        // Token: 0x170005E7 RID: 1511
        // (get) Token: 0x06001073 RID: 4211 RVA: 0x00055DC4 File Offset: 0x00053FC4
        public override int ListPosition
        {
            get { return 13; }
        }

        // Token: 0x06001074 RID: 4212 RVA: 0x00055DC8 File Offset: 0x00053FC8
        public override int GetAmount(Player p)
        {
            return 1;
        }

        // Token: 0x06001075 RID: 4213 RVA: 0x00055DCC File Offset: 0x00053FCC
        public override int GetPrice(Player p)
        {
            return 180;
        }

        // Token: 0x06001076 RID: 4214 RVA: 0x00055DD4 File Offset: 0x00053FD4
        public override bool GetIsListed(Player p)
        {
            return true;
        }

        // Token: 0x06001077 RID: 4215 RVA: 0x00055DD8 File Offset: 0x00053FD8
        public override string GetDescription(Player p)
        {
            return " - use /farewell [message] to set your farewell message.";
        }

        // Token: 0x06001078 RID: 4216 RVA: 0x00055DE0 File Offset: 0x00053FE0
        public override string GetHelp(Player p)
        {
            return "Farewell item lets you set the message that is diplayed when you disconnect from the server.";
        }

        // Token: 0x06001079 RID: 4217 RVA: 0x00055DE8 File Offset: 0x00053FE8
        public override bool OnBuying(Player p)
        {
            if (p.boughtFarewell)
            {
                Player.SendMessage(p,
                    "You have already bought this item. In order to use it write: /farewell [message]");
                return false;
            }

            return true;
        }

        // Token: 0x0600107A RID: 4218 RVA: 0x00055E00 File Offset: 0x00054000
        public override void OnBought(Player p)
        {
            p.boughtFarewell = true;
            Player.SendMessage(p, "In order to set your farewell message write: /farewell [message]");
        }
    }
}