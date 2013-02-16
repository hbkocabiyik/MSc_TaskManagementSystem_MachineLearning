using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace TaskManagement.Web.Models.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TaskManagementContext _context = new TaskManagementContext();

        public Employee FindByIdWithIncludedTasks(int id)
        {
            return _context.Employees.Include("Tasks").Single(e => e.ID.Equals(id));
        }
    
        public Employee FindByIdWithIncludedEmployeeSkills(int id)
        {
            return _context.Employees.Include("EmployeeSkills").Single(e => e.ID.Equals(id));
        }

        public void RemoveEmployeeSkill(int employeeSkillId)
        {
            var employeeSkill = _context.EmployeeSkills.Find(employeeSkillId);
            _context.EmployeeSkills.Remove(employeeSkill);
        }

        public Employee FindByLogin(string suggestedEmployeeLogin)
        {
            return _context.Employees.Single(e => e.Login.Equals(suggestedEmployeeLogin));
        }

        public IQueryable<EmployeeSkill> FindEmployeeSkillsById(int id)
        {
            return FindByIdWithIncludedEmployeeSkills(id).EmployeeSkills.AsQueryable();
        }

        public IEnumerable FindAllEmployeeUnboundedSkills(int id)
        {
            var engineerSkillsIDs = FindEmployeeSkillsById(id).Select(es => es.Skill.ID).ToArray();
            return _context.Skills.FindOthersThan(engineerSkillsIDs).OrderBy(s => s.Name).ToList();
        }

        public IQueryable<Employee> FindAll()
        {
            return _context.Employees;
        }

        public IQueryable<Employee> FindWhere(Expression<Func<Employee, bool>> predicate)
        {
            return _context.Employees.Where(predicate);
        }

        public Employee FindById(int id)
        {
            return _context.Employees.Single(e => e.ID == id);
        }

        public void Add(Employee newEntity)
        {
            _context.Employees.Add(newEntity);
        }

        public void Remove(Employee entity)
        {
            _context.Employees.Remove(entity);
        }

        public Employee FindByNameWithIncludedTasks(string name)
        {
            return _context.Employees.Include("Tasks").Single(e => e.Login == name);
        }

        public void Commit()
        {
            _context.Commit();
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}