using NUnit.Framework;
using TaskManagement.Web.Areas.Manager;
using TaskManagement.Web.Test.Helpers;

namespace TaskManagement.Web.Test.Routing
{
    [TestFixture]
    public class ManagerAreaRoutingTableTest : RoutesTestClassBase<ManagerAreaRegistration>
    {
// ReSharper disable InconsistentNaming

        [Test]
        public void url_when_slash_manager_slash_tasks_redirect_to_manager_area_tasks_controller_index_action()
        {
            const string Url = "Tasks";

            TestRoute(Url, new
            {
                controller = "Tasks",
                action = "Index"
            });
        }

        [Test]
        public void url_when_slash_manager_slash_tasks_slash_create_redirect_to_manager_area_tasks_controller_create_action()
        {
            const string Url = "Tasks/Create";

            TestRoute(Url, new
            {
                controller = "Tasks",
                action = "Create"
            });
        }

        [Test]
        public void url_when_slash_manager_slash_tasks_slash_details_slash_5_redirect_to_manager_area_tasks_controller_details_action()
        {
            const string Url = "Tasks/Details/5";

            TestRoute(Url, new
            {
                controller = "Tasks",
                action = "Details",
                id = 5
            });
        }

        [Test]
        public void url_when_slash_manager_slash_tasks_slash_edit_slash_3_redirect_to_manager_area_tasks_controller_edit_action()
        {
            const string Url = "Tasks/Edit/3";

            TestRoute(Url, new
            {
                controller = "Tasks",
                action = "Edit",
                id = 3
            });
        }

        [Test]
        public void url_when_slash_manager_slash_tasks_slash_delete_slash_1_redirect_to_manager_area_tasks_controller_delete_action()
        {
            const string Url = "Tasks/Delete/1";

            TestRoute(Url, new
            {
                controller = "Tasks",
                action = "Delete",
                id = 1
            });
        }

        [Test]
        public void url_when_slash_manager_slash_skill_redirect_to_manager_area_skill_controller_index_action()
        {
            const string Url = "Skill";

            TestRoute(Url, new
            {
                controller = "Skill",
                action = "Index"
            });
        }

        [Test]
        public void url_when_slash_manager_slash_skill_slash_create_redirect_to_manager_area_skill_controller_create_action()
        {
            const string Url = "Skill/Create";

            TestRoute(Url, new
            {
                controller = "Skill",
                action = "Create"
            });
        }

        [Test]
        public void url_when_slash_manager_slash_skill_slash_details_slash_5_redirect_to_manager_area_skill_controller_details_action()
        {
            const string Url = "Skill/Details/5";

            TestRoute(Url, new
            {
                controller = "Skill",
                action = "Details",
                id = 5
            });
        }

        [Test]
        public void url_when_slash_manager_slash_skill_slash_edit_slash_3_redirect_to_manager_area_skill_controller_edit_action()
        {
            const string Url = "Skill/Edit/3";

            TestRoute(Url, new
            {
                controller = "Skill",
                action = "Edit",
                id = 3
            });
        }

        [Test]
        public void url_when_slash_manager_slash_skill_slash_delete_slash_1_redirect_to_manager_area_skill_controller_delete_action()
        {
            const string Url = "Skill/Delete/1";

            TestRoute(Url, new
            {
                controller = "Skill",
                action = "Delete",
                id = 1
            });
        }

        [Test]
        public void url_when_slash_manager_slash_home_redirect_to_manager_area_home_controller_index_action()
        {
            const string Url = "Home";

            TestRoute(Url, new
            {
                controller = "Home",
                action = "Index"
            });
        }

        [Test]
        public void url_when_slash_manager_slash_employees_redirect_to_manager_area_employees_controller_index_action()
        {
            const string Url = "Employees";

            TestRoute(Url, new
            {
                controller = "Employees",
                action = "Index"
            });
        }

        [Test]
        public void url_when_slash_manager_slash_employees_slash_details_slash_5_redirect_to_manager_area_employees_controller_details_action()
        {
            const string Url = "Employees/Details/5";

            TestRoute(Url, new
            {
                controller = "Employees",
                action = "Details",
                id = 5
            });
        }

        [Test]
        public void url_when_slash_manager_slash_addskill_slash_details_slash_3_redirect_to_manager_area_employees_controller_addskill_action()
        {
            const string Url = "Employees/AddSkill/3";

            TestRoute(Url, new
            {
                controller = "Employees",
                action = "AddSkill",
                id = 3
            });
        }

        [Test]
        public void url_when_slash_manager_slash_employees_slash_removeskill_slash_1_redirect_to_manager_area_employees_controller_removeskill_action()
        {
            const string Url = "Employees/RemoveSkill/1";

            TestRoute(Url, new
            {
                controller = "Employees",
                action = "RemoveSkill",
                id = 1
            });
        }

        [Test]
        public void url_when_slash_manager_slash_area_redirect_to_manager_area_area_controller_index_action()
        {
            const string Url = "Employees/Area";

            TestRoute(Url, new
            {
                controller = "Employees",
                action = "Area"
            });
        }

        [Test]
        public void url_when_slash_manager_slash_area_slash_create_redirect_to_manager_area_area_controller_create_action()
        {
            const string Url = "Area/Create";

            TestRoute(Url, new
            {
                controller = "Area",
                action = "Create"
            });
        }

        [Test]
        public void url_when_slash_manager_slash_area_slash_details_slash_5_redirect_to_manager_area_area_controller_details_action()
        {
            const string Url = "Area/Details/5";

            TestRoute(Url, new
            {
                controller = "Area",
                action = "Details",
                id = 5
            });
        }

        [Test]
        public void url_when_slash_manager_slash_area_slash_delete_slash_4_redirect_to_manager_area_area_controller_delete_action()
        {
            const string Url = "Area/Delete/4";

            TestRoute(Url, new
            {
                controller = "Area",
                action = "Delete",
                id = 4
            });
        }

        [Test]
        public void url_when_slash_manager_slash_area_slash_addskill_slash_3_redirect_to_manager_area_area_controller_addskill_action()
        {
            const string Url = "Area/AddSkill/3";

            TestRoute(Url, new
            {
                controller = "Area",
                action = "AddSkill",
                id = 3
            });
        }

        [Test]
        public void url_when_slash_manager_slash_area_slash_removeskill_slash_1_redirect_to_manager_area_area_controller_removeskill_action()
        {
            const string Url = "Area/RemoveSkill/1";

            TestRoute(Url, new
            {
                controller = "Area",
                action = "RemoveSkill",
                id = 1
            });
        }

// ReSharper restore InconsistentNaming
    }
}