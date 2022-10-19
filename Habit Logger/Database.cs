using ConsoleTableExt;
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

        internal static void ListAllCompanies()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT * FROM JuniorUnicornFirms
                    LIMIT 5
                ";

            using SqliteDataReader reader = command.ExecuteReader();
            Console.WriteLine("HERE");
            Console.ReadLine();
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