namespace MCDzienny
{
    public class ActionElement
    {

        internal string actionString;
        internal ValueExplicitPair<BlockTrigger> blockTrigger;

        public ActionElement() {}

        public ActionElement(ValueExplicitPair<BlockTrigger> blockTrigger, string actionString)
        {
            this.blockTrigger = blockTrigger;
            this.actionString = actionString;
        }
    }
}