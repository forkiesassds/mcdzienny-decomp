using System;

namespace MCDzienny.SettingsFrame
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DefaultValueAttribute : Attribute
    {
        readonly string value;

        public DefaultValueAttribute(string value)
        {
            this.value = value;
        }

        public string Value { get { return value; } }
    }
}