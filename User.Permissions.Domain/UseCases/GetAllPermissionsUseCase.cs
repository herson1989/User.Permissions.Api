

using Microsoft.Extensions.Logging;
using System.Security;
using User.Permissions.Domain.Entities;
using User.Permissions.Domain.Interfaces.Repositories;
using User.Permissions.Domain.Interfaces.UnitOfWork;
using User.Permissions.Domain.Interfaces.UseCases;

namespace User.Permissions.Domain.UseCases
{
    public class GetAllPermissionsUseCase : IGetAllPermissionsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKafkaProducerRepository _kafkaProducerRepository;
        private readonly ILogger<GetAllPermissionsUseCase> _logger;

        public GetAllPermissionsUseCase(IUnitOfWork unitOfWork, IKafkaProducerRepository kafkaProducerRepository, ILogger<GetAllPermissionsUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _kafkaProducerRepository = kafkaProducerRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Permission>> Execute()
        {
            try
            {
                var permissions = await _unitOfWork.Permissions.GetAllAsync();
                await _kafkaProducerRepository.Send("Get");

                return permissions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all permissions");
                throw new Exception("Error getting all permissions", ex);
            }
        }
    }
}
