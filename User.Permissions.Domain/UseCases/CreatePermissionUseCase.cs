using Microsoft.Extensions.Logging;
using User.Permissions.Domain.Entities;
using User.Permissions.Domain.Interfaces.Repositories;
using User.Permissions.Domain.Interfaces.UnitOfWork;
using User.Permissions.Domain.Interfaces.UseCases;

namespace User.Permissions.Domain.UseCases
{
    public class CreatePermissionUseCase : ICreatePermissionUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKafkaProducerRepository _kafkaProducerRepository;
        private readonly ILogger<CreatePermissionUseCase> _logger;

        public CreatePermissionUseCase(IUnitOfWork unitOfWork, IKafkaProducerRepository kafkaProducerRepository, ILogger<CreatePermissionUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _kafkaProducerRepository = kafkaProducerRepository;
            _logger = logger;
        }

        public async Task Execute(Permission permission)
        {
            try
            {
                await _unitOfWork.Permissions.AddAsync(permission);
                await _unitOfWork.SaveChangesAsync();

                await _kafkaProducerRepository.Send("Request");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating permission for user: {EmployeeForename}", permission.EmployeeForename);
                throw new Exception("Error creating permission", ex);
            }
        }
    }
}
