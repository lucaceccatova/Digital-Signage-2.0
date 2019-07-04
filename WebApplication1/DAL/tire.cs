using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{

//-------------------------//
    //public enum tireType
    //{
    //    sport,
    //    turismo,
    //    strada,
    //}
//-----------------------//
    public class Tire
    {
        public int id { get; set; }
        public string model { get; set; }

        //public tireType typeValue { get; set; }// --> SISTEMARE IL CAMPO tireType NEL DB PER POTER USARE LA VAR. ENUM  
        public string tireType { get; set; } //--> per il momento uso una variabile stringa per il campo tipo di ruota
        public string tirePath { get; set; }
        public float size { get; set; }
        public float price { get; set; }
        public int FK_car { get; set; }
    }
}
