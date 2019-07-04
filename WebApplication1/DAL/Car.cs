using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Car
    {
        public int id { get; set; }
        public string InvokeName { get; set; }
        public string brand { get; set; }
        public string path { get; set; }
        public List<Tire> tires { get; set; }
    }
}
