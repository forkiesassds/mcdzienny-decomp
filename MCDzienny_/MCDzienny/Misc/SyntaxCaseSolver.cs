using System.Collections.Generic;

namespace MCDzienny.Misc
{
    // Token: 0x020001CB RID: 459
    public class SyntaxCaseSolver
    {
        // Token: 0x040006A8 RID: 1704
        private readonly List<CaseAction> caseAction = new List<CaseAction>();

        // Token: 0x040006A9 RID: 1705
        private readonly CaseAction.ActionDelegate defaultAction;

        // Token: 0x06000CD6 RID: 3286 RVA: 0x00049FB8 File Offset: 0x000481B8
        private SyntaxCaseSolver()
        {
        }

        // Token: 0x06000CD7 RID: 3287 RVA: 0x00049FCC File Offset: 0x000481CC
        public SyntaxCaseSolver(List<CaseAction> caseAction, CaseAction.ActionDelegate defaultAction = null)
        {
            this.caseAction = caseAction;
            this.defaultAction = defaultAction;
        }

        // Token: 0x06000CD8 RID: 3288 RVA: 0x00049FF0 File Offset: 0x000481F0
        public void Process(Player player, string message)
        {
            var caseMatched = false;
            caseAction.ForEach(delegate(CaseAction ca)
            {
                if (caseMatched) return;
                foreach (var text in ca.cases)
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
                            ca.action(player, message.Substring(text.Length).Trim());
                        else
                            ca.action(player, "");
                        caseMatched = true;
                        break;
                    }
            });
            if (!caseMatched && defaultAction != null) defaultAction(player, message);
        }
    }
}