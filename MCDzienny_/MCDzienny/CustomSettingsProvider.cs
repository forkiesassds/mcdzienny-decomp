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
    // Token: 0x0200022E RID: 558
    public class CustomSettingsProvider : SettingsProvider
    {
        // Token: 0x0400088B RID: 2187
        private const string SETTINGSROOT = "GeneralSettings";

        // Token: 0x0400088C RID: 2188
        private readonly string fileName = "properties/general.properties";

        // Token: 0x0400088D RID: 2189
        private XmlDocument m_SettingsXML;

        // Token: 0x170005C3 RID: 1475
        // (get) Token: 0x06001014 RID: 4116 RVA: 0x000552EC File Offset: 0x000534EC
        // (set) Token: 0x06001015 RID: 4117 RVA: 0x00055300 File Offset: 0x00053500
        public override string ApplicationName
        {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
            set { }
        }

        // Token: 0x170005C4 RID: 1476
        // (get) Token: 0x0600101A RID: 4122 RVA: 0x00055420 File Offset: 0x00053620
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
                    catch
                    {
                        var newChild = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", "yes");
                        m_SettingsXML.AppendChild(newChild);
                        m_SettingsXML.AppendChild(m_SettingsXML.CreateWhitespace("\n\r"));
                        var newChild2 = m_SettingsXML.CreateNode(XmlNodeType.Element, "GeneralSettings", "");
                        m_SettingsXML.AppendChild(newChild2);
                    }
                }

                return m_SettingsXML;
            }
        }

        // Token: 0x06001013 RID: 4115 RVA: 0x000552DC File Offset: 0x000534DC
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(ApplicationName, config);
        }

        // Token: 0x06001016 RID: 4118 RVA: 0x00055304 File Offset: 0x00053504
        public virtual string GetAppSettingsPath()
        {
            var fileInfo = new FileInfo(Application.ExecutablePath);
            return fileInfo.DirectoryName;
        }

        // Token: 0x06001017 RID: 4119 RVA: 0x00055324 File Offset: 0x00053524
        public virtual string GetAppSettingsFilename()
        {
            return fileName;
        }

        // Token: 0x06001018 RID: 4120 RVA: 0x0005532C File Offset: 0x0005352C
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

        // Token: 0x06001019 RID: 4121 RVA: 0x000553A4 File Offset: 0x000535A4
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

        // Token: 0x0600101B RID: 4123 RVA: 0x000554D8 File Offset: 0x000536D8
        private string GetValue(SettingsProperty setting)
        {
            var result = "";
            try
            {
                if (IsRoaming(setting))
                    result = SettingsXML.SelectSingleNode("GeneralSettings/" + setting.Name).InnerText;
                else
                    result = SettingsXML.SelectSingleNode("GeneralSettings/" + setting.Name).InnerText;
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

        // Token: 0x0600101C RID: 4124 RVA: 0x0005556C File Offset: 0x0005376C
        private void SetValue(SettingsPropertyValue propVal)
        {
            XmlElement xmlElement = null;
            try
            {
                if (IsRoaming(propVal.Property))
                    xmlElement = (XmlElement) SettingsXML.SelectSingleNode("GeneralSettings/" + propVal.Name);
                else
                    xmlElement = (XmlElement) SettingsXML.SelectSingleNode("GeneralSettings/" + propVal.Name);
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
                SettingsXML.SelectSingleNode("GeneralSettings").AppendChild(xmlElement);
                return;
            }

            xmlElement = SettingsXML.CreateElement(propVal.Name);
            xmlElement.InnerText = propVal.SerializedValue.ToString();
            SettingsXML.SelectSingleNode("GeneralSettings").AppendChild(xmlElement);
        }

        // Token: 0x0600101D RID: 4125 RVA: 0x0005567C File Offset: 0x0005387C
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