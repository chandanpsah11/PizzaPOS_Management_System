using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace PizzaPos
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie cookie =
                Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null) return;

            try
            {
                FormsAuthenticationTicket ticket =
                    FormsAuthentication.Decrypt(cookie.Value);

                if (ticket == null || ticket.Expired) return;
                string[] roles = string.IsNullOrWhiteSpace(ticket.UserData)
                    ? new string[0]
                    : ticket.UserData.Split(',');

                FormsIdentity identity = new FormsIdentity(ticket);
                GenericPrincipal principal = new GenericPrincipal(identity, roles);

  
                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }
            catch
            {
                FormsAuthentication.SignOut();
            }
        }
    }
}