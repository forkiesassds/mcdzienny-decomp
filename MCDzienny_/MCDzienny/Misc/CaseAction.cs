namespace MCDzienny.Misc
{
    // Token: 0x020001B2 RID: 434
    public class CaseAction
    {
        // Token: 0x020001B3 RID: 435
        // (Invoke) Token: 0x06000C5F RID: 3167
        public delegate void ActionDelegate(Player p, string message);

        // Token: 0x04000677 RID: 1655
        public ActionDelegate action;

        // Token: 0x04000676 RID: 1654
        public string[] cases;

        // Token: 0x06000C5C RID: 3164 RVA: 0x00048810 File Offset: 0x00046A10
        private CaseAction()
        {
        }

        // Token: 0x06000C5D RID: 3165 RVA: 0x00048818 File Offset: 0x00046A18
        public CaseAction(ActionDelegate action, params string[] cases)
        {
            this.cases = cases;
            this.action = action;
        }
    }
}