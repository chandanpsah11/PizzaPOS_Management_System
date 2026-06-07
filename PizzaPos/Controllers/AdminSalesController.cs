using BAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PizzaPos.Controllers
{
    [AuthorizeRole("Admin")]
    public class AdminSalesController : Controller
    {
        AdminSalesBAL bal = new AdminSalesBAL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPizzas()
        {
            try
            {
                var pizzas = bal.GetPizzas();
                return View(pizzas);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load pizzas. Please try again.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult CreatePizza()
        {
            try
            {
                LoadCategoryDropdown();
                return PartialView(new PizzaMasterModelCreate());
            }
            catch (Exception)
            {
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult CreatePizza(PizzaMasterModelCreate model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadCategoryDropdown(model.categoryid);
                    return PartialView(model);
                }

                bool saved = bal.CreatePizza(model);
                if (saved)
                    return Json(new { success = true, message = "Pizza created successfully." });

                ModelState.AddModelError("", "Failed to create pizza. Please try again.");
                LoadCategoryDropdown(model.categoryid);
                return PartialView(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong. Please try again.");
                LoadCategoryDropdown(model.categoryid);
                return PartialView(model);
            }
        }

        [HttpGet]
        public ActionResult PizzaDetail(int id)
        {
            try
            {
                var pizza = bal.GetPizzaById(id);
                if (pizza == null)
                    return HttpNotFound();

                LoadPizzaDetailDropdowns();
                return View(pizza);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load pizza details. Please try again.";
                return RedirectToAction("GetPizzas");
            }
        }

        [HttpPost]
        public JsonResult SavePizzaPrice(PizzaPriceModel model)
        {
            try
            {
                if (model == null || model.PizzaId == 0 || model.PizzaSizeId == 0)
                    return Json(new { success = false, message = "Invalid data." });

                bool result = bal.SavePizzaPrice(model);
                return Json(new { success = result });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Failed to save price. Please try again." });
            }
        }

        [HttpPost]
        public JsonResult SaveRecipe(List<RecipeItemModel> items)
        {
            try
            {
                if (items == null || items.Count == 0)
                    return Json(new { success = false, message = "No ingredients provided." });

                bool result = bal.SaveRecipe(items);
                return Json(new { success = result });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Failed to save recipe. Please try again." });
            }
        }

        [HttpGet]
        public JsonResult GetSizeData(int pizzaId, int pizzaSizeId)
        {
            try
            {
                if (pizzaId == 0 || pizzaSizeId == 0)
                    return Json(new { success = false, message = "Invalid parameters." },
                                JsonRequestBehavior.AllowGet);

                var recipe = bal.GetRecipeByPizzaAndSize(pizzaId, pizzaSizeId);
                var price = bal.GetPizzaPrice(pizzaId, pizzaSizeId);

                return Json(new
                {
                    success = true,
                    price = price != null ? price.Price : (decimal?)null,
                    recipe = recipe
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Failed to load size data. Please try again." },
                            JsonRequestBehavior.AllowGet);
            }
        }

        private void LoadCategoryDropdown(int selectedId = 0)
        {
            var categories = bal.GetPizzaCategory();
            ViewBag.Category = new SelectList(categories, "CategoryId", "CategoryName", selectedId);
        }

        private void LoadPizzaDetailDropdowns()
        {
            var sizes = bal.GetPizzaSizes();
            var inventory = bal.GetInventoryItemsForRecipe();
            var uoms = bal.GetUOMs();

            ViewBag.Sizes = new SelectList(sizes, "PizzaSizeId", "SizeName");
            ViewBag.Inventory = new SelectList(inventory, "ItemId", "ItemName");
            ViewBag.UOMs = new SelectList(uoms, "UnitId", "UnitName");
        }
    }
}