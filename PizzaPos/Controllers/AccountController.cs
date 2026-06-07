using BAL;
using MODELS;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PizzaPos.Controllers
{
    public class AccountController : Controller
    {
        AuthBAL auth = new AuthBAL();

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToDashboard();

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                LoggedUser user = auth.Login(model);
                if (user == null)
                {
                    ViewBag.Error = "Invalid credentials. Please try again.";
                    return View(model);
                }

                var ticket = new FormsAuthenticationTicket(
                    version: 1,
                    name: user.name,
                    issueDate: DateTime.Now,
                    expiration: DateTime.Now.AddMinutes(720),
                    isPersistent: false,
                    userData: user.role
                );

                string encrypted = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(
                    FormsAuthentication.FormsCookieName, encrypted));

                return RedirectToDashboard(user.role);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong. Please try again.";
                return View(model);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                if (auth.EmailExists(model.Email))
                {
                    ModelState.AddModelError("Email", "An account with this email already exists.");
                    return View(model);
                }

                bool saved = auth.Register(model);
                if (saved)
                {
                    TempData["Success"] = "Account created successfully. Please log in.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "Registration failed. Please try again.");
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong. Please try again.");
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }

        private ActionResult RedirectToDashboard(string role)
        {
            switch (role.ToLower().Trim())
            {
                case "admin":
                    return RedirectToAction("Index", "AdminInventory");
                case "employee":
                    return RedirectToAction("Index", "EmployeeSales");
                default:
                    FormsAuthentication.SignOut();
                    return RedirectToAction("AccessDenied", "Account");
            }
        }

        private ActionResult RedirectToDashboard()
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("GetAllInventory", "AdminInventory");
            if (User.IsInRole("Employee"))
                return RedirectToAction("Index", "EmployeeSales");

            return RedirectToAction("AccessDenied", "Account");
        }
        

    }
}