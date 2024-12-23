using System;

namespace MCDzienny.InfectionSystem
{
    public static class EventExt
    {
        public static void Trigger<T, E>(this EventHandler<T> e, object sender, E args) where T : EventArgs where E : EventArgs, T
        {
            if (e != null)
            {
                e(sender, args);
            }
        }
    }
}