using System;

namespace MCDzienny.Levels.Info
{
    public class LevelInfoConverter
    {
        public LevelInfoRaw ToRaw(LevelInfo info)
        {
            if (info == null)
            {
                throw new NullReferenceException("info");
            }
            LevelInfoRaw levelInfoRaw = new LevelInfoRaw();
            LevelInfoRawItem levelInfoRawItem = new LevelInfoRawItem();
            levelInfoRawItem.Key = "environment";
            levelInfoRawItem.Value = info.Environment;
            LevelInfoRawItem item = levelInfoRawItem;
            levelInfoRaw.Items.Add(item);
            LevelInfoRawItem levelInfoRawItem2 = new LevelInfoRawItem();
            levelInfoRawItem2.Key = "weather";
            levelInfoRawItem2.Value = info.Weather;
            item = levelInfoRawItem2;
            levelInfoRaw.Items.Add(item);
            LevelInfoRawItem levelInfoRawItem3 = new LevelInfoRawItem();
            levelInfoRawItem3.Key = "texture";
            levelInfoRawItem3.Value = info.Texture;
            item = levelInfoRawItem3;
            levelInfoRaw.Items.Add(item);
            return levelInfoRaw;
        }

        public LevelInfo FromRaw(LevelInfoRaw raw)
        {
            if (raw == null)
            {
                throw new NullReferenceException("raw");
            }
            LevelInfo levelInfo = new LevelInfo();
            foreach (LevelInfoRawItem item in raw.Items)
            {
                switch (item.Key.ToLower())
                {
                    case "environment":
                        levelInfo.Environment = item.Value;
                        break;
                    case "weather":
                        levelInfo.Weather = item.Value;
                        break;
                    case "texture":
                        levelInfo.Texture = item.Value;
                        break;
                }
            }
            return levelInfo;
        }
    }
}