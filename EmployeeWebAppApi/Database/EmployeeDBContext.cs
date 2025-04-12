using EmployeeWebAppApi.models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebAppApi.Database;

public class EmployeeDbContext(DbContextOptions<EmployeeDbContext> employeeDbContextOptions)
    : DbContext(employeeDbContextOptions)
{
    public DbSet<Employee> Employees { get; set; }
}