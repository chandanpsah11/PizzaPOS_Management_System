using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL
{
    public class SalesBAL
    {
        SalesDAL dal = new SalesDAL();

        public List<SalesPizzaModel> GetActivePizzas()
        {
            return dal.GetActivePizzas()
                      .Select(p => new SalesPizzaModel
                      {
                          PizzaId = p.PizzaId,
                          PizzaName = p.PizzaName,
                          CategoryName = p.PizzaCategory.CategoryName
                      }).ToList();
        }

        public List<SalesPizzaSizePrice> GetPizzaSizePrices(int pizzaId)
        {
            return dal.GetPizzaSizePrices(pizzaId)
                      .Select(p => new SalesPizzaSizePrice
                      {
                          PizzaSizeId = p.PizzaSizeId,
                          SizeName = p.PizzaSize.SizeName,
                          Price = p.Price
                      }).ToList();
        }

        public bool PlaceOrder(PlaceOrderModel model)
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                PaymentMode = model.PaymentMode,
                CreatedByUserId = model.CreatedByUserId,
                OrderItems = model.Items.Select(i => new OrderItem
                {
                    PizzaId = i.PizzaId,
                    PizzaSizeId = i.PizzaSizeId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    LineTotal = i.LineTotal
                }).ToList()
            };

            return dal.PlaceOrder(order);
        }
    }
}