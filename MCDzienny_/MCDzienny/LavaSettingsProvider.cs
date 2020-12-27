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
    // Token: 0x0200022D RID: 557
    public class LavaSettingsProvider : SettingsProvider
    {
        // Token: 0x04000888 RID: 2184
        private const string SETTINGSROOT = "LavaSettings";

        // Token: 0x04000889 RID: 2185
        private readonly string fileName = "properties/lava.properties";

        // Token: 0x0400088A RID: 2186
        private XmlDocument m_SettingsXML;

        // Token: 0x170005C1 RID: 1473
        // (get) Token: 0x06001008 RID: 4104 RVA: 0x00054EC4 File Offset: 0x000530C4
        // (set) Token: 0x06001009 RID: 4105 RVA: 0x00054ED8 File Offset: 0x000530D8
        public override string ApplicationName
        {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
            set { }
        }

        // Token: 0x170005C2 RID: 1474
        // (get) Token: 0x0600100E RID: 4110 RVA: 0x00054FF8 File Offset: 0x000531F8
        private XmlDocument SettingsXML
        {
            get
            {
                if (m_SettingsXML == null)
                {
                    m_SettingsXML = new XmlDocument();
                    try
                    {
                        m_SettingsXML.Load(GetAppSettingsFilename());
                    }
                    catch (Exception)
                    {
                        var newChild = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", "yes");
                        m_SettingsXML.AppendChild(newChild);
                        m_SettingsXML.AppendChild(m_SettingsXML.CreateWhitespace("\n\r"));
                        var newChild2 = m_SettingsXML.CreateNode(XmlNodeType.Element, "LavaSettings", "");
                        m_SettingsXML.AppendChild(newChild2);
                    }
                }

                return m_SettingsXML;
            }
        }

        // Token: 0x06001007 RID: 4103 RVA: 0x00054EB4 File Offset: 0x000530B4
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(ApplicationName, config);
        }

        // Token: 0x0600100A RID: 4106 RVA: 0x00054EDC File Offset: 0x000530DC
        public virtual string GetAppSettingsPath()
        {
            var fileInfo = new FileInfo(Application.ExecutablePath);
            return fileInfo.DirectoryName;
        }

        // Token: 0x0600100B RID: 4107 RVA: 0x00054EFC File Offset: 0x000530FC
        public virtual string GetAppSettingsFilename()
        {
            return fileName;
        }

        // Token: 0x0600100C RID: 4108 RVA: 0x00054F04 File Offset: 0x00053104
        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propvals)
        {
            foreach (var obj in propvals)
            {
                var value = (SettingsPropertyValue) obj;
                SetValue(value);
            }

            try
            {
                SettingsXML.Save(GetAppSettingsFilename());
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x0600100D RID: 4109 RVA: 0x00054F7C File Offset: 0x0005317C
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context,
            SettingsPropertyCollection props)
        {
            var settingsPropertyValueCollection = new SettingsPropertyValueCollection();
            foreach (var obj in props)
            {
                var settingsProperty = (SettingsProperty) obj;
                settingsPropertyValueCollection.Add(new SettingsPropertyValue(settingsProperty)
                {
                    IsDirty = false,
                    SerializedValue = GetValue(settingsProperty)
                });
            }

            return settingsPropertyValueCollection;
        }

        // Token: 0x0600100F RID: 4111 RVA: 0x000550B0 File Offset: 0x000532B0
        private string GetValue(SettingsProperty setting)
        {
            var result = "";
            try
            {
                if (IsRoaming(setting))
                    result = SettingsXML.SelectSingleNode("LavaSettings/" + setting.Name).InnerText;
                else
                    result = SettingsXML.SelectSingleNode("LavaSettings/" + setting.Name).InnerText;
            }
            catch
            {
                if (setting.DefaultValue != null)
                    result = setting.DefaultValue.ToString();
                else
                    result = "";
            }

            return result;
        }

        // Token: 0x06001010 RID: 4112 RVA: 0x00055144 File Offset: 0x00053344
        private void SetValue(SettingsPropertyValue propVal)
        {
            XmlElement xmlElement = null;
            try
            {
                if (IsRoaming(propVal.Property))
                    xmlElement = (XmlElement) SettingsXML.SelectSingleNode("LavaSettings/" + propVal.Name);
                else
                    xmlElement = (XmlElement) SettingsXML.SelectSingleNode("LavaSettings/" + propVal.Name);
            }
            catch (Exception)
            {
                xmlElement = null;
            }

            if (xmlElement != null)
            {
                xmlElement.InnerText = propVal.SerializedValue.ToString();
                return;
            }

            if (IsRoaming(propVal.Property))
            {
                xmlElement = SettingsXML.CreateElement(propVal.Name);
                xmlElement.InnerText = propVal.SerializedValue.ToString();
                SettingsXML.SelectSingleNode("LavaSettings").AppendChild(xmlElement);
                return;
            }

            xmlElement = SettingsXML.CreateElement(propVal.Name);
            xmlElement.InnerText = propVal.SerializedValue.ToString();
            SettingsXML.SelectSingleNode("LavaSettings").AppendChild(xmlElement);
        }

        // Token: 0x06001011 RID: 4113 RVA: 0x00055254 File Offset: 0x00053454
        private bool IsRoaming(SettingsProperty prop)
        {
            foreach (var obj in prop.Attributes)
            {
                var attribute = (Attribute) ((DictionaryEntry) obj).Value;
                if (attribute is SettingsManageabilityAttribute) return true;
            }

            return false;
        }
    }
}