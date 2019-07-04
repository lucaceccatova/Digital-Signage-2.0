using System;
using System.Collections.Generic;
using System.Text;
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
                                todo.listaID = (int)reader["lista_ID"];
                                if (reader["Tipo"].ToString().ToLower() == "vid")
                                {
                                    todo.value = type.vid;

                                }
                                else if (reader["Tipo"].ToString().ToLower() == "img")
                                {
                                    todo.value = type.img;
                                }
                                //todo.value = (tipo)reader["Tipo"];
                                todoList.Add(todo);
                            }
                        }

                        reader.Close();
                        // Customer ID is an IDENTITY value from the database.
                        //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                        // Put the Customer ID value into the read-only text box.
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

        public List<Media> GetListaById(int id)
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
                    sqlCommand.CommandText = "select * from media where lista_ID="+id;
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
                                todo.listaID = (int)reader["lista_ID"];
                                if (reader["Tipo"].ToString().ToLower() == "vid")
                                {
                                    todo.value = type.vid;

                                }
                                else if (reader["Tipo"].ToString().ToLower() == "img")
                                {
                                    todo.value = type.img;
                                }
                                //todo.value = (tipo)reader["Tipo"];
                                todoList.Add(todo);
                            }
                        }

                        reader.Close();
                        // Customer ID is an IDENTITY value from the database.
                        //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                        // Put the Customer ID value into the read-only text box.
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

         public List<ListaMedia> GetLista()
            {
                var todoList = new List<ListaMedia>();

                using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
                {
                    // Create a SqlCommand, and identify it as a stored procedure.
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        //sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.CommandText = "GetTodos";
                        //text -- query interna
                        sqlCommand.CommandText = "select * from listaMedia";
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
                                    var todo = new ListaMedia();
                                    todo.ID = (int)reader["ID"];
                                    todo.name = reader["Nome"].ToString();
                                    todo.description = reader["Descrizione"].ToString();
                                    todo.path = reader["Percorso"].ToString();

                                todoList.Add(todo);
                                }
                            }

                            reader.Close();
                            // Customer ID is an IDENTITY value from the database.
                            //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                            // Put the Customer ID value into the read-only text box.
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

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    try
                    {
                        cmd.Parameters.AddWithValue("@medname",med.name);
                        cmd.Parameters.AddWithValue("@meddescription", med.description);
                        cmd.Parameters.AddWithValue("@medcreate_date", med.create_date);
                        if (med.value == type.img )
                        {
                            cmd.Parameters.AddWithValue("@medvalue", "img");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@medvalue", "vid");
                        }
                        
                        cmd.Parameters.AddWithValue("@medlistaID", med.listaID);
                        cmd.Parameters.AddWithValue("@medtimer", med.timer);
                        cmd.Parameters.AddWithValue("@medpath", med.path);
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

        public bool EliminaMedia(int n)
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

        public bool EliminaLista(int n)
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
                    var query = "DELETE FROM dbo.media WHERE lista_ID=" + n + ";";
                    var query2 = "Delete from dbo.listaMedia where ID=" + n + ";";
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
                        catch(Exception)
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

        public List<Media> GetVideos()
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
                                todo.listaID = (int)reader["lista_ID"];
                                todo.value = type.vid;
                                //todo.value = (tipo)reader["Tipo"];
                                todoList.Add(todo);
                            }
                        }

                        reader.Close();
                        // Customer ID is an IDENTITY value from the database.
                        //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                        // Put the Customer ID value into the read-only text box.
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

        public List<Media> GetVideosById(int idVideo)
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
                    sqlCommand.CommandText = "select * from media where Tipo ='vid' AND lista_ID="+idVideo+"";
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
                                todo.listaID = (int)reader["lista_ID"];
                                todo.value = type.vid;
                                //todo.value = (tipo)reader["Tipo"];
                                todoList.Add(todo);
                            }
                        }

                        reader.Close();
                        // Customer ID is an IDENTITY value from the database.
                        //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                        // Put the Customer ID value into the read-only text box.
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

        public Media GetVideoById(int idVideo)
        {
            var todo = new Media();

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.CommandText = "GetTodos";
                    //text -- query interna
                    sqlCommand.CommandText = "select * from media where Tipo ='vid' AND ID=" + idVideo + "";
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
                                
                                todo.id = (int)reader["ID"];
                                todo.name = reader["Nome"].ToString();
                                todo.description = reader["Descrizione"].ToString();
                                todo.create_date = (DateTime)reader["DataCreazione"];
                                todo.timer = (int)reader["Timer"];
                                todo.path = reader["Percorso"].ToString();
                                todo.listaID = (int)reader["lista_ID"];
                                todo.value = type.vid;
                                //todo.value = (tipo)reader["Tipo"];
                              
                            }
                        }

                        reader.Close();
                        // Customer ID is an IDENTITY value from the database.
                        //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                        // Put the Customer ID value into the read-only text box.
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

                return todo;
            }
        }

        public Media GetVideoByName(string name)
        {
            var todo = new Media();

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.CommandText = "GetTodos";
                    //text -- query interna
                    sqlCommand.CommandText = "select * from media where Tipo ='vid' AND Nome='" + name + "'";
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

                                todo.id = (int)reader["ID"];
                                todo.name = reader["Nome"].ToString();
                                todo.description = reader["Descrizione"].ToString();
                                todo.create_date = (DateTime)reader["DataCreazione"];
                                todo.timer = (int)reader["Timer"];
                                todo.path = reader["Percorso"].ToString();
                                todo.listaID = (int)reader["lista_ID"];
                                todo.value = type.vid;
                                //todo.value = (tipo)reader["Tipo"];

                            }
                        }

                        reader.Close();
                        // Customer ID is an IDENTITY value from the database.
                        //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                        // Put the Customer ID value into the read-only text box.
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

                return todo;
            }
        }

        public Media GetVideoByNameId(string name,int id)
        {
            var todo = new Media();

            using (SqlConnection connection = new SqlConnection(_connectionString))  //MANCA GESTIONE ERRORI
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.CommandText = "GetTodos";
                    //text -- query interna
                    sqlCommand.CommandText = "select * from media where Tipo ='vid' AND Nome='" + name + "' AND ID="+id+"";
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

                                todo.id = (int)reader["ID"];
                                todo.name = reader["Nome"].ToString();
                                todo.description = reader["Descrizione"].ToString();
                                todo.create_date = (DateTime)reader["DataCreazione"];
                                todo.timer = (int)reader["Timer"];
                                todo.path = reader["Percorso"].ToString();
                                todo.listaID = (int)reader["lista_ID"];
                                todo.value = type.vid;
                                //todo.value = (tipo)reader["Tipo"];

                            }
                        }

                        reader.Close();
                        // Customer ID is an IDENTITY value from the database.
                        //this.parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;

                        // Put the Customer ID value into the read-only text box.
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

                return todo;
            }
        }

        public Car GetCarsAndTires(int id)
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
                                todo.InvokeName = reader["name"].ToString();
                                todo.brand = reader["brand"].ToString();
                                todo.path = reader["mediaPath"].ToString();
                                //todo.value = (tipo)reader["Tipo"];


                            }
                            reader.Close();
                        }
                        if (todo != null)
                        {
                            List<Tire> tmpTire = new List<Tire>();
                            sqlCommand.CommandText = "select * from tire where FK_car=" + id + "";
                            reader = sqlCommand.ExecuteReader();
                            if (reader.HasRows)
                            {
                                //CONTROLLA 
                                while (reader.Read())
                                {
                                    var tireCar = new Tire();
                                    tireCar.id = (int)reader["id"];
                                    tireCar.model = reader["model"].ToString();
                                    //tireCar.typeValue =(tireType)reader["tireType"]; // --> ENUM 
                                    tireCar.tireType = reader["tireType"].ToString();
                                    tireCar.tirePath = reader["tirePath"].ToString();
                                    tireCar.size =float.Parse(reader["size"].ToString());
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

        public Tire GetTire(int id)
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

    }
}

