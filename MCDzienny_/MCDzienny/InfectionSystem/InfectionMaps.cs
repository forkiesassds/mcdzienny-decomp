using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using MCDzienny.Misc;
using MCDzienny.Settings;

namespace MCDzienny.InfectionSystem
{
    public class InfectionMaps
    {

        const int DefaultCountdownSeconds = 30;

        public static List<InfectionMap> infectionMaps = new List<InfectionMap>();

        static readonly int DefaultRoundTimeMinutes = InfectionSettings.All.RoundTime;

        public static void SaveInfectionMapsXML()
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Expected O, but got Unknown
            XmlDocument val = new XmlDocument();
            val.AppendChild(val.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
            val.AppendChild(val.CreateWhitespace("\r\n"));
            val.AppendChild(val.CreateComment("For help visit http://mcdzienny.cba.pl and go to the Help section."));
            val.AppendChild(val.CreateComment("Infection map list"));
            val.AppendChild(val.CreateWhitespace("\r\n"));
            XmlElement val2 = val.CreateElement("Maps");
            val.AppendChild(val2);
            foreach (InfectionMap infectionMap in infectionMaps)
            {
                XmlElement val3 = val.CreateElement("Map");
                XmlAttribute val4 = val.CreateAttribute("name");
                val4.Value = infectionMap.Name;
                val3.SetAttributeNode(val4);
                XmlAttribute val5 = val.CreateAttribute("author");
                val5.Value = infectionMap.Author;
                val3.SetAttributeNode(val5);
                XmlAttribute val6 = val.CreateAttribute("countdown-seconds");
                val6.Value = infectionMap.CountdownSeconds.ToString();
                val3.SetAttributeNode(val6);
                XmlAttribute val7 = val.CreateAttribute("round-time-minutes");
                val7.Value = infectionMap.RoundTimeMinutes.ToString();
                val3.SetAttributeNode(val7);
                XmlAttribute val8 = val.CreateAttribute("allow-building");
                val8.Value = infectionMap.IsBuildingAllowed.ToString();
                val3.SetAttributeNode(val8);
                XmlAttribute val9 = val.CreateAttribute("allow-pillaring");
                val9.Value = infectionMap.IsPillaringAllowed.ToString();
                val3.SetAttributeNode(val9);
                val2.AppendChild(val3);
            }
            val.AppendChild(val2);
            val.Save("infection/maps.txt");
        }

