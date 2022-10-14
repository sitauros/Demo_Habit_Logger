﻿using Microsoft.Data.Sqlite;

string connectionString = "Data Source=habit_Tracker.db";


CreateDatabase();

void CreateDatabase()
{
    /*Creating a connection passing the connection string as an argument
    This will create the database for you, there's no need to manually create it.
    And no need to use File.Create().*/
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        //Creating the command that will be sent to the database
        using (var tableCmd = connection.CreateCommand())
        {
           
            //Declaring what is that command (in SQL syntax)
            tableCmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS yourHabit (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT,
                    Quantity INTEGER
                    )";

            // Executing the command, which isn't a query, it's not asking to return data from the database.
            tableCmd.ExecuteNonQuery();
        }
        // We don't need to close the connection or the command. The 'using statement' does that for us.
    }

    /* Once we check if the database exists and create it (or not),
    we will call the next method, which will handle the user's input. Your next step is to create this method*/
    GetUserInput();
}

void GetUserInput()
{
    throw new NotImplementedException();
}