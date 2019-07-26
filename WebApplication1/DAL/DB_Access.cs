using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class DB_Access
    {
        string _connectionString;

        public DB_Access(string conn)
        {
            _connectionString = conn;
        }

        public List<Media> GetTodos()
        {
            var todoList = new List<Media>();

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.CommandText = "GetTodos";
                    //text -- query interna
                    sqlCommand.CommandText = "select * from media";
                    sqlCommand.Connection = connection;

                    try
                    {
                        connection.Open();


                        // Run the stored procedure.
                        var reader = sqlCommand.ExecuteReader();

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                var todo = new Media();
                                todo.id = (int)reader["ID"];
                                todo.name = reader["Nome"].ToString();
                                todo.description = reader["Descrizione"].ToString();
                                todo.create_date = (DateTime)reader["DataCreazione"];
                                todo.timer = (int)reader["Timer"];
                                todo.path = reader["Percorso"].ToString();
                                todo.listid = (int)reader["lista_ID"];
                                if (reader["Tipo"].ToString().ToLower() == "vid")
                                {
                                    todo.format = type.vid;

                                }
                                else if (reader["Tipo"].ToString().ToLower() == "img")
                                {
                                    todo.format = type.img;
                                }
                                //todo.format = (tipo)reader["Tipo"];
                                todoList.Add(todo);
                            }
                        }

                        reader.Close();
                        // Customer ID is an IDENTITY format from the database.
                        //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                        // Put the Customer ID format into the read-only text box.
                        //this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return todoList;
            }
        }

        public List<Media> GetImages()
        {
            var todoList = new List<Media>();

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select * from media where Tipo ='img'";
                    sqlCommand.Connection = connection;

                    try
                    {
                        connection.Open();


                        // Run the stored procedure.
                        var reader = sqlCommand.ExecuteReader();

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                var todo = new Media();
                                todo.id = (int)reader["ID"];
                                todo.name = reader["Nome"].ToString();
                                todo.description = reader["Descrizione"].ToString();
                                todo.create_date = (DateTime)reader["DataCreazione"];
                                todo.timer = (int)reader["Timer"];
                                todo.path = reader["Percorso"].ToString();
                                todo.listid = (int)reader["lista_ID"];
                                todo.format = type.img;
                                todoList.Add(todo);
                            }
                        }

                        reader.Close();
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return todoList;
            }
        }

        public List<Media> GetVideosByCar(int idTire)
        {
            var todoList = new List<Media>();
            int idCar = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                   
                    sqlCommand.CommandText = "select FK_car from tire where id=" + idTire;
                    sqlCommand.Connection = connection;

                    try
                    {
                        connection.Open();


                        // Run the stored procedure.
                        var reader = sqlCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                idCar = (int)reader["FK_car"];

                            }
                        }
                        reader.Close();
                        if (idCar != 0)
                        {
                            sqlCommand.CommandText = "select * from media where FK_Car=" + idCar+" and Tipo='vid'";
                            var reader2 = sqlCommand.ExecuteReader();
                            while (reader2.Read()&&todoList.Count<=3)
                            {
                                if (reader2.HasRows)
                                {
                                    var todo = new Media();
                                    todo.id = (int)reader2["ID"];
                                    todo.name = reader2["Nome"].ToString();
                                    todo.description = reader2["Descrizione"].ToString();
                                    todo.create_date = (DateTime)reader2["DataCreazione"];
                                    todo.timer = (int)reader2["Timer"];
                                    todo.path = reader2["Percorso"].ToString();
                                    todo.gifpath = reader2["Gifpath"].ToString();
                                    todo.listid = (int)reader2["lista_ID"];
                                    todo.format = type.vid;
                                    todoList.Add(todo);
                                }
                            }
                            reader2.Close();
                        }
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return todoList;
            }
        }

        public List<Tire> GetTiresByCar(int idCar)
        {
            Tire tireCar = new Tire();
            List<Tire> tires = new List<Tire>();
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select * from tire where FK_car='" + idCar + "'";
                    sqlCommand.Connection = connection;

                    try
                    {

                        connection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {

                                tireCar = new Tire();
                                tireCar.id = (int)reader["id"];
                                tireCar.model = reader["model"].ToString();
                                //tireCar.typeValue =(tireType)reader["tireType"]; // --> ENUM 
                                tireCar.tireType = reader["tireType"].ToString();
                                tireCar.tirePath = reader["tirePath"].ToString();
                                tireCar.size = float.Parse(reader["size"].ToString());
                                tireCar.price = float.Parse(reader["price"].ToString());
                                tireCar.FK_car = (int)reader["FK_car"];
                                tires.Add(tireCar);

                            }

                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return tires;
            }
        }
    
    
    //-----------------------------------------------------------------------------------------------------------//
    //public List<Media> GetVideosByCategory(int id)
    //{
    //    var todoList = new List<Media>();

    //    using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
    //    {
    //        // Create a SqlCommand, and identify it as a stored procedure.
    //        using (SqlCommand sqlCommand = new SqlCommand())
    //        {
    //            //sqlCommand.CommandType = CommandType.StoredProcedure;
    //            //sqlCommand.CommandText = "GetTodos";
    //            //text -- query interna
    //            sqlCommand.CommandText = "select * from media where lista_ID=" + id;
    //            sqlCommand.Connection = connection;

    //            try
    //            {
    //                connection.Open();


    //                // Run the stored procedure.
    //                var reader = sqlCommand.ExecuteReader();

    //                if (reader.HasRows)
    //                {

    //                    while (reader.Read())
    //                    {
    //                        var todo = new Media();
    //                        todo.id = (int)reader["ID"];
    //                        todo.name = reader["Nome"].ToString();
    //                        todo.description = reader["Descrizione"].ToString();
    //                        todo.create_date = (DateTime)reader["DataCreazione"];
    //                        todo.timer = (int)reader["Timer"];
    //                        todo.path = reader["Percorso"].ToString();
    //                        todo.listid = (int)reader["lista_ID"];
    //                        if (reader["Tipo"].ToString().ToLower() == "vid")
    //                        {
    //                            todo.format = type.vid;

    //                        }
    //                        else if (reader["Tipo"].ToString().ToLower() == "img")
    //                        {
    //                            todo.format = type.img;
    //                        }
    //                        //todo.format = (tipo)reader["Tipo"];
    //                        todoList.Add(todo);
    //                    }
    //                }

    //                reader.Close();
    //                // Customer ID is an IDENTITY format from the database.
    //                //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

    //                // Put the Customer ID format into the read-only text box.
    //                //this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
    //                connection.Close();
    //            }
    //            catch (SqlException)
    //            {
    //                //Stringa errata
    //            }
    //            catch (ArgumentNullException)
    //            {
    //                //problemi nella tabella del database
    //            }
    //        }

    //        return todoList;
    //    }
    //}

    //    public List<listMedia> GetCategories()
    //    {
    //        var todoList = new List<listMedia>();

    //        using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
    //        {
    //            // Create a SqlCommand, and identify it as a stored procedure.
    //            using (SqlCommand sqlCommand = new SqlCommand())
    //            {
    //                //sqlCommand.CommandType = CommandType.StoredProcedure;
    //                //sqlCommand.CommandText = "GetTodos";
    //                //text -- query interna
    //                sqlCommand.CommandText = "select * from listaMedia";
    //                sqlCommand.Connection = connection;

    //                try
    //                {
    //                    connection.Open();


    //                    // Run the stored procedure.
    //                    var reader = sqlCommand.ExecuteReader();

    //                    if (reader.HasRows)
    //                    {

    //                        while (reader.Read())
    //                        {
    //                            var todo = new listMedia();
    //                            todo.ID = (int)reader["ID"];
    //                            todo.name = reader["Nome"].ToString();
    //                            todo.description = reader["Descrizione"].ToString();
    //                            todo.path = reader["Percorso"].ToString();
    //                            todoList.Add(todo);
    //                        }
    //                    }

    //                    reader.Close();
    //                    // Customer ID is an IDENTITY format from the database.
    //                    //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

    //                    // Put the Customer ID format into the read-only text box.
    //                    //this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
    //                    connection.Close();
    //                }
    //                catch (SqlException)
    //                {
    //                    //Stringa errata
    //                }
    //                catch (ArgumentNullException)
    //                {
    //                    //problemi nella tabella del database
    //                }
    //            }

    //            return todoList;
    //        }
    //    }
        //------------------------------------------------------------------------------------------------------------//

        //-------------------------------------------------------------------------------------------------------------------------------------//
        //public List<listMedia> GetLista()
        //{
        //    var todoList = new List<listMedia>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
        //    {
        //        // Create a SqlCommand, and identify it as a stored procedure.
        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            //sqlCommand.CommandType = CommandType.StoredProcedure;
        //            //sqlCommand.CommandText = "GetTodos";
        //            //text -- query interna
        //            sqlCommand.CommandText = "select * from listaMedia";
        //            sqlCommand.Connection = connection;

        //            try
        //            {
        //                connection.Open();


        //                // Run the stored procedure.
        //                var reader = sqlCommand.ExecuteReader();

        //                if (reader.HasRows)
        //                {

        //                    while (reader.Read())
        //                    {
        //                        var todo = new listMedia();
        //                        todo.ID = (int)reader["ID"];
        //                        todo.name = reader["Nome"].ToString();
        //                        todo.description = reader["Descrizione"].ToString();
        //                        todo.path = reader["Percorso"].ToString();

        //                        todoList.Add(todo);
        //                    }
        //                }

        //                reader.Close();
        //                // Customer ID is an IDENTITY format from the database.
        //                //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

        //                // Put the Customer ID format into the read-only text box.
        //                //this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
        //                connection.Close();
        //            }
        //            catch (SqlException)
        //            {
        //                //Stringa errata
        //            }
        //            catch (ArgumentNullException)
        //            {
        //                //problemi nella tabella del database
        //            }
        //        }

        //        return todoList;
        //    }
        //}

        public bool AddMedia(Media med)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                //connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.CommandText = "GetTodos";
                    //text -- query interna
                    var query = "INSERT INTO dbo.media (Nome, Descrizione, DataCreazione, Tipo, lista_ID, Timer,Percorso) VALUES (@medname, @meddescription, @medcreate_date, @medvalue, @medlistaID, @medtimer, @medpath) ";
                    var query2 = "UPDATE dbo.media SET last_name = @medname WHERE employee_id = 10; ";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    try
                    {
                        cmd.Parameters.AddWithValue("@medname", med.name);
                        cmd.Parameters.AddWithValue("@meddescription", med.description);
                        cmd.Parameters.AddWithValue("@medcreate_date", DateTime.Now); //GIORNO DI NEL QUALE VIENE AGGIUNTO IL FILE 
                        if (med.format == type.img)
                        {
                            cmd.Parameters.AddWithValue("@medvalue", "img");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@medvalue", "vid");
                        }

                        cmd.Parameters.AddWithValue("@medlistaID", med.listid);
                        cmd.Parameters.AddWithValue("@medtimer", med.timer);
                        cmd.Parameters.AddWithValue("@medpath", "path"); //SET A REAL PATH LATER
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public bool DeleteMedia(int n)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                //connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.CommandText = "GetTodos";
                    //text -- query interna
                    var query = "DELETE FROM dbo.media WHERE ID =" + n + "; ";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        //-------------------------------------------//
        //public bool AddCategory(listMedia med)
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
        //    {
        //        // Create a SqlCommand, and identify it as a stored procedure.
        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            var query = "INSERT INTO dbo.listaMedia (Nome, Descrizione, Percorso) VALUES (@catname, @catdescription, @catpath) ";
        //            SqlCommand cmd = new SqlCommand(query, connection);
        //            connection.Open();
        //            try
        //            {
        //                cmd.Parameters.AddWithValue("@catname", med.name);
        //                cmd.Parameters.AddWithValue("@catdescription", med.description);
        //                cmd.Parameters.AddWithValue("@catpath", med.path);
        //                cmd.ExecuteNonQuery();
        //                return true;
        //            }
        //            catch (Exception)
        //            {
        //                return false;
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //}

        //public bool DeleteCategory(int n)
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
        //    {
        //        // Create a SqlCommand, and identify it as a stored procedure.
        //        //connection.Open();
        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            //sqlCommand.CommandType = CommandType.StoredProcedure;
        //            //sqlCommand.CommandText = "GetTodos";
        //            //text -- query interna
        //            var query = "DELETE FROM dbo.media WHERE lista_ID=" + n + ";";
        //            var query2 = "Delete from dbo.listaMedia where ID=" + n + ";";
        //            SqlCommand cmd = new SqlCommand(query, connection);
        //            SqlCommand cmd2 = new SqlCommand(query2, connection);
        //            connection.Open();
        //            try
        //            {
        //                var control = false;
        //                try
        //                {
        //                    cmd.ExecuteNonQuery();
        //                    control = true;
        //                }
        //                catch (Exception)
        //                {
        //                    control = false;
        //                }
        //                if (control == true)
        //                {
        //                    cmd2.ExecuteNonQuery();
        //                }
        //                return true;
        //            }
        //            catch (Exception)
        //            {
        //                return false;
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //}
        //------------------------------------------//


        public bool AddCar(Car auto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    var query = "INSERT INTO dbo.car (name, brand, mediaPath) VALUES (@carname, @cardescription, @carpath) ";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    try
                    {
                        cmd.Parameters.AddWithValue("@carname", auto.invokeName);
                        cmd.Parameters.AddWithValue("@cardescription", auto.brand);
                        cmd.Parameters.AddWithValue("@carpath", auto.path);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public bool AddTire(Tire ruota)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                //connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.CommandText = "GetTodos";
                    //text -- query interna
                    var query = "INSERT INTO dbo.tire (model, tireType, tirePath, size, price, FK_car) VALUES (@model, @tiretype, @path, @size, @price, @fkcar) ";
                    var query2 = "UPDATE dbo.media SET last_name = @medname WHERE employee_id = 10; ";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    try
                    {
                        cmd.Parameters.AddWithValue("@model", ruota.model);
                        cmd.Parameters.AddWithValue("@tiretype", ruota.tireType);
                        cmd.Parameters.AddWithValue("@path", ruota.tirePath);
                        cmd.Parameters.AddWithValue("@size", ruota.size);
                        cmd.Parameters.AddWithValue("@price", ruota.model);
                        cmd.Parameters.AddWithValue("@fkcar", ruota.FK_car);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public List<Media> GetVideos()
        {
            var todoList = new List<Media>();

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                   
                    sqlCommand.CommandText = "select * from media where Tipo ='vid'";
                    sqlCommand.Connection = connection;

                    try
                    {
                        connection.Open();


                        // Run the stored procedure.
                        var reader = sqlCommand.ExecuteReader();

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                var todo = new Media();
                                todo.id = (int)reader["ID"];
                                todo.name = reader["Nome"].ToString();
                                todo.description = reader["Descrizione"].ToString();
                                todo.create_date = (DateTime)reader["DataCreazione"];
                                todo.timer = (int)reader["Timer"];
                                todo.path = reader["Percorso"].ToString();
                                todo.listid = (int)reader["lista_ID"];
                                todo.gifpath = reader["Gifpath"].ToString();
                                if (reader["FK_Car"].ToString().Length == 0)
                                {
                                    todo.FK_Car = null;
                                }
                                else
                                {
                                    todo.FK_Car = (int)reader["FK_Car"];
                                }
                                todo.format = type.vid;
                                
                                todoList.Add(todo);
                            }
                        }

                        reader.Close();
                  
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return todoList;
            }
        }

        //---------------------------------------------//
        //public List<Media> GetVideosById(int idVideo)
        //{
        //    var todoList = new List<Media>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
        //    {
        //        // Create a SqlCommand, and identify it as a stored procedure.
        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            //sqlCommand.CommandType = CommandType.StoredProcedure;
        //            //sqlCommand.CommandText = "GetTodos";
        //            //text -- query interna
        //            sqlCommand.CommandText = "select * from media where Tipo ='vid' AND lista_ID=" + idVideo + "";
        //            sqlCommand.Connection = connection;

        //            try
        //            {
        //                connection.Open();


        //                // Run the stored procedure.
        //                var reader = sqlCommand.ExecuteReader();

        //                if (reader.HasRows)
        //                {

        //                    while (reader.Read())
        //                    {
        //                        var todo = new Media();
        //                        todo.id = (int)reader["ID"];
        //                        todo.name = reader["Nome"].ToString();
        //                        todo.description = reader["Descrizione"].ToString();
        //                        todo.create_date = (DateTime)reader["DataCreazione"];
        //                        todo.timer = (int)reader["Timer"];
        //                        todo.path = reader["Percorso"].ToString();
        //                        todo.listid = (int)reader["lista_ID"];
        //                        todo.format = type.vid;
        //                        //todo.format = (tipo)reader["Tipo"];
        //                        todoList.Add(todo);
        //                    }
        //                }

        //                reader.Close();
        //                // Customer ID is an IDENTITY format from the database.
        //                //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

        //                // Put the Customer ID format into the read-only text box.
        //                //this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
        //                connection.Close();
        //            }
        //            catch (SqlException)
        //            {
        //                //Stringa errata
        //            }
        //            catch (ArgumentNullException)
        //            {
        //                //problemi nella tabella del database
        //            }
        //        }

        //        return todoList;
        //    }
        //}

        //public Media GetVideoById(int idVideo)
        //{
        //    var todo = new Media();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
        //    {
        //        // Create a SqlCommand, and identify it as a stored procedure.
        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            //sqlCommand.CommandType = CommandType.StoredProcedure;
        //            //sqlCommand.CommandText = "GetTodos";
        //            //text -- query interna
        //            sqlCommand.CommandText = "select * from media where Tipo ='vid' AND ID=" + idVideo + "";
        //            sqlCommand.Connection = connection;

        //            try
        //            {
        //                connection.Open();


        //                // Run the stored procedure.
        //                var reader = sqlCommand.ExecuteReader();

        //                if (reader.HasRows)
        //                {

        //                    while (reader.Read())
        //                    {

        //                        todo.id = (int)reader["ID"];
        //                        todo.name = reader["Nome"].ToString();
        //                        todo.description = reader["Descrizione"].ToString();
        //                        todo.create_date = (DateTime)reader["DataCreazione"];
        //                        todo.timer = (int)reader["Timer"];
        //                        todo.path = reader["Percorso"].ToString();
        //                        todo.listid = (int)reader["lista_ID"];
        //                        todo.format = type.vid;
        //                        //todo.format = (tipo)reader["Tipo"];

        //                    }
        //                }

        //                reader.Close();
        //                // Customer ID is an IDENTITY format from the database.
        //                //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

        //                // Put the Customer ID format into the read-only text box.
        //                //this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
        //                connection.Close();
        //            }
        //            catch (SqlException)
        //            {
        //                //Stringa errata
        //            }
        //            catch (ArgumentNullException)
        //            {
        //                //problemi nella tabella del database
        //            }
        //        }

        //        return todo;
        //    }
        //}

        //public Media GetVideoByName(string name)
        //{
        //    var todo = new Media();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
        //    {
        //        // Create a SqlCommand, and identify it as a stored procedure.
        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            //sqlCommand.CommandType = CommandType.StoredProcedure;
        //            //sqlCommand.CommandText = "GetTodos";
        //            //text -- query interna
        //            sqlCommand.CommandText = "select * from media where Tipo ='vid' AND Nome='" + name + "'";
        //            sqlCommand.Connection = connection;

        //            try
        //            {
        //                connection.Open();


        //                // Run the stored procedure.
        //                var reader = sqlCommand.ExecuteReader();

        //                if (reader.HasRows)
        //                {

        //                    while (reader.Read())
        //                    {

        //                        todo.id = (int)reader["ID"];
        //                        todo.name = reader["Nome"].ToString();
        //                        todo.description = reader["Descrizione"].ToString();
        //                        todo.create_date = (DateTime)reader["DataCreazione"];
        //                        todo.timer = (int)reader["Timer"];
        //                        todo.path = reader["Percorso"].ToString();
        //                        todo.listid = (int)reader["lista_ID"];
        //                        todo.format = type.vid;
        //                        //todo.format = (tipo)reader["Tipo"];

        //                    }
        //                }

        //                reader.Close();
        //                // Customer ID is an IDENTITY format from the database.
        //                //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

        //                // Put the Customer ID format into the read-only text box.
        //                //this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
        //                connection.Close();
        //            }
        //            catch (SqlException)
        //            {
        //                //Stringa errata
        //            }
        //            catch (ArgumentNullException)
        //            {
        //                //problemi nella tabella del database
        //            }
        //        }

        //        return todo;
        //    }
        //} 

        //public Media GetVideoByNameId(string name, int id)
        //{
        //    var todo = new Media();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
        //    {
        //        // Create a SqlCommand, and identify it as a stored procedure.
        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            //sqlCommand.CommandType = CommandType.StoredProcedure;
        //            //sqlCommand.CommandText = "GetTodos";
        //            //text -- query interna
        //            sqlCommand.CommandText = "select * from media where Tipo ='vid' AND Nome='" + name + "' AND ID=" + id + "";
        //            sqlCommand.Connection = connection;

        //            try
        //            {
        //                connection.Open();


        //                // Run the stored procedure.
        //                var reader = sqlCommand.ExecuteReader();

        //                if (reader.HasRows)
        //                {

        //                    while (reader.Read())
        //                    {

        //                        todo.id = (int)reader["ID"];
        //                        todo.name = reader["Nome"].ToString();
        //                        todo.description = reader["Descrizione"].ToString();
        //                        todo.create_date = (DateTime)reader["DataCreazione"];
        //                        todo.timer = (int)reader["Timer"];
        //                        todo.path = reader["Percorso"].ToString();
        //                        todo.listid = (int)reader["lista_ID"];
        //                        todo.format = type.vid;
        //                        //todo.format = (tipo)reader["Tipo"];

        //                    }
        //                }

        //                reader.Close();
        //                // Customer ID is an IDENTITY format from the database.
        //                //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

        //                // Put the Customer ID format into the read-only text box.
        //                //this.txtCustomerID.Text = Convert.ToString(parsedCustomerID);
        //                connection.Close();
        //            }
        //            catch (SqlException)
        //            {
        //                //Stringa errata
        //            }
        //            catch (ArgumentNullException)
        //            {
        //                //problemi nella tabella del database
        //            }
        //        }

        //        return todo;
        //    }
        //}
        //-----------------------------------------------//


        public Car GetCarsAndTires(int id) //VEDERE CON CHE VALORI FILTRARE LE RUOTE 
        {
            var todo = new Car();

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select * from car where id=" + id + "";
                    sqlCommand.Connection = connection;

                    try
                    {
                        connection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {

                                todo.id = (int)reader["id"];
                                todo.invokeName = reader["name"].ToString();
                                todo.brand = reader["brand"].ToString();
                                todo.path = reader["mediaPath"].ToString();
                                //todo.format = (tipo)reader["Tipo"];


                            }
                            reader.Close();
                        }
                        if (todo != null)
                        {
                            List<Tire> tmpTire = new List<Tire>();
                            sqlCommand.CommandText = "select * from tire where FK_car=" + id + "";
                            //sqlCommand.CommandText = "select * from tire where FK_car=" + id + " and tireType='"+tipo+"'";
                            reader = sqlCommand.ExecuteReader();
                            if (reader.HasRows)
                            {
                                //CONTROLLA 
                                while (reader.Read()&&tmpTire.Count<=3)
                                {
                                    var tireCar = new Tire();
                                    tireCar.id = (int)reader["id"];
                                    tireCar.model = reader["model"].ToString();
                                    //tireCar.typeValue =(tireType)reader["tireType"]; // --> ENUM 
                                    tireCar.tireType = reader["tireType"].ToString();
                                    tireCar.tirePath = reader["tirePath"].ToString();
                                    tireCar.size = float.Parse(reader["size"].ToString());
                                    tireCar.price = float.Parse(reader["price"].ToString());
                                    tireCar.FK_car = (int)reader["FK_car"];
                                    tmpTire.Add(tireCar);

                                }

                            }
                            todo.tires = tmpTire;
                            reader.Close();
                            connection.Close();
                        }
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return todo;
            }
        }

        public Tire GetSingleTire(int id)
        {
            Tire tireCar = new Tire();
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select * from tire where id=" + id + "";
                    sqlCommand.Connection = connection;

                    try
                    {

                        connection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {

                                tireCar = new Tire();
                                tireCar.id = (int)reader["id"];
                                tireCar.model = reader["model"].ToString();
                                //tireCar.typeValue =(tireType)reader["tireType"]; // --> ENUM 
                                tireCar.tireType = reader["tireType"].ToString();
                                tireCar.tirePath = reader["tirePath"].ToString();
                                tireCar.size = float.Parse(reader["size"].ToString());
                                tireCar.price = float.Parse(reader["price"].ToString());
                                tireCar.FK_car = (int)reader["FK_car"];


                            }

                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return tireCar;
            }
        }

        public List<Tire> GetTires(string tipo) 
        {
            Tire tireCar = new Tire();
            List<Tire> tires = new List<Tire>();
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select * from tire where tireType='" + tipo +"'";
                    sqlCommand.Connection = connection;

                    try
                    {

                        connection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read() && tires.Count <= 3)
                            {

                                tireCar = new Tire();
                                tireCar.id = (int)reader["id"];
                                tireCar.model = reader["model"].ToString();
                                //tireCar.typeValue =(tireType)reader["tireType"]; // --> ENUM 
                                tireCar.tireType = reader["tireType"].ToString();
                                tireCar.tirePath = reader["tirePath"].ToString();
                                tireCar.size = float.Parse(reader["size"].ToString());
                                tireCar.price = float.Parse(reader["price"].ToString());
                                tireCar.FK_car = (int)reader["FK_car"];
                                tires.Add(tireCar);

                            }

                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                      
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return tires;
            }
        }

        public bool TypeExists(string tipo)
        {
            bool exist;
            
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select * from tire where tireType='" + tipo + "'";
                    sqlCommand.Connection = connection;

                    try
                    {

                        connection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            exist=true;
                        }
                        else
                        {
                            exist=false;
                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        exist = false;
                    }
                    catch (ArgumentNullException)
                    {
                        exist = false;
                        //problemi nella tabella del database
                    }
                }

                return exist;
            }
        }
        public bool DeleteTires(int id)
        {
                using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
                {
                    // Create a SqlCommand, and identify it as a stored procedure.
                    //connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        //sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.CommandText = "GetTodos";
                        //text -- query interna
                        var query = "DELETE FROM dbo.tire WHERE id =" + id + "; ";

                        SqlCommand cmd = new SqlCommand(query, connection);
                        connection.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }

        public bool DeleteCar(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                //connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.CommandText = "GetTodos";
                    //text -- query interna
                    var query = "DELETE FROM dbo.tire WHERE FK_car=" + id + ";";
                    var query2 = "Delete from dbo.car where id=" + id + ";";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlCommand cmd2 = new SqlCommand(query2, connection);
                    connection.Open();
                    try
                    {
                        var control = false;
                        try
                        {
                            cmd.ExecuteNonQuery();
                            control = true;
                        }
                        catch (Exception)
                        {
                            control = false;
                        }
                        if (control == true)
                        {
                            cmd2.ExecuteNonQuery();
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        //--------------------------------------------------//
        //public bool UpdateCategory(listMedia cat)
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
        //    {
        //        // Create a SqlCommand, and identify it as a stored procedure.
        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            var query = "update dbo.listaMedia set Nome = '"+cat.name+"', Descrizione = '"+cat.description+"', Percorso = '"+cat.path+"' where ID='"+cat.ID+"'";
        //            SqlCommand cmd = new SqlCommand(query, connection);
        //            connection.Open();
        //            try
        //            {
        //                cmd.ExecuteNonQuery();
        //                return true;
        //            }
        //            catch (Exception)
        //            {
        //                return false;
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //}
        //--------------------------------------------------//

        public bool UpdateMedia(Media med)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    var query = "update dbo.media set Nome = '" + med.name + "', Descrizione = '" + med.description + "', Percorso = '" + med.path + "',Tipo = '"+med.format.ToString()+"', Timer = '"+med.timer+"', lista_ID = '"+med.listid+"' where ID='" + med.id + "'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public bool UpdateCar(Car car)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    var query = "update dbo.media set name = '" + car.invokeName + "', brand = '" + car.brand + "', Percorso = '" + car.path + "', where ID='" + car.id + "'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public bool UpdateTire(Tire ruota)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    var query = "update dbo.media set model = '" + ruota.model + "', tireType = '" + ruota.tireType + "', Percorso = '" + ruota.tirePath + "', size = '" + ruota.size + "', price = '" + ruota.price + "', FK_car = '" + ruota.FK_car + "' where id='" + ruota.id + "'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public List<string> GetTireTypes()
        {
            
            List<string> tires = new List<string>();
            
            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select tireType from tire";
                    sqlCommand.Connection = connection;

                    try
                    {

                        connection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                if(!tires.Contains(reader["tireType"].ToString())){
                                    tires.Add(reader["tireType"].ToString());
                                }
                            }

                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return tires;
            }
        }

        public bool CarExist(string brand)
        {
            bool exist;

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select * from car where brand='" + brand.ToUpper() + "'";
                    sqlCommand.Connection = connection;

                    try
                    {

                        connection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            exist = true;
                        }
                        else
                        {
                            exist = false;
                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        exist = false;
                    }
                    catch (ArgumentNullException)
                    {
                        exist = false;
                        //problemi nella tabella del database
                    }
                }

                return exist;
            }
        }

        public List<string> GetCarModel(string brand)
        {
            List<string> models = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select name from car where brand = '"+brand.ToUpper()+"'";
                    sqlCommand.Connection = connection;

                    try
                    {

                        connection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                if (!models.Contains(reader["name"].ToString()))
                                {
                                    models.Add(reader["name"].ToString());
                                }
                            }

                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return models;
            }


        }

        public List<Car> GetCars()
        {
            List<Car> cars = new List<Car>();

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select name,brand from car";
                    sqlCommand.Connection = connection;

                    try
                    {

                        connection.Open();
                        var reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Car car = new Car();
                                car.brand = reader["brand"].ToString();
                                car.invokeName = reader["name"].ToString();
                                cars.Add(car);
                            }

                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (SqlException)
                    {
                        //Stringa errata
                    }
                    catch (ArgumentNullException)
                    {
                        //problemi nella tabella del database
                    }
                }

                return cars;
            }
        }
    }

}



