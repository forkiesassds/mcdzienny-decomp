using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using MCDzienny.SettingsFrame;
using DefaultValueAttribute = MCDzienny.SettingsFrame.DefaultValueAttribute;

namespace MCDzienny.SettingsExtended
{
    public class SettingsMediator
    {
        readonly SettingsFrame.SettingsFrame settingsFrame;

        readonly List<SettingsPropertyExtended> settingsProperties;

        public SettingsMediator(SettingsFrame.SettingsFrame settings)
        {
            settingsProperties = new List<SettingsPropertyExtended>();
            settingsFrame = settings;
        }

        public bool SetProperty(string key, string value, out string errorMessage)
        {
            errorMessage = "";
            Type type = settingsFrame.GetType();
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo[] array = properties;
            foreach (PropertyInfo propertyInfo in array)
            {
                bool flag = false;
                object defaultValue = null;
                string description = "";
                object[] customAttributes = propertyInfo.GetCustomAttributes(inherit: false);
                for (int j = 0; j < customAttributes.Length; j++)
                {
                    Attribute attribute = (Attribute)customAttributes[j];
                    if (attribute is SettingAttribute)
                    {
                        flag = true;
                    }
                    else if (attribute is DefaultValueAttribute)
                    {
                        defaultValue = ((DefaultValueAttribute)attribute).Value;
                    }
                    else if (attribute is DescriptionAttribute)
                    {
                        description = ((DescriptionAttribute)attribute).Description;
                    }
                }
                if (flag)
                {
                    SettingsPropertyExtended settingsPropertyExtended = new SettingsPropertyExtended(propertyInfo.Name);
                    settingsPropertyExtended.DefaultValue = defaultValue;
                    settingsPropertyExtended.PropertyType = propertyInfo.PropertyType;
                    settingsPropertyExtended.Description = description;
                    settingsProperties.Add(settingsPropertyExtended);
                }
            }
            return true;
        }

        public string GetPropertyValue(string key)
        {
            return "";
        }

        public string GetPropertyDescription(string key)
        {
            return "";
        }

        public List<string> GetAllKeys()
        {
            return null;
        }

        public string GetPropertyType()
        {
            return null;
        }

        public string GetPropertyPossibleValues()
        {
            return null;
        }
    }
}