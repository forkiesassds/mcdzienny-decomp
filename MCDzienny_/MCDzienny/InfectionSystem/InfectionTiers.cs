using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MCDzienny.InfectionSystem
{
    // Token: 0x020000A1 RID: 161
    public class InfectionTiers
    {
        // Token: 0x04000240 RID: 576
        public static int[] tierTreshold =
        {
            300,
            700,
            1300,
            1900,
            2700,
            2900,
            4000,
            5200,
            6500,
            8000,
            10000,
            12000,
            14500,
            17000,
            19500,
            22000,
            25000,
            28000,
            35000
        };

        // Token: 0x04000241 RID: 577
        private static List<Tier> tiers = new List<Tier>();

        // Token: 0x04000242 RID: 578
        private static readonly string filePath = "infection\\levelsystem.txt";

        // Token: 0x0600044A RID: 1098 RVA: 0x00018C0C File Offset: 0x00016E0C
        public static void SetDefaultTiers()
        {
            tierTreshold = new[]
            {
                300,
                700,
                1300,
                1900,
                2700,
                2900,
                4000,
                5200,
                6500,
                8000,
                10000,
                12000,
                14500,
                17000,
                19500,
                22000,
                25000,
                28000,
                35000
            };
        }

        // Token: 0x0600044B RID: 1099 RVA: 0x00018C28 File Offset: 0x00016E28
        public static void InitTierSystem()
        {
            LoadTiersXML();
            ListToArrays();
        }

        // Token: 0x0600044C RID: 1100 RVA: 0x00018C34 File Offset: 0x00016E34
        public static void GiveItems(Player p)
        {
        }

        // Token: 0x0600044D RID: 1101 RVA: 0x00018C38 File Offset: 0x00016E38
        public static void ArraysToList()
        {
            var list = new List<Tier>();
            var item = new Tier
            {
                tier = 1
            };
            list.Add(item);
            for (var i = 0; i < tierTreshold.Length; i++)
                list.Add(new Tier
                {
                    tier = i + 2,
                    experience = tierTreshold[i]
                });
            tiers = list;
        }

        // Token: 0x0600044E RID: 1102 RVA: 0x00018CA0 File Offset: 0x00016EA0
        public static void ListToArrays()
        {
            var tier = tiers[tiers.Count - 1].tier;
            tierTreshold = new int[tier - 1];
            for (var i = 0; i < tier; i++)
            {
                var tier2 = tiers[i].tier;
                if (tier2 != 1) tierTreshold[tier2 - 2] = tiers[i].experience;
            }

            var num = 0;
            for (var j = 0; j < tierTreshold.Length; j++)
            {
                if (tierTreshold[j] < num)
                {
                    Server.s.Log("Error: List of levels is corrupted at level: " + (j + 2) + ". Using default levels.");
                    SetDefaultTiers();
                    return;
                }

                num = tierTreshold[j];
            }
        }

        // Token: 0x0600044F RID: 1103 RVA: 0x00018D68 File Offset: 0x00016F68
        public static int GetTier(int exp)
        {
            for (var i = tierTreshold.Length - 1; i >= 0; i--)
                if (exp > tierTreshold[i])
                    return i + 2;
            return 1;
        }

        // Token: 0x06000450 RID: 1104 RVA: 0x00018D98 File Offset: 0x00016F98
        private static int SortByTier(Tier x, Tier y)
        {
            if (x.tier > y.tier) return 1;
            if (x.tier < y.tier) return -1;
            return 0;
        }

        // Token: 0x06000451 RID: 1105 RVA: 0x00018DBC File Offset: 0x00016FBC
        public static void SortTiers()
        {
            tiers.RemoveAll(t => t.tier <= 0);
            tiers.RemoveAll(t => t.experience < 0);
            tiers.Sort(SortByTier);
        }

        // Token: 0x06000452 RID: 1106 RVA: 0x00018E30 File Offset: 0x00017030
        public static void ReloadLevels()
        {
            tiers.Clear();
            LoadTiersXML();
        }

        // Token: 0x06000453 RID: 1107 RVA: 0x00018E44 File Offset: 0x00017044
        public static void LoadTiersXML()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var xmlDocument = new XmlDocument();
                    using (var fileStream =
                        new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        xmlDocument.Load(fileStream);
                    }

                    var elementsByTagName = xmlDocument.GetElementsByTagName("Level");
                    for (var i = 0; i < elementsByTagName.Count; i++)
                    {
                        var attributes = elementsByTagName[i].Attributes;
                        var tier = new Tier();
                        for (var j = 0; j < attributes.Count; j++)
                        {
                            string a;
                            if ((a = attributes[j].Name.ToLower()) != null)
                            {
                                if (!(a == "level"))
                                {
                                    if (!(a == "experience")) goto IL_13A;
                                }
                                else
                                {
                                    try
                                    {
                                        tier.tier = int.Parse(attributes[j].Value);
                                        if (!int.TryParse(attributes[j].Value, out tier.tier)) tier.tier = -1;
                                        goto IL_13A;
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of tier in tier's list.");
                                        goto IL_13A;
                                    }
                                }

                                try
                                {
                                    tier.experience = int.Parse(attributes[j].Value);
                                }
                                catch
                                {
                                    Server.s.Log("Wrong value of experience in tier's list(tier=" + tier.tier +
                                                 "\", using default value.");
                                }
                            }

                            IL_13A: ;
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
                return;
            }

            ArraysToList();
            SaveTiersXML();
        }

        // Token: 0x06000454 RID: 1108 RVA: 0x00019044 File Offset: 0x00017244
        public static void SaveTiersXML()
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
                xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
                xmlDocument.AppendChild(xmlDocument.CreateComment(
                    "You can add as many tiers as you wish, but minimum number is 10. Also tier 1 always requires 0 experience."));
                xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
                var xmlElement = xmlDocument.CreateElement("Levels");
                for (var i = 0; i < tiers.Count; i++)
                {
                    var xmlElement2 = xmlDocument.CreateElement("Level");
                    var xmlAttribute = xmlDocument.CreateAttribute("level");
                    var xmlAttribute2 = xmlDocument.CreateAttribute("experience");
                    xmlAttribute.Value = tiers[i].tier.ToString();
                    xmlAttribute2.Value = tiers[i].experience.ToString();
                    xmlElement2.SetAttributeNode(xmlAttribute);
                    xmlElement2.SetAttributeNode(xmlAttribute2);
                    xmlElement.AppendChild(xmlElement2);
                }

                var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                xmlDocument.AppendChild(xmlElement);
                xmlDocument.Save(fileStream);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x020000A2 RID: 162
        private class Tier
        {
            // Token: 0x04000246 RID: 582
            public int experience;

            // Token: 0x04000245 RID: 581
            public int tier;
        }
    }
}