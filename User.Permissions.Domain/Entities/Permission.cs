
namespace User.Permissions.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; } = string.Empty;
        public string EmployeeSurname { get; set; } = string.Empty;
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; } = DateTime.UtcNow;

        public PermissionType PermissionTypeNavigation { get; set; }
    }
}
