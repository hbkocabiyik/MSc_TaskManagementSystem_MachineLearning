using System;
using System.Linq;
using System.Linq.Expressions;

namespace TaskManagement.Web.Models.Data
{
    public class SkillRepository : ISkillRepository
    {
        private readonly TaskManagementContext _context = new TaskManagementContext();

        public IQueryable<Skill> FindWhere(Expression<Func<Skill, bool>> predicate)
        {
            return _context.Skills.Where(predicate);
        }

        public Skill FindById(int id)
        {
            return _context.Skills.Single(s => s.ID == id);
        }

        public IQueryable<Skill> FindAll()
        {
            return _context.Skills.AsQueryable();
        }

        public void Add(Skill skill)
        {
            _context.Skills.Add(skill);
        }

        public void Remove(Skill skill)
        {
            _context.Skills.Remove(skill);
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