using System;
using System.Linq;
using System.Web.Mvc;
using TaskManagement.Web.Authentication;
using TaskManagement.Web.Models;
using TaskManagement.Web.Models.Data;
using TaskManagement.Web.Models.Models;
using TaskManagement.Web.Models.ModelsExtensions;

namespace TaskManagement.Web.Areas.Manager.Controllers
{
    //TODO: partial views/templates
    [Authorize(Roles = UserRole.Manager)]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // **************************
        // URL: /Manager/Employees/
        // **************************

        public ActionResult Index(string q = null)
        {
            var engineers = _employeeRepository.FindWhere(e => (Role)e.Role == Role.Engineer)
                .Where(employee => (q == null || employee.Name.StartsWith(q)))
                .OrderBy(e => e.Name).ThenBy(e => e.Surname)
                .ToList();

            return View(engineers);
        }

        // **************************
        // URL: /Manager/Employees/Details/5
        // **************************

        public ActionResult Details(int id)
        {
            var engineer = _employeeRepository.FindByIdWithIncludedTasks(id);

            ViewBag.Tasks = engineer.Tasks;
            ViewBag.Skills = engineer.EmployeeSkills;

            return View(engineer);
        }

        // **************************
        // URL: //Manager/Employees/AddSkill/5
        // **************************

        public ActionResult AddSkill(int id)
        {
            var engineerSkillItems = _employeeRepository.FindAllEmployeeUnboundedSkills(id);

            ViewBag.Skill = new SelectList(engineerSkillItems, "ID", "Name");

            var skillLevels = EnumHelper.GetItemsOf<SkillLevel>();
            
            ViewBag.Experience = new SelectList(skillLevels, "ID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddSkill(int id, FormCollection collection)
        {
            var engineer = _employeeRepository.FindById(id);

            engineer.EmployeeSkills.Add(new EmployeeSkill {
                                                              EmployeeID = id,
                                                              SkillID = Convert.ToInt32(collection["skill"]),
                                                              Experience = Convert.ToInt32(collection["experience"])
                                                          });

            _employeeRepository.Commit();

            return RedirectToAction("Index");
        }

        // **************************
        // URL: /Manager/Employee/Delete/5
        // **************************

        public ActionResult RemoveSkill(int id)
        {
            var engineer = _employeeRepository.FindByIdWithIncludedEmployeeSkills(id);

            var items = from EmployeeSkill employeeSkill in engineer.EmployeeSkills
                        select new EnumEntity {
                                                  ID = employeeSkill.ID,
                                                  Name = employeeSkill.Skill.Name
                                              };

            ViewBag.Skill = new SelectList(items, "ID", "Name");

            return View(engineer);
        }

        [HttpPost]
        public ActionResult RemoveSkill(int id, FormCollection collection)
        {
            var employeeSkillId = Convert.ToInt32(collection["skill"]);

            _employeeRepository.RemoveEmployeeSkill(employeeSkillId);
            _employeeRepository.Commit();

            return RedirectToAction("Index");
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _employeeRepository.Dispose();
        }
    }
}
