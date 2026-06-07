using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class AdminInventoryDAL
    {
        public List<UOM> GetUom()
        {
            try
            {
                using (var db = new PizzaPOSEntities())
                {
                    return db.UOMs.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching UOM list: " + ex.Message, ex);
            }
        }

        public List<InventoryItem> GetAllInventory()
        {
            try
            {
                using (var db = new PizzaPOSEntities())
                {
                    return db.InventoryItems.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching inventory list: " + ex.Message, ex);
            }
        }
        public bool AddInventory(InventoryItem item)
        {
            using (var db = new PizzaPOSEntities())
            {
                db.InventoryItems.Add(item);
                return db.SaveChanges() > 0;
            }
        }
        public decimal GetYesterdayTotalSales()
        {
            using (var db = new PizzaPOSEntities())
            {
                DateTime yesterday = DateTime.Today.AddDays(-1);
                DateTime today = DateTime.Today;

                return db.Orders
                         .Where(o => o.OrderDate >= yesterday
                                  && o.OrderDate < today)
                         .Sum(o => (decimal?)o.TotalAmount) ?? 0;
            }
        }
    }
}