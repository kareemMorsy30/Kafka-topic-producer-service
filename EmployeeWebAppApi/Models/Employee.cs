namespace EmployeeWebAppApi.models;

public record Employee(string Name, string Role)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = Name;
    public string Role { get; set; } = Role;
}