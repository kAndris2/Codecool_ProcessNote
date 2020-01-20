using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ProcessNote
{
    class DataManager
    {
        public void WriteXML(string filename, Task[] tasks)
        {
            string text = "";
            if (tasks.Length >= 1)
            {
                text += "<Tasks>\n";
                foreach (Task task in tasks)
                {
                    text += $"<{task.name}>\n";
                    text += $"\t<ID>{task.id}</ID>\n" +
                            $"\t<CPU>{task.cpu}</CPU>\n" +
                            $"\t<RAM>{task.ram}</RAM>\n" +
                            $"\t<Runtime>{task.runtime}</Runtime>\n" +
                            $"\t<StartTime>{task.start}</StartTime>\n" +
                            $"\t<Comment>{task.comment}</Comment>";
                    text += $"</{task.name}>\n";
                }
                text += "</Tasks>";
                File.WriteAllText(filename, text);
            }
        }
    }
}
