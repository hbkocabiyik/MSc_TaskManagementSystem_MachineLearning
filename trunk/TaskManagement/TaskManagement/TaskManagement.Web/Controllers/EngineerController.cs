using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using TaskManagement.Web.Authentication;
using TaskManagement.Web.Models;

namespace TaskManagement.Web.Controllers
{
    [Authorize(Roles = UserRole.Engineer)]
    public class EngineerController : Controller
    {
        // **************************
        // URL: /Engineer/TaskList/
        // **************************

        public ActionResult TaskList()
        {
            var login = HttpContext.User.Identity.Name;

            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var employee = ctx.Employees.Include("Tasks").Single(e => e.Login == login);

            return View(employee.Tasks);
        }

        // **************************
        // URL: /Engineer/Details/5
        // **************************

        public ActionResult Details(int id)
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var task = ctx.Tasks.Find(id);

            return View(task);
        }

        // **************************
        // URL: /Engineer/Edit/5
        // **************************

        public ActionResult Edit(int id)
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var task = ctx.Tasks.Find(id);

            AddStatusListToViewData(task);
            AddAreasToViewBag(ctx);
            AddPrioritiesToViewData(task);

            return View(task);
        }

        // **************************
        // POST: /Engineer/Edit/5
        // **************************

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var task = ctx.Tasks.Find(id);

            if (TryUpdateModel(task))
            {
                ctx.Commit();
                return RedirectToAction("TaskList");
            }

            return View(task);
        }

        private void AddStatusListToViewData(Task task)
        {
            var items = from Status status in Enum.GetValues(typeof(Status))
                        select new {
                                       ID = (int)status,
                                       Name = status.ToString()
                                   };

            ViewBag.Status = new SelectList(items, "ID", "Name", task.Status);
        }

        private void AddAreasToViewBag(ITaskManagementContext ctx)
        {
            var items = (from Area area in ctx.Areas
                         select new {
                                        area.ID,
                                        area.Name
                                    }).ToList();

            ViewBag.Areas = items;
        }

        private void AddPrioritiesToViewData(Task task)
        {
            var items = from Priority priority in Enum.GetValues(typeof(Priority))
                        select new {
                                       ID = (int)priority,
                                       Name = priority.ToString()
                                   };

            ViewBag.Priority = new SelectList(items, "ID", "Name", task.Priority);
        }
    }

    public enum EngineerPosition
    {
        [Description("Software Engineer")]
        SoftwareEngineer,

        [Description("Senior Software Engineer")]
        SeniorSoftwareEngineer,

        [Description("Lead Software Engineer")]
        LeadSoftwareEngineer
    }
}
