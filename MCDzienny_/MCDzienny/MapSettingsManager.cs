using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MCDzienny
{
    // Token: 0x020001AB RID: 427
    public class MapSettingsManager
    {
        // Token: 0x04000661 RID: 1633
        internal List<CommandBlock> commandBlocks;

        // Token: 0x04000660 RID: 1632
        private string settingsPath;

        // Token: 0x06000C46 RID: 3142 RVA: 0x000475D8 File Offset: 0x000457D8
        public MapSettingsManager(string settingsPath)
        {
            Load(settingsPath);
        }

        // Token: 0x06000C47 RID: 3143 RVA: 0x000475E8 File Offset: 0x000457E8
        public void Save()
        {
            Save(settingsPath);
        }

        // Token: 0x06000C48 RID: 3144 RVA: 0x000475F8 File Offset: 0x000457F8
        public void Save(string newPath)
        {
            settingsPath = newPath;
            var xmlDocument = new XmlDocument();
            XmlNode xmlNode = xmlDocument.CreateElement("MapSettings");
            xmlDocument.AppendChild(xmlNode);
            if (commandBlocks != null)
            {
                XmlNode xmlNode2 = xmlDocument.CreateElement("CommandBlocks");
                foreach (var commandBlock in commandBlocks)
                {
                    var xmlElement = xmlDocument.CreateElement("CommandBlock");
                    xmlElement.SetAttribute("x", commandBlock.x.ToString());
                    xmlElement.SetAttribute("y", commandBlock.y.ToString());
                    xmlElement.SetAttribute("z", commandBlock.z.ToString());
                    xmlElement.SetAttribute("block", commandBlock.blockType);
                    if (commandBlock.changeAction.IsExplicit)
                        xmlElement.SetAttribute("onChange", commandBlock.changeAction.Value.ToString());
                    if (commandBlock.commandElements != null)
                        foreach (var commandElement in commandBlock.commandElements)
                        {
                            var xmlElement2 = xmlDocument.CreateElement("Command");
                            if (commandElement.blockTrigger.IsExplicit)
                                xmlElement2.SetAttribute("trigger", commandElement.blockTrigger.ToString());
                            if (commandElement.consoleUse.IsExplicit)
                                xmlElement2.SetAttribute("console", commandElement.consoleUse.ToString());
                            if (commandElement.cooldown.IsExplicit)
                                xmlElement2.SetAttribute("cooldown", commandElement.cooldown.ToString());
                            xmlElement2.InnerText = commandElement.commandString;
                            xmlElement.AppendChild(xmlElement2);
                        }

                    if (commandBlock.actionElements != null)
                        foreach (var actionElement in commandBlock.actionElements)
                        {
                            var xmlElement3 = xmlDocument.CreateElement("Action");
                            if (actionElement.blockTrigger.IsExplicit)
                                xmlElement3.SetAttribute("trigger", actionElement.blockTrigger.ToString());
                            xmlElement3.InnerText = actionElement.actionString;
                            xmlElement.AppendChild(xmlElement3);
                        }

                    if (xmlElement.ChildNodes.Count == 0)
                        throw new XmlException("No commands or actions are assigned to command block.");
                    xmlNode2.AppendChild(xmlElement);
                }

                xmlNode.AppendChild(xmlNode2);
            }

            xmlDocument.AppendChild(xmlNode);
            xmlDocument.Save(settingsPath);
        }

        // Token: 0x06000C49 RID: 3145 RVA: 0x00047908 File Offset: 0x00045B08
        public void Reload()
        {
            Load(settingsPath);
        }

        // Token: 0x06000C4A RID: 3146 RVA: 0x00047918 File Offset: 0x00045B18
        public void Load(string settingsPath)
        {
            this.settingsPath = settingsPath;
            if (!File.Exists(settingsPath))
                using (var fileStream = File.Create(settingsPath))
                {
                    using (var streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("<MapSettings>");
                        streamWriter.Write("</MapSettings>");
                    }
                }

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(settingsPath);
            var documentElement = xmlDocument.DocumentElement;
            var xmlNode = documentElement.SelectSingleNode("CommandBlocks");
            if (xmlNode != null)
            {
                commandBlocks = new List<CommandBlock>();
                foreach (var obj in xmlNode.SelectNodes("CommandBlock"))
                {
                    var xmlNode2 = (XmlNode) obj;
                    var commandBlock = new CommandBlock();
                    commandBlock.x = int.Parse(xmlNode2.Attributes["x"].Value);
                    commandBlock.y = int.Parse(xmlNode2.Attributes["y"].Value);
                    commandBlock.z = int.Parse(xmlNode2.Attributes["z"].Value);
                    commandBlock.blockType = xmlNode2.Attributes["block"].Value;
                    if (xmlNode2.Attributes["onChange"] != null)
                    {
                        commandBlock.changeAction.Value = (ChangeAction) Enum.Parse(typeof(ChangeAction),
                            xmlNode2.Attributes["onChange"].Value);
                        commandBlock.changeAction.IsExplicit = true;
                    }
                    else
                    {
                        commandBlock.changeAction.Value = ChangeAction.Restore;
                        commandBlock.changeAction.IsExplicit = false;
                    }

                    foreach (var obj2 in xmlNode2.SelectNodes("Command"))
                    {
                        var xmlNode3 = (XmlNode) obj2;
                        var commandElement = new CommandElement();
                        if (xmlNode3.Attributes["trigger"] != null)
                        {
                            commandElement.blockTrigger.Value = (BlockTrigger) Enum.Parse(typeof(BlockTrigger),
                                xmlNode3.Attributes["trigger"].Value);
                            commandElement.blockTrigger.IsExplicit = true;
                        }
                        else
                        {
                            if (Block.Walkthrough(Block.Byte(commandBlock.blockType)))
                                commandElement.blockTrigger.Value = BlockTrigger.Walk;
                            else
                                commandElement.blockTrigger.Value = BlockTrigger.Hit;
                            commandElement.blockTrigger.IsExplicit = false;
                        }

                        if (xmlNode3.Attributes["asConsole"] != null)
                        {
                            commandElement.consoleUse.Value = bool.Parse(xmlNode3.Attributes["asConsole"].Value);
                            commandElement.consoleUse.IsExplicit = true;
                        }
                        else
                        {
                            commandElement.consoleUse.Value = false;
                            commandElement.consoleUse.IsExplicit = false;
                        }

                        if (xmlNode3.Attributes["cooldown"] != null)
                        {
                            commandElement.cooldown.Value = float.Parse(xmlNode3.Attributes["cooldown"].Value);
                            commandElement.cooldown.IsExplicit = true;
                        }
                        else
                        {
                            commandElement.cooldown.Value = 1f;
                            commandElement.cooldown.IsExplicit = false;
                        }

                        commandElement.commandString = xmlNode3.InnerText;
                        commandBlock.commandElements.Add(commandElement);
                    }

                    foreach (var obj3 in xmlNode2.SelectNodes("Action"))
                    {
                        var xmlNode4 = (XmlNode) obj3;
                        var actionElement = new ActionElement();
                        if (xmlNode4.Attributes["trigger"] != null)
                        {
                            actionElement.blockTrigger.Value = (BlockTrigger) Enum.Parse(typeof(BlockTrigger),
                                xmlNode4.Attributes["trigger"].Value);
                            actionElement.blockTrigger.IsExplicit = true;
                        }
                        else
                        {
                            if (Block.Walkthrough(Block.Byte(commandBlock.blockType)))
                                actionElement.blockTrigger.Value = BlockTrigger.Walk;
                            else
                                actionElement.blockTrigger.Value = BlockTrigger.Hit;
                            actionElement.blockTrigger.IsExplicit = false;
                        }

                        actionElement.actionString = xmlNode4.InnerText;
                        commandBlock.actionElements.Add(actionElement);
                    }

                    commandBlocks.Add(commandBlock);
                }
            }
        }

        // Token: 0x06000C4B RID: 3147 RVA: 0x00047E3C File Offset: 0x0004603C
        public void DeployBlocks(Level level)
        {
            if (commandBlocks == null) return;
            foreach (var commandBlock in commandBlocks)
            {
                var b = Block.Byte(commandBlock.blockType);
                if (b != 255)
                    level.Blockchange((ushort) commandBlock.x, (ushort) commandBlock.y, (ushort) commandBlock.z, b,
                        true);
            }
        }
    }
}