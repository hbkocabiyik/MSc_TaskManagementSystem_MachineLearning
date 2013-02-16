using System.ComponentModel;

namespace TaskManagement.Web.Areas.Manager.Controllers
{
    public enum ManagerPosition
    {
        Manager,

        [Description("Senior Manager")]
        SeniorManager,

        [Description("Department Manager")]
        DepartmentManager
    }
}