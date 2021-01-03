using System;
using System.Collections.Generic;
using System.IO;
using MCDzienny.MultiMessages;

namespace MCDzienny
{
    // Token: 0x02000309 RID: 777
    internal static class Language
    {
        // Token: 0x04000AF4 RID: 2804
        private static readonly Random rand = new Random();

        // Token: 0x04000AF5 RID: 2805
        private static readonly MessagesCollection messages = new MessagesCollection();

        // Token: 0x04000AF6 RID: 2806
        public static List<mTexts> multiTexts = new List<mTexts>();

        // Token: 0x04000AF7 RID: 2807
        public static string[] texts =
        {
            "stood in &cmagma and melted.",
            "%5(right)Look out! Lava is coming!!!",
            " minute left to lava flood!",
            " minutes left to lava flood",
            "%5Survivors, Congratulations!!!",
            "%6Winners list:",
            " minute left to the end of lava flood!",
            " minutes left to the end of lava flood",
            " has joined the game.",
            " disconnected.",
            " is a ghost now.",
            "%bYou were sent to heaven!",
            "%aIf you want to come back to lava arena buy a life.",
            "%5(right)Look out! Water is coming!!!",
            " minute left to water flood!",
            " minutes left to water flood",
            " minute left to the end of water flood!",
            " minutes left to the end of water flood",
            "stepped in &dcold water and froze.",
            "was hit by &cflowing magma and melted.",
            "died due to lack of &5brain.",
            "was killed &cb-SSSSSSSSSSSSSS",
            "%cVirus will be released in:",
            " was infected!!",
            "%b RUN HUMANS, RUN!",
            " %cwas bitten by "
        };

        // Token: 0x0600167B RID: 5755 RVA: 0x00087B34 File Offset: 0x00085D34
        public static void Init()
        {
            lock (multiTexts)
            {
                LoadCustomTexts();
            }
        }

        // Token: 0x0600167C RID: 5756 RVA: 0x00087B6C File Offset: 0x00085D6C
        public static string GetString(string key)
        {
            return messages[key];
        }

        // Token: 0x0600167D RID: 5757 RVA: 0x00087B7C File Offset: 0x00085D7C
        public static void LoadCustomMessages(string culture = null)
        {
            try
            {
                var str = "Messages/";
                var text = "messages";
                var text2 = ".txt";
                var str2 = text + text2;
                if (culture == null || culture.ToLower() == "default" || culture.ToLower() == "")
                {
                    if (!File.Exists(str + str2)) File.Create(str + str2).Close();
                }
                else
                {
                    str2 = string.Format("{0}.{1}{2}", text, culture, text2);
                    if (!File.Exists(str + str2)) File.Create(str + str2).Close();
                }

                using (var streamReader = new StreamReader(str + str2))
                {
                    string text3;
                    while ((text3 = streamReader.ReadLine()) != null || !text3.StartsWith("#"))
                    {
                        text3 = text3.Trim();
                        if (!(text3 == "") && !text3.StartsWith("//") && !text3.StartsWith("#") &&
                            text3.IndexOf("=") != -1)
                        {
                            text3 = text3.TrimEnd(',');
                            var key = text3.Substring(0, text3.IndexOf("=")).Trim();
                            var text4 = text3.Substring(text3.IndexOf("=") + 1, text3.Length - 1 - text3.IndexOf("="));
                            var array = text4.Split(',');
                            for (var i = 0; i < array.Length; i++)
                            {
                                array[i] = array[i].Trim().Remove(0, 1);
                                array[i] = array[i].Remove(array[i].Length - 1);
                            }

                            messages.Add(key, array);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x0600167E RID: 5758 RVA: 0x00087DB4 File Offset: 0x00085FB4
        public static void LoadCustomTexts()
        {
            for (var i = 0; i < texts.Length; i++)
                multiTexts.Add(new mTexts
                {
                    index = (ushort) (i + 1),
                    texts = new[]
                    {
                        texts[i]
                    }
                });
            if (!File.Exists("textdata.txt"))
            {
                File.Create("textdata.txt").Close();
                using (var streamWriter = new StreamWriter("textdata.txt"))
                {
                    for (var j = 0; j < texts.Length; j++)
                        streamWriter.WriteLine(string.Concat(j + 1, ": \"", texts[j], "\""));
                }
            }

            using (var streamReader = new StreamReader("textdata.txt"))
            {
                string text;
                while ((text = streamReader.ReadLine()) != null)
                    try
                    {
                        if (text.Split(':').Length > 1)
                        {
                            var array = text.Split(':');
                            var temp = new mTexts();
                            temp.index = ushort.Parse(array[0].Trim(' '));
                            temp.texts = new string[array.Length - 1];
                            for (var k = 0; k < array.Length - 1; k++) temp.texts[k] = array[k + 1].Trim(' ').Trim('"');
                            var mTexts = multiTexts.Find(mtex => mtex.index == temp.index);
                            if (mTexts != null) mTexts.texts = temp.texts;
                        }
                    }
                    catch
                    {
                    }
            }
        }

        // Token: 0x0600167F RID: 5759 RVA: 0x0008801C File Offset: 0x0008621C
        public static string GetText(ushort index)
        {
            var mTexts2 = multiTexts.Find(mTexts => mTexts.index == index);
            if (mTexts2.index == 0) return texts[index - 1];
            var num = rand.Next(0, mTexts2.texts.Length);
            return mTexts2.texts[num];
        }

        // Token: 0x0200030A RID: 778
        public class mTexts
        {
            // Token: 0x04000AF9 RID: 2809
            public ushort index;

            // Token: 0x04000AF8 RID: 2808
            public string[] texts;
        }
    }
}