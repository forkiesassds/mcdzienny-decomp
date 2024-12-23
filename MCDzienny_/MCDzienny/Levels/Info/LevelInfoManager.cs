using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MCDzienny.Levels.Info
{
    public class LevelInfoManager
    {
        public LevelInfoRaw Load(Level level)
        {
            //IL_0031: Unknown result type (might be due to invalid IL or missing references)
            //IL_0037: Expected O, but got Unknown
            if (level == null)
            {
                throw new NullReferenceException("level");
            }
            string infoPath = GetInfoPath(level);
            if (!File.Exists(infoPath))
            {
                return null;
            }
            try
            {
                XmlReader val = XmlReader.Create(infoPath);
                LevelInfoRaw result;
                try
                {
                    XmlSerializer val2 = new XmlSerializer(typeof(LevelInfoRaw));
                    result = (LevelInfoRaw)val2.Deserialize(val);
                }
                finally
                {
                    ((IDisposable)val).Dispose();
                }
                return result;
            }
            catch (InvalidOperationException ex)
            {
                Server.ErrorLog(ex);
                try
                {
                    string text = infoPath + ".old";
                    File.Delete(text);
                    File.Move(infoPath, text);
                }
                catch (Exception ex2)
                {
                    Server.ErrorLog(ex2);
                }
                return null;
            }
        }

        public void Save(Level level, LevelInfoRaw info)
        {
            //IL_0024: Unknown result type (might be due to invalid IL or missing references)
            //IL_002b: Expected O, but got Unknown
            //IL_0050: Unknown result type (might be due to invalid IL or missing references)
            //IL_0056: Expected O, but got Unknown
            if (level == null)
            {
                throw new NullReferenceException("level");
            }
            if (info == null)
            {
                throw new NullReferenceException("info");
            }
            string infoPath = GetInfoPath(level);
            try
            {
                XmlWriterSettings val = new XmlWriterSettings();
                val.Encoding = Encoding.UTF8;
                val.Indent = true;
                XmlWriterSettings val2 = val;
                XmlWriter val3 = XmlWriter.Create(infoPath, val2);
                try
                {
                    XmlSerializer val4 = new XmlSerializer(info.GetType());
                    val4.Serialize(val3, info);
                }
                finally
                {
                    ((IDisposable)val3).Dispose();
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        string GetInfoPath(Level level)
        {
            return level.directoryPath + "/" + level.fileName + ".txt";
        }
    }
}