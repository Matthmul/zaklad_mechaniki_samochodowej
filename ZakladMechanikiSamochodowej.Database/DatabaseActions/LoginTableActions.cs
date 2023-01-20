using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Database.DatabaseActions
{
    public class LoginTableActions
    {
        public static void SaveUser(User user)
        {
            var database = new DatabaseConnection();
            database.LoginTable.Add(user);
            database.SaveChanges();
        }

        public static List<User> GetAllUsers()
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.LoginTable.ToList();
        }

        public static User? TryGetUserByName(string name)
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.LoginTable.SingleOrDefault(u => u.Username == name);
        }
    }
}
