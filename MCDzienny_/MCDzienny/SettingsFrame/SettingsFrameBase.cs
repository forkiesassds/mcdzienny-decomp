using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MCDzienny.SettingsFrame
{
    public abstract class SettingsFrameBase
    {

        List<SettingsProperty> settingsProperties;

        public virtual object this[string key] { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public virtual List<SettingsProperty> SettingProperties { get { return new List<SettingsProperty>(settingsProperties); } }

        public virtual List<SettingsPropertyElement> SettingsPropertyElements
        {
            get
            {
                lock (this)
                {
                    return GetPropertyElements();
                }
            }
        }

        [Browsable(false)]
        public bool IsSynchronized { get; private set; }

        public void Initialize(List<SettingsProperty> settingsProperties)
        {
            this.settingsProperties = settingsProperties;
        }

        List<SettingsPropertyElement> GetPropertyElements()
        {
            var list = new List<SettingsPropertyElement>();
            foreach (SettingsProperty settingsProperty in settingsProperties)
            {
                list.Add(new SettingsPropertyElement(settingsProperty));
            }
            return list;
        }

        public static SettingsFrameBase Synchronized(SettingsFrameBase settingsFrame)
        {
            settingsFrame.IsSynchronized = true;
            return settingsFrame;
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }
    }
}