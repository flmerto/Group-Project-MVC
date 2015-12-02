using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Group_Project.Filters
{
    public class CustomAuthorization : ActionFilterAttribute
    {
        public string Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.IsInRole(Role))
            {
                filterContext.Result =
                new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary{{"Controller", "Home"},
                                                                {"Action", "Index"}});
            }
            base.OnActionExecuting(filterContext);
        }
    }
}