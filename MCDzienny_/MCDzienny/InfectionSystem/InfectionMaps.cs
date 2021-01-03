using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using MCDzienny.Misc;
using MCDzienny.Settings;

namespace MCDzienny.InfectionSystem
{
    // Token: 0x0200009F RID: 159
    public class InfectionMaps
    {
        // Token: 0x04000236 RID: 566
        private const int DefaultCountdownSeconds = 30;

        // Token: 0x04000237 RID: 567
        public static List<InfectionMap> infectionMaps = new List<InfectionMap>();

        // Token: 0x04000238 RID: 568
        private static readonly int DefaultRoundTimeMinutes = InfectionSettings.All.RoundTime;

        // Token: 0x06000434 RID: 1076 RVA: 0x00018354 File Offset: 0x00016554
        public static void SaveInfectionMapsXML()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
            xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
            xmlDocument.AppendChild(
                xmlDocument.CreateComment("For help visit http://mcdzienny.cba.pl and go to the Help section."));
            xmlDocument.AppendChild(xmlDocument.CreateComment("Infection map list"));
            xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
            var xmlElement = xmlDocument.CreateElement("Maps");
            xmlDocument.AppendChild(xmlElement);
            foreach (var infectionMap in infectionMaps)
            {
                var xmlElement2 = xmlDocument.CreateElement("Map");
                var xmlAttribute = xmlDocument.CreateAttribute("name");
                xmlAttribute.Value = infectionMap.Name;
                xmlElement2.SetAttributeNode(xmlAttribute);
                var xmlAttribute2 = xmlDocument.CreateAttribute("author");
                xmlAttribute2.Value = infectionMap.Author;
                xmlElement2.SetAttributeNode(xmlAttribute2);
                var xmlAttribute3 = xmlDocument.CreateAttribute("countdown-seconds");
                xmlAttribute3.Value = infectionMap.CountdownSeconds.ToString();
                xmlElement2.SetAttributeNode(xmlAttribute3);
                var xmlAttribute4 = xmlDocument.CreateAttribute("round-time-minutes");
                xmlAttribute4.Value = infectionMap.RoundTimeMinutes.ToString();
                xmlElement2.SetAttributeNode(xmlAttribute4);
                var xmlAttribute5 = xmlDocument.CreateAttribute("allow-building");
                xmlAttribute5.Value = infectionMap.IsBuildingAllowed.ToString();
                xmlElement2.SetAttributeNode(xmlAttribute5);
                var xmlAttribute6 = xmlDocument.CreateAttribute("allow-pillaring");
                xmlAttribute6.Value = infectionMap.IsPillaringAllowed.ToString();
                xmlElement2.SetAttributeNode(xmlAttribute6);
                xmlElement.AppendChild(xmlElement2);
            }

            xmlDocument.AppendChild(xmlElement);
            xmlDocument.Save("infection/maps.txt");
        }

        // Token: 0x06000435 RID: 1077 RVA: 0x00018550 File Offset: 0x00016750
        public static void LoadInfectionMapsXML()
        {
            FileUtil.CreateIfNotExists("infection/maps.txt");
            infectionMaps.Clear();
            var xmlDocument = new XmlDocument();
            using (var streamReader = new StreamReader("infection/maps.txt"))
            {
                xmlDocument.Load(streamReader);
            }

            var elementsByTagName = xmlDocument.GetElementsByTagName("Map");
            for (var i = 0; i < elementsByTagName.Count; i++)
            {
                var list = new List<InfectionCommands.InfectionCommand>();
                var infectionMap = new InfectionMap();
                var attributes = elementsByTagName[i].Attributes;
                foreach (var obj in attributes)
                {
                    var xmlAttribute = (XmlAttribute) obj;
                    var a = xmlAttribute.Name.ToLower();
                    if (a == "name")
                    {
                        infectionMap.Name = xmlAttribute.Value.ToLower();
                    }
                    else
                    {
                        if (!(a == "countdown-seconds"))
                            if (!(a == "phase1"))
                            {
                                if (!(a == "round-time-minutes"))
                                    if (!(a == "phase2"))
                                    {
                                        if (a == "allow-pillaring")
                                            try
                                            {
                                                infectionMap.IsPillaringAllowed = bool.Parse(xmlAttribute.Value);
                                                continue;
                                            }
                                            catch
                                            {
                                                infectionMap.IsPillaringAllowed = true;
                                                continue;
                                            }

                                        if (a == "author")
                                        {
                                            infectionMap.Author = xmlAttribute.Value;
                                            continue;
                                        }

                                        if (a == "allow-building")
                                            try
                                            {
                                                infectionMap.IsBuildingAllowed = bool.Parse(xmlAttribute.Value);
                                            }
                                            catch
                                            {
                                                infectionMap.IsBuildingAllowed = true;
                                            }

                                        continue;
                                    }

                                try
                                {
                                    infectionMap.RoundTimeMinutes = int.Parse(xmlAttribute.Value);
                                }
                                catch
                                {
                                }

                                if (infectionMap.RoundTimeMinutes <= 0)
                                {
                                    infectionMap.RoundTimeMinutes = DefaultRoundTimeMinutes;
                                }

                                continue;
                            }

                        try
                        {
                            infectionMap.CountdownSeconds = int.Parse(xmlAttribute.Value);
                        }
                        catch
                        {
                        }

                        if (infectionMap.CountdownSeconds <= 0) infectionMap.CountdownSeconds = 30;
                    }
                }

                var childNodes = elementsByTagName[i].ChildNodes;
                foreach (var obj2 in childNodes)
                {
                    var xmlNode = (XmlNode) obj2;
                    if (xmlNode.Name.ToLower() == "command")
                    {
                        var attributes2 = xmlNode.Attributes;
                        var infectionCommand = new InfectionCommands.InfectionCommand();
                        foreach (var obj3 in attributes2)
                        {
                            var xmlAttribute2 = (XmlAttribute) obj3;
                            string a2;
                            if ((a2 = xmlAttribute2.Name.ToLower()) != null)
                            {
                                if (!(a2 == "command"))
                                {
                                    if (a2 == "delay")
                                        try
                                        {
                                            infectionCommand.Delay = int.Parse(xmlAttribute2.Value);
                                        }
                                        catch
                                        {
                                        }
                                }
                                else
                                {
                                    infectionCommand.Command = xmlAttribute2.Value;
                                }
                            }
                        }

                        list.Add(infectionCommand);
                    }
                }

                infectionMap.InfectionCommands = list;
                infectionMaps.Add(infectionMap);
            }

            VerifyMapNames();
        }

