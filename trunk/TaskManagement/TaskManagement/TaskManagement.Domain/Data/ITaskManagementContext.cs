using System.Data.Entity;

namespace TaskManagement.Web.Models
{
    public interface ITaskManagementContext
    {
        DbSet<Task> Tasks { get; }
        DbSet<Employee> Employees { get; }
        DbSet<Area> Areas { get; }
        DbSet<Skill> Skills { get; }
        DbSet<EmployeeSkill> EmployeeSkills { get; }
        DbSet<aspnet_Users> aspnet_Users { get; }
        DbSet<TaskSkill> TaskSkills { get; }

        int Commit();
    }
}