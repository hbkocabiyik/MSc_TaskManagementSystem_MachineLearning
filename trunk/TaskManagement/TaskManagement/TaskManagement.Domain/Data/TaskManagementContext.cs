using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TaskManagement.Web.Models.Data
{
    public class TaskManagementContext : DbContext, ITaskManagementContext
    {
        #region ITaskManagementUnitOfWork Members

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<TaskSkill> TaskSkills { get; set; }

        public int Commit()
        {
            return SaveChanges();
        }

        public DbSet<aspnet_Users> aspnet_Users { get; set; }

        #endregion
    }

    public static class AreasRepositoryExtensions
    {
        public static List<EnumEntity> FindAll(this DbSet<Area> areas)
        {
            return (from Area area in areas
                    select new EnumEntity {
                                              ID = area.ID,
                                              Name = area.Name
                                          }).ToList();
        }
    }

    public static class SkillsRepositoryExtensions
    {
        public static List<EnumEntity> FindOthersThan(this DbSet<Skill> skills, IEnumerable<int> skillsIDs)
        {
            return (from Skill skill in skills
                    where !skillsIDs.Contains(skill.ID)
                    select new EnumEntity {
                                              ID = skill.ID,
                                              Name = skill.Name
                                          }).ToList();
        }
    }

    public static class EmployeesRepositoryExtensions
    {
        public static List<EnumEntity> ToEnumEntities(this IQueryable<Employee> employees)
        {
            return (from Employee employee in employees
                    select new EnumEntity {
                                              ID = employee.ID,
                                              Name = employee.Name + " " + employee.Surname
                                          }).ToList();
        }

        public static IQueryable<Employee> GetEngineers(this DbSet<Employee> employees)
        {
            return employees.Where(e => e.Role == (int)Role.Engineer);
        }

        public static Employee IncludeTasksAndFindBy(this DbSet<Employee> employees, int id)
        {
            return employees.Include("Tasks").Single(e => e.ID == id);
        }

        public static Employee IncludeEmployeeSkillsAndFindBy(this DbSet<Employee> employees, int id)
        {
            return employees.Include("EmployeeSkills").Single(e => e.ID == id);
        }

        public static IEnumerable<int> FindEmployeeSkillsBy(this DbSet<Employee> employees, int id)
        {
            return employees.IncludeEmployeeSkillsAndFindBy(id).EmployeeSkills.Select(es => es.Skill.ID);
        }

        public static Employee FindBy(this DbSet<Employee> employees, string login)
        {
            return employees.Single(e => e.Login == login);
        }
    }
}
