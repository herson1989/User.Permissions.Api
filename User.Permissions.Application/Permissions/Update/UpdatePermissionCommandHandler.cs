using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;
using User.Permissions.Domain.Entities;
using User.Permissions.Domain.Interfaces.UseCases;

namespace User.Permissions.Application.Permissions.Update
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, UpdatePermissionCommandResponse>
    {
        private readonly IUpdatePermissionUseCase _updatePermissionUseCase;
        private readonly ILogger<UpdatePermissionCommandHandler> _logger;

        public UpdatePermissionCommandHandler(IUpdatePermissionUseCase updatePermissionUseCase, ILogger<UpdatePermissionCommandHandler> logger)
        {
            _updatePermissionUseCase = updatePermissionUseCase;
            _logger = logger;
        }

        public async Task<UpdatePermissionCommandResponse> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            StringBuilder errors;
            if (ModelIsValid(request, out errors))
            {
                var permission = new Permission { Id = request.Id, EmployeeForename = request.EmployeeForename, EmployeeSurname = request.EmployeeSurname, PermissionType = request.PermissionType };
                await _updatePermissionUseCase.Execute(permission);

                _logger.LogInformation("Permission updated successfully!");

                return GetOperationResult(true, "Permission updated successfully!");
            }

            return GetOperationResult(false, errors.ToString());
        }

        private static bool ModelIsValid(UpdatePermissionCommand updatePermissionCommand, out StringBuilder errors)
        {
            errors = new StringBuilder();

            if (string.IsNullOrEmpty(updatePermissionCommand.EmployeeForename))
                errors.Append("Forename must not be empty.\n");

            if (string.IsNullOrEmpty(updatePermissionCommand.EmployeeSurname))
                errors.Append("Surname must not be empty.\n");

            if (updatePermissionCommand.PermissionType != 1 && updatePermissionCommand.PermissionType != 2)
            {
                errors.Append("Permission type must be either 1 (admin) or 2 (Supervisor).\n");
            }

            return errors.Length == 0;
        }

        private static UpdatePermissionCommandResponse GetOperationResult(bool isSuccess, string message)
        {
            return new UpdatePermissionCommandResponse
            {
                IsSuccess = isSuccess,
                Message = message
            };
        }
    }
}
