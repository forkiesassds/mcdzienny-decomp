using System;
using System.Collections.Generic;

namespace MCDzienny.Plugins
{
    public class AvailablePluginCollection : List<AvailablePlugin>
    {
        public event EventHandler CollectionChanged;

        protected void OnCollectionChanged()
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, EventArgs.Empty);
            }
        }

        public new void Add(AvailablePlugin pluginToAdd)
        {
            base.Add(pluginToAdd);
            OnCollectionChanged();
        }

        public new void Remove(AvailablePlugin pluginToRemove)
        {
            base.Remove(pluginToRemove);
            OnCollectionChanged();
        }

        public AvailablePlugin Find(string pluginNameOrPath)
        {
            AvailablePlugin result = null;
            using (Enumerator enumerator = GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    AvailablePlugin current = enumerator.Current;
                    if (current.Instance.Name.Equals(pluginNameOrPath) || current.AssemblyPath.Equals(pluginNameOrPath))
                    {
                        result = current;
                        break;
                    }
                }
            }
            return result;
        }
    }
}