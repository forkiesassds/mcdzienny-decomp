using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MCDzienny.Levels.Info
{
    // Token: 0x0200003C RID: 60
    public class LevelInfoManager
    {
        // Token: 0x06000150 RID: 336 RVA: 0x000087C8 File Offset: 0x000069C8
        public LevelInfoRaw Load(Level level)
        {
            if (level == null) throw new NullReferenceException("level");
            var infoPath = GetInfoPath(level);
            if (!File.Exists(infoPath)) return null;
            LevelInfoRaw result;
            try
            {
                LevelInfoRaw levelInfoRaw;
                using (var xmlReader = XmlReader.Create(infoPath))
                {
                    var xmlSerializer = new XmlSerializer(typeof(LevelInfoRaw));
                    levelInfoRaw = (LevelInfoRaw) xmlSerializer.Deserialize(xmlReader);
                }

                result = levelInfoRaw;
            }
            catch (InvalidOperationException ex)
            {
                Server.ErrorLog(ex);
                try
                {
                    var text = infoPath + ".old";
                    File.Delete(text);
                    File.Move(infoPath, text);
                }
                catch (Exception ex2)
                {
                    Server.ErrorLog(ex2);
                }

                result = null;
            }

            return result;
        }

        // Token: 0x06000151 RID: 337 RVA: 0x0000888C File Offset: 0x00006A8C
        public void Save(Level level, LevelInfoRaw info)
        {
            if (level == null) throw new NullReferenceException("level");
            if (info == null) throw new NullReferenceException("info");
            var infoPath = GetInfoPath(level);
            try
            {
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true
                };
                using (var xmlWriter = XmlWriter.Create(infoPath, settings))
                {
                    var xmlSerializer = new XmlSerializer(info.GetType());
                    xmlSerializer.Serialize(xmlWriter, info);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000152 RID: 338 RVA: 0x0000892C File Offset: 0x00006B2C
        private string GetInfoPath(Level level)
        {
            return level.directoryPath + "/" + level.fileName + ".txt";
        }
    }
}