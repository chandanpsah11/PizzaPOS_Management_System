using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class AdminSalesDAL
    {
        public List<PizzaMaster> GetPizzas()
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.PizzaMasters
                         .Include("PizzaCategory")
                         .Where(p => p.IsActive)
                         .ToList();
            }
        }

        public List<PizzaCategory> GetCategory()
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.PizzaCategories.ToList();
            }
        }

        public bool CreatePizza(PizzaMaster pizza)
        {
            using (var db = new PizzaPOSEntities())
            {
                db.PizzaMasters.Add(pizza);
                return db.SaveChanges() > 0;
            }
        }

        public PizzaMaster GetPizzaById(int pizzaId)
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.PizzaMasters
                         .Include("PizzaCategory")
                         .FirstOrDefault(x => x.PizzaId == pizzaId);
            }
        }

        public List<PizzaSize> GetPizzaSizes()
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.PizzaSizes.ToList();
            }
        }

        public List<InventoryItem> GetInventoryItems()
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.InventoryItems.ToList();
            }
        }

        public bool SavePizzaPrice(PizzaPrice price)
        {
            using (var db = new PizzaPOSEntities())
            {
                var existing = db.PizzaPrices
                    .FirstOrDefault(p => p.PizzaId == price.PizzaId
                                     && p.PizzaSizeId == price.PizzaSizeId);

                if (existing != null)
                    existing.Price = price.Price;
                else
                    db.PizzaPrices.Add(price);

                return db.SaveChanges() > 0;
            }
        }

        public bool SaveRecipe(List<Recipe> items)
        {
            using (var db = new PizzaPOSEntities())
            {
                var first = items.First();

                var old = db.Recipes
                    .Where(r => r.PizzaId == first.PizzaId
                             && r.PizzaSizeId == first.PizzaSizeId)
                    .ToList();

                db.Recipes.RemoveRange(old);

                foreach (var item in items)
                    db.Recipes.Add(item);

                return db.SaveChanges() > 0;
            }
        }

        public List<UOM> GetUOMs()
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.UOMs.ToList();
            }
        }

        public List<Recipe> GetRecipeByPizzaAndSize(int pizzaId, int pizzaSizeId)
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.Recipes
                         .Include("InventoryItem")
                         .Include("UOM")
                         .Where(r => r.PizzaId == pizzaId
                                  && r.PizzaSizeId == pizzaSizeId)
                         .ToList();
            }
        }

        public PizzaPrice GetPizzaPrice(int pizzaId, int pizzaSizeId)
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.PizzaPrices
                         .FirstOrDefault(p => p.PizzaId == pizzaId
                                           && p.PizzaSizeId == pizzaSizeId);
            }
        }
    }
}