
namespace User.Permissions.Domain.Interfaces.Repositories
{
    public interface IKafkaProducerRepository
    {
        Task Send(string operationName);
    }
}
