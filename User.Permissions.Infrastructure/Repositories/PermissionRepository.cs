using Microsoft.EntityFrameworkCore;
using User.Permissions.Domain.Entities;
using User.Permissions.Domain.Interfaces.Repositories;
using User.Permissions.Infrastructure.Persistence;

namespace User.Permissions.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context) {
            _context = context; 
        }

        public async Task<IEnumerable<Permission>> GetAllAsync() {
            return await _context.Permissions
                .Include(p => p.PermissionTypeNavigation)
                .ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id) {
            return await _context.Permissions.FindAsync(id);
        } 

        public async Task AddAsync(Permission permission) {
            await _context.Permissions.AddAsync(permission);
        } 

        public void Update(Permission permission) {
            _context.Permissions.Update(permission);
        } 
    }
}
