using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessNote
{
    class Task
    {
        public double cpu { get; set; }
        public double ram { get; set; }
        public int runtime { get; set; }
        public int id { get; set; }
        public string start { get; set; }
        public string comment { get; set; }
        public string name { get; set; }

        public Task(List<string> table)
        {
            name = table[0];
            id = int.Parse(table[1]);
            cpu = double.Parse(table[2]);
            ram = double.Parse(table[3]);
            runtime = int.Parse(table[4]);
            start = table[5];
            comment = table[6];
        }
    }
}
