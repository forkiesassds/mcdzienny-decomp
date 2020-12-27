using System.Collections.Generic;

namespace MCDzienny.Levels.Effects
{
    // Token: 0x02000030 RID: 48
    public class Environment
    {
        // Token: 0x0600011D RID: 285 RVA: 0x000077E4 File Offset: 0x000059E4
        public Environment()
        {
            Items = new List<EnvironmentItem>();
        }

        // Token: 0x17000046 RID: 70
        // (get) Token: 0x0600011B RID: 283 RVA: 0x000077D0 File Offset: 0x000059D0
        // (set) Token: 0x0600011C RID: 284 RVA: 0x000077D8 File Offset: 0x000059D8
        public List<EnvironmentItem> Items { get; set; }
    }
}