using System.Text.Json;
using Confluent.Kafka;
using EmployeeWebAppApi.models;

namespace EmployeeWebAppApi.Services.Kafka;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly ProducerConfig _config;
    private readonly string? _employeeCreatedTopic;

    public KafkaProducerService(IConfiguration configuration)
    {
        _config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            Acks = Acks.All
        };

        _employeeCreatedTopic = configuration["Kafka:TopicName"];
    }

    public async Task ProduceEmployeeCreatedAsync(Employee employee)
    {
        var message = new Message<string, string>
        {
            Key = employee.Id.ToString(),
            Value = JsonSerializer.Serialize(employee)
        };

        using var producer = new ProducerBuilder<string, string>(_config).Build();
        await producer.ProduceAsync(_employeeCreatedTopic, message);
    }
}