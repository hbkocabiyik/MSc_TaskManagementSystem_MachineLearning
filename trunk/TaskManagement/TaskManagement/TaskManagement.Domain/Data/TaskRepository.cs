using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace TaskManagement.Web.Models.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementContext _context = new TaskManagementContext();

        #region Implementation of ITaskRepository

        public IQueryable<Task> FindAllByEmployeeId(int employeeID)
        {
            return _context.Tasks.Where(task => task.EmployeeID == employeeID);
        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
        }

        public IQueryable<Task> FindAll()
        {
            return _context.Tasks;
        }

        public IQueryable<Task> FindWhere(Expression<Func<Task, bool>> predicate)
        {
            return _context.Tasks.Where(predicate);
        }

        public Task FindById(int id)
        {
            return _context.Tasks.Single(t => t.ID == id);
        }

        public void Commit()
        {
            _context.Commit();
        }

        public void Remove(Task task)
        {
            _context.Tasks.Remove(task);
        }

        public IEnumerable FindUnboundedSkills(int id)
        {
            var area = FindById(id);
            var areaSkillIds = area.TaskSkills.Select(s => s.SkillID).ToArray();

            return _context.Skills.FindOthersThan(areaSkillIds).OrderBy(s => s.Name);
        }

        public IQueryable<Task> FindAllWithIncludedTaskSkills()
        {
            return _context.Tasks.Include("TaskSkills").AsQueryable();
        }

        public void AddSkillTo(Task task, int skillID)
        {
            var taskSkill = new TaskSkill {
                                              TaskID = task.ID,
                                              SkillID = skillID
                                          };

            task.TaskSkills.Add(taskSkill);
        }

        public void RemoveSkillFrom(Task task, int skillID)
        {
            var taskSkill = _context.TaskSkills.First(a => a.TaskID.Equals(task.ID) && a.SkillID.Equals(skillID));
            _context.TaskSkills.Remove(taskSkill);
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}
