using Microsoft.Extensions.Logging;
using Moq;
using User.Permissions.Domain.Entities;
using User.Permissions.Domain.Interfaces.Repositories;
using User.Permissions.Domain.Interfaces.UnitOfWork;
using User.Permissions.Domain.UseCases;

namespace User.Permissions.Tests
{
    [TestClass]
    public class CreatePermissionUseCaseTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IKafkaProducerRepository> _kafkaProducerRepositoryMock;
        private Mock<ILogger<CreatePermissionUseCase>> _loggerMock;
        private CreatePermissionUseCase _useCase;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _kafkaProducerRepositoryMock = new Mock<IKafkaProducerRepository>();
            _loggerMock = new Mock<ILogger<CreatePermissionUseCase>>();

            _useCase = new CreatePermissionUseCase(_unitOfWorkMock.Object, _kafkaProducerRepositoryMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task Execute_ShouldAddPermissionAndSaveChanges()
        {
            // Arrange
            var permission = new Permission
            {
                Id = 1,
                EmployeeForename = "Herson",
                EmployeeSurname = "Garcia",
                PermissionType = 1,
                PermissionDate = DateTime.UtcNow
            };

            // Simular la llamados
            _unitOfWorkMock.Setup(u => u.Permissions.AddAsync(It.IsAny<Permission>()))
                           .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                           .Returns(Task.CompletedTask);
            _kafkaProducerRepositoryMock.Setup(k => k.Send(It.IsAny<string>())).Returns(Task.CompletedTask);

            // Act
            await _useCase.Execute(permission);

            // Assert
            _unitOfWorkMock.Verify(u => u.Permissions.AddAsync(permission), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task Execute_WhenExceptionThrown()
        {
            // Arrange
            var permission = new Permission { EmployeeForename = "Herson" };

            _unitOfWorkMock.Setup(u => u.Permissions.AddAsync(It.IsAny<Permission>()))
                           .Throws(new Exception("Database error"));

            // Act
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _useCase.Execute(permission));

            //Assert
            Assert.AreEqual("Error creating permission", ex.Message);
        }
    }
}
