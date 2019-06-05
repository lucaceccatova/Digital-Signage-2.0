using System;

namespace DAL
{
    public enum tipo
    {
        vid,
        img,
    }
    public class Media
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descrizione { get; set; }
        public tipo value { get; set; }
        public int durata { get; set; }
        public DateTime data { get; set; }
        public string path { get; set; }
       

     
    }
}
