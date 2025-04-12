using EmployeeWebAppApi.models;

namespace EmployeeWebAppApi.Services.Kafka;

public interface IKafkaProducerService
{
    Task ProduceEmployeeCreatedAsync(Employee employee);
}