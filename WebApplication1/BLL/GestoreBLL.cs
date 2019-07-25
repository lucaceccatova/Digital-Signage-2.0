using System;
using DAL;
using System.Collections.Generic;
namespace BLL
{
    public static class GestoreBLL
    {
        private static string connectionDB = @" Server = tcp:sd-msdn.database.windows.net;Database=vianima-dev-db;User ID=db_user@sd-msdn.database.windows.net;Password=Password.1!;Trusted_Connection=False;Encrypt=True;";

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

        public static List<Media> GetImages()
        {
            return new DB_Access(connectionDB).GetImages();
        }

        public static List<Media> GetVideosByCar(int idTire)
        {
            return new DB_Access(connectionDB).GetVideosByCar(idTire);
        }

        public static List<Tire> GetTiresByCar(int idCar)
        {
            return new DB_Access(connectionDB).GetTiresByCar(idCar);
        }

//----------------------------------------------------------------------------------------//
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

//-------------------------------------------------------------------------------------//
        //public static List<Media> GetVideosByCategoryId(int id)
        //{
        //    var medias = new List<Media>();
        //    try
        //    {
        //        medias = new DB_Access(connectionDB).GetVideosByCategory(id); //MANCA GESTIONE ERRORI

        //        //foreach (Media x in mediasDB)
        //        //{
        //        //    var med = new MediaBL();
        //        //    med.id = x.id;
        //        //    med.name = x.name;
        //        //    med.timer = x.timer;
        //        //    med.format = (type)x.format;
        //        //    med.create_date = x.create_date;
        //        //    med.path = x.path;
        //        //    med.ListaID = x.listId;
        //        //    med.description = x.description;
        //        //    medias.Add(med);
        //        //}
        //        return medias;
        //    }
        //    catch (Exception)
        //    {
        //        return medias;
        //    }
        //}

        //public static List<listMedia> GetCategories()
        //{
        //    var medias = new List<listMedia>();
        //    try
        //    {
        //        medias = new DB_Access(connectionDB).GetCategories(); //MANCA GESTIONE ERRORI

        //        //foreach (Media x in mediasDB)
        //        //{
        //        //    var med = new MediaBL();
        //        //    med.id = x.id;
        //        //    med.name = x.name;
        //        //    med.timer = x.timer;
        //        //    med.format = (type)x.format;
        //        //    med.create_date = x.create_date;
        //        //    med.path = x.path;
        //        //    med.ListaID = x.listId;
        //        //    med.description = x.description;
        //        //    medias.Add(med);
        //        //}
        //        return medias;
        //    }
        //    catch (Exception)
        //    {
        //        return medias;
        //    }
        //}
        //-------------------------------------------------------------------------------------//


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

        //------------------------------------------------------------//
        //public static bool AddCategory(listMedia m)
        //{
        //    //var media = new Media();
        //    //media.name = m.name;
        //    //media.description = m.description;
        //    //media.timer = m.timer;
        //    //media.path = m.path;
        //    //media.format = (DAL.type)m.format;
        //    //media.create_date = m.create_date;
        //    //media.listId = m.ListaID;

        //    return new DB_Access(connectionDB).AddCategory(m);
        //}
        //------------------------------------------------------------//

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

//----------------------------------------------------------------------------//
        //public static List<Media> GetVideosByCategory(int id)
        //{
        //    List<Media> listavideo = new List<Media>();
        //    listavideo = new DB_Access(connectionDB).GetVideosById(id);
        //    return listavideo;

        //}

        //public static Media GetVideosByName(string name)
        //{
        //    Media media = new Media();
        //    media = new DB_Access(connectionDB).GetVideoByName(name);
        //    return media;
        //}
//---------------------------------------------------------------------------//

        public static Car GetCarAndTires(int id)
        {
            Car auto = new Car();
            auto = new DB_Access(connectionDB).GetCarsAndTires(id);
            return auto;
        }

        public static List<Tire> GetTires(string tipo)
        {
            return new DB_Access(connectionDB).GetTires(tipo);
        }

        public static Tire GetTireById(int id)
        {
            Tire ruota = new Tire();
            ruota = new DB_Access(connectionDB).GetSingleTire(id);
            return ruota;
        }

        public static bool DeleteMedia(int id)
        {
            return new DB_Access(connectionDB).DeleteMedia(id);
        }

//------------------------------------------------------------------------------//
        //public static bool DeleteCategory(int id)
        //{
        //    return new DB_Access(connectionDB).DeleteCategory(id);
        //}
//-----------------------------------------------------------------------------//


        public static bool DeleteTire(int id)
        {
            return new DB_Access(connectionDB).DeleteTires(id);
        }

        public static bool DeleteCar(int id)
        {
            return new DB_Access(connectionDB).DeleteCar(id);
        }

//----------------------------------------------------------------------------//
        //public static bool UpdateCategory(listMedia cat)
        //{
        //    return new DB_Access(connectionDB).UpdateCategory(cat);
        //}
//----------------------------------------------------------------------------//

        public static bool UpdateMedia(Media med)
        {
            return new DB_Access(connectionDB).UpdateMedia(med);
        }

        public static bool UpdateCar(Car car)
        {
            return new DB_Access(connectionDB).UpdateCar(car);
        }

        public static bool UpdateTire(Tire tire)
        {
            return new DB_Access(connectionDB).UpdateTire(tire);
        }

        public static List<string> GetTiresType()
        {
            return new DB_Access(connectionDB).GetTireTypes();
        }

        public static bool TireTypeExist(string tiipo)
        {
            return new DB_Access(connectionDB).TypeExists(tiipo);
        } 

        public static bool CarExists(string brand)
        {
            return new DB_Access(connectionDB).CarExist(brand);
        }

        public static List<string> GetCarModels(string brand)
        {
            return new DB_Access(connectionDB).GetCarModel(brand);
        }

        public static List<Car> GetCars()
        {
            return new DB_Access(connectionDB).GetCars();
        }
    }


}
