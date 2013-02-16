using System.Web.Mvc;
using TaskManagement.Web.Authentication;
using TaskManagement.Web.Models;
using TaskManagement.Web.Models.Data;

namespace TaskManagement.Web.Areas.Manager.Controllers
{
    [Authorize(Roles = UserRole.Manager)]
    public class AreaController : Controller
    {
        private readonly IAreaRepository _areaRepository;

        public AreaController(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        // **************************
        // URL: /Manager/Area/
        // **************************

        public ActionResult Index()
        {
            var areas = _areaRepository.FindAll();

            return View(areas);
        }

        // **************************
        // URL: /Manager/Area/Create/
        // **************************

        public ActionResult Create()
        {
            var area = new Area();

            return View(area);
        }

        [HttpPost]
        public ActionResult Create(Area area)
        {
            if (ModelState.IsValid)
            {
                _areaRepository.Add(area);
                _areaRepository.Commit();

                return RedirectToAction("Index");
            }

            return View(area);
        }

        // **************************
        // URL: /Manager/Area/Details/5
        // **************************

        public ActionResult Details(int id)
        {
            var area = _areaRepository.FindById(id);

            return View(area);
        }

        // **************************
        // URL: /Manager/Area/Delete/5
        // **************************

        public ActionResult Delete(int id)
        {
            var area = _areaRepository.FindById(id);

            return View(area);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var area = _areaRepository.FindById(id);

            _areaRepository.Remove(area);
            _areaRepository.Commit();

            return View("Deleted");
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _areaRepository.Dispose();
        }
    }
}
