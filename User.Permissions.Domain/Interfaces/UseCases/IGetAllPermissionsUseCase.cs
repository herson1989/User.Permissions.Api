

using User.Permissions.Domain.Entities;

namespace User.Permissions.Domain.Interfaces.UseCases
{
    public interface IGetAllPermissionsUseCase
    {
        Task<IEnumerable<Permission>> Execute();
    }
}
