using Microsoft.Data.Sqlite;

namespace HabitLogger
{
    internal class Menu
    {
        internal static void PrintMenu()
        {
            Console.WriteLine(@"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Welcome to the Junior Unicorn Habit Logger. 
Discover companies across the globe with ravenous desires for new Junior Unicorns.
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

Enter a number below:

1) View all companies in the Unicorn Pride database.
2) Create a new company in the Unicorn Pride database.
3) Update an entry in the Unicorn Pride database.
4) Delete an entry in the Unicorn Pride database.
0) Quit program.

Your input: ");
            int result;
            bool isValidInput = int.TryParse(Console.ReadLine(), out result);

            while (!isValidInput || result < 0 || result > 4) {
                Console.WriteLine("Invalid input. Please try again.");
                Console.WriteLine("Your input: ");
                isValidInput = int.TryParse(Console.ReadLine(), out result);
            }

            switch (result)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 0:
                    ExitProgram();
                    break;         
            }
        }


        private static void ExitProgram()
        {
            Environment.Exit(1);
        }

        private void CreateDatabase()
        {
            string connectionString = "Data Source=unicornPride.db";
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

    }
}