using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class SalesDAL
    {
        public List<PizzaMaster> GetActivePizzas()
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.PizzaMasters
                         .Include("PizzaCategory")
                         .Where(p => p.IsActive)
                         .ToList();
            }
        }

        public List<PizzaPrice> GetPizzaSizePrices(int pizzaId)
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.PizzaPrices
                         .Include("PizzaSize")
                         .Where(p => p.PizzaId == pizzaId)
                         .ToList();
            }
        }

        public bool PlaceOrder(Order order)
        {
            using (var db = new PizzaPOSEntities())
            {
                using (var transaction = db.Database.BeginTransaction(
                    System.Data.IsolationLevel.RepeatableRead))
                {
                    try
                    {
                        db.Orders.Add(order);
                        db.SaveChanges();

                        DeductInventory(db, order.OrderItems.ToList());
                        db.SaveChanges();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void DeductInventory(PizzaPOSEntities db, List<OrderItem> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                var recipeRows = db.Recipes
                                   .Where(r => r.PizzaId == orderItem.PizzaId
                                            && r.PizzaSizeId == orderItem.PizzaSizeId)
                                   .ToList();

                foreach (var recipe in recipeRows)
                {
                    var stock = db.Database
                                  .SqlQuery<InventoryItem>(
                                      "SELECT * FROM InventoryItems WITH (UPDLOCK) WHERE ItemId = @p0",
                                      recipe.ItemId)
                                  .FirstOrDefault();

                    if (stock == null)
                        throw new Exception("Inventory item not found for ItemId: " + recipe.ItemId);

                    decimal deduction = recipe.QuantityRequired * orderItem.Quantity;

                    if (stock.CurrentStock < deduction)
                        throw new Exception("Insufficient stock for: " + stock.ItemName);

                    var trackedStock = db.InventoryItems.Find(stock.ItemId);
                    trackedStock.CurrentStock -= deduction;
                }
            }
        }
    }
}