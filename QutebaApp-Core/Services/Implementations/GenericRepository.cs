using Microsoft.EntityFrameworkCore;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Data;
using System;
using System.Collections.Generic;

namespace QutebaApp_Core.Services.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private QutebaAppDbContext _context = null;
        private DbSet<T> table = null;

        public GenericRepository(QutebaAppDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
