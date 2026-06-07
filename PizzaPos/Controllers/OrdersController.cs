using BAL;
using System;
using System.Web.Mvc;

namespace PizzaPos.Controllers
{
    [AuthorizeRole("Admin", "Employee")]
    public class OrdersController : Controller
    {
        OrdersBAL bal = new OrdersBAL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrders()
        {

            try
            {
                var role = (User as System.Security.Principal.GenericPrincipal)?.IsInRole("Admin");
                TempData["Debug"] = "IsAdmin: " + role + " | Name: " + User.Identity.Name;
                var orders = bal.GetAllOrders();
                return View(orders);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load orders. Please try again.";
                return RedirectToAction("Index");
            }
        }
    }
}