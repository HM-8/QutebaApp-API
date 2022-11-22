using Microsoft.EntityFrameworkCore;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QutebaApp_Core.Services.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private QutebaAppDbContext context = null;
        private DbSet<T> table = null;

        public GenericRepository(QutebaAppDbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            var query = table;
            return query;
        }

        public T GetById(object id)
        {
            var query = table.Find(id);
            return query;
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            var obj = table.Find(id);

            if (context.Entry(obj).State == EntityState.Detached)
            {
                table.Attach(obj);
            }
            table.Remove(obj);
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = table.Where(predicate).FirstOrDefault();
            return query;
        }

        public T FindBy(Expression<Func<T, bool>> predicate, string include)
        {
            var query = table.Where(predicate).Include(include).FirstOrDefault();

            return query;
        }

        public IEnumerable<T> FindAllBy(Expression<Func<T, bool>> predicate)
        {
            var query = table.Where(predicate).ToList();
            return query;
        }

        public IEnumerable<T> FindAllBy(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = table.Where(predicate).AsQueryable();

            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            query.ToList();

            return query;
        }

        public void DetachEntry(T obj)
        {
            context.Entry(obj).State = EntityState.Detached;
            Console.WriteLine($"State >>>> {context.Entry(obj)} {context.Entry(obj).State}");
        }

    }
}
