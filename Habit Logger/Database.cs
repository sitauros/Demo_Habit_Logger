using System.Data;
using Microsoft.Data.Sqlite;

namespace HabitLogger
{
    internal static class Database
    {
        internal static string? ConnectionString { get; set; }

        internal static void CreateDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS JuniorUnicornFirms (
                        ID              INTEGER     PRIMARY KEY AUTOINCREMENT NOT NULL,
                        Name            TEXT        NOT NULL,
                        DesiredSkill    TEXT        NOT NULL,
                        YearsOfExp      INTEGER     NOT NULL,
                        Perk            TEXT        NOT NULL 
                    )";
            command.ExecuteNonQuery();
        }

        internal static int GetRecordCount()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT COUNT(*) FROM JuniorUnicornFirms
                    ";

            int count = Convert.ToInt32((Int64?)command.ExecuteScalar());

            return count;
        }

        internal static DataTable RetrievePageBeforeID(int ID_offset)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT * FROM JuniorUnicornFirms
                    WHERE ID < $ID_offset
                    ORDER BY ID DESC
                    LIMIT 5
                    ";
            command.Parameters.AddWithValue("ID_offset", ID_offset);

            using SqliteDataReader reader = command.ExecuteReader();
            // Rows returned are in descending order and require sorting
            DataTable resultSet = new DataTable();
            resultSet.Load(reader);
            resultSet.DefaultView.Sort = "ID ASC";
            resultSet = resultSet.DefaultView.ToTable();

            return resultSet;
        }

        internal static DataTable RetrievePageAfterID(int ID_offset)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT * FROM JuniorUnicornFirms
                    WHERE ID > $ID_offset
                    ORDER BY ID ASC
                    LIMIT 5;
                    ";
            command.Parameters.AddWithValue("ID_offset", ID_offset);
            
            using SqliteDataReader reader = command.ExecuteReader();

            DataTable resultSet = new DataTable();
            resultSet.Load(reader);

            return resultSet;
        }

        internal static void AddNewCompany(string name, string skill, int yearsOfExp, string perk)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                    INSERT INTO JuniorUnicornFirms (Name, DesiredSkill, YearsOfExp, Perk) 
                    VALUES ($name, $skill, $yearsOfExp, $perk
                    )";
            command.Parameters.AddWithValue("$name", name);
            command.Parameters.AddWithValue("$skill", skill);
            command.Parameters.AddWithValue("$yearsOfExp", yearsOfExp);
            command.Parameters.AddWithValue("$perk", perk);
            command.ExecuteNonQuery();
        }
    }
}