using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessNote
{
    public class Target
    {
        public float CPU { get; set; }
        public float RAM { get; set; }
        public string Runtime { get; set; }
        public int ID { get; set; }
        public string Start { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public int Threads { get; set; }

        public Target(List<string> table)
        {
            ID = int.Parse(table[0]);
            Name = table[1];
            Start = table[2];
            Runtime = table[3];
            Threads = int.Parse(table[4]);
            RAM = float.Parse(table[5]);
            CPU = float.Parse(table[6]);
            Comment = table[7];
        }

        public Target()
        {
        }
    }
}
