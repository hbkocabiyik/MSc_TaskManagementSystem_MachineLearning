using System.Linq;
using System.Web.Mvc;
using TaskManagement.Web.Authentication;
using TaskManagement.Web.Models;
using TaskManagement.Web.Models.Data;

namespace TaskManagement.Web.Areas.Manager.Controllers
{
    [Authorize(Roles = UserRole.Manager)]
    public class SkillController : Controller
    {
        private readonly ISkillRepository _skillRepository;

        public SkillController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        // **************************
        // URL: /Manager/Skill/
        // **************************

        public ActionResult Index()
        {
            var skills = _skillRepository.FindAll()
                .OrderBy(s => s.Name).ToList();

            return View(skills);
        }

        // **************************
        // URL: /Manager/Skill/Details/5
        // **************************

        public ActionResult Details(int id)
        {
            var skill = _skillRepository.FindById(id);
            return View(skill);
        }

        // **************************
        // URL: /Manager/Skill/Create/
        // **************************

        public ActionResult Create()
        {
            var skill = new Skill();
            return View(skill);
        }

        [HttpPost]
        public ActionResult Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                _skillRepository.Add(skill);
                _skillRepository.Commit();

                return RedirectToAction("Index");
            }

            return View(skill);
        }

        // **************************
        // URL: /Manager/Skill/Edit/5
        // **************************

        public ActionResult Edit(int id)
        {
            var skill = _skillRepository.FindById(id);
            return View(skill);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var skill = _skillRepository.FindById(id);

            if (TryUpdateModel(skill))
            {
                _skillRepository.Commit();
                return RedirectToAction("Index");
            }

            return View(skill);
        }

        // **************************
        // URL: /Manager/Skill/Delete/5
        // **************************

        public ActionResult Delete(int id)
        {
            var skill = _skillRepository.FindById(id);
            return View(skill);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var skill = _skillRepository.FindById(id);

            _skillRepository.Remove(skill);
            _skillRepository.Commit();

            return View("Deleted");
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _skillRepository.Dispose();
        }
    }
}
