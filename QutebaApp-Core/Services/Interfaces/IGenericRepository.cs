using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        T GetByEmail(Expression<Func<T, bool>> email);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
    }
}
