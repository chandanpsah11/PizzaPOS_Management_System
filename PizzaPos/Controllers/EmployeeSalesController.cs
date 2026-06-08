using BAL;
using MODELS;
using System;
using System.Web.Mvc;

namespace PizzaPos.Controllers
{
    [AuthorizeRole("Employee")]
    public class EmployeeSalesController : Controller
    {
        SalesBAL bal = new SalesBAL();

        public ActionResult Index()
        {
            try
            {
                var pizzas = bal.GetActivePizzas();
                return View(pizzas);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load menu. Please try again.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public JsonResult GetPizzaSizePrices(int pizzaId)
        {
            try
            {
                if (pizzaId == 0)
                    return Json(new { success = false, message = "Invalid pizza." },
                                JsonRequestBehavior.AllowGet);

                var data = bal.GetPizzaSizePrices(pizzaId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Failed to load sizes. Please try again." },
                            JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult PlaceOrder(PlaceOrderModel model)
        {
            try
            {
                if (model == null || model.Items == null || model.Items.Count == 0)
                    return Json(new { success = false, message = "Order has no items." });

                model.CreatedByUserId = Session["UserId"] != null
                    ? (int)Session["UserId"]
                    : 0;

                if (model.CreatedByUserId == 0)
                    return Json(new { success = false, message = "Session expired. Please log in again." });

                bool result = bal.PlaceOrder(model);
                return Json(new { success = result });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Failed to place order. Please try again." });
            }
        }
    }
}