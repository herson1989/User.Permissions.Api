
using MediatR;

namespace User.Permissions.Application.Permissions.Update
{
    public class UpdatePermissionCommand : IRequest<UpdatePermissionCommandResponse>
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; } = string.Empty;
        public string EmployeeSurname { get; set; } = string.Empty;
        public int PermissionType { get; set; }
    }
}
