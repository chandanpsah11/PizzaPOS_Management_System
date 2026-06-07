using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MODELS;

namespace DAL
{
    public class OrdersDAL
    {
        public List<OrderListViewModel> GetAllOrders()
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.Orders
                         .OrderByDescending(o => o.OrderDate)
                         .Select(o => new OrderListViewModel
                         {
                             OrderId = o.OrderId,
                             OrderDate = o.OrderDate,
                             PaymentMode = o.PaymentMode,
                             TotalAmount = o.TotalAmount,
                             PlacedBy = o.User.UserName,
                             Items = o.OrderItems.Select(i => new OrderItemViewModel
                             {
                                 PizzaName = i.PizzaMaster.PizzaName,
                                 SizeName = i.PizzaSize.SizeName,
                                 Quantity = i.Quantity,
                                 UnitPrice = i.UnitPrice,
                                 LineTotal = i.LineTotal
                             }).ToList()
                         }).ToList();
            }
        }
    }
}