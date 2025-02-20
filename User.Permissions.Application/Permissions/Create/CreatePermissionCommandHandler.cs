using MediatR;
using User.Permissions.Domain.Interfaces.UseCases;
using User.Permissions.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text;

namespace User.Permissions.Application.Permissions.Create
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, CreatePermissionCommandResponse>
    {
        private readonly ICreatePermissionUseCase _createPermissionUseCase;
        private readonly ILogger<CreatePermissionCommandHandler> _logger;

        public CreatePermissionCommandHandler(ICreatePermissionUseCase createPermissionUseCase, ILogger<CreatePermissionCommandHandler> logger)
        {
            _createPermissionUseCase = createPermissionUseCase;
            _logger = logger;
        } 

        public async Task<CreatePermissionCommandResponse> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            StringBuilder errors;
            if (ModelIsValid(request, out errors))
            {
                var permission = new Permission { EmployeeForename = request.EmployeeForename, EmployeeSurname = request.EmployeeSurname, PermissionType = request.PermissionType };
                await _createPermissionUseCase.Execute(permission);

                _logger.LogInformation("Permission saved successfully!");

                return GetOperationResult(true, "Permission saved successfully!");
            }

            return GetOperationResult(false, errors.ToString());

        }

        private static bool ModelIsValid(CreatePermissionCommand createPermissionCommand, out StringBuilder errors)
        {
            errors = new StringBuilder();

            if (string.IsNullOrEmpty(createPermissionCommand.EmployeeForename))
                errors.Append("Forename must not be empty.\n");

            if (string.IsNullOrEmpty(createPermissionCommand.EmployeeSurname))
                errors.Append("Surname must not be empty.\n");

            if (createPermissionCommand.PermissionType != 1 && createPermissionCommand.PermissionType != 2)
            {
                errors.Append("Permission type must be either 1 (admin) or 2 (Supervisor).\n");
            }

            return errors.Length == 0;
        }

        private static CreatePermissionCommandResponse GetOperationResult(bool isSuccess, string message)
        {
            return new CreatePermissionCommandResponse
            {
                IsSuccess = isSuccess,
                Message = message
            };
        }
    }
}
