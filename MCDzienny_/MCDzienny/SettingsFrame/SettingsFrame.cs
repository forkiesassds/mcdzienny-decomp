using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MCDzienny.SettingsFrame
{
    public abstract class SettingsFrame : SettingsFrameBase, INotifyPropertyChanged
    {

        readonly SettingsProvider provider;
        List<SettingsProperty> settingsProperties;

        List<SettingsPropertyElement> settingsPropertyElements;

        protected SettingsFrame()
        {
            settingsProperties = new List<SettingsProperty>();
            settingsPropertyElements = new List<SettingsPropertyElement>();
            Type type = GetType();
            object[] customAttributes = type.GetCustomAttributes(inherit: false);
            foreach (object obj in customAttributes)
            {
                if (obj is SettingsPathAttribute)
                {
                    provider = new ConcreteSettingsProvider(((SettingsPathAttribute)obj).Path, type.Name);
                }
            }
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo[] array = properties;
            foreach (PropertyInfo propertyInfo in array)
            {
                bool flag = false;
                object defaultValue = null;
                object[] customAttributes2 = propertyInfo.GetCustomAttributes(inherit: false);
                for (int k = 0; k < customAttributes2.Length; k++)
                {
                    Attribute attribute = (Attribute)customAttributes2[k];
                    if (attribute is SettingAttribute)
                    {
                        flag = true;
                    }
                    else if (attribute is DefaultValueAttribute)
                    {
                        defaultValue = ((DefaultValueAttribute)attribute).Value;
                    }
                }
                if (flag)
                {
                    SettingsProperty item = new SettingsProperty(propertyInfo.Name)
                    {
                        DefaultValue = defaultValue,
                        PropertyType = propertyInfo.PropertyType
                    };
                    settingsProperties.Add(item);
                }
            }
            foreach (SettingsProperty settingsProperty in settingsProperties)
            {
                settingsPropertyElements.Add(new SettingsPropertyElement(settingsProperty));
            }
            Initialize(SettingProperties);
            Reload();
            Save();
        }

        public override object this[string key]
        {
            get { return settingsPropertyElements.Find(e => e.Name == key).PropertyValue; }
            set { settingsPropertyElements.Find(e => e.Name == key).PropertyValue = value; }
        }

        [Browsable(false)]
        public override List<SettingsProperty> SettingProperties
        {
            get
            {
                if (settingsProperties == null)
                {
                    settingsProperties = new List<SettingsProperty>();
                }
                return settingsProperties;
            }
        }

        [Browsable(false)]
        public override List<SettingsPropertyElement> SettingsPropertyElements
        {
            get
            {
                if (settingsPropertyElements == null)
                {
                    settingsPropertyElements = new List<SettingsPropertyElement>();
                }
                return settingsPropertyElements;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, e);
            }
        }

        public override void Save()
        {
            provider.SetPropertyValues(settingsPropertyElements);
        }

        public void Reload()
        {
            settingsPropertyElements = provider.GetPropertyValues(settingsProperties);
        }

        public void Reset() {}
    }
}