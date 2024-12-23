using System;

namespace MCDzienny.SettingsFrame
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SettingsPathAttribute : Attribute
    {
        readonly string path;

        public SettingsPathAttribute(string path)
        {
            this.path = path;
        }

        public string Path { get { return path; } }
    }
}