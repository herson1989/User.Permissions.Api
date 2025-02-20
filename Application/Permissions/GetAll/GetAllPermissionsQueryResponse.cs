

using User.Permissions.Domain.Entities;

namespace User.Permissions.Application.Permissions.GetAll
{
    public class GetAllPermissionsQueryResponse
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public string PermissionTypeDescription { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
