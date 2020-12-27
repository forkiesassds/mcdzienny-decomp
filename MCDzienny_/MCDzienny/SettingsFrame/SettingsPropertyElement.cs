using System;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x02000226 RID: 550
    public class SettingsPropertyElement
    {
        // Token: 0x0400087F RID: 2175
        private bool plainText = true;

        // Token: 0x0400087E RID: 2174

        // Token: 0x0400087D RID: 2173

        // Token: 0x06000F20 RID: 3872 RVA: 0x00053774 File Offset: 0x00051974
        public SettingsPropertyElement(SettingsProperty property)
        {
            Property = property;
            SerializedValue = Property.DefaultValue as string;
        }

        // Token: 0x17000554 RID: 1364
        // (get) Token: 0x06000F21 RID: 3873 RVA: 0x000537A0 File Offset: 0x000519A0
        public string Name
        {
            get { return Property.Name; }
        }

        // Token: 0x17000555 RID: 1365
        // (get) Token: 0x06000F22 RID: 3874 RVA: 0x000537B0 File Offset: 0x000519B0
        public SettingsProperty Property { get; private set; }

        // Token: 0x17000556 RID: 1366
        // (get) Token: 0x06000F23 RID: 3875 RVA: 0x000537B8 File Offset: 0x000519B8
        // (set) Token: 0x06000F24 RID: 3876 RVA: 0x000537C0 File Offset: 0x000519C0
        public object PropertyValue { get; set; }

        // Token: 0x17000557 RID: 1367
        // (get) Token: 0x06000F25 RID: 3877 RVA: 0x000537CC File Offset: 0x000519CC
        // (set) Token: 0x06000F26 RID: 3878 RVA: 0x000537E8 File Offset: 0x000519E8
        public string SerializedValue
        {
            get
            {
                if (plainText) return PropertyValue.ToString();
                throw new NotImplementedException();
            }
            set
            {
                if (value == null || value == "")
                {
                    PropertyValue = Activator.CreateInstance(Property.PropertyType);
                    return;
                }

                if (Property.PropertyType == typeof(string))
                {
                    PropertyValue = value;
                    return;
                }

                if (Property.PropertyType == typeof(int))
                {
                    PropertyValue = int.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(long))
                {
                    PropertyValue = long.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(decimal))
                {
                    PropertyValue = decimal.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(short))
                {
                    PropertyValue = short.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(ushort))
                {
                    PropertyValue = ushort.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(uint))
                {
                    PropertyValue = uint.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(ulong))
                {
                    PropertyValue = ulong.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(byte))
                {
                    PropertyValue = byte.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(bool))
                {
                    PropertyValue = bool.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(char))
                {
                    PropertyValue = char.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(float))
                {
                    PropertyValue = float.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(double))
                {
                    PropertyValue = double.Parse(value);
                    return;
                }

                if (Property.PropertyType == typeof(sbyte))
                {
                    PropertyValue = sbyte.Parse(value);
                    return;
                }

                if (Property.PropertyType.IsEnum)
                {
                    PropertyValue = Enum.Parse(Property.PropertyType, value);
                    return;
                }

                plainText = false;
                throw new NotImplementedException();
            }
        }
    }
}