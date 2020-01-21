﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace ProcessNote
{
    class DataManager
    {
        public void WriteXML(string filename, List<Target> tasks)
        {
            string text = "";
            if (tasks.Count >= 1)
            {
                text += "<Tasks>\n";
                foreach (Target task in tasks)
                {
                    text += $"\t<{task.name}>\n";
                    text += $"\t\t<ID>{task.id}</ID>\n" +
                            $"\t\t<CPU>{task.cpu}</CPU>\n" +
                            $"\t\t<RAM>{task.ram}</RAM>\n" +
                            $"\t\t<Runtime>{task.runtime}</Runtime>\n" +
                            $"\t\t<StartTime>{task.start}</StartTime>\n" +
                            $"\t\t<Comment>{task.comment}</Comment>\n";
                    text += $"\t</{task.name}>\n";
                }
                text += "</Tasks>";
                File.WriteAllText(filename, text);
            }
        }
        public List<string> ReadXML(string filename)
        {
            XmlTextReader reader = new XmlTextReader(filename);
            List<string> properties = new List<string>();
            
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        break;


                    case XmlNodeType.Text:
                        properties.Add(reader.Value);


                        break;
                    case XmlNodeType.EndElement:
                        break;
                }
            }
            return properties;
        }
    }
}
