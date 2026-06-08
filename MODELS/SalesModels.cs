using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MODELS
{
    public class SalesPizzaModel
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public string CategoryName { get; set; }
    }

    public class SalesPizzaSizePrice
    {
        public int PizzaSizeId { get; set; }
        public string SizeName { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderItemModel
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public int PizzaSizeId { get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class PlaceOrderModel
    {
        public int CreatedByUserId { get; set; }

        [Required(ErrorMessage = "Payment mode is required.")]
        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Order total must be greater than zero.")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Order must have at least one item.")]
        public List<OrderItemModel> Items { get; set; }
    }
}