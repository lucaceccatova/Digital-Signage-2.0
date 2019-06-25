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
                    med.name = x.name;
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

        public static bool AddMedia(MediaBL m)
        {
            var media = new Media();
            media.name = m.name;
            media.description = m.description;
            media.timer = m.timer;
            media.path = m.path;
            media.value = (DAL.type)m.value;
            media.create_date = m.create_date;
            media.listaID = m.ListaID;
            return new DB_Access(@"Server=localhost\SQLEXPRESS02;Database=MediaDB;Trusted_Connection=True;").AddMedia(media);
        }

        public static bool EliminaSlide(int id)
        {
            return new DB_Access(@"Server = localhost\SQLEXPRESS02; Database = MediaDB; Trusted_Connection = True;").EliminaMedia(id);
        }

        public static bool EliminaLista(int id)
        {
            return new DB_Access(@"Server = localhost\SQLEXPRESS02; Database = MediaDB; Trusted_Connection = True;").EliminaLista(id);
        }
    }


}
