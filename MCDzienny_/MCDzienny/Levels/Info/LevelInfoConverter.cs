using System;

namespace MCDzienny.Levels.Info
{
    // Token: 0x0200003A RID: 58
    public class LevelInfoConverter
    {
        // Token: 0x06000148 RID: 328 RVA: 0x00008624 File Offset: 0x00006824
        public LevelInfoRaw ToRaw(LevelInfo info)
        {
            if (info == null) throw new NullReferenceException("info");
            var levelInfoRaw = new LevelInfoRaw();
            var item = new LevelInfoRawItem
            {
                Key = "environment",
                Value = info.Environment
            };
            levelInfoRaw.Items.Add(item);
            item = new LevelInfoRawItem
            {
                Key = "weather",
                Value = info.Weather
            };
            levelInfoRaw.Items.Add(item);
            item = new LevelInfoRawItem
            {
                Key = "texture",
                Value = info.Texture
            };
            levelInfoRaw.Items.Add(item);
            return levelInfoRaw;
        }

        // Token: 0x06000149 RID: 329 RVA: 0x000086CC File Offset: 0x000068CC
        public LevelInfo FromRaw(LevelInfoRaw raw)
        {
            if (raw == null) throw new NullReferenceException("raw");
            var levelInfo = new LevelInfo();
            foreach (var levelInfoRawItem in raw.Items)
            {
                string a;
                if ((a = levelInfoRawItem.Key.ToLower()) != null)
                {
                    if (!(a == "environment"))
                    {
                        if (!(a == "weather"))
                        {
                            if (a == "texture") levelInfo.Texture = levelInfoRawItem.Value;
                        }
                        else
                        {
                            levelInfo.Weather = levelInfoRawItem.Value;
                        }
                    }
                    else
                    {
                        levelInfo.Environment = levelInfoRawItem.Value;
                    }
                }
            }

            return levelInfo;
        }
    }
}