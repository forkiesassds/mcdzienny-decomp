using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace MCDzienny.SettingsFrame
{
    class ConcreteSettingsProvider : SettingsProvider
    {
        readonly string settingsFilePath;

        readonly string settingsRoot = "GeneralSettings";

        XmlDocument m_SettingsXML;

        public ConcreteSettingsProvider(string settingsFilePath, string settingsRoot)
        {
            this.settingsFilePath = settingsFilePath;
            this.settingsRoot = settingsRoot;
        }

        public string ApplicationName { get { return Assembly.GetExecutingAssembly().GetName().Name; } }

        XmlDocument SettingsXML
        {
            get
            {
                //IL_000c: Unknown result type (might be due to invalid IL or missing references)
                //IL_0016: Expected O, but got Unknown
                if (m_SettingsXML == null)
                {
                    m_SettingsXML = new XmlDocument();
                    try
                    {
                        m_SettingsXML.Load(GetAppSettingsFilename());
                    }
                    catch (Exception)
                    {
                        XmlDeclaration val = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", "yes");
                        m_SettingsXML.AppendChild(val);
                        m_SettingsXML.AppendChild(m_SettingsXML.CreateWhitespace("\n\r"));
                        XmlNode val2 = m_SettingsXML.CreateNode((XmlNodeType)1, settingsRoot, "");
                        m_SettingsXML.AppendChild(val2);
                    }
                }
                return m_SettingsXML;
            }
        }

        public virtual string GetAppSettingsPath()
        {
            FileInfo fileInfo = new FileInfo(Directory.GetCurrentDirectory());
            return fileInfo.DirectoryName;
        }

        public virtual string GetAppSettingsFilename()
        {
            return settingsFilePath;
        }

        public override void SetPropertyValues(List<SettingsPropertyElement> propvals)
        {
            foreach (SettingsPropertyElement propval in propvals)
            {
                SetValue(propval);
            }
            try
            {
                SettingsXML.Save(GetAppSettingsFilename());
            }
            catch (Exception) {}
        }

        public override List<SettingsPropertyElement> GetPropertyValues(List<SettingsProperty> props)
        {
            var list = new List<SettingsPropertyElement>();
            foreach (SettingsProperty prop in props)
            {
                SettingsPropertyElement settingsPropertyElement = new SettingsPropertyElement(prop);
                settingsPropertyElement.SerializedValue = GetValue(prop);
                list.Add(settingsPropertyElement);
            }
            return list;
        }

        string GetValue(SettingsProperty setting)
        {
            string text = "";
            try
            {
                return SettingsXML.SelectSingleNode(settingsRoot + "/" + setting.Name).InnerText;
            }
            catch (Exception)
            {
                if (setting.DefaultValue != null)
                {
                    return setting.DefaultValue.ToString();
                }
                return "";
            }
        }

        void SetValue(SettingsPropertyElement propVal)
        {
            //IL_0023: Unknown result type (might be due to invalid IL or missing references)
            //IL_0029: Expected O, but got Unknown
            XmlElement val = null;
            try
            {
                val = (XmlElement)SettingsXML.SelectSingleNode(settingsRoot + "/" + propVal.Name);
            }
            catch (Exception)
            {
                val = null;
            }
            if (val != null)
            {
                val.InnerText = propVal.SerializedValue;
                return;
            }
            val = SettingsXML.CreateElement(propVal.Name);
            val.InnerText = propVal.SerializedValue;
            SettingsXML.SelectSingleNode(settingsRoot).AppendChild(val);
        }
    }
}