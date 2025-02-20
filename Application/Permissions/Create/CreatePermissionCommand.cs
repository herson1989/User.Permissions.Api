using MediatR;

namespace User.Permissions.Application.Permissions.Create
{
    public class CreatePermissionCommand : IRequest<CreatePermissionCommandResponse>
    {
        public string EmployeeForename { get; set; } = string.Empty;
        public string EmployeeSurname { get; set; } = string.Empty;
        public int PermissionType { get; set; }
    }
}
