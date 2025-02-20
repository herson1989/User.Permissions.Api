using Microsoft.Extensions.Logging;
using System.Security;
using User.Permissions.Domain.Entities;
using User.Permissions.Domain.Interfaces.Repositories;
using User.Permissions.Domain.Interfaces.UnitOfWork;
using User.Permissions.Domain.Interfaces.UseCases;

namespace User.Permissions.Domain.UseCases
{
    public class UpdatePermissionUseCase : IUpdatePermissionUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKafkaProducerRepository _kafkaProducerRepository;
        private readonly ILogger<UpdatePermissionUseCase> _logger;

        public UpdatePermissionUseCase(IUnitOfWork unitOfWork, IKafkaProducerRepository kafkaProducerRepository, ILogger<UpdatePermissionUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _kafkaProducerRepository = kafkaProducerRepository;
            _logger = logger;
        }

        public async Task Execute(Permission permission)
        {
            try
            {
                var existingPermission = await _unitOfWork.Permissions.GetByIdAsync(permission.Id);

                if (existingPermission == null)
                {
                    _logger.LogWarning("Permission with ID {PermissionId} not found.", permission.Id);
                    throw new KeyNotFoundException($"Permission with ID {permission.Id} not found.");
                }

                existingPermission.EmployeeForename = permission.EmployeeForename;
                existingPermission.EmployeeSurname = permission.EmployeeSurname;
                existingPermission.PermissionType = permission.PermissionType;

                _unitOfWork.Permissions.Update(existingPermission);
                await _unitOfWork.SaveChangesAsync();

                await _kafkaProducerRepository.Send("Modify");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating permission for user: {EmployeeForename}", permission.EmployeeForename);
                throw new Exception("Error updating permission", ex);
            }
        }
    }
}
