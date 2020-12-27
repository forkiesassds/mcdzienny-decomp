namespace MCDzienny
{
    // Token: 0x020000AE RID: 174
    public class ActionElement
    {
        // Token: 0x04000262 RID: 610
        internal string actionString;

        // Token: 0x04000261 RID: 609
        internal ValueExplicitPair<BlockTrigger> blockTrigger;

        // Token: 0x060005DF RID: 1503 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
        public ActionElement()
        {
        }

        // Token: 0x060005E0 RID: 1504 RVA: 0x0001C0AC File Offset: 0x0001A2AC
        public ActionElement(ValueExplicitPair<BlockTrigger> blockTrigger, string actionString)
        {
            this.blockTrigger = blockTrigger;
            this.actionString = actionString;
        }
    }
}