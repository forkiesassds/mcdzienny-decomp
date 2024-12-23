using System;
using System.Collections.Generic;
using System.IO;
using MCDzienny.MultiMessages;

namespace MCDzienny
{
    static class Language
    {

        static readonly Random rand = new Random();

        static readonly MessagesCollection messages = new MessagesCollection();

        public static List<mTexts> multiTexts = new List<mTexts>();

        public static string[] texts = new string[26]
        {
            "stood in &cmagma and melted.", "%5(right)Look out! Lava is coming!!!", " minute left to lava flood!", " minutes left to lava flood",
            "%5Survivors, Congratulations!!!", "%6Winners list:", " minute left to the end of lava flood!", " minutes left to the end of lava flood",
            " has joined the game.", " disconnected.", " is a ghost now.", "%bYou were sent to heaven!", "%aIf you want to come back to lava arena buy a life.",
            "%5(right)Look out! Water is coming!!!", " minute left to water flood!", " minutes left to water flood", " minute left to the end of water flood!",
            " minutes left to the end of water flood", "stepped in &dcold water and froze.", "was hit by &cflowing magma and melted.", "died due to lack of &5brain.",
            "was killed &cb-SSSSSSSSSSSSSS", "%cVirus will be released in:", " was infected!!", "%b RUN HUMANS, RUN!", " %cwas bitten by "
        };

        public static void Init()
        {
            lock (multiTexts)
            {
                LoadCustomTexts();
            }
        }

        public static string GetString(string key)
        {
            return messages[key];
        }

        public static void LoadCustomMessages(string culture = null)
        {
            try
            {
                string text = "Messages/";
                string text2 = "messages";
                string text3 = ".txt";
                string text4 = text2 + text3;
                if (culture == null || culture.ToLower() == "default" || culture.ToLower() == "")
                {
                    if (!File.Exists(text + text4))
                    {
                        File.Create(text + text4).Close();
                    }
                }
                else
                {
                    text4 = string.Format("{0}.{1}{2}", text2, culture, text3);
                    if (!File.Exists(text + text4))
                    {
                        File.Create(text + text4).Close();
                    }
                }
                using (StreamReader streamReader = new StreamReader(text + text4))
                {
                    string text5;
                    while ((text5 = streamReader.ReadLine()) != null || !text5.StartsWith("#"))
                    {
                        text5 = text5.Trim();
                        if (!(text5 == "") && !text5.StartsWith("//") && !text5.StartsWith("#") && text5.IndexOf("=") != -1)
                        {
                            text5 = text5.TrimEnd(',');
                            string key = text5.Substring(0, text5.IndexOf("=")).Trim();
                            string text6 = text5.Substring(text5.IndexOf("=") + 1, text5.Length - 1 - text5.IndexOf("="));
                            string[] array = text6.Split(',');
                            for (int i = 0; i < array.Length; i++)
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

        public static void LoadCustomTexts()
        {
            for (int i = 0; i < texts.Length; i++)
            {
                multiTexts.Add(new mTexts
                {
                    index = (ushort)(i + 1),
                    texts = new string[1]
                    {
                        texts[i]
                    }
                });
            }
            if (!File.Exists("textdata.txt"))
            {
                File.Create("textdata.txt").Close();
                using (StreamWriter streamWriter = new StreamWriter("textdata.txt"))
                {
                    for (int j = 0; j < texts.Length; j++)
                    {
                        streamWriter.WriteLine(j + 1 + ": \"" + texts[j] + "\"");
                    }
                }
            }
            using (StreamReader streamReader = new StreamReader("textdata.txt"))
            {
                string text;
                while ((text = streamReader.ReadLine()) != null)
                {
                    try
                    {
                        if (text.Split(':').Length > 1)
                        {
                            string[] array = text.Split(':');
                            mTexts temp = new mTexts();
                            temp.index = ushort.Parse(array[0].Trim(' '));
                            temp.texts = new string[array.Length - 1];
                            for (int k = 0; k < array.Length - 1; k++)
                            {
                                temp.texts[k] = array[k + 1].Trim(' ').Trim('"');
                            }
                            mTexts mTexts = multiTexts.Find(mtex => mtex.index == temp.index);
                            if (mTexts != null)
                            {
                                mTexts.texts = temp.texts;
                            }
                        }
                    }
                    catch {}
                }
            }
        }

        public static string GetText(ushort index)
        {
            mTexts mTexts2 = multiTexts.Find(mTexts => mTexts.index == index);
            if (mTexts2.index == 0)
            {
                return texts[index - 1];
            }
            int num = rand.Next(0, mTexts2.texts.Length);
            return mTexts2.texts[num];
        }

        public class mTexts
        {

            public ushort index;
            public string[] texts;
        }
    }
}