using System.Collections.Generic;

namespace MCDzienny.Misc
{
    public class CaseActionList : List<CaseAction>
    {
        public void Add(CaseAction.ActionDelegate action, params string[] cases)
        {
            Add(new CaseAction(action, cases));
        }
    }
}