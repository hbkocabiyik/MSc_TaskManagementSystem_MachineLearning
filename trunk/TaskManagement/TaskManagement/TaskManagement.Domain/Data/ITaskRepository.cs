using System.Collections;
using System.Linq;

namespace TaskManagement.Web.Models.Data
{
    public interface ITaskRepository : IRepoistory<Task>
    {
        IQueryable<Task> FindAllByEmployeeId(int employeeID);
        IEnumerable FindUnboundedSkills(int id);
        IQueryable<Task> FindAllWithIncludedTaskSkills();
        void AddSkillTo(Task task, int skillID);
        void RemoveSkillFrom(Task task, int skillID);
    }
}