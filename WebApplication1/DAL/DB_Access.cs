using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DAL
{
    public class DB_Access
{ 
     string _connectionString;
    public DB_Access()
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
                        todo.name = reader["nome"].ToString();
                        todo.description = reader["descrizione"].ToString();
                        todo.create_date = (DateTime)reader["dataCreazione"];
                        todo.timer = (int)reader["durata"];
                        todo.path = reader["percorso"].ToString();
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
        }

        return todoList;
    }
}
}

