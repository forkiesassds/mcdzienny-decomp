using System;

namespace MCDzienny.InfectionSystem
{
    // Token: 0x0200002F RID: 47
    public static class EventExt
    {
        // Token: 0x0600011A RID: 282 RVA: 0x000077AC File Offset: 0x000059AC
        public static void Trigger<T, E>(this EventHandler<T> e, object sender, E args)
            where T : EventArgs where E : EventArgs, T
        {
            if (e != null) e(sender, args);
        }
    }
}