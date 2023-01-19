using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Database.DatabaseActions
{
    public class UsersActions
    {
        public UsersActions() { }

        public static void SaveUser(Users user)
        {
            var database = new DatabaseConnection();
            database.Users.Add(user);
            database.SaveChanges();
        }
    }
}
