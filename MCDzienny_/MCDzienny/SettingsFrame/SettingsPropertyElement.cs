using System;

namespace MCDzienny.SettingsFrame
{
    public class SettingsPropertyElement
    {
        readonly SettingsProperty settingsProperty;

        bool plainText = true;

        public SettingsPropertyElement(SettingsProperty property)
        {
            settingsProperty = property;
            SerializedValue = settingsProperty.DefaultValue as string;
        }

        public string Name { get { return settingsProperty.Name; } }

        public SettingsProperty Property { get { return settingsProperty; } }

        public object PropertyValue { get; set; }

        public string SerializedValue
        {
            get
            {
                if (plainText)
                {
                    return PropertyValue.ToString();
                }
                throw new NotImplementedException();
            }
            set
            {
                if (value == null || value == "")
                {
                    PropertyValue = Activator.CreateInstance(settingsProperty.PropertyType);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(string))
                {
                    PropertyValue = value;
                    return;
                }
                if (settingsProperty.PropertyType == typeof(int))
                {
                    PropertyValue = int.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(long))
                {
                    PropertyValue = long.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(decimal))
                {
                    PropertyValue = decimal.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(short))
                {
                    PropertyValue = short.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(ushort))
                {
                    PropertyValue = ushort.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(uint))
                {
                    PropertyValue = uint.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(ulong))
                {
                    PropertyValue = ulong.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(byte))
                {
                    PropertyValue = byte.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(bool))
                {
                    PropertyValue = bool.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(char))
                {
                    PropertyValue = char.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(float))
                {
                    PropertyValue = float.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(double))
                {
                    PropertyValue = double.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType == typeof(sbyte))
                {
                    PropertyValue = sbyte.Parse(value);
                    return;
                }
                if (settingsProperty.PropertyType.IsEnum)
                {
                    PropertyValue = Enum.Parse(settingsProperty.PropertyType, value);
                    return;
                }
                plainText = false;
                throw new NotImplementedException();
            }
        }
    }
}