        public static void LoadInfectionMapsXML()
        {
            //IL_0015: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Expected O, but got Unknown
            //IL_007d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0084: Expected O, but got Unknown
            //IL_025a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0261: Expected O, but got Unknown
            //IL_0216: Unknown result type (might be due to invalid IL or missing references)
            //IL_021d: Expected O, but got Unknown
            FileUtil.CreateIfNotExists("infection/maps.txt");
            infectionMaps.Clear();
            XmlDocument val = new XmlDocument();
            using (StreamReader streamReader = new StreamReader("infection/maps.txt"))
            {
                val.Load(streamReader);
            }
            XmlNodeList elementsByTagName = val.GetElementsByTagName("Map");
            for (int i = 0; i < elementsByTagName.Count; i++)
            {
                var list = new List<InfectionCommands.InfectionCommand>();
                InfectionMap infectionMap = new InfectionMap();
                XmlAttributeCollection attributes = elementsByTagName[i].Attributes;
                foreach (XmlAttribute item in attributes)
                {
                    XmlAttribute val2 = item;
                    switch (val2.Name.ToLower())
                    {
                        case "name":
                            infectionMap.Name = val2.Value.ToLower();
                            break;
                        case "countdown-seconds":
                        case "phase1":
                            try
                            {
                                infectionMap.CountdownSeconds = int.Parse(val2.Value);
                            }
                            catch {}
                            if (infectionMap.CountdownSeconds <= 0)
                            {
                                infectionMap.CountdownSeconds = 30;
                            }
                            break;
                        case "round-time-minutes":
                        case "phase2":
                            try
                            {
                                infectionMap.RoundTimeMinutes = int.Parse(val2.Value);
                            }
                            catch {}
                            if (infectionMap.RoundTimeMinutes <= 0)
                            {
                                infectionMap.RoundTimeMinutes = DefaultRoundTimeMinutes;
                            }
                            break;
                        case "allow-pillaring":
                            try
                            {
                                infectionMap.IsPillaringAllowed = bool.Parse(val2.Value);
                            }
                            catch
                            {
                                infectionMap.IsPillaringAllowed = true;
                            }
                            break;
                        case "author":
                            infectionMap.Author = val2.Value;
                            break;
                        case "allow-building":
                            try
                            {
                                infectionMap.IsBuildingAllowed = bool.Parse(val2.Value);
                            }
                            catch
                            {
                                infectionMap.IsBuildingAllowed = true;
                            }
                            break;
                    }
                }
                XmlNodeList childNodes = elementsByTagName[i].ChildNodes;
                foreach (XmlNode item2 in childNodes)
                {
                    XmlNode val3 = item2;
                    if (!(val3.Name.ToLower() == "command"))
                    {
                        continue;
                    }
                    XmlAttributeCollection attributes2 = val3.Attributes;
                    InfectionCommands.InfectionCommand infectionCommand = new InfectionCommands.InfectionCommand();
                    foreach (XmlAttribute item3 in attributes2)
                    {
                        XmlAttribute val4 = item3;
                        switch (val4.Name.ToLower())
                        {
                            case "command":
                                infectionCommand.Command = val4.Value;
                                break;
                            case "delay":
                                try
                                {
                                    infectionCommand.Delay = int.Parse(val4.Value);
                                }
                                catch {}
                                break;
                        }
                    }
                    list.Add(infectionCommand);
                }
                infectionMap.InfectionCommands = list;
                infectionMaps.Add(infectionMap);
            }
            VerifyMapNames();
        }

        public static void VerifyMapNames()
        {
            DirectoryInfo directoryInfo =
                new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.Length - 15) +
                                                        "/infection/maps/"));
            FileInfo[] files = directoryInfo.GetFiles();
            var list = new List<string>();
            FileInfo[] array = files;
            foreach (FileInfo fileInfo in array)
            {
                string text = fileInfo.Name.Substring(fileInfo.Name.LastIndexOf('.'));
                if (text == ".lvl")
                {
                    list.Add(fileInfo.Name.ToLower().Remove(fileInfo.Name.LastIndexOf('.')));
                }
            }
            var list2 = new List<InfectionMap>();
            foreach (InfectionMap infectionMap in infectionMaps)
            {
                if (!list.Contains(infectionMap.Name))
                {
                    list2.Add(infectionMap);
                }
            }
            foreach (InfectionMap item in list2)
            {
                infectionMaps.Remove(item);
                Server.s.Log("Map removal.");
            }
            list2.Clear();
            list.Clear();
        }

        public class InfectionMap
        {

            public InfectionMap()
            {
                IsPillaringAllowed = true;
                IsBuildingAllowed = true;
            }

            public InfectionMap(string name, int countdownSeconds, int roundTimeMinutes)
            {
                IsPillaringAllowed = true;
                IsBuildingAllowed = true;
                Name = name;
                CountdownSeconds = countdownSeconds;
                RoundTimeMinutes = roundTimeMinutes;
            }

            public InfectionMap(string name, int countdownSeconds, int roundTimeMinutes, List<InfectionCommands.InfectionCommand> infectionCommands)
            {
                IsPillaringAllowed = true;
                IsBuildingAllowed = true;
                Name = name;
                CountdownSeconds = countdownSeconds;
                RoundTimeMinutes = roundTimeMinutes;
                InfectionCommands = infectionCommands;
            }
            public string Name { get; set; }

            public string Author { get; set; }

            public int CountdownSeconds { get; set; }

            public int RoundTimeMinutes { get; set; }

            public bool IsPillaringAllowed { get; set; }

            public bool IsBuildingAllowed { get; set; }

            public List<InfectionCommands.InfectionCommand> InfectionCommands { get; set; }
        }
    }
}