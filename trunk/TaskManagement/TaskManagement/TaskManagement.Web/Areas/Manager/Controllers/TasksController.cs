using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManagement.Web.App_Start;
using TaskManagement.Web.Authentication;
using TaskManagement.Web.Models;
using TaskManagement.Web.Models.Data;
using TaskManagement.Web.Models.ModelsExtensions;

namespace TaskManagement.Web.Areas.Manager.Controllers
{
    [Authorize(Roles = UserRole.Manager)]
    public class TasksController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAreaRepository _areaRepository;

        public TasksController(ITaskRepository taskRepository, IEmployeeRepository employeeRepository, IAreaRepository areaRepository)
        {
            _taskRepository = taskRepository;
            _areaRepository = areaRepository;
            _employeeRepository = employeeRepository;
        }

        // **************************
        // URL: /Manager/Tasks/
        // **************************

        public ActionResult Index()
        {
            var manager = _employeeRepository.FindByNameWithIncludedTasks(HttpContext.User.Identity.Name);
            var taskList = _taskRepository.FindAllByEmployeeId(manager.ID).ToList();

            return View(taskList);
        }

        // **************************
        // URL: /Manager/Tasks/Create/
        // **************************

        public ActionResult Create()
        {
            var task = new Task();

            ViewBag.Employees = GetEngineersAndMe();
            ViewBag.Areas = _areaRepository.FindAll();

            var items = EnumHelper.GetItemsOf<Priority>();
            ViewBag.Priority = new SelectList(items, "ID", "Name", task.Priority);

            return View(task);
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                task.RemainingTime = task.EstimatedTime;
                task.Status = (int)Status.New;
                task.SpentTime = 0;

                _taskRepository.Add(task);
                _taskRepository.Commit();
                
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // **************************
        // URL: /Manager/Tasks/Details/5
        // **************************

        public ActionResult Details(int id)
        {
            var task = _taskRepository.FindAllWithIncludedTaskSkills().Single(t => t.ID.Equals(id));

            ViewBag.Skills = task.TaskSkills.Select(a => a.Skill).ToList();

            return View(task);
        }

        // **************************
        // URL: /Manager/Tasks/Edit/5
        // **************************

        public ActionResult Edit(int id)
        {
            var task = _taskRepository.FindById(id);
            
            ViewBag.Employees = GetEngineersAndMe();
            ViewBag.Areas = _areaRepository.FindAll();

            var items = EnumHelper.GetItemsOf<Priority>();
            ViewBag.Priority = new SelectList(items, "ID", "Name", task.Priority);

            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var task = _taskRepository.FindById(id);

            if (TryUpdateModel(task))
            {
                _taskRepository.Commit();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // **************************
        // URL: /Manager/Tasks/Delete/5
        // **************************

        public ActionResult Delete(int id)
        {
            var task = _taskRepository.FindById(id);

            return View(task);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var task = _taskRepository.FindById(id);

            _taskRepository.Remove(task);
            _taskRepository.Commit();

            return View("Deleted");
        }

        private List<EnumEntity> GetEngineersAndMe()
        {
            var engineers = _employeeRepository.FindWhere(e => (Role)e.Role == Role.Engineer);
            var items = engineers.ToEnumEntities();

            var login = HttpContext.User.Identity.Name;
            var manager = _employeeRepository.FindByNameWithIncludedTasks(login);

            items.Add(new EnumEntity {
                                         ID = manager.ID,
                                         Name = "@Me"
                                     });

            return items.OrderBy(i => i.Name).ToList();
        }

        // **************************
        // URL: /Manager/Tasks/AddSkill/5
        // **************************

        public ActionResult AddSkill(int id)
        {
            var task = _taskRepository.FindById(id);
            var items = _taskRepository.FindUnboundedSkills(id);
        
            ViewBag.Skill = new SelectList(items, "ID", "Name");
        
            return View(task);
        }

        [HttpPost]
        public ActionResult AddSkill(int id, FormCollection collection)
        {
            var task = _taskRepository.FindAllWithIncludedTaskSkills().Single(a => a.ID.Equals(id));
                    
            var skillID = Convert.ToInt32(collection["skill"]);

            _taskRepository.AddSkillTo(task, skillID);
            _taskRepository.Commit();
        
            return RedirectToAction("Index");
        }

        // **************************
        // URL: /Manager/Tasks/RemoveSkill/5
        // **************************

        public ActionResult RemoveSkill(int id)
        {
            var task = _taskRepository.FindAllWithIncludedTaskSkills().Single(a => a.ID.Equals(id));
        
            var items = from skill in task.TaskSkills.Select(a => a.Skill)
                        select new {
                                        skill.ID,
                                        skill.Name
                                    };
        
            ViewBag.Skill = new SelectList(items.OrderBy(i => i.Name), "ID", "Name");
        
            return View(task);
        }
        
        [HttpPost]
        public ActionResult RemoveSkill(int id, FormCollection collection)
        {
            var task = _taskRepository.FindById(id);
        
            var skillID = Convert.ToInt32(collection["skill"]);
        
            _taskRepository.RemoveSkillFrom(task, skillID);
            _taskRepository.Commit();
        
            return RedirectToAction("Index");
        }

        // **************************
        // URL: /Manager/Tasks/SuggestEmployee/5
        // **************************

        public ActionResult SuggestEmployee(int id)
        {
            var dataToClassify = PrepareDataToClassify(id);

            var suggestedEmployeeLogin = ID3DecisionTreeEngine.Classify(dataToClassify);

            if (suggestedEmployeeLogin == null)
                return View("SuggestEmployeeEmpty");
            
            var suggestedEmployee = _employeeRepository.FindByLogin(suggestedEmployeeLogin);

            return View(suggestedEmployee);
        }

        // **************************

        // URL: /Manager/Tasks/SuggestEmployeeOptimized/5

        // **************************

        public ActionResult SuggestEmployeeOptimized(int id)
        {
            var dataToClassify = PrepareDataToClassify(id);

            var suggestedEmployeeLogin = ID3DecisionTreeEngine.ClassifyOptimized(dataToClassify);

            if (suggestedEmployeeLogin == null)
                return View("SuggestEmployeeEmpty");
            
            var suggestedEmployee = _employeeRepository.FindByLogin(suggestedEmployeeLogin);

            return View(suggestedEmployee);
        }

        private IList<string> PrepareDataToClassify(int id)
        {
            var task = _taskRepository.FindById(id);

            IList<string> dataToClassify = new List<string>();

            var areaName = task.Area.Name.Replace(" ", string.Empty);
            dataToClassify.Add(areaName);

            var skills = task.TaskSkills.Select(taskSkill => taskSkill.Skill.Name).ToList();

            dataToClassify.Add((skills.Contains("WCF Programming")).ToString());
            dataToClassify.Add((skills.Contains("WPF Programming")).ToString());
            dataToClassify.Add((skills.Contains("WF Programming")).ToString());
            dataToClassify.Add((skills.Contains("Ms Sql Server")).ToString());
            dataToClassify.Add((skills.Contains("Oracle Database")).ToString());
            dataToClassify.Add((skills.Contains("JavaScript Programming")).ToString());
            dataToClassify.Add((skills.Contains("CSS Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("OPC Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("Silverlight Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("ASP.NET Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("Localization Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("Architecture Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("Performance Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("COM Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("Refactoring Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("Build Server")).ToString());
            dataToClassify.Add((skills.Contains("VB.net Programming")).ToString());
            dataToClassify.Add((skills.Contains("Sharepoint")).ToString());
            dataToClassify.Add((skills.Contains("IIS Administration Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("BizTalk Server Administration Fundamentals")).ToString());
            dataToClassify.Add((skills.Contains("XML Framework Fundamentals")).ToString());
            return dataToClassify;
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _areaRepository.Dispose();
            _employeeRepository.Dispose();
            _taskRepository.Dispose();
        }
    }
}
