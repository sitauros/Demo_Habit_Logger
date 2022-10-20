namespace HabitLogger
{
    internal class GameEngine
    {  
        static void Main()
        {
            Database.ConnectionString = "Data Source = UnicornPride.db";
            Database.CreateDatabase();

            if (Database.GetRecordCount() == 0)
            {
                Database.AddNewCompany("CoarseLotions", "PHP", 5, "Mandatory afterwork self-training");
                Database.AddNewCompany("ScandalWeb", "React", 1, "$400-700 USD/month during probation");
                Database.AddNewCompany("FDM Troup", "DevOps", 2, "Will not verify employment at firms they subcontract you to");
                Database.AddNewCompany("Revatorture", "Java", 3, "Must relocate to anywhere in home country");
                Database.AddNewCompany("Crossrover for Work", "C++", 2, "No vacation/sick days + constant motion tracking");
                Database.AddNewCompany("Company 1", "Skill 1", 1, "Amazing Benefits 1");
                Database.AddNewCompany("Company 2", "Skill 2", 2, "Amazing Benefits 2");
                Database.AddNewCompany("Company 3", "Skill 3", 3, "Amazing Benefits 3");
                Database.AddNewCompany("Company 4", "Skill 4", 4, "Amazing Benefits 4");
                Database.AddNewCompany("Company 5", "Skill 5", 5, "Amazing Benefits 5");
                Database.AddNewCompany("Company 6", "Skill 6", 6, "Amazing Benefits 6");
                Database.AddNewCompany("Company 7", "Skill 7", 7, "Amazing Benefits 7");
                Database.AddNewCompany("Company 8", "Skill 8", 8, "Amazing Benefits 8");
                Database.AddNewCompany("Company 9", "Skill 9", 9, "Amazing Benefits 9");
                Database.AddNewCompany("Company 10", "Skill 10", 10, "Amazing Benefits 10");
            }

            Menu.PrintMainMenu();
        }
    }
}