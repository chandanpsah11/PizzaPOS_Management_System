using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class PizzaDetailViewModel
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }

    public class PizzaSizeModel
    {
        public int PizzaSizeId { get; set; }
        public string SizeName { get; set; }
    }

    public class PizzaPriceModel
    {
        public int PizzaId { get; set; }
        public int PizzaSizeId { get; set; }
        public decimal Price { get; set; }
    }

    public class InventoryItemDropdownModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
    }

    public class UOMDropdownModel
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
    }

    public class RecipeItemModel
    {
        public int PizzaId { get; set; }
        public int PizzaSizeId { get; set; }
        public int ItemId { get; set; }
        public decimal QuantityRequired { get; set; }
        public int? UnitId { get; set; }
    }
    public class RecipeDisplayModel
    {
        public int RecipeId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal QuantityRequired { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
    }

    public class PizzaPriceDisplayModel
    {
        public int PizzaSizeId { get; set; }
        public decimal Price { get; set; }
    }
}