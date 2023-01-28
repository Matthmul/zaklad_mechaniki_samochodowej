using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Database.DatabaseActions
{
    public class OrdersTableActions
    {
        public static void SaveOrder(Order order)
        {
            var database = new DatabaseConnection();
            database.OrderTable.Add(order);
            database.SaveChanges();
        }

        public static List<Order> GetOrdersByOrderState(OrderState orderState)
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.OrderTable.Where(o => o.OrderState == orderState).ToList();
        }

        public static void UpdateOrder(Order order)
        {
            using var dbContext = new DatabaseConnection();
            dbContext.Update(order);
            dbContext.SaveChanges();
        }

        public static List<Order> GetLastOrder(int userId)
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.OrderTable.Where(o => o.ClientId == userId).OrderByDescending(c => c.Id).Take(1).ToList();
        }

        public static List<Order> GetAllUserOrders(int userId)
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.OrderTable.Where(o => o.ClientId == userId).ToList();
        }

        public static void RemoveAllUserOrders(int userId)
        {
            using var dbContext = new DatabaseConnection();
            var orders = dbContext.OrderTable.Where(o => o.ClientId == userId).ToList();
            foreach (var order in orders)
            {
                dbContext.Remove(order);
            }
            dbContext.SaveChanges();
        }

        public static void RemoveOrder(Order order)
        {
            using var dbContext = new DatabaseConnection();
            dbContext.Remove(order);
            dbContext.SaveChanges();
        }
    }
}
