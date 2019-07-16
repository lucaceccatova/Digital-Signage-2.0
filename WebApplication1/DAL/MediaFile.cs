using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class MediaFile
    {
        public string name { get; set; }
        public string description { get; set;}
        public int value { get; set; }
        public int timer { get; set; }
        public int listID { get; set; }
        public byte[] file { get; set;}
    }
}
