using System;
using System.Collections.Generic;
using System.ComponentModel;
using MCDzienny.SettingsFrame;
using DefaultValueAttribute = MCDzienny.SettingsFrame.DefaultValueAttribute;

namespace MCDzienny.SettingsExtended
{
    // Token: 0x02000219 RID: 537
    public class SettingsMediator
    {
        // Token: 0x04000864 RID: 2148
        private readonly SettingsFrame.SettingsFrame settingsFrame;

        // Token: 0x04000865 RID: 2149
        private readonly List<SettingsPropertyExtended> settingsProperties;

        public SettingsMediator(SettingsFrame.SettingsFrame settings)
        {
            settingsProperties = new List<SettingsPropertyExtended>();
            settingsFrame = settings;
        }

        public bool SetProperty(string key, string value, out string errorMessage)
        {
            errorMessage = "";
            var type = settingsFrame.GetType();
            var properties = type.GetProperties();
            var array = properties;
            foreach (var propertyInfo in array)
            {
                var flag = false;
                object defaultValue = null;
                var description = "";
                var customAttributes = propertyInfo.GetCustomAttributes(false);
                for (var j = 0; j < customAttributes.Length; j++)
                {
                    var attribute = (Attribute) customAttributes[j];
                    if (attribute is SettingAttribute)
                        flag = true;
                    else if (attribute is DefaultValueAttribute)
                        defaultValue = ((DefaultValueAttribute) attribute).Value;
                    else if (attribute is DescriptionAttribute)
                        description = ((DescriptionAttribute) attribute).Description;
                }

                if (flag)
                {
                    var settingsPropertyExtended = new SettingsPropertyExtended(propertyInfo.Name);
                    settingsPropertyExtended.DefaultValue = defaultValue;
                    settingsPropertyExtended.PropertyType = propertyInfo.PropertyType;
                    settingsPropertyExtended.Description = description;
                    settingsProperties.Add(settingsPropertyExtended);
                }
            }

            return true;
        }

        // Token: 0x06000ED4 RID: 3796 RVA: 0x00052E88 File Offset: 0x00051088
        public string GetPropertyValue(string key)
        {
            return "";
        }

        // Token: 0x06000ED5 RID: 3797 RVA: 0x00052E90 File Offset: 0x00051090
        public string GetPropertyDescription(string key)
        {
            return "";
        }

        // Token: 0x06000ED6 RID: 3798 RVA: 0x00052E98 File Offset: 0x00051098
        public List<string> GetAllKeys()
        {
            return null;
        }

        // Token: 0x06000ED7 RID: 3799 RVA: 0x00052E9C File Offset: 0x0005109C
        public string GetPropertyType()
        {
            return null;
        }

        // Token: 0x06000ED8 RID: 3800 RVA: 0x00052EA0 File Offset: 0x000510A0
        public string GetPropertyPossibleValues()
        {
            return null;
        }
    }
}