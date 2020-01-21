using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessNote
{
    class Target
    {
        public float cpu { get; set; }
        public float ram { get; set; }
        public string runtime { get; set; }
        public string id { get; set; }
        public string start { get; set; }
        public string comment { get; set; }
        public string name { get; set; }

        public Target(List<string> table)
        {
            name = table[0];
            //id = int.Parse(table[1]);
            id = table[1];
            cpu = float.Parse(table[2]);
            ram = float.Parse(table[3]);
            runtime = table[4];
            start = table[5];
            comment = table[6];
        }
    }
}
