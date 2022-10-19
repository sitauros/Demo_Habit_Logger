namespace HabitLogger
{
    internal class GameEngine
    {  
        static void Main()
        {
            Database.ConnectionString = "Data Source = UnicornPride.db";
            Database.CreateDatabase();
            Menu.PrintMainMenu();
        }
    }
}