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
                                todo.listaID = (int)reader["ListaID"];
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
                    sqlCommand.CommandText = "select * from media where ListaID="+id;
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
                                todo.listaID = (int)reader["ListaID"];
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
                        sqlCommand.CommandText = "select * from ListaID";
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
                                    todo.ID = (int)reader["ID_lista"];
                                    todo.path = reader["Foto"].ToString();
                                    todo.description = reader["Descrizione"].ToString();
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
        
    }
}

