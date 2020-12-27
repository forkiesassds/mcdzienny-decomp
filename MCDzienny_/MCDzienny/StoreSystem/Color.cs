namespace MCDzienny.StoreSystem
{
    // Token: 0x02000233 RID: 563
    public class Color : Item
    {
        // Token: 0x170005E4 RID: 1508
        // (get) Token: 0x06001068 RID: 4200 RVA: 0x00055D5C File Offset: 0x00053F5C
        public override string Name
        {
            get { return "Color"; }
        }

        // Token: 0x170005E5 RID: 1509
        // (get) Token: 0x06001069 RID: 4201 RVA: 0x00055D64 File Offset: 0x00053F64
        public override int ListPosition
        {
            get { return 8; }
        }

        // Token: 0x0600106A RID: 4202 RVA: 0x00055D68 File Offset: 0x00053F68
        public override int GetAmount(Player p)
        {
            return 1;
        }

        // Token: 0x0600106B RID: 4203 RVA: 0x00055D6C File Offset: 0x00053F6C
        public override int GetPrice(Player p)
        {
            return 280;
        }

        // Token: 0x0600106C RID: 4204 RVA: 0x00055D74 File Offset: 0x00053F74
        public override bool GetIsListed(Player p)
        {
            return true;
        }

        // Token: 0x0600106D RID: 4205 RVA: 0x00055D78 File Offset: 0x00053F78
        public override string GetDescription(Player p)
        {
            return " - use /color [color] to set your name color,";
        }

        // Token: 0x0600106E RID: 4206 RVA: 0x00055D80 File Offset: 0x00053F80
        public override string GetHelp(Player p)
        {
            return "In order to use this item use /color command.";
        }

        // Token: 0x0600106F RID: 4207 RVA: 0x00055D88 File Offset: 0x00053F88
        public override bool OnBuying(Player p)
        {
            if (p.boughtColor)
            {
                Player.SendMessage(p, "You have already bought this item. In order to use it write: /color");
                return false;
            }

            return true;
        }

        // Token: 0x06001070 RID: 4208 RVA: 0x00055DA0 File Offset: 0x00053FA0
        public override void OnBought(Player p)
        {
            p.boughtColor = true;
            Player.SendMessage(p, "In order to change the color of your name use the command: /color");
        }
    }
}