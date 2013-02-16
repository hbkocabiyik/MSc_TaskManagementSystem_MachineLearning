using System.Web.Mvc;

namespace TaskManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        // **************************
        // URL: /Home/
        // **************************

        public ActionResult Index()
        {
            return View();
        }
    }
}
