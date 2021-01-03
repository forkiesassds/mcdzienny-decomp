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
    // Token: 0x0200022B RID: 555
    public class GuiSettingsProvider : SettingsProvider
    {
        // Token: 0x04000884 RID: 2180
        private const string SETTINGSROOT = "GuiSettings";

        // Token: 0x04000885 RID: 2181
        private readonly string fileName = "properties/gui.properties";

        // Token: 0x04000886 RID: 2182
        private XmlDocument m_SettingsXML;

        // Token: 0x17000598 RID: 1432
        // (get) Token: 0x06000FAD RID: 4013 RVA: 0x00054490 File Offset: 0x00052690
        // (set) Token: 0x06000FAE RID: 4014 RVA: 0x000544A4 File Offset: 0x000526A4
        public override string ApplicationName
        {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
            set { }
        }

        // Token: 0x17000599 RID: 1433
        // (get) Token: 0x06000FB3 RID: 4019 RVA: 0x000545C4 File Offset: 0x000527C4
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
                        var newChild2 = m_SettingsXML.CreateNode(XmlNodeType.Element, "GuiSettings", "");
                        m_SettingsXML.AppendChild(newChild2);
                    }
                }

                return m_SettingsXML;
            }
        }

        // Token: 0x06000FAC RID: 4012 RVA: 0x00054480 File Offset: 0x00052680
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(ApplicationName, config);
        }

        // Token: 0x06000FAF RID: 4015 RVA: 0x000544A8 File Offset: 0x000526A8
        public virtual string GetAppSettingsPath()
        {
            var fileInfo = new FileInfo(Application.ExecutablePath);
            return fileInfo.DirectoryName;
        }

        // Token: 0x06000FB0 RID: 4016 RVA: 0x000544C8 File Offset: 0x000526C8
        public virtual string GetAppSettingsFilename()
        {
            return fileName;
        }

        // Token: 0x06000FB1 RID: 4017 RVA: 0x000544D0 File Offset: 0x000526D0
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
            catch
            {
            }
        }

        // Token: 0x06000FB2 RID: 4018 RVA: 0x00054548 File Offset: 0x00052748
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

        // Token: 0x06000FB4 RID: 4020 RVA: 0x0005467C File Offset: 0x0005287C
        private string GetValue(SettingsProperty setting)
        {
            var result = "";
            try
            {
                if (IsRoaming(setting))
                    result = SettingsXML.SelectSingleNode("GuiSettings/" + setting.Name).InnerText;
                else
                    result = SettingsXML.SelectSingleNode("GuiSettings/" + setting.Name).InnerText;
            }
            catch (Exception)
            {
                if (setting.DefaultValue != null)
                    result = setting.DefaultValue.ToString();
                else
                    result = "";
            }

            return result;
        }

        // Token: 0x06000FB5 RID: 4021 RVA: 0x00054710 File Offset: 0x00052910
        private void SetValue(SettingsPropertyValue propVal)
        {
            XmlElement xmlElement = null;
            try
            {
                if (IsRoaming(propVal.Property))
                    xmlElement = (XmlElement) SettingsXML.SelectSingleNode("GuiSettings/" + propVal.Name);
                else
                    xmlElement = (XmlElement) SettingsXML.SelectSingleNode("GuiSettings/" + propVal.Name);
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
                SettingsXML.SelectSingleNode("GuiSettings").AppendChild(xmlElement);
                return;
            }

            xmlElement = SettingsXML.CreateElement(propVal.Name);
            xmlElement.InnerText = propVal.SerializedValue.ToString();
            SettingsXML.SelectSingleNode("GuiSettings").AppendChild(xmlElement);
        }

        // Token: 0x06000FB6 RID: 4022 RVA: 0x00054820 File Offset: 0x00052A20
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