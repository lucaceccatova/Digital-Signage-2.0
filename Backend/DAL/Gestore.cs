using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class Gestore
    {
        string _connectionString;
        public Gestore()
        {
            _connectionString = @"Server=localhost\SQLEXPRESS02;Database=MediaDB;Trusted_Connection=True;";
        }

        public List<Media> GetTodos()
        {
            var todoList = new List<Media>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Create a SqlCommand, and identify it as a stored procedure.
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.CommandText = "GetTodos";
                    //text -- query interna
                    sqlCommand.CommandText = "select * from Media";
                    sqlCommand.Connection = connection;
                    

                    connection.Open();

                    // Run the stored procedure.
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            var todo = new Media();
                            todo.id = (int)reader["Id"];
                            todo.nome = reader["nome"].ToString();
                            todo.descrizione = reader["descrizione"].ToString();
                            todo.data = (DateTime)reader["dataCreazione"];
                            todo.durata = (int)reader["durata"];
                            todo.path = reader["percorso"].ToString();
                            if (reader["Tipo"].ToString().ToLower() == "vid")
                            {
                                todo.value = tipo.vid;

                            }
                            else if (reader["Tipo"].ToString().ToLower() == "img")
                            {
                                todo.value = tipo.img;
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
            }

            return todoList;
        }
    }
}
