using BAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PizzaPos.Controllers
{
    [AuthorizeRole("Admin")]
    public class AdminInventoryController : Controller
    {
        AdminInventoryBAL bal = new AdminInventoryBAL();

        public ActionResult Index()
        {
            try
            {
                ViewBag.YesterdaySales = bal.GetYesterdayTotalSales();
                return View();
            }
            catch (Exception)
            {
                ViewBag.YesterdaySales = 0;
                return View();
            }
        }

        public ActionResult GetAllInventory()
        {
            try
            {
                var model = bal.GetAllInventory();
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load inventory. Please try again.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult AddInventory()
        {
            try
            {
                LoadUOMDropdown();
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load form. Please try again.";
                return RedirectToAction("GetAllInventory");
            }
        }

        [HttpPost]
        public ActionResult AddInventory(InventoryItemModel item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadUOMDropdown(item.UOMId);
                    return View(item);
                }

                bool saved = bal.AddInventory(item);
                if (saved)
                {
                    TempData["Success"] = "Item added successfully.";
                    return RedirectToAction("GetAllInventory");
                }

                ModelState.AddModelError("", "Failed to save item. Please try again.");
                LoadUOMDropdown(item.UOMId);
                return View(item);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong. Please try again.");
                LoadUOMDropdown(item.UOMId);
                return View(item);
            }
        }

        private void LoadUOMDropdown(int selectedId = 0)
        {
            var uoms = bal.GetUom();
            ViewBag.UOM = new SelectList(uoms, "UOMId", "UOMName", selectedId);
        }
        public ActionResult EditItem()
        {
            return View();
        }
        public ActionResult DeleteItem()
        {
            return View();
        }
    }
}