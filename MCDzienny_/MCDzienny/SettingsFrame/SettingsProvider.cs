using System.Collections.Generic;

namespace MCDzienny.SettingsFrame
{
    abstract class SettingsProvider
    {
        public abstract List<SettingsPropertyElement> GetPropertyValues(List<SettingsProperty> collection);

        public abstract void SetPropertyValues(List<SettingsPropertyElement> collection);
    }
}