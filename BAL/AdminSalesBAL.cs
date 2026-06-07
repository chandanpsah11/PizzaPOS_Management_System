using DAL;
using MODELS;
using System.Collections.Generic;
using System.Linq;

namespace BAL
{
    public class AdminSalesBAL
    {
        AdminSalesDAL dal = new AdminSalesDAL();

        public List<PizzaMasterModel> GetPizzas()
        {
            return dal.GetPizzas()
                      .Select(x => new PizzaMasterModel
                      {
                          PizzaId = x.PizzaId,
                          PizzaName = x.PizzaName,
                          CategoryName = x.PizzaCategory.CategoryName,
                          IsActive = x.IsActive
                      }).ToList();
        }

        public List<PizzaCategoryModel> GetPizzaCategory()
        {
            return dal.GetCategory()
                      .Select(x => new PizzaCategoryModel
                      {
                          CategoryId = x.CategoryId,
                          CategoryName = x.CategoryName
                      }).ToList();
        }

        public bool CreatePizza(PizzaMasterModelCreate model)
        {
            var pizza = new PizzaMaster
            {
                PizzaName = model.PizzaName,
                CategoryId = model.categoryid,
                IsActive = model.IsActive
            };
            return dal.CreatePizza(pizza);
        }

        public PizzaDetailViewModel GetPizzaById(int pizzaId)
        {
            var p = dal.GetPizzaById(pizzaId);
            if (p == null) return null;

            return new PizzaDetailViewModel
            {
                PizzaId = p.PizzaId,
                PizzaName = p.PizzaName,
                IsActive = p.IsActive,
                CategoryName = p.PizzaCategory.CategoryName
            };
        }

        public List<PizzaSizeModel> GetPizzaSizes()
        {
            return dal.GetPizzaSizes()
                      .Select(s => new PizzaSizeModel
                      {
                          PizzaSizeId = s.PizzaSizeId,
                          SizeName = s.SizeName
                      }).ToList();
        }

        public List<InventoryItemDropdownModel> GetInventoryItemsForRecipe()
        {
            return dal.GetInventoryItems()
                      .Select(i => new InventoryItemDropdownModel
                      {
                          ItemId = i.ItemId,
                          ItemName = i.ItemName
                      }).ToList();
        }

        public bool SavePizzaPrice(PizzaPriceModel model)
        {
            return dal.SavePizzaPrice(new PizzaPrice
            {
                PizzaId = model.PizzaId,
                PizzaSizeId = model.PizzaSizeId,
                Price = model.Price
            });
        }

        public bool SaveRecipe(List<RecipeItemModel> items)
        {
            var dalItems = items.Select(r => new Recipe
            {
                PizzaId = r.PizzaId,
                PizzaSizeId = r.PizzaSizeId,
                ItemId = r.ItemId,
                QuantityRequired = r.QuantityRequired,
                UnitId = r.UnitId
            }).ToList();

            return dal.SaveRecipe(dalItems);
        }

        public List<UOMDropdownModel> GetUOMs()
        {
            return dal.GetUOMs()
                      .Select(u => new UOMDropdownModel
                      {
                          UnitId = u.UOMId,
                          UnitName = u.UOMName
                      }).ToList();
        }

        public List<RecipeDisplayModel> GetRecipeByPizzaAndSize(int pizzaId, int pizzaSizeId)
        {
            return dal.GetRecipeByPizzaAndSize(pizzaId, pizzaSizeId)
                      .Select(r => new RecipeDisplayModel
                      {
                          RecipeId = r.RecipeId,
                          ItemId = r.ItemId,
                          ItemName = r.InventoryItem.ItemName,
                          QuantityRequired = r.QuantityRequired,
                          UnitId = r.UnitId,
                          UnitName = r.UOM != null ? r.UOM.UOMName : ""
                      }).ToList();
        }

        public PizzaPriceDisplayModel GetPizzaPrice(int pizzaId, int pizzaSizeId)
        {
            var p = dal.GetPizzaPrice(pizzaId, pizzaSizeId);
            if (p == null) return null;

            return new PizzaPriceDisplayModel
            {
                PizzaSizeId = p.PizzaSizeId,
                Price = p.Price
            };
        }
    }
}