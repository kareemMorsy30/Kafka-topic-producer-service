using System.Text.Json;
using Confluent.Kafka;
using EmployeeWebAppApi.Database;
using EmployeeWebAppApi.models;
using EmployeeWebAppApi.Services.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController(EmployeeDbContext employeeDbContext, IKafkaProducerService kafkaProducerService, ILogger<EmployeesController> logger)
{
    [HttpGet]
    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        logger.LogInformation("Requesting all employees!");
        return await employeeDbContext.Employees.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(string name, string role)
    {
        var employee = new Employee(name, role);
        employeeDbContext.Employees.Add(employee);
        await employeeDbContext.SaveChangesAsync();
        
        // Push message to Kafka
        await kafkaProducerService.ProduceEmployeeCreatedAsync(employee);
        
        return employee;
    }
}