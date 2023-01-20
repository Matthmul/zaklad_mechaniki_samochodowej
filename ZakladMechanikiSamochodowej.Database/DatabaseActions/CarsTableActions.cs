using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Database.DatabaseActions
{
    public class CarsTableActions
    {


        public static List<Cars> GetAllCars()
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.CarsTable.ToList();
        }
    }
}
