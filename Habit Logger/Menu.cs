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

            var result = ValidateIntegerRange(0, 4);

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

        private static void ListAllCompanies()
        {
            bool continueLoop = true;
            int ID_offset = 0;
            int currentPage = 1;
            int totalRecords = Database.GetRecordCount();
            int numPages = CalculateNumPages(totalRecords);

            var resultSet = Database.RetrievePageAfterID(ID_offset);
            FormatPage(resultSet, numPages, currentPage, totalRecords);

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

                Console.WriteLine("\nYour input: ");
                var result = ValidateIntegerRange(minValue, maxValue);

                switch (result)
                {
                    case 0:
                        currentPage = currentPage - 1;
                        ID_offset = Convert.ToInt32(resultSet.Rows[0]["CompanyID"]); // First record in result set
                        resultSet = Database.RetrievePageBeforeID(ID_offset);
                        FormatPage(resultSet, numPages, currentPage, totalRecords);
                        break;
                    case 1:
                        continueLoop = false;
                        break;
                    case 2:
                        currentPage = currentPage + 1;
                        ID_offset = Convert.ToInt32(resultSet.Rows[4]["CompanyID"]); // Last record in result set
                        resultSet = Database.RetrievePageAfterID(ID_offset);
                        FormatPage(resultSet, numPages, currentPage, totalRecords);
                        break;
                }
            }

            PrintMainMenu();
        }

        private static void AddNewCompany()
        {
            Console.Clear();
            Console.WriteLine(@"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Add a new company to the Unicorn Pride Database
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
1) Enter the company's name: ");
            string companyName = ValidateUserString();
            Console.WriteLine("2) Enter desired coding skill: ");
            string skill = ValidateUserString();
            Console.WriteLine("3) Enter years of experience required (max: 10): ");
            int yearsOfExp = ValidateIntegerRange(0, 10);
            Console.WriteLine("4) Enter company perk: ");
            string perk = ValidateUserString();

            var resultSet = Database.AddNewCompany(companyName, skill, yearsOfExp, perk);
            FormatTable(resultSet);
            BackToMainMenu("New company added.");
        }

        private static void UpdateCompany()
        {
            Console.Clear();
            Console.WriteLine(@"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Update an entry in the Unicorn Pride database
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Enter the company's ID: ");

            int CompanyID;
            var input = Console.ReadLine();

            if (Int32.TryParse(input, out CompanyID))
            {
                var resultSet = Database.GetCompanyByID(CompanyID);

                if (resultSet.Rows.Count == 1)
                {
                    FormatTable(resultSet);
                    Console.WriteLine("Enter a number below: \n");
                    Console.WriteLine(@"1) Update Company's name.
2) Update Company's desired coding skill.
3) Update Years of experience required.
4) Update Company perk.

Your input: ");

                    var result = ValidateIntegerRange(1, 4);
                    string return_message = "";

                    switch (result)
                    {
                        case 1:
                            Console.WriteLine("Enter the company's name: ");
                            string companyName = ValidateUserString();
                            resultSet.Rows[0]["Name"] = companyName;
                            return_message = "Updated company's name to: " + companyName;
                            break;
                        case 2:
                            Console.WriteLine("Enter desired coding skill: ");
                            string skill = ValidateUserString();
                            resultSet.Rows[0]["DesiredSkill"] = skill;
                            return_message = "Updated company's desired skill to: " + skill;
                            break;
                        case 3:
                            Console.WriteLine("Enter years of experience required (max: 10): ");
                            int yearsOfExp = ValidateIntegerRange(0, 10);
                            resultSet.Rows[0]["YearsOfExp"] = yearsOfExp;
                            return_message = "Updated years of experience required to: " + yearsOfExp;
                            break;
                        case 4:
                            Console.WriteLine("4) Enter company perk: ");
                            string perk = ValidateUserString();
                            resultSet.Rows[0]["Perk"] = perk;
                            return_message = "Updated company's perk to: " + perk;
                            break;
                    }

                    Database.UpdateCompany(resultSet);
                    resultSet = Database.GetCompanyByID(CompanyID);
                    FormatTable(resultSet);
                    BackToMainMenu(return_message);
                }
                else
                {
                    BackToMainMenu("Company not found with ID value: " + input);
                }
            }
            else
            {
                BackToMainMenu("Input is not a number: " + input);
            }
        }

        private static void DeleteCompany()
        {
            Console.Clear();
            Console.WriteLine(@"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Delete an entry in the Unicorn Pride database
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Enter the company's ID: ");

            int CompanyID;
            var input = Console.ReadLine();
            
            if (Int32.TryParse(input, out CompanyID))
            {
                var resultSet = Database.GetCompanyByID(CompanyID);

                if (resultSet.Rows.Count == 1)
                {
                    Database.DeleteCompany(CompanyID);
                    FormatTable(resultSet);
                    BackToMainMenu("Company deleted with ID: " + CompanyID);
                }
                else
                {
                    BackToMainMenu("Company not found with ID value: " + input);
                }
            }
            else
            {
                BackToMainMenu("Input is not a number: " + input);
            }
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

        private static void FormatPage(DataTable resultSet, int numPages, int currentPage, int totalRecords)
        {
            Console.Clear();
            Console.WriteLine(@"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
Showing companies listed in the Unicorn Pride Database
Now viewing page " + currentPage + " of " + numPages +
"\nTotal: " + totalRecords + "\n=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=" + "\n");

            FormatTable(resultSet);
        }

        private static void FormatTable(DataTable resultSet)
        {
            ConsoleTableBuilder
               .From(resultSet)
               .WithColumn("Id", "Company", "Skill", "Years of Exp.", "Perk")
               .WithTextAlignment(TextAlignments)
               .WithCharMapDefinition(CharMapDefinition.FramePipDefinition)
               .WithCharMapDefinition(CharMapDefinition.FramePipDefinition, CharMapPositions)
               .ExportAndWriteLine(TableAligntment.Center);
        }

        internal static int ValidateIntegerRange(int minValue, int maxValue)
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

        internal static string ValidateUserString()
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

        private static int CalculateNumPages(int totalRecords)
        {
            int numPages;

            if (totalRecords == 0)
                numPages = 1;
            else if (totalRecords % 5 == 0)
                numPages = totalRecords / 5;
            else
                numPages = 1 + totalRecords / 5;

            return numPages;
        }

    }
}