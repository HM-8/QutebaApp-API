using QutebaApp_Core.Services.Implementations;
using QutebaApp_Data.Models;
using System;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<User> UserRepository { get; }
        GenericRepository<Profile> ProfileRepository { get; }
        GenericRepository<Role> RoleRepository { get; }
        GenericRepository<Category> CategoryRepository { get; }
        GenericRepository<Spending> SpendingRepository { get; }
        GenericRepository<Income> IncomeRepository { get; }
        GenericRepository<Code> CodeRepository { get; }

        void Save();
    }
}
