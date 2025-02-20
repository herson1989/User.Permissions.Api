using User.Permissions.Application.Permissions.GetAll;
using User.Permissions.Domain.Entities;

namespace User.Permissions.Application.Mappers
{
    public static class PermissionResponseMapper
    {
        public static List<GetAllPermissionsQueryResponse> Map(IEnumerable<Permission> permissions)
        {
            List<GetAllPermissionsQueryResponse> response = [];
            foreach (var permission in permissions)
            {
                response.Add(CreatePermissionRow(permission));
            }

            return response;
        }

        private static GetAllPermissionsQueryResponse CreatePermissionRow(Permission permission)
        {
            return new GetAllPermissionsQueryResponse
            {
                Id = permission.Id,
                EmployeeForename = permission.EmployeeForename,
                EmployeeSurname = permission.EmployeeSurname,
                PermissionTypeId = permission.PermissionTypeNavigation.Id,
                PermissionTypeDescription = permission.PermissionTypeNavigation?.Description ?? string.Empty,
                PermissionDate = permission.PermissionDate
            };
        }
    }
}
