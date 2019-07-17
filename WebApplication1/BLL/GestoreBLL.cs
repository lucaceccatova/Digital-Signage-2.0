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
                //    med.format = (type)x.format;
                //    med.create_date = x.create_date;
                //    med.path = x.path;
                //    med.ListaID = x.listId;
                //    med.description = x.description;
                //    medias.Add(med);
                //}
                //return medias;
                return medias;
            }
            catch (Exception)
            {
                return medias;
            }
        }

        //public static List<listMedia> GetLista()
        //{
        //    var medias = new List<listMedia>();
        //    try
        //    {
        //        medias = new DB_Access(connectionDB).GetLista(); //MANCA GESTIONE ERRORI

        //        //foreach (listMedia x in mediasDB)
        //        //{
        //        //    var med = new ListaMediaBL();
        //        //    med.ID = x.ID;
        //        //    med.description = x.description;
        //        //    med.path = x.path;
        //        //    med.name = x.name;
        //        //    medias.Add(med);
        //        //}
        //        return medias;
        //    }
        //    catch (Exception)
        //    {
        //        return medias;
        //    }
        //}

        public static List<Media> GetVideosByCategoryId(int id)
        {
            var medias = new List<Media>();
            try
            {
                medias = new DB_Access(connectionDB).GetVideosByCategory(id); //MANCA GESTIONE ERRORI

                //foreach (Media x in mediasDB)
                //{
                //    var med = new MediaBL();
                //    med.id = x.id;
                //    med.name = x.name;
                //    med.timer = x.timer;
                //    med.format = (type)x.format;
                //    med.create_date = x.create_date;
                //    med.path = x.path;
                //    med.ListaID = x.listId;
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

        public static List<listMedia> GetCategories()
        {
            var medias = new List<listMedia>();
            try
            {
                medias = new DB_Access(connectionDB).GetCategories(); //MANCA GESTIONE ERRORI

                //foreach (Media x in mediasDB)
                //{
                //    var med = new MediaBL();
                //    med.id = x.id;
                //    med.name = x.name;
                //    med.timer = x.timer;
                //    med.format = (type)x.format;
                //    med.create_date = x.create_date;
                //    med.path = x.path;
                //    med.ListaID = x.listId;
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
            //media.format = (DAL.type)m.format;
            //media.create_date = m.create_date;
            //media.listId = m.ListaID;

            return new DB_Access(connectionDB).AddMedia(m);
        }

        public static bool AddCategory(listMedia m)
        {
            //var media = new Media();
            //media.name = m.name;
            //media.description = m.description;
            //media.timer = m.timer;
            //media.path = m.path;
            //media.format = (DAL.type)m.format;
            //media.create_date = m.create_date;
            //media.listId = m.ListaID;

            return new DB_Access(connectionDB).AddCategory(m);
        }

        public static bool AddCar(Car auto)
        {
            return new DB_Access(connectionDB).AddCar(auto);
        }

        public static bool AddTire(Tire ruota)
        {
            return new DB_Access(connectionDB).AddTire(ruota);
        }

        //public static bool EliminaSlide(int id)
        //{
        //    return new DB_Access(connectionDB).DeleteMedia(id);
        //}

        //public static bool DeleteCategory(int id)
        //{
        //    return new DB_Access(connectionDB).DeleteCategory(id);
        //}

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

        public static Media GetVideosByName(string name)
        {
            Media media = new Media();
            media = new DB_Access(connectionDB).GetVideoByName(name);
            return media;
        }

        public static Car GetCarAndTires(int id)
        {
            Car auto = new Car();
            auto = new DB_Access(connectionDB).GetCarsAndTires(id);
            return auto;
        }

        public static Tire GetTireById(int id)
        {
            Tire ruota = new Tire();
            ruota = new DB_Access(connectionDB).GetTire(id);
            return ruota;
        }

        public static bool DeleteMedia(int id)
        {
            return new DB_Access(connectionDB).DeleteMedia(id);
        }

        public static bool DeleteCategory(int id)
        {
            return new DB_Access(connectionDB).DeleteCategory(id);
        }

        public static bool DeleteTire(int id)
        {
            return new DB_Access(connectionDB).DeleteTires(id);
        }

        public static bool DeleteCar(int id)
        {
            return new DB_Access(connectionDB).DeleteCar(id);
        }
    }


}
