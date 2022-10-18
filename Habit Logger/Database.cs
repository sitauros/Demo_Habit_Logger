using Microsoft.Data.Sqlite;

namespace HabitLogger
{
    internal class Database
    {
        private readonly string connectionString;

        internal Database(string connectionString)
        {
           this.connectionString = connectionString;
        }

        internal void CreateDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var tableCmd = connection.CreateCommand())
                {
                    tableCmd.CommandText =  @"CREATE TABLE IF NOT EXISTS JuniorUnicornCompany (
                                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                                Name TEXT,
                                                DesiredSkill TEXT,
                                                YearsOfExp INTEGER,
                                                Perk TEXT   )";
                    tableCmd.ExecuteNonQuery();
                }
            }
        }
    }
}