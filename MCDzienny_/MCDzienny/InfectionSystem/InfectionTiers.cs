using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MCDzienny.InfectionSystem
{
    public class InfectionTiers
    {

        public static int[] tierTreshold = new int[19]
        {
            300, 700, 1300, 1900, 2700, 2900, 4000, 5200, 6500, 8000, 10000, 12000, 14500, 17000, 19500, 22000, 25000, 28000, 35000
        };

        static List<Tier> tiers = new List<Tier>();

        static readonly string filePath = "infection\\levelsystem.txt";

        public static void SetDefaultTiers()
        {
            tierTreshold = new int[19]
            {
                300, 700, 1300, 1900, 2700, 2900, 4000, 5200, 6500, 8000, 10000, 12000, 14500, 17000, 19500, 22000, 25000, 28000, 35000
            };
        }

        public static void InitTierSystem()
        {
            LoadTiersXML();
            ListToArrays();
        }

        public static void GiveItems(Player p) {}

        public static void ArraysToList()
        {
            var list = new List<Tier>();
            Tier tier = new Tier();
            tier.tier = 1;
            Tier item = tier;
            list.Add(item);
            for (int i = 0; i < tierTreshold.Length; i++)
            {
                Tier tier2 = new Tier();
                tier2.tier = i + 2;
                tier2.experience = tierTreshold[i];
                list.Add(tier2);
            }
            tiers = list;
        }

        public static void ListToArrays()
        {
            int tier = tiers[tiers.Count - 1].tier;
            tierTreshold = new int[tier - 1];
            for (int i = 0; i < tier; i++)
            {
                int tier2 = tiers[i].tier;
                if (tier2 != 1)
                {
                    tierTreshold[tier2 - 2] = tiers[i].experience;
                }
            }
            int num = 0;
            for (int j = 0; j < tierTreshold.Length; j++)
            {
                if (tierTreshold[j] < num)
                {
                    Server.s.Log("Error: List of levels is corrupted at level: " + (j + 2) + ". Using default levels.");
                    SetDefaultTiers();
                    break;
                }
                num = tierTreshold[j];
            }
        }

        public static int GetTier(int exp)
        {
            for (int num = tierTreshold.Length - 1; num >= 0; num--)
            {
                if (exp > tierTreshold[num])
                {
                    return num + 2;
                }
            }
            return 1;
        }

        static int SortByTier(Tier x, Tier y)
        {
            if (x.tier > y.tier)
            {
                return 1;
            }
            if (x.tier < y.tier)
            {
                return -1;
            }
            return 0;
        }

        public static void SortTiers()
        {
            tiers.RemoveAll(t => t.tier <= 0);
            tiers.RemoveAll(t => t.experience < 0);
            tiers.Sort(SortByTier);
        }

        public static void ReloadLevels()
        {
            tiers.Clear();
            LoadTiersXML();
        }

        public static void LoadTiersXML()
        {
            //IL_000f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0015: Expected O, but got Unknown
            if (File.Exists(filePath))
            {
                try
                {
                    XmlDocument val = new XmlDocument();
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        val.Load(fileStream);
                    }
                    XmlNodeList elementsByTagName = val.GetElementsByTagName("Level");
                    for (int i = 0; i < elementsByTagName.Count; i++)
                    {
                        XmlAttributeCollection attributes = elementsByTagName[i].Attributes;
                        Tier tier = new Tier();
                        for (int j = 0; j < attributes.Count; j++)
                        {
                            switch (attributes[j].Name.ToLower())
                            {
                                case "level":
                                    try
                                    {
                                        tier.tier = int.Parse(attributes[j].Value);
                                        if (!int.TryParse(attributes[j].Value, out tier.tier))
                                        {
                                            tier.tier = -1;
                                        }
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of tier in tier's list.");
                                    }
                                    break;
                                case "experience":
                                    try
                                    {
                                        tier.experience = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of experience in tier's list(tier=" + tier.tier + "\", using default value.");
                                    }
                                    break;
                            }
                        }
                        tiers.Add(tier);
                    }
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
                SortTiers();
                SaveTiersXML();
            }
            else
            {
                ArraysToList();
                SaveTiersXML();
            }
        }

        public static void SaveTiersXML()
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Expected O, but got Unknown
            try
            {
                XmlDocument val = new XmlDocument();
                val.AppendChild(val.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
                val.AppendChild(val.CreateWhitespace("\r\n"));
                val.AppendChild(val.CreateComment("You can add as many tiers as you wish, but minimum number is 10. Also tier 1 always requires 0 experience."));
                val.AppendChild(val.CreateWhitespace("\r\n"));
                XmlElement val2 = val.CreateElement("Levels");
                for (int i = 0; i < tiers.Count; i++)
                {
                    XmlElement val3 = val.CreateElement("Level");
                    XmlAttribute val4 = val.CreateAttribute("level");
                    XmlAttribute val5 = val.CreateAttribute("experience");
                    val4.Value = tiers[i].tier.ToString();
                    val5.Value = tiers[i].experience.ToString();
                    val3.SetAttributeNode(val4);
                    val3.SetAttributeNode(val5);
                    val2.AppendChild(val3);
                }
                FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                val.AppendChild(val2);
                val.Save(fileStream);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        class Tier
        {

            public int experience;
            public int tier;
        }
    }
}