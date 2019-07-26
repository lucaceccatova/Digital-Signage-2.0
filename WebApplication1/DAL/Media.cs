using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public enum type
    {
        vid,
        img,
    }
    public class Media
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public type format { get; set; }
        public int timer { get; set; }
        public DateTime create_date { get; set; }
        public string path { get; set; }
        public int listid { get; set; }
        public Nullable<int> FK_Car { get; set; }

        public string gifpath { get; set; }

    }
}
