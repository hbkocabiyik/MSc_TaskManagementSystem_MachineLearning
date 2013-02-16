using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TaskManagement.Web.Models.Data
{
    public class AreaRepository : IAreaRepository
    {
        private readonly TaskManagementContext _context = new TaskManagementContext();

        #region IAreaRepository Members

        public IQueryable<Area> FindAll()
        {
            return _context.Areas;
        }

        public IQueryable<Area> FindWhere(Expression<Func<Area, bool>> predicate)
        {
            return _context.Areas.Where(predicate);
        }

        public IEnumerable<EnumEntity> FindAllAndWrapToEnumEntity()
        {
            return from Area area in FindAll()
                   select new EnumEntity {
                                             ID = area.ID,
                                             Name = area.Name
                                         };
        }

        public void Add(Area area)
        {
            _context.Areas.Add(area);
        }

        public void Commit()
        {
            _context.Commit();
        }

        public Area FindById(int id)
        {
            return _context.Areas.Single(a => a.ID == id);
        }

        public void Remove(Area area)
        {
            _context.Areas.Remove(area);
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
