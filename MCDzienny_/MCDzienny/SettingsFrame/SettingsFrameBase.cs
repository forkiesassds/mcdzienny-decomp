using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x0200021B RID: 539
    public abstract class SettingsFrameBase
    {
        // Token: 0x0400086C RID: 2156
        private List<SettingsProperty> settingsProperties;

        // Token: 0x0400086B RID: 2155

        // Token: 0x17000541 RID: 1345
        public virtual object this[string key]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        // Token: 0x17000542 RID: 1346
        // (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00052F44 File Offset: 0x00051144
        public virtual List<SettingsProperty> SettingProperties
        {
            get { return new List<SettingsProperty>(settingsProperties); }
        }

        // Token: 0x17000543 RID: 1347
        // (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00052F54 File Offset: 0x00051154
        public virtual List<SettingsPropertyElement> SettingsPropertyElements
        {
            get
            {
                List<SettingsPropertyElement> propertyElements;
                lock (this)
                {
                    propertyElements = GetPropertyElements();
                }

                return propertyElements;
            }
        }

        // Token: 0x17000544 RID: 1348
        // (get) Token: 0x06000EEB RID: 3819 RVA: 0x00052FEC File Offset: 0x000511EC
        [Browsable(false)] public bool IsSynchronized { get; private set; }

        // Token: 0x06000EE5 RID: 3813 RVA: 0x00052F28 File Offset: 0x00051128
        public void Initialize(List<SettingsProperty> settingsProperties)
        {
            this.settingsProperties = settingsProperties;
        }

        // Token: 0x06000EEA RID: 3818 RVA: 0x00052F8C File Offset: 0x0005118C
        private List<SettingsPropertyElement> GetPropertyElements()
        {
            var list = new List<SettingsPropertyElement>();
            foreach (var property in settingsProperties) list.Add(new SettingsPropertyElement(property));
            return list;
        }

        // Token: 0x06000EEC RID: 3820 RVA: 0x00052FF4 File Offset: 0x000511F4
        public static SettingsFrameBase Synchronized(SettingsFrameBase settingsFrame)
        {
            settingsFrame.IsSynchronized = true;
            return settingsFrame;
        }

        // Token: 0x06000EED RID: 3821 RVA: 0x00053000 File Offset: 0x00051200
        public virtual void Save()
        {
            throw new NotImplementedException();
        }
    }
}