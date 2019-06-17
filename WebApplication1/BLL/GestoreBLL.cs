using System;
using DAL;
using System.Collections.Generic;
namespace BLL
{
    public static class GestoreBLL
    {
        public static List<MediaBL> GetMedia()
        {
            var medias = new List<MediaBL>();
            try
            {
                var mediasDB = new DB_Access(@"Server=localhost\SQLEXPRESS02;Database=MediaDB;Trusted_Connection=True;").GetTodos(); //MANCA GESTIONE ERRORI

                foreach (Media x in mediasDB)
                {
                    var med = new MediaBL();
                    med.id = x.id;
                    med.name = x.name;
                    med.timer = x.timer;
                    med.value = (type)x.value;
                    med.create_date = x.create_date;
                    med.path = x.path;
                    med.ListaID = x.listaID;
                    med.description = x.description;
                    medias.Add(med);
                }
                return medias;
            }
            catch(Exception)
            {
                return medias;
            }
        }

        public static List<ListaMediaBL> GetLista()
        {
            var medias = new List<ListaMediaBL>();
            try
            {
                var mediasDB = new DB_Access(@"Server=localhost\SQLEXPRESS02;Database=MediaDB;Trusted_Connection=True;").GetLista(); //MANCA GESTIONE ERRORI

                foreach (ListaMedia x in mediasDB)
                {
                    var med = new ListaMediaBL();
                    med.ID = x.ID;
                    med.description = x.description;
                    med.path = x.path;
                    
                    medias.Add(med);
                }
                return medias;
            }
            catch (Exception)
            {
                return medias;
            }
        }

        public static List<MediaBL> GetListById(int id)
        {
            var medias = new List<MediaBL>();
            try
            {
                var mediasDB = new DB_Access(@"Server=localhost\SQLEXPRESS02;Database=MediaDB;Trusted_Connection=True;").GetListaById(id); //MANCA GESTIONE ERRORI

                foreach (Media x in mediasDB)
                {
                    var med = new MediaBL();
                    med.id = x.id;
                    med.name = x.name;
                    med.timer = x.timer;
                    med.value = (type)x.value;
                    med.create_date = x.create_date;
                    med.path = x.path;
                    med.ListaID = x.listaID;
                    med.description = x.description;
                    medias.Add(med);
                }
                return medias;
            }
            catch (Exception)
            {
                return medias;
            }
        }
    }
}
