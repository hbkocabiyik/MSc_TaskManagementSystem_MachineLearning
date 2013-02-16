using System.Collections;
using System.Linq;

namespace TaskManagement.Web.Models.Data
{
    public interface IEmployeeRepository : IRepoistory<Employee>
    {
        Employee FindByIdWithIncludedTasks(int id);
        IQueryable<EmployeeSkill> FindEmployeeSkillsById(int id);
        IEnumerable FindAllEmployeeUnboundedSkills(int id);
        Employee FindByNameWithIncludedTasks(string name);
        Employee FindByIdWithIncludedEmployeeSkills(int id);
        void RemoveEmployeeSkill(int employeeSkillId);

        Employee FindByLogin(string suggestedEmployeeLogin);
    }
}