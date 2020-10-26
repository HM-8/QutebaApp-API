using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Data;
using QutebaApp_Data.Models;
using System;

namespace QutebaApp_Core.Services.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool isDisposed = false;
        private QutebaAppDbContext context;
        GenericRepository<User> userRepository;
        GenericRepository<Profile> profileRepository;
        GenericRepository<Role> roleRepository;
        GenericRepository<Category> categoryRepository;
        GenericRepository<Spending> spendingRepository;
        GenericRepository<Income> incomeRepository;

        public UnitOfWork(QutebaAppDbContext context)
        {
            this.context = context;
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<Profile> ProfileRepository
        {
            get
            {
                if (this.profileRepository == null)
                {
                    this.profileRepository = new GenericRepository<Profile>(context);
                }
                return profileRepository;
            }
        }

        public GenericRepository<Role> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new GenericRepository<Role>(context);
                }
                return roleRepository;
            }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }
                return categoryRepository;
            }
        }

        public GenericRepository<Spending> SpendingRepository
        {
            get
            {
                if (this.spendingRepository == null)
                {
                    this.spendingRepository = new GenericRepository<Spending>(context);
                }
                return spendingRepository;
            }
        }

        public GenericRepository<Income> IncomeRepository
        {
            get
            {
                if (this.incomeRepository == null)
                {
                    this.incomeRepository = new GenericRepository<Income>(context);
                }
                return incomeRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.isDisposed = true;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
