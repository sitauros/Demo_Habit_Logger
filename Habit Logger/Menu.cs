using System.Data;
using ConsoleTableExt;

namespace HabitLogger
{
    internal class Menu
    {
        internal static Dictionary<HeaderCharMapPositions, char> CharMapPositions = new Dictionary<HeaderCharMapPositions, char> {
            {HeaderCharMapPositions.TopLeft, '╒' },
            {HeaderCharMapPositions.TopCenter, '═' },
            {HeaderCharMapPositions.TopRight, '╕' },
            {HeaderCharMapPositions.BottomLeft, '╞' },
            {HeaderCharMapPositions.BottomCenter, '╤' },
            {HeaderCharMapPositions.BottomRight, '╡' },
            {HeaderCharMapPositions.BorderTop, '═' },
            {HeaderCharMapPositions.BorderRight, '│' },
            {HeaderCharMapPositions.BorderBottom, '═' },
            {HeaderCharMapPositions.BorderLeft, '│' },
            {HeaderCharMapPositions.Divider, ' ' },
        };

        internal static Dictionary<int, TextAligntment> TextAlignments = new Dictionary<int, TextAligntment> {
            {0, TextAligntment.Center},
            {1, TextAligntment.Center},
            {2, TextAligntment.Center},
            {3, TextAligntment.Center},
            {4, TextAligntment.Center}
        };

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
            bool continueLoop = true;
            int ID_offset = 0;
            int currentPage = 1;
            int totalRecords = Database.GetRecordCount();
            int numPages = 1 + (totalRecords / 5);

            var resultSet = Database.RetrievePageAfterID(ID_offset);
            FormatPage(resultSet, numPages, currentPage);

            while (continueLoop)
            {
                int minValue = 1;
                int maxValue = 1;
                Console.WriteLine("Enter a number below: ");

                if (currentPage > 1)
                {
                    Console.WriteLine("0) Return to previous page. ");
                    minValue = 0;
                }

                Console.WriteLine("1) Return to main menu.");

                if (currentPage < numPages)
                {
                    Console.WriteLine("2) Advance to next page. ");
                    maxValue = 2;
                }
                
                var result = validateUserInteger(minValue, maxValue);

                switch (result)
                {
                    case 0:
                        currentPage = currentPage - 1;
                        ID_offset = Convert.ToInt32(resultSet.Rows[0]["ID"]);
                        resultSet = Database.RetrievePageBeforeID(ID_offset);
                        FormatPage(resultSet, numPages, currentPage);
                        break;
                    case 1:
                        continueLoop = false;
                        break;
                    case 2:
                        currentPage = currentPage + 1;
                        ID_offset = Convert.ToInt32(resultSet.Rows[4]["ID"]);
                        resultSet = Database.RetrievePageAfterID(ID_offset);
                        FormatPage(resultSet, numPages, currentPage);
                        break;
                }
            }

            PrintMainMenu();
        }
        
        private static void FormatPage(DataTable resultSet, int numPages, int currentPage)
        {
            Console.Clear();
            Console.WriteLine(@"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Showing companies listed in the Unicorn Pride Database
Now viewing page " + currentPage + " of " + numPages +
"\n=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=" + "\n");

            ConsoleTableBuilder
               .From(resultSet)
               .WithColumn("Id", "Company", "Skill", "Years of Exp.", "Perk")
               .WithTextAlignment(TextAlignments)
               .WithCharMapDefinition(CharMapDefinition.FramePipDefinition)
               .WithCharMapDefinition(CharMapDefinition.FramePipDefinition, CharMapPositions)
               .ExportAndWriteLine(TableAligntment.Center);
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