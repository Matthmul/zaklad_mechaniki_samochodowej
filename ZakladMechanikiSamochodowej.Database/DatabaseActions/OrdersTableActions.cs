﻿using ZakladMechanikiSamochodowej.Database.DatabaseModels;

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
    }
}