using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x02000221 RID: 545
    internal class ConcreteSettingsProvider : SettingsProvider
    {
        // Token: 0x04000876 RID: 2166
        private XmlDocument m_SettingsXML;

        // Token: 0x04000874 RID: 2164
        private readonly string settingsFilePath;

        // Token: 0x04000875 RID: 2165
        private readonly string settingsRoot = "GeneralSettings";

        // Token: 0x06000F09 RID: 3849 RVA: 0x000533D4 File Offset: 0x000515D4
        public ConcreteSettingsProvider(string settingsFilePath, string settingsRoot)
        {
            this.settingsFilePath = settingsFilePath;
            this.settingsRoot = settingsRoot;
        }

        // Token: 0x1700054C RID: 1356
        // (get) Token: 0x06000F0A RID: 3850 RVA: 0x000533F8 File Offset: 0x000515F8
        public string ApplicationName
        {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        // Token: 0x1700054D RID: 1357
        // (get) Token: 0x06000F0F RID: 3855 RVA: 0x00053510 File Offset: 0x00051710
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
                        var newChild2 = m_SettingsXML.CreateNode(XmlNodeType.Element, settingsRoot, "");
                        m_SettingsXML.AppendChild(newChild2);
                    }
                }

                return m_SettingsXML;
            }
        }

        // Token: 0x06000F0B RID: 3851 RVA: 0x0005340C File Offset: 0x0005160C
        public virtual string GetAppSettingsPath()
        {
            var fileInfo = new FileInfo(Directory.GetCurrentDirectory());
            return fileInfo.DirectoryName;
        }

        // Token: 0x06000F0C RID: 3852 RVA: 0x0005342C File Offset: 0x0005162C
        public virtual string GetAppSettingsFilename()
        {
            return settingsFilePath;
        }

        // Token: 0x06000F0D RID: 3853 RVA: 0x00053434 File Offset: 0x00051634
        public override void SetPropertyValues(List<SettingsPropertyElement> propvals)
        {
            foreach (var value in propvals) SetValue(value);
            try
            {
                SettingsXML.Save(GetAppSettingsFilename());
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x06000F0E RID: 3854 RVA: 0x000534A4 File Offset: 0x000516A4
        public override List<SettingsPropertyElement> GetPropertyValues(List<SettingsProperty> props)
        {
            var list = new List<SettingsPropertyElement>();
            foreach (var settingsProperty in props)
                list.Add(new SettingsPropertyElement(settingsProperty)
                {
                    SerializedValue = GetValue(settingsProperty)
                });
            return list;
        }

        // Token: 0x06000F10 RID: 3856 RVA: 0x000535C8 File Offset: 0x000517C8
        private string GetValue(SettingsProperty setting)
        {
            var result = "";
            try
            {
                result = SettingsXML.SelectSingleNode(settingsRoot + "/" + setting.Name).InnerText;
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

        // Token: 0x06000F11 RID: 3857 RVA: 0x00053634 File Offset: 0x00051834
        private void SetValue(SettingsPropertyElement propVal)
        {
            XmlElement xmlElement = null;
            try
            {
                xmlElement = (XmlElement) SettingsXML.SelectSingleNode(settingsRoot + "/" + propVal.Name);
            }
            catch (Exception)
            {
                xmlElement = null;
            }

            if (xmlElement != null)
            {
                xmlElement.InnerText = propVal.SerializedValue;
                return;
            }

            xmlElement = SettingsXML.CreateElement(propVal.Name);
            xmlElement.InnerText = propVal.SerializedValue;
            SettingsXML.SelectSingleNode(settingsRoot).AppendChild(xmlElement);
        }
    }
}