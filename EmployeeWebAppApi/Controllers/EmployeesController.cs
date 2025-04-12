using EmployeeWebAppApi.Database;
using EmployeeWebAppApi.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController(EmployeeDbContext employeeDbContext, ILogger<EmployeesController> logger)
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
        return employee;
    }
}