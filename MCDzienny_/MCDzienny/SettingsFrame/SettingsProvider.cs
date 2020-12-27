using System.Collections.Generic;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x02000220 RID: 544
    internal abstract class SettingsProvider
    {
        // Token: 0x06000F07 RID: 3847
        public abstract List<SettingsPropertyElement> GetPropertyValues(List<SettingsProperty> collection);

        // Token: 0x06000F08 RID: 3848
        public abstract void SetPropertyValues(List<SettingsPropertyElement> collection);
    }
}