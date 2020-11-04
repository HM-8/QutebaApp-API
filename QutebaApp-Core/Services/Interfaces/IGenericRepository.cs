using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        T FindBy(Expression<Func<T, bool>> predicate);
        T FindBy(Expression<Func<T, bool>> predicate, string include);
        IEnumerable<T> FindAllBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindAllBy(Expression<Func<T, bool>> predicate, params string[] includes);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        public void DetachEntry(T obj);
    }
}
