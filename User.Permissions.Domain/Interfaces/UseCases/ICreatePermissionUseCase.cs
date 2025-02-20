using User.Permissions.Domain.Entities;

namespace User.Permissions.Domain.Interfaces.UseCases
{
    public interface ICreatePermissionUseCase
    {
        Task Execute(Permission permission);
    }
}
