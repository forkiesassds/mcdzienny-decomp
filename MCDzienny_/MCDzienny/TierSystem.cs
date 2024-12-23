using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using MCDzienny.Settings;

namespace MCDzienny
{
    public class TierSystem
    {

        public static int[] tierTreshold = new int[19]
        {
            500, 1300, 2350, 3820, 5878, 8759, 11928, 15255, 18748, 22415, 26448, 30682, 35128, 39796, 44697, 49844, 55248, 60652, 66056
        };

        public static int[] waterBlocsPerTier = new int[20]
        {
            2, 5, 10, 10, 10, 10, 10, 10, 10, 20, 20, 20, 25, 25, 30, 30, 30, 30, 30, 50
        };

        public static int[] hammerPerTier = new int[20]
        {
            0, 0, 0, 0, 0, 100, 100, 100, 100, 200, 200, 200, 300, 300, 400, 400, 400, 400, 400, 600
        };

        public static int[] spongeBlocksPerTier = new int[20]
        {
            0, 0, 0, 1, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 6
        };

        public static int[] doorBlocksPerTier = new int[20]
        {
            0, 0, 0, 0, 0, 4, 4, 4, 4, 6, 6, 6, 6, 8, 8, 8, 10, 10, 10, 12
        };

        static List<Tier> tiers = new List<Tier>();

        static readonly string filePath = "lava\\levelsystem.txt";

        public static void InitTiers()
        {
            tierTreshold = new int[19]
            {
                500, 1300, 2350, 3820, 5878, 8759, 11928, 15255, 18748, 22415, 26448, 30682, 35128, 39796, 44697, 49844, 55248, 60652, 66056
            };
            waterBlocsPerTier = new int[20]
            {
                2, 5, 10, 10, 10, 10, 10, 10, 10, 20, 20, 20, 25, 25, 30, 30, 30, 30, 30, 50
            };
            hammerPerTier = new int[20]
            {
                0, 0, 0, 0, 0, 100, 100, 100, 100, 200, 200, 200, 300, 300, 400, 400, 400, 400, 400, 600
            };
            spongeBlocksPerTier = new int[20]
            {
                0, 0, 0, 1, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 7
            };
            doorBlocksPerTier = new int[20]
            {
                0, 0, 0, 0, 0, 4, 4, 4, 4, 6, 6, 6, 6, 8, 8, 8, 10, 10, 10, 12
            };
        }

        public static void InitTierSystem()
        {
            LoadTiersXML();
            ListToArrays();
        }

        public static void GiveItems(Player p)
        {
            p.waterBlocks = waterBlocsPerTier[p.tier - 1];
            p.hammer = hammerPerTier[p.tier - 1];
            p.spongeBlocks = spongeBlocksPerTier[p.tier - 1];
            p.doorBlocks = doorBlocksPerTier[p.tier - 1];
        }

        public static void ColorSet(Player who)
        {
            if (LavaSettings.All.AutoNameColoring)
            {
                if (who.tier > 15)
                {
                    who.color = "&3";
                }
                else if (who.tier > 5)
                {
                    who.color = "&2";
                }
                else if (who.tier > 2)
                {
                    who.color = "&f";
                }
            }
        }

        public static void ArraysToList()
        {
            var list = new List<Tier>();
            Tier tier = new Tier();
            tier.tier = 1;
            tier.waterBlocks = waterBlocsPerTier[0];
            tier.hammer = hammerPerTier[0];
            tier.spongeBlocks = spongeBlocksPerTier[0];
            tier.experience = 0;
            tier.doorBlocks = doorBlocksPerTier[0];
            Tier item = tier;
            list.Add(item);
            for (int i = 0; i < tierTreshold.Length; i++)
            {
                Tier tier2 = new Tier();
                tier2.tier = i + 2;
                tier2.experience = tierTreshold[i];
                tier2.doorBlocks = doorBlocksPerTier[i + 1];
                tier2.hammer = hammerPerTier[i + 1];
                tier2.spongeBlocks = spongeBlocksPerTier[i + 1];
                tier2.waterBlocks = waterBlocsPerTier[i + 1];
                list.Add(tier2);
            }
            tiers = list;
        }

        public static void ListToArrays()
        {
            int tier = tiers[tiers.Count - 1].tier;
            tierTreshold = new int[tier - 1];
            waterBlocsPerTier = new int[tier];
            spongeBlocksPerTier = new int[tier];
            hammerPerTier = new int[tier];
            doorBlocksPerTier = new int[tier];
            for (int i = 0; i < tier; i++)
            {
                int tier2 = tiers[i].tier;
                if (tier2 != 1)
                {
                    tierTreshold[tier2 - 2] = tiers[i].experience;
                    waterBlocsPerTier[tier2 - 1] = tiers[i].waterBlocks;
                    spongeBlocksPerTier[tier2 - 1] = tiers[i].spongeBlocks;
                    hammerPerTier[tier2 - 1] = tiers[i].hammer;
                    doorBlocksPerTier[tier2 - 1] = tiers[i].doorBlocks;
                }
                else
                {
                    waterBlocsPerTier[tier2 - 1] = tiers[i].waterBlocks;
                    spongeBlocksPerTier[tier2 - 1] = tiers[i].spongeBlocks;
                    hammerPerTier[tier2 - 1] = tiers[i].hammer;
                    doorBlocksPerTier[tier2 - 1] = tiers[i].doorBlocks;
                }
            }
            int num = 0;
            for (int j = 0; j < tierTreshold.Length; j++)
            {
                if (tierTreshold[j] < num)
                {
                    Server.s.Log("Error: List of levels is corrupted at level: " + (j + 2) + ". Using default levels.");
                    InitTiers();
                    break;
                }
                num = tierTreshold[j];
            }
        }

        public static void TierSet(Player who)
        {
            who.tier = TierCheck(who.totalScore);
        }

        public static int TierCheck(int exp)
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
                                case "water":
                                    try
                                    {
                                        tier.waterBlocks = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of water in tier's list(tier=" + tier.tier + "\", using default value.");
                                    }
                                    break;
                                case "hammer":
                                    try
                                    {
                                        tier.hammer = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of hammer in tier's list(tier=" + tier.tier + "\", using default value.");
                                    }
                                    break;
                                case "door":
                                    try
                                    {
                                        tier.doorBlocks = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of door in tier's list(tier=" + tier.tier + "\", using default value.");
                                    }
                                    break;
                                case "sponge":
                                    try
                                    {
                                        tier.spongeBlocks = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of sponge in tier's list(tier=" + tier.tier + "\", using default value.");
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
                    XmlAttribute val6 = val.CreateAttribute("water");
                    XmlAttribute val7 = val.CreateAttribute("sponge");
                    XmlAttribute val8 = val.CreateAttribute("door");
                    XmlAttribute val9 = val.CreateAttribute("hammer");
                    val4.Value = tiers[i].tier.ToString();
                    val5.Value = tiers[i].experience.ToString();
                    val6.Value = tiers[i].waterBlocks.ToString();
                    val9.Value = tiers[i].hammer.ToString();
                    val7.Value = tiers[i].spongeBlocks.ToString();
                    val8.Value = tiers[i].doorBlocks.ToString();
                    val3.SetAttributeNode(val4);
                    val3.SetAttributeNode(val5);
                    val3.SetAttributeNode(val6);
                    val3.SetAttributeNode(val9);
                    val3.SetAttributeNode(val8);
                    val3.SetAttributeNode(val7);
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

            public int doorBlocks;

            public int experience;

            public int hammer;

            public int spongeBlocks;
            public int tier;

            public int waterBlocks;
        }
    }
}