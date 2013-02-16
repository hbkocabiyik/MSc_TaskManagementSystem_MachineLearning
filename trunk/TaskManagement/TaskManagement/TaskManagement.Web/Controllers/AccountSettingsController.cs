using System.Web.Mvc;
using TaskManagement.Web.Authentication;
using TaskManagement.Web.Models;
using TaskManagement.Web.Models.Data;

namespace TaskManagement.Web.Controllers
{
    [Authorize(Roles = UserRole.ManagerOrEngineer)]
    public class AccountSettingsController : Controller
    {
        // **************************
        // URL: /AccountSettings/
        // **************************

        public ActionResult Index()
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();

            var employee = ctx.Employees.FindBy(HttpContext.User.Identity.Name);

            return View(employee);
        }
    }
}