        // Token: 0x06000436 RID: 1078 RVA: 0x00018968 File Offset: 0x00016B68
        public static void VerifyMapNames()
        {
            var directoryInfo = new DirectoryInfo(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.Length - 15) +
                "/infection/maps/"));
            var files = directoryInfo.GetFiles();
            var list = new List<string>();
            foreach (var fileInfo in files)
            {
                var a = fileInfo.Name.Substring(fileInfo.Name.LastIndexOf('.'));
                if (a == ".lvl") list.Add(fileInfo.Name.ToLower().Remove(fileInfo.Name.LastIndexOf('.')));
            }

            var list2 = new List<InfectionMap>();
            foreach (var infectionMap in infectionMaps)
                if (!list.Contains(infectionMap.Name))
                    list2.Add(infectionMap);
            foreach (var item in list2)
            {
                infectionMaps.Remove(item);
                Server.s.Log("Map removal.");
            }

            list2.Clear();
            list.Clear();
        }

        // Token: 0x020000A0 RID: 160
        public class InfectionMap
        {
            // Token: 0x06000447 RID: 1095 RVA: 0x00018B94 File Offset: 0x00016D94
            public InfectionMap()
            {
                IsPillaringAllowed = true;
                IsBuildingAllowed = true;
            }

            // Token: 0x06000448 RID: 1096 RVA: 0x00018BAC File Offset: 0x00016DAC
            public InfectionMap(string name, int countdownSeconds, int roundTimeMinutes)
            {
                IsPillaringAllowed = true;
                IsBuildingAllowed = true;
                Name = name;
                CountdownSeconds = countdownSeconds;
                RoundTimeMinutes = roundTimeMinutes;
            }

            // Token: 0x06000449 RID: 1097 RVA: 0x00018BD8 File Offset: 0x00016DD8
            public InfectionMap(string name, int countdownSeconds, int roundTimeMinutes,
                List<InfectionCommands.InfectionCommand> infectionCommands)
            {
                IsPillaringAllowed = true;
                IsBuildingAllowed = true;
                Name = name;
                CountdownSeconds = countdownSeconds;
                RoundTimeMinutes = roundTimeMinutes;
                InfectionCommands = infectionCommands;
            }

            // Token: 0x17000131 RID: 305
            // (get) Token: 0x06000439 RID: 1081 RVA: 0x00018B08 File Offset: 0x00016D08
            // (set) Token: 0x0600043A RID: 1082 RVA: 0x00018B10 File Offset: 0x00016D10
            public string Name { get; set; }

            // Token: 0x17000132 RID: 306
            // (get) Token: 0x0600043B RID: 1083 RVA: 0x00018B1C File Offset: 0x00016D1C
            // (set) Token: 0x0600043C RID: 1084 RVA: 0x00018B24 File Offset: 0x00016D24
            public string Author { get; set; }

            // Token: 0x17000133 RID: 307
            // (get) Token: 0x0600043D RID: 1085 RVA: 0x00018B30 File Offset: 0x00016D30
            // (set) Token: 0x0600043E RID: 1086 RVA: 0x00018B38 File Offset: 0x00016D38
            public int CountdownSeconds { get; set; }

            // Token: 0x17000134 RID: 308
            // (get) Token: 0x0600043F RID: 1087 RVA: 0x00018B44 File Offset: 0x00016D44
            // (set) Token: 0x06000440 RID: 1088 RVA: 0x00018B4C File Offset: 0x00016D4C
            public int RoundTimeMinutes { get; set; }

            // Token: 0x17000135 RID: 309
            // (get) Token: 0x06000441 RID: 1089 RVA: 0x00018B58 File Offset: 0x00016D58
            // (set) Token: 0x06000442 RID: 1090 RVA: 0x00018B60 File Offset: 0x00016D60
            public bool IsPillaringAllowed { get; set; }

            // Token: 0x17000136 RID: 310
            // (get) Token: 0x06000443 RID: 1091 RVA: 0x00018B6C File Offset: 0x00016D6C
            // (set) Token: 0x06000444 RID: 1092 RVA: 0x00018B74 File Offset: 0x00016D74
            public bool IsBuildingAllowed { get; set; }

            // Token: 0x17000137 RID: 311
            // (get) Token: 0x06000445 RID: 1093 RVA: 0x00018B80 File Offset: 0x00016D80
            // (set) Token: 0x06000446 RID: 1094 RVA: 0x00018B88 File Offset: 0x00016D88
            public List<InfectionCommands.InfectionCommand> InfectionCommands { get; set; }
        }
    }
}