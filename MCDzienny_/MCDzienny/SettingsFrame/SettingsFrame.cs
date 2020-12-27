using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x0200021C RID: 540
    public abstract class SettingsFrame : SettingsFrameBase, INotifyPropertyChanged
    {
        // Token: 0x0400086F RID: 2159
        private readonly SettingsProvider provider;

        // Token: 0x0400086D RID: 2157
        private List<SettingsProperty> settingsProperties;

        // Token: 0x0400086E RID: 2158
        private List<SettingsPropertyElement> settingsPropertyElements;

        // Token: 0x06000EF0 RID: 3824 RVA: 0x00053078 File Offset: 0x00051278
        protected SettingsFrame()
        {
            settingsProperties = new List<SettingsProperty>();
            settingsPropertyElements = new List<SettingsPropertyElement>();
            var type = GetType();
            foreach (var obj in type.GetCustomAttributes(false))
                if (obj is SettingsPathAttribute)
                    provider = new ConcreteSettingsProvider(((SettingsPathAttribute) obj).Path, type.Name);
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var flag = false;
                object defaultValue = null;
                foreach (Attribute attribute in propertyInfo.GetCustomAttributes(false))
                    if (attribute is SettingAttribute)
                        flag = true;
                    else if (attribute is DefaultValueAttribute)
                        defaultValue = ((DefaultValueAttribute) attribute).Value;
                if (flag)
                {
                    var settingsProperty = new SettingsProperty(propertyInfo.Name);
                    settingsProperty.DefaultValue = defaultValue;
                    settingsProperty.PropertyType = propertyInfo.PropertyType;
                    settingsProperties.Add(settingsProperty);
                }
            }

            foreach (var property in settingsProperties)
                settingsPropertyElements.Add(new SettingsPropertyElement(property));
            Initialize(SettingProperties);
            Reload();
            Save();
        }

        // Token: 0x17000545 RID: 1349
        public override object this[string key]
        {
            get { return settingsPropertyElements.Find(e => e.Name == key).PropertyValue; }
            set { settingsPropertyElements.Find(e => e.Name == key).PropertyValue = value; }
        }

        // Token: 0x17000546 RID: 1350
        // (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0005327C File Offset: 0x0005147C
        [Browsable(false)]
        public override List<SettingsProperty> SettingProperties
        {
            get
            {
                if (settingsProperties == null) settingsProperties = new List<SettingsProperty>();
                return settingsProperties;
            }
        }

        // Token: 0x17000547 RID: 1351
        // (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00053298 File Offset: 0x00051498
        [Browsable(false)]
        public override List<SettingsPropertyElement> SettingsPropertyElements
        {
            get
            {
                if (settingsPropertyElements == null) settingsPropertyElements = new List<SettingsPropertyElement>();
                return settingsPropertyElements;
            }
        }

        // Token: 0x14000023 RID: 35
        // (add) Token: 0x06000EEE RID: 3822 RVA: 0x00053008 File Offset: 0x00051208
        // (remove) Token: 0x06000EEF RID: 3823 RVA: 0x00053040 File Offset: 0x00051240
        public event PropertyChangedEventHandler PropertyChanged;

        // Token: 0x06000EF5 RID: 3829 RVA: 0x000532B4 File Offset: 0x000514B4
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(sender, e);
        }

        // Token: 0x06000EF6 RID: 3830 RVA: 0x000532CC File Offset: 0x000514CC
        public override void Save()
        {
            provider.SetPropertyValues(settingsPropertyElements);
        }

        // Token: 0x06000EF7 RID: 3831 RVA: 0x000532E0 File Offset: 0x000514E0
        public void Reload()
        {
            settingsPropertyElements = provider.GetPropertyValues(settingsProperties);
        }

        // Token: 0x06000EF8 RID: 3832 RVA: 0x000532FC File Offset: 0x000514FC
        public void Reset()
        {
        }
    }
}