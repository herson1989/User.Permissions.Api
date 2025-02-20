using Microsoft.Extensions.Logging;
using Moq;
using User.Permissions.Domain.Entities;
using User.Permissions.Domain.Interfaces.Repositories;
using User.Permissions.Domain.Interfaces.UnitOfWork;
using User.Permissions.Domain.UseCases;

namespace User.Permissions.Tests
{
    [TestClass]
    public class GetAllPermissionsUseCaseTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IKafkaProducerRepository> _kafkaProducerRepositoryMock;
        private Mock<ILogger<GetAllPermissionsUseCase>> _loggerMock;
        private GetAllPermissionsUseCase _useCase;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _kafkaProducerRepositoryMock = new Mock<IKafkaProducerRepository>();
            _loggerMock = new Mock<ILogger<GetAllPermissionsUseCase>>();

            _useCase = new GetAllPermissionsUseCase(_unitOfWorkMock.Object, _kafkaProducerRepositoryMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task Execute_ReturnsPermissionsList_OK()
        {
            // Arrange
            int expectedTotal = 2;
            var expectedPermissions = new List<Permission>
            {
                new Permission { Id = 1, EmployeeForename = "Herson", EmployeeSurname = "Garcia", PermissionDate = DateTime.UtcNow },
                new Permission { Id = 2, EmployeeForename = "Pedro", EmployeeSurname = "Perez", PermissionDate = DateTime.UtcNow }
            };

            _unitOfWorkMock.Setup(u => u.Permissions.GetAllAsync()).ReturnsAsync(expectedPermissions);
            _kafkaProducerRepositoryMock.Setup(k => k.Send(It.IsAny<string>())).Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.Execute();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTotal, result.Count());
            Assert.AreEqual("Herson", result.First().EmployeeForename);
        }

        [TestMethod]
        public async Task Execute_WhenExceptionThrown()
        {
            // Arrange
            var exceptionMessage = "Database error";
            _unitOfWorkMock.Setup(u => u.Permissions.GetAllAsync()).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _useCase.Execute());

            //Assert
            Assert.AreEqual("Error getting all permissions", ex.Message);
        }
    }
}
