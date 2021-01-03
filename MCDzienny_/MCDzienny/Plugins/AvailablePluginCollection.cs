using System;
using System.Collections.Generic;

namespace MCDzienny.Plugins
{
    // Token: 0x020001E1 RID: 481
    public class AvailablePluginCollection : List<AvailablePlugin>
    {
        // Token: 0x14000016 RID: 22
        // (add) Token: 0x06000D64 RID: 3428 RVA: 0x0004C5DC File Offset: 0x0004A7DC
        // (remove) Token: 0x06000D65 RID: 3429 RVA: 0x0004C614 File Offset: 0x0004A814
        public event EventHandler CollectionChanged;

        // Token: 0x06000D66 RID: 3430 RVA: 0x0004C64C File Offset: 0x0004A84C
        protected void OnCollectionChanged()
        {
            var collectionChanged = CollectionChanged;
            if (collectionChanged != null) collectionChanged(this, EventArgs.Empty);
        }

        // Token: 0x06000D67 RID: 3431 RVA: 0x0004C670 File Offset: 0x0004A870
        public new void Add(AvailablePlugin pluginToAdd)
        {
            base.Add(pluginToAdd);
            OnCollectionChanged();
        }

        // Token: 0x06000D68 RID: 3432 RVA: 0x0004C680 File Offset: 0x0004A880
        public new void Remove(AvailablePlugin pluginToRemove)
        {
            base.Remove(pluginToRemove);
            OnCollectionChanged();
        }

        // Token: 0x06000D69 RID: 3433 RVA: 0x0004C690 File Offset: 0x0004A890
        public AvailablePlugin Find(string pluginNameOrPath)
        {
            AvailablePlugin result = null;
            foreach (var availablePlugin in this)
                if (availablePlugin.Instance.Name.Equals(pluginNameOrPath) ||
                    availablePlugin.AssemblyPath.Equals(pluginNameOrPath))
                {
                    result = availablePlugin;
                    break;
                }

            return result;
        }
    }
}