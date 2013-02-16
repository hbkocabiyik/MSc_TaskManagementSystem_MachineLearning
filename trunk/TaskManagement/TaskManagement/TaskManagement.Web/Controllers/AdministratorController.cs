using System;
using System.Linq;
using System.Web.Mvc;
using TaskManagement.Web.Authentication;
using TaskManagement.Web.Areas.Manager.Controllers;
using TaskManagement.Web.Models;
using TaskManagement.Web.Models.Utils;

namespace TaskManagement.Web.Controllers
{
    [Authorize(Roles = UserRole.Administrator)]
    public class AdministratorController : Controller
    {
        // **************************
        // URL: /Administrator/
        // **************************

        public ActionResult Index()
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var employees = ctx.Employees
                .OrderBy(e => e.Role).ThenBy(e => e.Login)
                .ToList();

            return View(employees);
        }

        // **************************
        // URL: /Administrator/Create/
        // **************************

        public ActionResult Create()
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();

            var employee = new Employee {
                                            EmploymentDate = DateTime.Now.Date
                                        };

            LoadRolesFor(employee);

            var activeUsersLogins = ctx.Employees.Select(e => e.Login).ToList();
            
            var users = ctx.aspnet_Users.ToList();

            var logins = users.Select(u => u.UserName).Except(activeUsersLogins);
            var items = from login in logins
                        select new {
                                       ID = login,
                                       Value = login
                                   };

            ViewData["Login"] = new SelectList(items, "ID", "Value", employee.Login);
            
            return View(employee);
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();

                ctx.Employees.Add(employee);
                ctx.Commit();

                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // **************************
        // URL: /Administrator/Details/5
        // **************************

        public ActionResult Details(int id)
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var employee = ctx.Employees.Single(e => e.ID == id);

            return View(employee);
        }

        // **************************
        // URL: /Administrator/Edit/5
        // **************************

        public ActionResult Edit(int id)
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var employee = ctx.Employees.Single(e => e.ID == id);

            if (employee.Role == (int)Role.Manager)
                LoadManagerPositionsFor(employee);
            else
                LoadEngineerPositionsFor(employee);

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var employee = ctx.Employees.Single(e => e.ID == id);

            if (TryUpdateModel(employee))
            {
                ctx.Commit();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // **************************
        // URL: /Administrator/Delete/5
        // **************************

        public ActionResult Delete(int id)
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var employee = ctx.Employees.Single(e => e.ID == id);

            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var ctx = DependencyResolver.Current.GetService<ITaskManagementContext>();
            var employee = ctx.Employees.Single(e => e.ID == id);

            ctx.Employees.Remove(employee);
            ctx.Commit();

            return View("Deleted");
        }

        private void LoadRolesFor(Employee employee)
        {
            var items = from Role role in Enum.GetValues(typeof(Role))
                        select new {
                                       ID = (int)role,
                                       Name = role.ToString()
                                   };

            ViewBag.Role = new SelectList(items, "ID", "Name", employee.Role);
        }

        private void LoadManagerPositionsFor(Employee employee)
        {
            var items = from ManagerPosition position in Enum.GetValues(typeof(ManagerPosition))
                        select new {
                                       ID = (int)position,
                                       Name = position.GetDescription()
                                   };

            ViewBag.PositionLevel = new SelectList(items, "ID", "Name", employee.PositionLevel);
        }

        private void LoadEngineerPositionsFor(Employee employee)
        {
            var items = from EngineerPosition position in Enum.GetValues(typeof(EngineerPosition))
                        select new {
                                       ID = (int)position,
                                       Name = position.GetDescription()
                                   };

            ViewBag.PositionLevel = new SelectList(items, "ID", "Name", employee.PositionLevel);
        }
    }
}
