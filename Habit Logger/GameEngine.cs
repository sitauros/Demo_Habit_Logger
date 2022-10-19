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
            }

            Menu.PrintMainMenu();
        }
    }
}