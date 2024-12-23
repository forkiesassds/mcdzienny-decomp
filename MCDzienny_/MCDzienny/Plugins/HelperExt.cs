using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using MCDzienny.Plugins.KeyboardShortcuts;

namespace MCDzienny.Plugins
{
    static class HelperExt
    {
        public static void RemoveNullOrEmpty(this ItemCollection ic)
        {
            var list = new List<ShortcutInfo>();
            foreach (ShortcutInfo item in ic)
            {
                if (string.IsNullOrEmpty(item.Shortcut) || string.IsNullOrEmpty(item.Command))
                {
                    list.Add(item);
                }
            }
            foreach (ShortcutInfo item2 in list)
            {
                ic.Remove(item2);
            }
        }
    }
}