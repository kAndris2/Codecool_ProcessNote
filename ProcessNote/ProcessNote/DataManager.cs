using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ProcessNote
{
    class DataManager
    {
        public void XmlWriter(string filename, Target[] tasks)
        {
            XmlSerializer writer = new XmlSerializer(typeof(Target[]));

            using (TextWriter writerfinal = new StreamWriter(filename))
            {
                writer.Serialize(writerfinal, tasks);
            }
        }
        public List<Target> XmlReader(string filename)
        {
            XmlSerializer reader = new XmlSerializer(typeof(List<Target>));
            List<Target> i;
            using (FileStream readfile = File.OpenRead(filename))
            {
                i = (List<Target>)reader.Deserialize(readfile);
            }
            return i;
        }

    }
}
