using System;
using DAL;
using System.Collections.Generic;
namespace BLL
{
    public static class GestoreBLL
    {
        private static string connectionDB = @"Server = localhost\SQLEXPRESS02; Database = MediaDB; Trusted_Connection = True;";
        public static List<Media> GetMedia()
        {
            var medias = new List<Media>();
            try
            {
                 medias = new DB_Access(connectionDB).GetTodos(); //MANCA GESTIONE ERRORI

                //foreach (Media x in mediasDB)
                //{
                //    var med = new MediaBL();
                //    med.id = x.id;
                //    med.name = x.name;
                //    med.timer = x.timer;
                //    med.value = (type)x.value;
                //    med.create_date = x.create_date;
                //    med.path = x.path;
                //    med.ListaID = x.listaID;
                //    med.description = x.description;
                //    medias.Add(med);
                //}
                //return medias;
                return medias;
            }
            catch(Exception)
            {
                return medias;
            }
        }

        public static List<ListaMedia> GetLista()
        {
            var medias = new List<ListaMedia>();
            try
            {
                medias = new DB_Access(connectionDB).GetLista(); //MANCA GESTIONE ERRORI

                //foreach (ListaMedia x in mediasDB)
                //{
                //    var med = new ListaMediaBL();
                //    med.ID = x.ID;
                //    med.description = x.description;
                //    med.path = x.path;
                //    med.name = x.name;
                //    medias.Add(med);
                //}
                return medias;
            }
            catch (Exception)
            {
                return medias;
            }
        }

        public static List<Media> GetListById(int id)
        {
            var medias = new List<Media>();
            try
            {
                 medias = new DB_Access(connectionDB).GetListaById(id); //MANCA GESTIONE ERRORI

                //foreach (Media x in mediasDB)
                //{
                //    var med = new MediaBL();
                //    med.id = x.id;
                //    med.name = x.name;
                //    med.timer = x.timer;
                //    med.value = (type)x.value;
                //    med.create_date = x.create_date;
                //    med.path = x.path;
                //    med.ListaID = x.listaID;
                //    med.description = x.description;
                //    medias.Add(med);
                //}
                return medias;
            }
            catch (Exception)
            {
                return medias;
            }
        }

        public static bool AddMedia(Media m)
        {
            //var media = new Media();
            //media.name = m.name;
            //media.description = m.description;
            //media.timer = m.timer;
            //media.path = m.path;
            //media.value = (DAL.type)m.value;
            //media.create_date = m.create_date;
            //media.listaID = m.ListaID;

            return new DB_Access(connectionDB).AddMedia(m);
        }

        public static bool EliminaSlide(int id)
        {
            return new DB_Access(connectionDB).EliminaMedia(id);
        }

        public static bool EliminaLista(int id)
        {
            return new DB_Access(connectionDB).EliminaLista(id);
        }

        public static List<Media> GetAllVideos()
        {
            List<Media> listavideo = new List<Media>();
            listavideo = new DB_Access(connectionDB).GetVideos();
            return listavideo;
        }
        public static List<Media> GetVideosByCategory(int id)
        {
            List<Media> listavideo = new List<Media>();
            listavideo = new DB_Access(connectionDB).GetVideosById(id);
            return listavideo;

        }
    }


}
