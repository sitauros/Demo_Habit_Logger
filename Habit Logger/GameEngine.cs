namespace HabitLogger
{
    internal class GameEngine
    {
        static void Main()
        {
            bool continueGame = true;
            while (continueGame)
            {
                Console.Clear();
                Menu.PrintMenu();
            }
        }
    }
}