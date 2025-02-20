using User.Permissions.Domain.Entities;

namespace User.Permissions.Domain.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(int id);
        Task AddAsync(Permission permission);
        void Update(Permission permission);
    }
}
