using System.Collections.Generic;

namespace MCDzienny.Misc
{
    public class SyntaxCaseSolver
    {
        readonly List<CaseAction> caseAction = new List<CaseAction>();

        readonly CaseAction.ActionDelegate defaultAction;

        SyntaxCaseSolver() {}

        public SyntaxCaseSolver(List<CaseAction> caseAction, CaseAction.ActionDelegate defaultAction = null)
        {
            this.caseAction = caseAction;
            this.defaultAction = defaultAction;
        }

        public void Process(Player player, string message)
        {
            bool caseMatched = false;
            caseAction.ForEach(delegate(CaseAction ca)
            {
                if (!caseMatched)
                {
                    string[] cases = ca.cases;
                    foreach (string text in cases)
                    {
                        if (text == "")
                        {
                            if (message == "")
                            {
                                ca.action(player, "");
                                caseMatched = true;
                                break;
                            }
                        }
                        else if (message.Length >= text.Length && message.Substring(0, text.Length) == text)
                        {
                            if (message.Length > text.Length)
                            {
                                ca.action(player, message.Substring(text.Length).Trim());
                            }
                            else
                            {
                                ca.action(player, "");
                            }
                            caseMatched = true;
                            break;
                        }
                    }
                }
            });
            if (!caseMatched && defaultAction != null)
            {
                defaultAction(player, message);
            }
        }
    }
}