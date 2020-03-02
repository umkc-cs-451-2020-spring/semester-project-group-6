using System;
using MySql.Data.MySqlClient;

namespace CommerceApi
{
  public class MySqlDatabase : IDisposable
  {
    public MySqlConnection Connection;

    public MySqlDatabase(string connectionString)
    {
      Connection = new MySqlConnection(connectionString);
      this.Connection.Open();
      Console.Write(Connection);
     
      
      using (var createTableCommand = Connection.CreateCommand())
            {
                createTableCommand.CommandText = @"
                    CREATE TABLE IF NOT EXISTS
                    votes(
                        vote_id SERIAL NOT NULL,
                        time_cast timestamp NOT NULL,
                        candidate CHAR(6) NOT NULL,
                        PRIMARY KEY (vote_id)   
                    )";
                createTableCommand.ExecuteNonQuery();
            }
    }

    public void Dispose()
    {
      Connection.Close();
    }
  }
}