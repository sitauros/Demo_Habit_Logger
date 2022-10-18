namespace HabitLogger
{
    internal class GameEngine
    {  
        static void Main()
        {
            Database unicornDB = new Database("Data Source = UnicornPride.db");
            unicornDB.CreateDatabase();
            Menu.PrintMainMenu();
        }
    }
}