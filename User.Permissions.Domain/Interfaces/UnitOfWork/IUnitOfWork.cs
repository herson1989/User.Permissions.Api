using User.Permissions.Domain.Interfaces.Repositories;

namespace User.Permissions.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository Permissions { get; }
        Task SaveChangesAsync();
    }
}
