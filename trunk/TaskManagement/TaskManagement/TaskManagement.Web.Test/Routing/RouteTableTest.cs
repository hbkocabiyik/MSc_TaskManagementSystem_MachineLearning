using NUnit.Framework;
using TaskManagement.Web.Test.Helpers;

namespace TaskManagement.Web.Test.Routing
{
    [TestFixture]
    public class RouteTableTest
    {

// ReSharper disable InconsistentNaming
        [Test]
        public void url_when_slash_redirect_to_home_controller_index_action()
        {
            const string Url = "~/";

            RoutingHelper.TestRoute(Url, new {
                                                 Controller = "Home",
                                                 Action = "Index"
                                             });
        }

        [Test]
        public void url_when_slash_accountsettings_redirect_to_accountsettings_controller_index_action()
        {
            const string Url = "~/AccountSettings";

            RoutingHelper.TestRoute(Url, new
            {
                Controller = "AccountSettings",
                Action = "Index"
            });
        }

        [Test]
        public void url_when_slash_administrator_redirect_to_administrator_controller_index_action()
        {
            const string Url = "~/Administrator";

            RoutingHelper.TestRoute(Url, new
            {
                Controller = "Administrator",
                Action = "Index"
            });
        }

        [Test]
        public void url_when_slash_administrator_slash_create_redirect_to_administrator_controller_create_action()
        {
            const string Url = "~/Administrator/Create";

            RoutingHelper.TestRoute(Url, new
            {
                Controller = "Administrator",
                Action = "Create"
            });
        }

        [Test]
        public void url_when_slash_administrator_slash_details_slash_5_redirect_to_administrator_controller_details_action()
        {
            const string Url = "~/Administrator/Details/5";

            RoutingHelper.TestRoute(Url, new
            {
                Controller = "Administrator",
                Action = "Details",
                Id = 5
            });
        }

        [Test]
        public void url_when_slash_administrator_slash_edit_slash_3_redirect_to_administrator_controller_edit_action()
        {
            const string Url = "~/Administrator/Edit/3";

            RoutingHelper.TestRoute(Url, new
            {
                Controller = "Administrator",
                Action = "Edit",
                Id = 3
            });
        }

        [Test]
        public void url_when_slash_administrator_slash_delete_slash_1_redirect_to_administrator_controller_delete_action()
        {
            const string Url = "~/Administrator/Delete/1";

            RoutingHelper.TestRoute(Url, new
            {
                Controller = "Administrator",
                Action = "Delete",
                Id = 1
            });
        }

        [Test]
        public void url_when_slash_engineer_slash_tasklist_redirect_to_engineer_controller_tasklist_action()
        {
            const string Url = "~/Engineer/TaskList";

            RoutingHelper.TestRoute(Url, new
            {
                Controller = "Engineer",
                Action = "TaskList"
            });
        }

        [Test]
        public void url_when_slash_engineer_slash_details_slash_5_redirect_to_engineer_controller_details_action()
        {
            const string Url = "~/Engineer/Details/5";

            RoutingHelper.TestRoute(Url, new
            {
                Controller = "Engineer",
                Action = "Details",
                Id = 5
            });
        }

        [Test]
        public void url_when_slash_engineer_slash_edit_slash_3_redirect_to_engineer_controller_edit_action()
        {
            const string Url = "~/Engineer/Edit/3";

            RoutingHelper.TestRoute(Url, new
            {
                Controller = "Engineer",
                Action = "Edit",
                Id = 3
            });
        }

// ReSharper restore InconsistentNaming
    }
}