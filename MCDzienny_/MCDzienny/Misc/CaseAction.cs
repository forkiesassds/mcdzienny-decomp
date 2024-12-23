namespace MCDzienny.Misc
{
    public class CaseAction
    {
        public delegate void ActionDelegate(Player p, string message);

        public ActionDelegate action;

        public string[] cases;

        CaseAction() {}

        public CaseAction(ActionDelegate action, params string[] cases)
        {
            this.cases = cases;
            this.action = action;
        }
    }
}