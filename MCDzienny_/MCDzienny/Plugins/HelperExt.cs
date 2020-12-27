using System.Collections.Generic;
using System.Windows.Controls;
using MCDzienny.Plugins.KeyboardShortcuts;

namespace MCDzienny.Plugins
{
    // Token: 0x02000041 RID: 65
    internal static class HelperExt
    {
        // Token: 0x0600016B RID: 363 RVA: 0x00008A70 File Offset: 0x00006C70
        public static void RemoveNullOrEmpty(this ItemCollection ic)
        {
            var list = new List<ShortcutInfo>();
            foreach (var obj in ic)
            {
                var shortcutInfo = (ShortcutInfo) obj;
                if (string.IsNullOrEmpty(shortcutInfo.Shortcut) || string.IsNullOrEmpty(shortcutInfo.Command))
                    list.Add(shortcutInfo);
            }

            foreach (var removeItem in list) ic.Remove(removeItem);
        }
    }
}