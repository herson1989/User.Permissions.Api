using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using User.Permissions.Domain.Interfaces.Repositories;

namespace User.Permissions.Infrastructure.Repositories
{
    public class KafkaProducerRepository : IKafkaProducerRepository
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;
        private readonly ILogger<KafkaProducerRepository> _logger;

        public KafkaProducerRepository(ILogger<KafkaProducerRepository> logger)
        {
            _logger = logger;

            _bootstrapServers = "localhost:9092";
            _topic = "permissions-topic";
        }


        public async Task Send(string operationName)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var messageContent = new
                    {
                        Id = Guid.NewGuid(),
                        OperationName = operationName
                    };

                    var message = JsonSerializer.Serialize(messageContent);

                    await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });

                    _logger.LogInformation($"Message sent to Kafka. Topic: {_topic}, Operation: {operationName}");

                }
                catch (ProduceException<Null, string> e)
                {
                    _logger.LogError(e, "Error with sending to kafka");
                }
            }
        }
    }
}
