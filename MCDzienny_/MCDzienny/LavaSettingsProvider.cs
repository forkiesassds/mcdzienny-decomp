using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace MCDzienny
{
    public class LavaSettingsProvider : SettingsProvider
    {
        const string SETTINGSROOT = "LavaSettings";

        readonly string fileName = "properties/lava.properties";

        XmlDocument m_SettingsXML;

        public override string ApplicationName { get { return Assembly.GetExecutingAssembly().GetName().Name; } set {} }

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
                        XmlNode val2 = m_SettingsXML.CreateNode((XmlNodeType)1, "LavaSettings", "");
                        m_SettingsXML.AppendChild(val2);
                    }
                }
                return m_SettingsXML;
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            Initialize(ApplicationName, config);
        }

        public virtual string GetAppSettingsPath()
        {
            FileInfo fileInfo = new FileInfo(Application.ExecutablePath);
            return fileInfo.DirectoryName;
        }

        public virtual string GetAppSettingsFilename()
        {
            return fileName;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propvals)
        {
            //IL_000f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0015: Expected O, but got Unknown
            foreach (SettingsPropertyValue propval in propvals)
            {
                SettingsPropertyValue value = propval;
                SetValue(value);
            }
            try
            {
                SettingsXML.Save(GetAppSettingsFilename());
            }
            catch (Exception) {}
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection props)
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Expected O, but got Unknown
            //IL_0015: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Expected O, but got Unknown
            //IL_001c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0022: Expected O, but got Unknown
            SettingsPropertyValueCollection val = new SettingsPropertyValueCollection();
            foreach (SettingsProperty prop in props)
            {
                SettingsProperty val2 = prop;
                SettingsPropertyValue val3 = new SettingsPropertyValue(val2);
                val3.IsDirty = false;
                val3.SerializedValue = GetValue(val2);
                val.Add(val3);
            }
            return val;
        }

        string GetValue(SettingsProperty setting)
        {
            string text = "";
            try
            {
                if (IsRoaming(setting))
                {
                    return SettingsXML.SelectSingleNode("LavaSettings/" + setting.Name).InnerText;
                }
                return SettingsXML.SelectSingleNode("LavaSettings/" + setting.Name).InnerText;
            }
            catch
            {
                if (setting.DefaultValue != null)
                {
                    return setting.DefaultValue.ToString();
                }
                return "";
            }
        }

        void SetValue(SettingsPropertyValue propVal)
        {
            //IL_004e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0054: Expected O, but got Unknown
            //IL_002b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0031: Expected O, but got Unknown
            XmlElement val = null;
            try
            {
                val = !IsRoaming(propVal.Property) ? (XmlElement)SettingsXML.SelectSingleNode("LavaSettings/" + propVal.Name)
                    : (XmlElement)SettingsXML.SelectSingleNode("LavaSettings/" + propVal.Name);
            }
            catch (Exception)
            {
                val = null;
            }
            if (val != null)
            {
                val.InnerText = propVal.SerializedValue.ToString();
            }
            else if (IsRoaming(propVal.Property))
            {
                val = SettingsXML.CreateElement(propVal.Name);
                val.InnerText = propVal.SerializedValue.ToString();
                SettingsXML.SelectSingleNode("LavaSettings").AppendChild(val);
            }
            else
            {
                val = SettingsXML.CreateElement(propVal.Name);
                val.InnerText = propVal.SerializedValue.ToString();
                SettingsXML.SelectSingleNode("LavaSettings").AppendChild(val);
            }
        }

        bool IsRoaming(SettingsProperty prop)
        {
            foreach (DictionaryEntry item in prop.Attributes)
            {
                Attribute attribute = (Attribute)item.Value;
                if (attribute is SettingsManageabilityAttribute)
                {
                    return true;
                }
            }
            return false;
        }
    }
}