using System.Web.Mvc;
using TaskManagement.Web.Authentication;

namespace TaskManagement.Web.Areas.Manager.Controllers
{
    [Authorize(Roles = UserRole.Manager)]
    public class HomeController : Controller
    {
        // **************************
        // URL: /Manager/Home/
        // **************************

        public ActionResult Index()
        {
            return View();
        }
    }
}
