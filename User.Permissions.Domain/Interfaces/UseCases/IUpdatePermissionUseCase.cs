

using User.Permissions.Domain.Entities;

namespace User.Permissions.Domain.Interfaces.UseCases
{
    public interface IUpdatePermissionUseCase
    {
        Task Execute(Permission permission);
    }
}
