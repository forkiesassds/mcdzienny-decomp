using System.Collections.Generic;

namespace MCDzienny.Misc
{
    // Token: 0x020001B4 RID: 436
    public class CaseActionList : List<CaseAction>
    {
        // Token: 0x06000C62 RID: 3170 RVA: 0x00048830 File Offset: 0x00046A30
        public void Add(CaseAction.ActionDelegate action, params string[] cases)
        {
            base.Add(new CaseAction(action, cases));
        }
    }
}