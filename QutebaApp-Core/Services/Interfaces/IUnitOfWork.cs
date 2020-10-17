using QutebaApp_Core.Services.Implementations;
using QutebaApp_Data.Models;
using System;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Account> AccountRepository { get; }
        GenericRepository<Profile> ProfileRepository { get; }
        GenericRepository<Role> RoleRepository { get; }
        GenericRepository<Category> CategoryRepository { get; }
        GenericRepository<Spending> SpendingRepository { get; }

        void Save();
    }
}
