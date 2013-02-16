using System;
using System.Linq;
using System.Linq.Expressions;
using TaskManagement.Web.Models.Models;

namespace TaskManagement.Web.Models.Data
{
    public interface IRepoistory<T> : IDisposable where T : class, IEntity
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate);
        T FindById(int id);
        void Add(T newEntity);
        void Remove(T entity);
        void Commit();
    }
}