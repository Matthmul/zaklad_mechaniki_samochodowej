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

        public static User? GetUserById(int id)
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.LoginTable.SingleOrDefault(u => u.Id == id);
        }

        public static List<User> GetAllNewUsers()
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.LoginTable.Where(u => u.IsNew == true).ToList();
        }
    }
}
