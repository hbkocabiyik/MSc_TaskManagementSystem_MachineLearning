using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TaskManagement.Web.Areas.Manager.Controllers;
using TaskManagement.Web.Controllers;
using TaskManagement.Web.Models;
using TaskManagement.Web.Models.Models;
using TaskManagement.Web.Models.Utils;

namespace TaskManagement.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString ActionLinkForAnonymousUser(this HtmlHelper html)
        {
            var roles = new[] {"Administrator", "Manager", "Engineer"};
            var isInRole = roles.Any(HttpContext.Current.User.IsInRole);

            return !isInRole
                       ? new MvcHtmlString(string.Format("<li>{0}</li>", html.ActionLink("Home", "Index", "Home", new
                                                                                                                  {
                                                                                                                      area = ""
                                                                                                                  }, null)))
                       : MvcHtmlString.Empty;
        }

        public static MvcHtmlString ActionLinkWithManagerOrEngineerRole(this HtmlHelper html, string linkText, string actionName,
                                                                        string controllerName, string area)
        {
            return GetLiHtmlStringIfHasRole(html, linkText, actionName, controllerName, area, "Engineer", "Manager");
        }

        public static MvcHtmlString ActionLinkWithEngineerRole(this HtmlHelper html, string linkText, string actionName,
                                                               string controllerName, string area)
        {
            return GetLiHtmlStringIfHasRole(html, linkText, actionName, controllerName, area, "Engineer");
        }

        public static MvcHtmlString ActionLinkWithManagerRole(this HtmlHelper html, string linkText, string actionName,
                                                              string controllerName)
        {
            return GetLiHtmlStringIfHasRole(html, linkText, actionName, controllerName, "Manager", "Manager");
        }

        public static MvcHtmlString ActionLinkWithAdministratorRole(this HtmlHelper html, string linkText, string actionName,
                                                                    string controllerName, string area)
        {
            return GetLiHtmlStringIfHasRole(html, linkText, actionName, controllerName, area, "Administrator");
        }

        private static MvcHtmlString GetLiHtmlStringIfHasRole(HtmlHelper html, string linkText, string actionName, string controllerName,
                                                              string area, params string[] roles)
        {
            var isInRole = roles.Any(HttpContext.Current.User.IsInRole);

            return isInRole
                       ? new MvcHtmlString(string.Format("<li>{0}</li>", html.ActionLink(linkText, actionName, controllerName, new
                                                                                                                               {
                                                                                                                                   area
                                                                                                                               }, null)))
                       : MvcHtmlString.Empty;
        }

        public static MvcHtmlString GetRole(this HtmlHelper html, int role)
        {
            return new MvcHtmlString(((Role)role).ToString());
        }

        public static MvcHtmlString GetPosition(this HtmlHelper html, int position, int role)
        {
            var positionRole = (Role)role;

            switch (positionRole)
            {
                case Role.Manager:
                    var managerPosition = (ManagerPosition)position;
                    return new MvcHtmlString(managerPosition.GetDescription());
                case Role.Engineer:
                    var engineerPosition = (EngineerPosition)position;
                    return new MvcHtmlString(engineerPosition.GetDescription());
            }

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString GetStatus(this HtmlHelper html, int status)
        {
            return new MvcHtmlString(((Status)status).ToString());
        }

        public static MvcHtmlString GetSkillLevel(this HtmlHelper html, int level)
        {
            return new MvcHtmlString(((SkillLevel)level).ToString());
        }

        public static MvcHtmlString GetPriority(this HtmlHelper html, int priority)
        {
            return new MvcHtmlString(((Priority)priority).ToString());
        }

        public static MvcHtmlString Truncate(this HtmlHelper helper, string input, int length = 30)
        {
            return input.Length <= length ? new MvcHtmlString(input) : new MvcHtmlString(input.Substring(0, length) + "...");
        }

        public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectedValue)
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            var items = from value in values
                        select new SelectListItem
                               {
                                   Text = value.ToString(),
                                   Value = Convert.ToInt32(value).ToString(),
                                   Selected = (value.Equals(selectedValue))
                               };

            return htmlHelper.DropDownList(name, items);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
                                                                       Expression<Func<TModel, TEnum>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            var items = values.Select(value => new SelectListItem
                                               {
                                                   Text = value.ToString(),
                                                   Value = Convert.ToInt32(value).ToString(),
                                                   Selected = value.Equals(metadata.Model)
                                               });

            return htmlHelper.DropDownListFor(expression, items);
        }
    }
}
