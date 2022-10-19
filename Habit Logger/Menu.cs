namespace HabitLogger
{
    internal class Menu
    {
        internal static void PrintMainMenu()
        {
            Console.Clear();
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

            var result = validateUserInteger(0, 4);

            switch (result)
            {
                case 1:
                    ListAllCompanies();
                    break;
                case 2:
                    AddNewCompany();
                    break;
                case 3:
                    UpdateCompany();
                    break;
                case 4:
                    DeleteCompany();
                    break;
                case 0:
                    ExitProgram();
                    break;         
            }
        }

        internal static int validateUserInteger(int minValue, int maxValue)
        {
            int result;
            var input = Console.ReadLine();
            bool isValidInput = int.TryParse(input, out result);

            while (!isValidInput || result < minValue || result > maxValue)
            {
                Console.WriteLine("Invalid input. Please try again.");
                Console.WriteLine("Your input: ");
                isValidInput = int.TryParse(Console.ReadLine(), out result);
            }

            return result;
        }

        internal static string validateUserString()
        {
            string? input = Console.ReadLine();

            while (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
                Console.WriteLine("Your input: ");
                input = Console.ReadLine();
            }

            return input;
        }

        private static void ListAllCompanies()
        {
            throw new NotImplementedException();
        }

        private static void AddNewCompany()
        {
            Console.Clear();
            Console.WriteLine(@"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Add a new company to the Unicorn Pride Database
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
1) Enter the company's name: ");
            string companyName = validateUserString();
            Console.WriteLine("2) Enter desired coding skill: ");
            string skill = validateUserString();
            Console.WriteLine("3) Enter years of experience required (max: 10): ");
            int yearsOfExp = validateUserInteger(0, 10);
            Console.WriteLine("4) Enter company perk: ");
            string perk = validateUserString();

            Database.AddNewCompany(companyName, skill, yearsOfExp, perk);
            BackToMainMenu("New company added.");
        }

        private static void UpdateCompany()
        {
            throw new NotImplementedException();
        }

        private static void DeleteCompany()
        {
            throw new NotImplementedException();
        }

        private static void ExitProgram()
        {
            Environment.Exit(1);
        }

        private static void BackToMainMenu(string message)
        {
            Console.WriteLine("\n" + "-----------------------------------------------------------------------------------");
            Console.WriteLine(message);
            Console.WriteLine("Press any key to return to main menu.");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.ReadLine();
            PrintMainMenu();
        }
    }
}