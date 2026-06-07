// Infrastructure/AuthorizeRoleAttribute.cs
using System.Web.Mvc;

public class AuthorizeRoleAttribute : AuthorizeAttribute
{
    public AuthorizeRoleAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }

    // Redirect to login if not authenticated,
    // redirect to Access Denied if authenticated but wrong role
    protected override void HandleUnauthorizedRequest(
        AuthorizationContext filterContext)
    {
        if (filterContext.HttpContext.User.Identity.IsAuthenticated)
        {
            filterContext.Result = new RedirectResult("~/Account/AccessDenied");
        }
        else
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}